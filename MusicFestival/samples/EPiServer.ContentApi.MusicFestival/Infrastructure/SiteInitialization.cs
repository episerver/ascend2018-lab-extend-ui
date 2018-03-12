using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EPiServer.ContentApi.Infrastructure;
using EPiServer.ContentApi.Search.Extensions;
using EPiServer.ContentApi.Search.Infrastructure;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web;
using EPiServer.ContentApi.Core;
using EPiServer.ContentApi.Core.Infrastructure;
using EPiServer.ContentApi.Core.Security;
using EPiServer.ContentApi.MusicFestival.Features.ContentApi;
using EPiServer.ContentApi.MusicFestival.Infrastructure.WebApi;
using EPiServer.Web;

namespace EPiServer.ContentApi.MusicFestival.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class SiteInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {

            var contentApiOptions = new ContentApiOptions
            {
                MultiSiteFilteringEnabled = false
            };
            context.InitializeContentApi(contentApiOptions);
            context.InitializeContentSearchApi(new ContentSearchApiOptions()
            {
                SearchCacheDuration = TimeSpan.Zero
            });

            //context.Services.AddSingleton<IPropertyModelHandler, LowercaseLongStringPropertyModelHandler>();

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap()));

            GlobalConfiguration.Configure(config =>
            {
                config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
                config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
                config.Formatters.XmlFormatter.UseXmlSerializer = true;
                config.DependencyResolver = new StructureMapResolver(context.StructureMap());
                config.MapHttpAttributeRoutes();
                config.EnableCors();
            });

        }

        public void Initialize(InitializationEngine context)
        {
            var options = ServiceLocator.Current.GetInstance<DisplayOptions>();
            options
                .Add("full", "Full", EPiServerApplication.ContentAreaTags.FullWidth, "", "epi-icon__layout--full")
                .Add("wide", "Wide", EPiServerApplication.ContentAreaTags.TwoThirdsWidth, "", "epi-icon__layout--two-thirds")
                .Add("half", "Half", EPiServerApplication.ContentAreaTags.HalfWidth, "", "epi-icon__layout--half")
                .Add("narrow", "Narrow", EPiServerApplication.ContentAreaTags.OneThirdWidth, "", "epi-icon__layout--one-third");

            AreaRegistration.RegisterAllAreas();
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}