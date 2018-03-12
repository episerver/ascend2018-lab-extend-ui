using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace EPiServer.ContentApi.MusicFestival.Models.Media
{
    [ContentType(DisplayName = "Image File", GUID = "a736bc13-d17c-46e2-ad5d-e37bd3af086b", AvailableInEditMode = false)]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageFile : ImageData
    {
        /*
                [CultureSpecific]
                [Editable(true)]
                [Display(
                    Name = "Description",
                    Description = "Description field's description",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual string Description { get; set; }
         */
    }
}