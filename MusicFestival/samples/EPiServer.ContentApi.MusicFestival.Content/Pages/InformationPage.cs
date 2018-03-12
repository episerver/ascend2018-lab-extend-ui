using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;

namespace EPiServer.ContentApi.MusicFestival.Content.Pages
{
    [ContentType(DisplayName = "Information Page", GUID = "9bbf34c8-809a-415f-af42-99c543918f08", Description = "")]
    public class InformationPage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "Title",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Title { get; set; }
        
        [Display(
            Name = "Hero Content",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        //[UIHint(UIHint.Image)]
        public virtual ContentArea HeroContentArea { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Preamble",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [UIHint(UIHint.LongString)]
        public virtual string Preamble { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Main body",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual XhtmlString MainBody { get; set; }
        
        [Display(
            Name = "Main Content",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        public virtual ContentArea MainContant { get; set; }
    }
}