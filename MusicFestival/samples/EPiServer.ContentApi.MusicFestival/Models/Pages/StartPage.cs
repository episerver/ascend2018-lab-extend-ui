using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.ContentApi.MusicFestival.Models.Pages
{
    [ContentType(DisplayName = "Start Page", GUID = "6c312a46-6fba-4389-8e23-fe91567abfcf", Description = "")]
    public class StartPage : PageData
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         */
    }
}