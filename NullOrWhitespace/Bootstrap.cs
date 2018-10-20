using AutoMapper;
using NullOrWhitespace.Models;
using NullOrWhitespace.Web.Mapping;
using NullOrWhitespace.Web.ViewModels;
using Our.Umbraco.HeadRest;
using Our.Umbraco.HeadRest.Web.Mapping;
using Umbraco.Core;

namespace NullOrWhitespace
{
    public class Bootstrap : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase app, ApplicationContext ctx)
        {
            Mapper.AddProfile<ViewModelMappingProfile>();

            HeadRest.ConfigureEndpoint(new HeadRestOptions
            {
                ViewModelMappings = new HeadRestViewModelMap()
                    .For(HomePage.ModelTypeAlias).If(x => x.Request.QueryString["subRoute"] == "init").MapTo<InitViewModel>()
                    .For(HomePage.ModelTypeAlias).If(x => x.Request.QueryString["subRoute"] == "routes").MapTo<RoutesViewModel>()
                    .For(HomePage.ModelTypeAlias).MapTo<HomePageViewModel>()
                    .For(StandardPage.ModelTypeAlias).MapTo<StandardPageViewModel>()
                    .For(BlogPage.ModelTypeAlias).MapTo<BlogPageViewModel>()
                    .For(BlogPostPage.ModelTypeAlias).MapTo<BlogPostPageViewModel>(),
                RoutesResolver = null
            });
        }
    }
}
