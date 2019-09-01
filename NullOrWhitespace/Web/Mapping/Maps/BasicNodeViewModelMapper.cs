using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using System.Linq;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping.Maps
{
    public class BasicNodeViewModelMapper : BaseMapping<BasicNodeViewModelMapper, Page, BasicNodeViewModel>
    {
        public override void Map(Page src, BasicNodeViewModel dst, MapperContext ctx)
        {
            dst.Id = src.Id;
            dst.Slug = src.ContentType.Alias == HomePage.ModelTypeAlias ? "" : src.UrlSegment;
            dst.Type = src.ContentType.Alias;
            dst.Children = src.Children.Select(x => Map((Page)x, ctx));
        }
    }
}
