using AutoMapper;
using Our.Umbraco.HeadRest.Web.Mapping;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NullOrWhitespace.Web.Mapping.Resolvers
{
    public class RoutesResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            var currentPage = source.Context.SourceValue as IPublishedContent;
            var context = source.Context.Options.Items["HeadRestMappingContext"] as HeadRestMappingContext;
            var navigator = context.UmbracoContext.ContentCache.GetXPathNavigator();
            var itterator = navigator.Select($"id({currentPage.Id})/descendant-or-self::*[@isDoc]");

            var routes = new List<string>();

            while (itterator.MoveNext())
            {
                if (int.TryParse(itterator.Current.Evaluate("string(@id)").ToString(), out int id))
                {
                    var contentType = itterator.Current.Evaluate("string(@nodeTypeAlias)").ToString();

                    // TODO: Handled paginated blog posts

                    routes.Add(context.UmbracoContext.UrlProvider.GetUrl(id));
                }
            }

            return source.New(routes);
        }
    }
}
