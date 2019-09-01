using NullOrWhitespace.Models;
using NullOrWhitespace.Web.Mapping.Resolvers;
using NullOrWhitespace.Web.ViewModels;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping.Maps
{
    public class BasePageViewModelMapper : BaseMapping<BasePageViewModelMapper, Page, BasePageViewModel>
    {
        public override void Map(Page src, BasePageViewModel dst, MapperContext ctx)
        {
            dst.Id = src.Id;
            dst.Name = src.Name;
            dst.Url = src.Url;
            dst.Type = src.ContentType.Alias;
            dst.MetaTitle = MetaTitleResolver.Resolve(ctx);
            dst.MetaDescription = src.MetaDescription;
            dst.MetaKeywords = src.MetaKeywords;
        }
    }
}
