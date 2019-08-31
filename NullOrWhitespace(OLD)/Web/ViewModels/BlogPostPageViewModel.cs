using System;

namespace NullOrWhitespace.Web.ViewModels
{
    public class BlogPostPageViewModel : BasePageViewModel
    {
        public string Excerpt { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
    }
}
