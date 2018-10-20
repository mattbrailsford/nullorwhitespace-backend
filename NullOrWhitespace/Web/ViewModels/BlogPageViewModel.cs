using System.Collections.Generic;

namespace NullOrWhitespace.Web.ViewModels
{
    public class BlogPageViewModel : BasePageViewModel
    {
        public IEnumerable<BaseBlogPostPageViewModel> BlogPosts { get; set; }
    }
}
