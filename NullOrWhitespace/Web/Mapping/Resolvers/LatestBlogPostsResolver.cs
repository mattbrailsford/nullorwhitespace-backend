using AutoMapper;
using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace NullOrWhitespace.Web.Mapping.Resolvers
{
    public class LatestBlogPostsResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            var currentPage = source.Context.SourceValue as IPublishedContent;
            var homePage = currentPage.AncestorOrSelf(HomePage.ModelTypeAlias);
            var blogPage = homePage.Children.OfType<BlogPage>().First();
            var latestBlogPosts = blogPage.Children.OfType<BlogPostPage>()
                .OrderByDescending(x => x.PublishDate)
                .Take(3);

            var mapped = Mapper.Map<IEnumerable<BaseBlogPostPageViewModel>>(latestBlogPosts);

            return source.New(mapped);
        }
    }
}
