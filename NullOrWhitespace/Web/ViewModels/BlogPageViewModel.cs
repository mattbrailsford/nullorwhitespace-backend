using Umbraco.Core.Models;

namespace NullOrWhitespace.Web.ViewModels
{
    public class BlogPageViewModel : BasePageViewModel
    {
        public PagedResult<BaseBlogPostPageViewModel> BlogPosts { get; set; }
    }
}
