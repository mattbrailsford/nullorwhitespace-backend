using AutoMapper;
using NullOrWhitespace.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace NullOrWhitespace.Web.Mapping.Resolvers
{
    public class MetaTitleResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            var currentPage = source.Context.SourceValue as IPublishedContent;
            var homePage = currentPage.AncestorOrSelf(HomePage.ModelTypeAlias).OfType<HomePage>();

            var siteName = homePage.SiteName;
            var siteDescription = homePage.SiteDescription;
            var metaTitle = currentPage.Id == homePage.Id
                ? $"{siteName} - {siteDescription}"
                : $"{currentPage.Name} | {siteName}";

            return source.New(metaTitle);
        }
    }
}
