using System.Collections.Generic;

namespace NullOrWhitespace.Web.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        public string Intro { get; set; }

        public string Image { get; set; }

        public IEnumerable<BaseBlogPostPageViewModel> LatestBlogPosts { get; set; }
    }
}
