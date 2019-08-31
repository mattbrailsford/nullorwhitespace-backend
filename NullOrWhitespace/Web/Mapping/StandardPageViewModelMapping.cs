using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping
{
    public class StandardPageViewModelMapping : BaseMapping<StandardPageViewModelMapping, StandardPage, StandardPageViewModel>
    {
        public override void Map(StandardPage src, StandardPageViewModel dst, MapperContext ctx)
        {
            // Base mappings
            BasePageViewModelMapper.Instance.Map(src, dst, ctx);

            // Custom maps
            dst.Body = src.Body.ToString();
        }
    }
}
