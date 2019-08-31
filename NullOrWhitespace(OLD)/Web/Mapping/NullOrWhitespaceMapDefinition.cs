using NullOrWhitespace.Models;
using NullOrWhitespace.Web.Mapping.Resolvers;
using NullOrWhitespace.Web.ViewModels;
using System.Linq;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping
{
    public class NullOrWhitespaceMapDefinition : IMapDefinition
    {
        public void DefineMaps(UmbracoMapper mapper)
        {
            DefineCommonMaps(mapper);
            DefineInitMap(mapper);
            DefineSitemapMap(mapper);
            DefinePageMaps(mapper);
        }

        private void DefineCommonMaps(UmbracoMapper mapper)
        {
            mapper.Define<Page, BasePageViewModel>(
                (src, ctx) => new BasePageViewModel(),
                (src, dst, ctx) =>
                {
                    dst.Id = src.Id;
                    dst.Name = src.Name;
                    dst.Url = src.Url;
                    dst.Type = src.ContentType.Alias;
                    dst.MetaTitle = MetaTitleResolver.Resolve(ctx);
                    dst.MetaDescription = src.MetaDescription;
                    dst.MetaKeywords = src.MetaKeywords;
                }
            );
        }

        private void DefineInitMap(UmbracoMapper mapper)
        {
            mapper.Define<HomePage, InitViewModel>(
                (src, ctx) => new InitViewModel(),
                (src, dst, ctx) =>
                {
                    dst.SiteName = src.SiteName;
                    dst.SiteDescription = src.SiteDescription;
                    dst.SiteSocialLinks = src.SiteSocialLinks.Select(x => new LinkViewModel
                    {
                        Title = x.Name,
                        Url = x.Url,
                        Target = x.Target
                    });
                }
            );
        }

        private void DefineSitemapMap(UmbracoMapper mapper)
        {
            mapper.Define<HomePage, SitemapViewModel>(
                (src, ctx) => new SitemapViewModel(),
                (src, dst, ctx) =>
                {
                    //dst.Root
                }
            );
        }

        private void DefinePageMaps(UmbracoMapper mapper)
        {
            // HomePage
            mapper.Define<HomePage, HomePageViewModel>(
                (src, ctx) => new HomePageViewModel(),
                (src, dst, ctx) =>
                {
                    //ctx.Map(src, dst);
                    
                }
            );
        }
    }
}
