using AutoMapper;
using Examine;
using Lucene.Net.Documents;
using NullOrWhitespace.Models;
using NullOrWhitespace.Web.Mapping;
using NullOrWhitespace.Web.ViewModels;
using Our.Umbraco.HeadRest;
using Our.Umbraco.HeadRest.Web.Extensions;
using Our.Umbraco.HeadRest.Web.Mapping;
using Our.Umbraco.HeadRest.Web.Routing;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Net;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Services;
using Umbraco.Web.Cache;
using UmbracoExamine;

namespace NullOrWhitespace
{
    public class Bootstrap : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase app, ApplicationContext ctx)
        {
            // Configure mapping
            Mapper.AddProfile<ViewModelMappingProfile>();

            // Configure endpoint
            HeadRest.ConfigureEndpoint(new HeadRestOptions
            {
                CustomRouteMappings = new HeadRestRouteMap()
                    .For("^/(?<altRoute>init|routes|content-types)/?$").MapTo("/")
                    .For("^/(blog)/(?<page>[0-9]+)/?$").MapTo("/$1/"),
                ViewModelMappings = new HeadRestViewModelMap()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "init")
                        .MapTo<InitViewModel>()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "routes")
                        .MapTo<RoutesViewModel>()
                    .For(HomePage.ModelTypeAlias)
                        .If(x => x.Request.HeadRestRouteParam("altRoute") == "content-types")
                        .MapTo<ContentTypesViewModel>()
                    .For(HomePage.ModelTypeAlias).MapTo<HomePageViewModel>()
                    .For(StandardPage.ModelTypeAlias).MapTo<StandardPageViewModel>()
                    .For(BlogPage.ModelTypeAlias).MapTo<BlogPageViewModel>()
                    .For(BlogPostPage.ModelTypeAlias).MapTo<BlogPostPageViewModel>()
            });

            // Configure auto build
            CacheRefresherBase<PageCacheRefresher>.CacheUpdated += (sender, args) =>
            {
                var url = ConfigurationManager.AppSettings["NetlifyWebhookUrl"];
                if (!url.IsNullOrWhiteSpace())
                {
                    using (var client = new WebClient())
                    {
                        client.UploadValues(url, "POST", new NameValueCollection());
                    }
                }
            };

            // Configure default values
            ContentService.Created += (sender, args) =>
            {
                if (args.Entity.ContentType.Alias == BlogPostPage.ModelTypeAlias)
                {
                    args.Entity.SetValue("publishDate", DateTime.Now);
                }
            };

            // Configure lucene
            var indexer = (UmbracoContentIndexer)ExamineManager.Instance.IndexProviderCollection["ExternalIndexer"];
            indexer.DocumentWriting += (sender, args) =>
            {
                if (args.Fields["nodeTypeAlias"].InvariantEquals(BlogPostPage.ModelTypeAlias))
                {
                    var publishDate = args.Fields.ContainsKey("publishDate")
                        ? DateTime.Parse(args.Fields["publishDate"])
                        : DateTime.Parse(args.Fields["createDate"]);

                    var sortableField = new Field("__Sort_publishDate",
                        publishDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED);

                    args.Document.Add(sortableField);
                }
            };

            indexer.GatheringNodeData += (sender, args) =>
            {
                var publishDate = args.Fields.ContainsKey("publishDate")
                        ? DateTime.Parse(args.Fields["publishDate"])
                        : DateTime.Parse(args.Fields["createDate"]);

                args.Fields["searchPublishDate"] = DateTools.DateToString(publishDate, DateTools.Resolution.SECOND);

                args.Fields.Add("searchPath", string.Join(" ", args.Fields["path"].Split(',')));
            };
        }
    }
}
