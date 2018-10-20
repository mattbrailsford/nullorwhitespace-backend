using System.Collections.Generic;

namespace NullOrWhitespace.Web.ViewModels
{
    public class BlogPostPageViewModel : BaseBlogPostPageViewModel
    {
        public IEnumerable<BaseBlockViewModel> Blocks { get; set; }
    }
}
