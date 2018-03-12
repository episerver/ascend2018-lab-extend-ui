using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace EPiServer.ContentApi.MusicFestival.Models.Blocks
{
    [ContentType(DisplayName = "Buy Ticket Block", GUID = "ac096c4f-56ab-4396-9f5c-cfa923875c18", Description = "")]
    public class BuyTicketBlock : BlockData
    {
        [CultureSpecific]
        [Required]
        public virtual string Message { get; set; }
    }
}