using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping.Maps
{
    public class SitemapViewModelMapper : BaseMapping<SitemapViewModelMapper, HomePage, SitemapViewModel>
    {
        public override void Map(HomePage src, SitemapViewModel dst, MapperContext ctx)
        {
            dst.Root = BasicNodeViewModelMapper.Instance.Map(src, ctx);
        }
    }
}
