using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using System;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping
{
    public class NullOrWhitespaceMapDefinition : IMapDefinition
    {
        public void DefineMaps(UmbracoMapper mapper)
        {
            DefineMap<Page, BasePageViewModel>(mapper, BasePageViewModelMapper.Instance.Map);
            DefineMap<Page, BasicNodeViewModel>(mapper, BasicNodeViewModelMapper.Instance.Map);
            DefineMap<HomePage, InitViewModel>(mapper, InitViewModelMapper.Instance.Map);
            DefineMap<HomePage, SitemapViewModel>(mapper, SitemapViewModelMapper.Instance.Map);
            DefineMap<BlogPostPage, BlogPostPageViewModel>(mapper, BlogPostPageViewModelMapping.Instance.Map);
            DefineMap<StandardPage, StandardPageViewModel>(mapper, StandardPageViewModelMapping.Instance.Map);
        }

        private void DefineMap<TFrom, TTo>(UmbracoMapper mapper, Action<TFrom, TTo, MapperContext> map)
            where TTo : new()
        {
            mapper.Define(
               (src, ctx) => new TTo(),
               map
            );
        }
    }
}
