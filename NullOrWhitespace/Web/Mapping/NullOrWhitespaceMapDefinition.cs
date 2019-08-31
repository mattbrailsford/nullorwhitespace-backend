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
            mapper.Define<Page, BasicNodeViewModel>(
                (src, ctx) => new BasicNodeViewModel(),
                (src, dst, ctx) =>
                {
                    dst.Id = src.Id;
                    dst.Slug = src.ContentType.Alias == HomePage.ModelTypeAlias ? "" : src.UrlSegment;
                    dst.Type = src.ContentType.Alias;
                    dst.Children = src.Children.Select(x => ctx.Map<BasicNodeViewModel>(x));
                }
            );

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
                    }).ToList();
                }
            );
        }

        private void DefineSitemapMap(UmbracoMapper mapper)
        {
            mapper.Define<HomePage, SitemapViewModel>(
                (src, ctx) => new SitemapViewModel(),
                (src, dst, ctx) =>
                {
                    dst.Root = ctx.Map<BasicNodeViewModel>(src);
                }
            );
        }

        private void DefinePageMaps(UmbracoMapper mapper)
        {
            // BlogPostPage
            mapper.Define<BlogPostPage, BlogPostPageViewModel>(
                (src, ctx) => new BlogPostPageViewModel(),
                (src, dst, ctx) =>
                {
                    // Run base mappers
                    ctx.Map<Page, BasePageViewModel>(src, dst);

                    // Custom maps
                    dst.Excerpt = src.Excerpt;
                    dst.Body = src.Body.ToString();
                    dst.PublishDate = src.PublishDate;
                    dst.Author = src.WriterName;
                }
            );

            // StandardPage
            mapper.Define<StandardPage, StandardPageViewModel>(
                (src, ctx) => new StandardPageViewModel(),
                (src, dst, ctx) =>
                {
                    // Run base mappers
                    ctx.Map<Page, BasePageViewModel>(src, dst);

                    // Custom maps
                    dst.Body = src.Body.ToString();
                }
            );
        }
    }
}
