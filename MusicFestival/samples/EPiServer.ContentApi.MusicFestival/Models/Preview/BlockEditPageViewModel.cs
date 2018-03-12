using EPiServer.Core;

namespace EPiServer.ContentApi.MusicFestival.Models.Preview
{
    public class BlockEditPageViewModel
    {
        public BlockEditPageViewModel(PageData page, IContent content)
        {
            PreviewBlock = new PreviewBlock(page, content);
            CurrentPage = page;
        }

        public PreviewBlock PreviewBlock { get; set; }
        public PageData CurrentPage { get; set; }
    }
}