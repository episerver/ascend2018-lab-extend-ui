using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.ContentApi.MusicFestival.Models.Preview;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Framework.Web.Mvc;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;

namespace EPiServer.ContentApi.MusicFestival.Controllers
{
    [TemplateDescriptor(Inherited = true,
        TemplateTypeCategory = TemplateTypeCategories.MvcController,
        Tags = new[] { RenderingTags.Preview, RenderingTags.Edit },
        AvailableWithoutTag = false)]
    [RequireClientResources]
    public class PreviewController : ActionControllerBase, IRenderTemplate<BlockData>

    {
        public ActionResult Index(IContent currentContent)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var startPage = contentRepository.Get<PageData>(ContentReference.StartPage);
            var model = new BlockEditPageViewModel(startPage, currentContent);

            return View(model);
        }
    }
}