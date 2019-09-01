using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Our.Umbraco.HeadRest;
using Our.Umbraco.HeadRest.Web;
using Our.Umbraco.HeadRest.Web.Mapping;
using Our.Umbraco.HeadRest.Web.Routing;
using Umbraco.Core.Composing;

namespace NullOrWhitespace.Composing
{
    public class NullOrWhitespaceComponent : IComponent 
    {
        private readonly HeadRest _headRest;

        public NullOrWhitespaceComponent(HeadRest headRest)
            => _headRest = headRest;

        public void Initialize()
        {
            _headRest.ConfigureEndpoint(new HeadRestOptions 
            {
                CustomRouteMappings = new HeadRestRouteMap()
                    .For("^/(?<altRoute>init|sitemap)/?$").MapTo("/"), 
                ViewModelMappings = new HeadRestViewModelMap()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "init")
                        .MapTo<InitViewModel>()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "sitemap")
                        .MapTo<SitemapViewModel>()
                    .For(StandardPage.ModelTypeAlias).MapTo<StandardPageViewModel>()
                    .For(BlogPostPage.ModelTypeAlias).MapTo<BlogPostPageViewModel>()
            });
        }

        public void Terminate()
        { }
    }
}
