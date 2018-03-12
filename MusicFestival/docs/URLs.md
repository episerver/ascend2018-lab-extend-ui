# URL Handling

When content is retrieved from the Content Api, it often contains URLs within a variety of properties, including the built-in `Url` property for routable content, as well as developer-defined content properties of type `Url`, `XhtmlString`, and `LinkItemCollection`.

These URLs are contextually generated based upon where and how the Content Api is accessed. For developers, it's important to understand how URLs will be generated within different contexts to ensure the content returned in the Api will fit your use case. In general, this contextual URL generation behaves in the same manner as `IUrlResolver`, with a few exceptions.

## Relative URLs ##

URLs are rendered relatively when the linked content is associated with the current Episerver site, based on the domain of the request.

For example, let's assume an Episerver installation contains Site Foo, with a configured primary domain of `foo.local`, and a configured Start Page with an ID of `6`. If this content were to be loaded via the Content Api at `http://sitea.local/api/episerver/content/6`, the `Url` property would be returned as relative: `/`, because the content is associated with the same site that the Api was accessed from. This extends to an Episerver Urls contained within `XhtmlString` and `LinkItemCollection` properties on that page.

Routable Media contained within `For This Page` and `For This Site` folders will have URLs generated relatively if the Api is accessed from the same site as the associated content. Routable Media contained within the `For All Sites / Global Assets` folder will always be generated relatively, since it is not associated with any one site.

## Absolute URLs ##

If we extend our above example, let's add Site Bar to the the same installation with a configured primary domain of `bar.local` and a configured Start Page with an ID of `10`. If we load Site Bar's Start Page from Site Foo's Api at `http://foo.local/api/episerver/content/10`, the `Url` property would be returned as absolute: `http://bar.local/`, because the content is associated with a different site than the one the Api was accessed from, and thus the Primary domain is used to generate an absolute URL.

This pattern extends to Routable Media contained within `For This Page` and `For This Site` folders, generating absolute URLs when accessed from a different site's Api. As noted above, Routable Media contained within the `For All Sites / Global Assets` folder will always be generated with a relative URL.

In order to generate Absolute URLs more consistently across sites, access the Api from a domain not associated with any Episerver site. For example, if we added a binding to our Episerver example above with the domain of `api.foobar.local` and used it to access the Content Api, our requests would generate Absolute URLs in all cases, except for Media contained within the `For All Sites / Global Assets` folder. This setup can be convenient for utilizing Episerver content in external applications.

## URLs in Episerver Find ##

When content is indexed into Episerver Find for use in the Content Search Api, it is indexed as if it is accessed from a different site. This results in Absolute Urls in most cases, except in the `For All Sites / Global Assets` folder exception indicated above. As such, when using the Content Search Api with the `personalize` flag set to `false`, which is the default, Urls will not be generated based on site context, since the content is retrieved directly from the Find index.