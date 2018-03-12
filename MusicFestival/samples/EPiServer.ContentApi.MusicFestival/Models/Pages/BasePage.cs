using EPiServer.ContentApi.MusicFestival.Models.Blocks;
using EPiServer.Core;

namespace EPiServer.ContentApi.MusicFestival.Models.Pages
{
    public class BasePage : PageData
    {
        public virtual BuyTicketBlock BuyTicketBlock { get; set; }
    }
}