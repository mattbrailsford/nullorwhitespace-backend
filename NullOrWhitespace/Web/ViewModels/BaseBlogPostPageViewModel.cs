using System;

namespace NullOrWhitespace.Web.ViewModels
{
    public class BaseBlogPostPageViewModel : BasePageViewModel
    {
        public string Excerpt { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }
    }
}
