using AutoMapper;
using Examine;
using NullOrWhitespace.Models;
using Our.Umbraco.HeadRest.Web.Extensions;
using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace NullOrWhitespace.Web.Mapping.Resolvers
{
    public class RoutesResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            var currentPage = source.Context.SourceValue as IPublishedContent;
            var context = source.Context.GetHeadRestMappingContext();
            var navigator = context.UmbracoContext.ContentCache.GetXPathNavigator();
            var itterator = navigator.Select($"id({currentPage.Id})/descendant-or-self::*[@isDoc]");

            var routes = new List<string>();

            while (itterator.MoveNext())
            {
                if (int.TryParse(itterator.Current.Evaluate("string(@id)").ToString(), out int id))
                {
                    var contentType = itterator.Current.Evaluate("string(@nodeTypeAlias)").ToString();
                    
                    if (contentType == BlogPage.ModelTypeAlias)
                    {
                        // TODO: Create a helper for this as it's currently duplicated
                        // in the PagniatedBlogPostsResolver class
                        var searcher = ExamineManager.Instance.DefaultSearchProvider;
                        var criteria = searcher.CreateSearchCriteria()
                            .Field("nodeTypeAlias", BlogPostPage.ModelTypeAlias)
                            .And().Field("searchPath", id.ToInvariantString())
                            .Not().Field("umbracoNaviHide", "1")
                            .And().OrderByDescending("publishDate")
                            .Compile();

                        var results = searcher.Search(criteria, 0);
                        var totalPages = (int)Math.Ceiling((double)results.TotalItemCount / NullOrWhitespaceConstants.BlogPageSize);
                        var baseUrl = context.UmbracoContext.UrlProvider.GetUrl(id);

                        routes.Add(baseUrl);
                        for(var p = 2; p <= totalPages; p++)
                        {
                            routes.Add(baseUrl + p + '/');
                        }
                    }
                    else
                    {
                        routes.Add(context.UmbracoContext.UrlProvider.GetUrl(id));
                    }
                }
            }

            return source.New(routes);
        }
    }
}
