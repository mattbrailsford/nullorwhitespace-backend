using AutoMapper;
using Examine;
using NullOrWhitespace.Models;
using Our.Umbraco.HeadRest.Web.Mapping;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

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
            
            var searcher = ExamineManager.Instance.DefaultSearchProvider;
            var criteria = searcher.CreateSearchCriteria()
                .Field("nodeTypeAlias", BlogPostPage.ModelTypeAlias)
                .And().Field("searchPath", blogPage.Id.ToInvariantString())
                .Not().Field("umbracoNaviHide", "1")
                .And().OrderByDescending("publishDate")
                .Compile();

            var results = searcher.Search(criteria, page * NullOrWhitespaceConstants.BlogPageSize);
            var result = new PagedResult<BlogPostPage>(results.TotalItemCount, page, NullOrWhitespaceConstants.BlogPageSize);
            result.Items = results.Skip(result.GetSkipSize())
                .Select(x => context.UmbracoContext.ContentCache.GetById(int.Parse(x.Fields["id"])).OfType<BlogPostPage>());

            return source.New(result);
        }
    }
}
