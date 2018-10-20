using AutoMapper;
using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Our.Umbraco.HeadRest.Web.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace NullOrWhitespace.Web.Mapping.Resolvers
{
    public class PagniatedBlogPostsResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            var blogPage = source.Context.SourceValue as BlogPage;
            if (blogPage == null)
                return source.Ignore();

            var context = source.Context.Options.Items["HeadRestMappingContext"] as HeadRestMappingContext;
            if (context == null)
                return source.Ignore();

            var page = int.Parse("0" + context.Request.QueryString["p"]);
            if (page == 0) page = 1;

            // TODO: Switch to XPath or Examine query

            var posts = blogPage.Children.OfType<BlogPostPage>()
                .OrderByDescending(x => x.PublishDate)
                .Skip((page - 1) * NullOrWhitespaceConstants.BlogPageSize)
                .Take(NullOrWhitespaceConstants.BlogPageSize);

            var mapped = Mapper.Map<IEnumerable<BaseBlogPostPageViewModel>>(posts);

            return source.New(mapped);
        }
    }
}
