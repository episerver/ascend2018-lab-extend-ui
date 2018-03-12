using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EPiServer.ContentApi.MusicFestival.Content.Blocks
{
    [ContentType(DisplayName = "Festival Day Time Range Block", GUID = "5879858e-8a73-4553-86e9-4b54304bfc26", Description = "")]
    public class FestivalDayTimeRangeBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Day of Week",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string DayOfWeek { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Performances Start Time",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual DateTime PerformancesStartTime { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Performances End Time",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual DateTime PerformancesEndTime { get; set; }
    }
}