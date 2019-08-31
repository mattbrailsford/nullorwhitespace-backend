using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Our.Umbraco.HeadRest;
using Our.Umbraco.HeadRest.Web;
using Our.Umbraco.HeadRest.Web.Mapping;
using Our.Umbraco.HeadRest.Web.Routing;
using System;
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
                    .For("^/(?<altRoute>init|sitemap|content-types)/?$").MapTo("/")
                    .For("^/(blog)/(?<page>[0-9]+)/?$").MapTo("/$1/"),
                ViewModelMappings = new HeadRestViewModelMap()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "init")
                        .MapTo<InitViewModel>()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "sitemap")
                        .MapTo<BasicNodeViewModel>()
                    //.For(HomePage.ModelTypeAlias)
                    //    .If(x => x.Request.HeadRestRouteParam("altRoute") == "content-types")
                    //    .MapTo<ContentTypesViewModel>()
                    .For(HomePage.ModelTypeAlias).MapTo<HomePageViewModel>()
                    .For(StandardPage.ModelTypeAlias).MapTo<StandardPageViewModel>()
                    .For(BlogPage.ModelTypeAlias).MapTo<BlogPageViewModel>()
                    .For(BlogPostPage.ModelTypeAlias).MapTo<BlogPostPageViewModel>()
            });
        }

        public void Terminate()
        { }
    }
}
