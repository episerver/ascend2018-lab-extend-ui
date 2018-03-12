using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.Reference.Commerce.Site.Features.Start.Pages
{
    [ContentType(DisplayName = "All Properties Page", GUID = "a29628b6-c856-465f-b0de-a1691ca41034", Description = "")]
    public class AllPropertiesPage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "String Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual String StringProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "XhtmlString Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString XhtmlStringProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Int Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual int IntProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "DateTime Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual DateTime DateTimeProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Double Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual double DoubleProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Boolean Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual bool BooleanProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "PageType Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual PageType PageTypeProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "PageReference Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual PageReference PageReferenceProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "ContentReference Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentReference ContentReferenceProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Url Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual Url UrlProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "ContentArea Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea ContentAreaProperty { get; set; }

        [CultureSpecific]
        [Display(
            Name = "LinkItemCollection Property",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual LinkItemCollection LinkItemCollectionProperty { get; set; }
    }
}