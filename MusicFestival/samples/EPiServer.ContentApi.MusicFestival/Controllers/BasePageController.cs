using System.Web.Mvc;
using EPiServer.ContentApi.MusicFestival.Models.Pages;
using EPiServer.Core;
using EPiServer.Web.Mvc;

namespace EPiServer.ContentApi.MusicFestival.Controllers
{
    public class BasePageController : PageController<BasePage>
    {
        public ViewResult Index(PageData currentPage)
        {
            return View(currentPage);
        }
    }
}