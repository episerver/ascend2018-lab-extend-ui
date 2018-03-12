using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EPiServer.ContentApi.MusicFestival.Content.Blocks
{
    [ContentType(DisplayName = "VIP Detail Block", GUID = "90ac5c08-0d79-4541-aa52-2206a6b0670c", Description = "")]
    public class VIPDetailBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Artist Title",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string ArtistTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Artist Image",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [UIHint(UIHint.Image)]
        public virtual Url ArtistImage { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Location",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [UIHint(UIHint.Image)]
        public virtual string PerformanceLocation { get; set; }

        [Display(
            Name = "Performance Start Time",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        public virtual DateTime PerformanceStartTime { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Performance End Time",
            GroupName = SystemTabNames.Content,
            Order = 55)]
        public virtual DateTime PerformanceEndTime { get; set; }
    }
}