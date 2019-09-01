using NullOrWhitespace.Models;
using NullOrWhitespace.Web.Mapping.Maps;
using NullOrWhitespace.Web.ViewModels;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping
{
    public class NullOrWhitespaceMapDefinition : IMapDefinition
    {
        public void DefineMaps(UmbracoMapper mapper)
        {
            mapper.Define<HomePage, InitViewModel>(InitViewModelMapper.Instance.Map);
            mapper.Define<HomePage, SitemapViewModel>(SitemapViewModelMapper.Instance.Map);
            mapper.Define<BlogPostPage, BlogPostPageViewModel>(BlogPostPageViewModelMapping.Instance.Map);
            mapper.Define<StandardPage, StandardPageViewModel>(StandardPageViewModelMapping.Instance.Map);
            mapper.Define<Page, BasicNodeViewModel>(BasicNodeViewModelMapper.Instance.Map);
            mapper.Define<Page, BasePageViewModel>(BasePageViewModelMapper.Instance.Map);
        }
    }
}
