using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EPiServer.ContentApi.MusicFestival
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Infrastructure.Attributes.LocalizedRequiredAttribute), typeof(RequiredAttributeAdapter));

            AreaRegistration.RegisterAllAreas();
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional });

        }

        public static class ContentAreaTags
        {
            public const string FullWidth = "u-md-sizeFull";
            public const string TwoThirdsWidth = "u-md-size2of3";
            public const string HalfWidth = "u-md-size1of2";
            public const string OneThirdWidth = "u-md-size1of3";
            public const string NoRenderer = "norenderer";
        }

    }
}