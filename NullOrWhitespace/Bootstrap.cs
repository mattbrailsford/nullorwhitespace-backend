using AutoMapper;
using Examine;
using Lucene.Net.Documents;
using NullOrWhitespace.Models;
using NullOrWhitespace.Web.Mapping;
using NullOrWhitespace.Web.ViewModels;
using Our.Umbraco.HeadRest;
using Our.Umbraco.HeadRest.Web.Mapping;
using System;
using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Services;
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
                ViewModelMappings = new HeadRestViewModelMap()
                    .For(HomePage.ModelTypeAlias).If(x => x.Request.QueryString["subRoute"] == "init").MapTo<InitViewModel>()
                    .For(HomePage.ModelTypeAlias).If(x => x.Request.QueryString["subRoute"] == "routes").MapTo<RoutesViewModel>()
                    .For(HomePage.ModelTypeAlias).MapTo<HomePageViewModel>()
                    .For(StandardPage.ModelTypeAlias).MapTo<StandardPageViewModel>()
                    .For(BlogPage.ModelTypeAlias).MapTo<BlogPageViewModel>()
                    .For(BlogPostPage.ModelTypeAlias).MapTo<BlogPostPageViewModel>(),
                RoutesResolver = null
            });

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

                    var sortableField = new Field("_Sort_publishDate",
                        publishDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED);
                }
            };

            indexer.GatheringNodeData += (sender, args) =>
            {
                args.Fields.Add("searchPath", string.Join(" ", args.Fields["path"].Split(',')));
            };
        }
    }
}
