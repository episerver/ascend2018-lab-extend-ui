using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EPiServer.ContentApi.MusicFestival.Models.Pages
{
    [ContentType(DisplayName = "Landing Page", GUID = "46278700-3173-4945-b143-befe071f0f71", Description = "")]
    public class LandingPage : BasePage
    {
        [CultureSpecific]
        [Display(
            Name = "Hero Image",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [UIHint(UIHint.Image)]
        public virtual Url HeroImage { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Title",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual string Title { get; set; }


        [CultureSpecific]
        [Display(
            Name = "Subtitle",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual string Subtitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Main Content Area",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual ContentArea MainContentArea { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Footer Content Area",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        public virtual ContentArea FooterContentArea { get; set; }
    }
}