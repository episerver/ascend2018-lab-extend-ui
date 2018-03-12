using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.ContentApi.MusicFestival.Content.Pages
{
    [ContentType(DisplayName = "App Root Page", GUID = "f220cf2d-3dc2-4be2-9f38-be1a180650db", Description = "")]
    public class AppRootPage : PageData
    {

        [CultureSpecific]
        [Display(
            Name = "Additional Menu Items",
            Description = "This will hold additional pages that could be added to the app menu.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual ContentArea MenuItems { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Festival Day Detail Content Area",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual ContentArea FestivalDayDetailBlocks { get; set; }
    }
}