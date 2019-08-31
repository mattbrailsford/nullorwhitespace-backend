using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping
{
    public class BlogPostPageViewModelMapping : BaseMapping<BlogPostPageViewModelMapping, BlogPostPage, BlogPostPageViewModel>
    {
        public override void Map(BlogPostPage src, BlogPostPageViewModel dst, MapperContext ctx)
        {
            // Base mappings
            BasePageViewModelMapper.Instance.Map(src, dst, ctx);

            // Custom maps
            dst.Excerpt = src.Excerpt;
            dst.Body = src.Body.ToString();
            dst.PublishDate = src.PublishDate;
            dst.Author = src.WriterName;
        }
    }
}
