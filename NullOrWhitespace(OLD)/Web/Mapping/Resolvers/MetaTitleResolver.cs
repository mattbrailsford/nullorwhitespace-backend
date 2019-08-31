using NullOrWhitespace.Models;
using Umbraco.Web;
using Our.Umbraco.HeadRest;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping.Resolvers
{
    public class MetaTitleResolver
    {
        public static string Resolve(MapperContext mapperContext)
        {
            var headRestContext = mapperContext.GetHeadRestMappingContext();

            var currentPage = headRestContext.Content;
            var homePage = currentPage.AncestorOrSelf<HomePage>();

            var siteName = homePage.SiteName;
            var siteDescription = homePage.SiteDescription;
            var metaTitle = currentPage.Id == homePage.Id
                ? $"{siteName} - {siteDescription}"
                : $"{currentPage.Name} | {siteName}";

            return metaTitle;
        }
    }
}
