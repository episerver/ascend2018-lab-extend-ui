# Serialization

This document summarizes how the Content Api serializes instances of IContent into models to be returned by the Content Api. 

## How it Works ##

Within the Content Api, IContent instances are mapped to a `ContentApiModel` class, which provides a serializable representation of the content. This is useful for returning content as JSON in Api calls, as well as indexing content into Episerver Find (when the Search Api is in use). When indexed into Find, The `ContentApiModel` is serialized and attached as a property (via IClientConventions) on the standard IContent items that are indexed into Find.

In order to perform the mapping required to generate the model, IContent is passed to the default implementation of `IContentModelMapper` via the `TransformContent` method, which handles the mapping of base class properties that are common across all instances of IContent, as well as mapping properties that have been added to classes by developers.

In order to map non-base class properties, a set of `EPiServer.ContentApi.Core.PropertyModel` implementations are bundled with the Content Api, which handle mapping various `PropertyData` instances to serializable classes. These implementations vary in complexity, and some map to custom objects to enable easier serialization. For example, `ContentAreaPropertyModel` maps a `PropertyContentArea` to a `List<ContentAreaItemModel>`, handling content area filtering, personalization, and expansion, but `StringPropertyModel` simply maps to a `string`.

Mapped properties are stored on the `Properties` dictionary of the `ContentApiModel` class, which is flattened using the `JsonExtensionData` attribute from JSON.Net at runtime, allowing them to live alongside base-class properties at the same level on the object.


## Custom Property Models ##

Custom property models can be created to customize how existing `PropertyData` implementations are converted and serialized in the Content Api, or to support custom property types in your solution. 

In order to create a custom property model, create a class which inherits from `EPiServer.ContentApi.Core.PropertyModel`, and set the `Value` property in your constructor. It's possible to map your property type to any class, as long as it's serializable and can be properly indexed into Episerver Find.

Example: The following custom property model forces all `PropertyLongString` instances to lowercase.

```c#
    public class LowercaseLongStringPropertyModel : PropertyModel<string, PropertyLongString>
    {
        public LowercaseLongStringPropertyModel(PropertyLongString propertyLongString) : base(propertyLongString)
        {
            if (propertyLongString != null)
            {
                Value = propertyLongString.ToString().ToLower();
            }
        }
    }
```

Along with `EPiServer.ContentApi.Core.PropertyModel,` custom property models can also inherit from `EPiServer.ContentApi.Core.PersonalizablePropertyModel`. This class is used when custom property models contain data that is dependent on Personalization. It adds an additional constructor parameter `excludePersonalizedContent`, which allows you to implement logic in your property model to set the `Value` property differently in personalized vs. non-personalized contexts. 

## Registering Custom Property Models ##

In order for your custom property models to take effect, you need to create a custom implementation of `IPropertyModelHandler` and register it in an initialization module during startup. The implementation of `IPropertyModelHandler` has a collection of `EPiServer.ContentApi.Core.TypeModel` called `ModelTypes`, which maintains a map of which PropertyData instances that the handler is capable of handling.

The simplest method of adding a custom handler is to extend the default one, `DefaultPropertyModelHandler`, and override the `SortOrder` and `ModelTypes` property.

Example: The following handler registers our `LowercaseLongStringPropertyModel` from above, with a SortOrder higher than the default handler, ensuring that our custom handler will be used for all Long String properties.

```c#
    public class LowercaseLongStringPropertyModelHandler : DefaultPropertyModelHandler
    {
        public LowercaseLongStringPropertyModelHandler()
        {
            ModelTypes = new List<TypeModel>
            {
                new TypeModel { ModelType = typeof(LowercaseLongStringPropertyModel), ModelTypeString = nameof(LowercaseLongStringPropertyModel), PropertyType = typeof(PropertyLongString) },
            };
        }

        public override int SortOrder { get; } = 100;
    }
```

In order to register this handler, register it as a Singleton in an IConfigurableModule:

```c#
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class SiteInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            ...

            context.Services.AddSingleton<IPropertyModelHandler, LowercaseLongStringPropertyModelHandler>();

            ...
        }

        public void Initialize(InitializationEngine context) { }

        public void Uninitialize(InitializationEngine context) { }
    }
```

In some cases, a full implementation of `IPropertyModelHandler` may be preferable, such as when custom logic is required to choose between different `PropertyModel` implementations. In that case, your custom handler must also implement `CanHandleProperty`, which verifies, based on the provided `PropertyData`, that the implementation of `IPropertyModelHandler` is able to handle the provided type. In addition, your custom handler must also implement `GetValue`, which creates and returns any instances of your custom property models based on the provided instance of `PropertyData`. 