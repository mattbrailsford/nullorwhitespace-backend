using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping.Maps
{
    public class HomePageViewModelMapping : BaseMapping<HomePageViewModelMapping, HomePage, HomePageViewModel>
    {
        public override void Map(HomePage src, HomePageViewModel dst, MapperContext ctx)
        {
            // Base mappings
            BasePageViewModelMapper.Instance.Map(src, dst, ctx);

            // Custom maps
            dst.Intro = src.Intro;
            dst.Image = src.Image.Url;
        }
    }
}
