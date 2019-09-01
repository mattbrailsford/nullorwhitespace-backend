using NullOrWhitespace.Models;
using NullOrWhitespace.Web.ViewModels;
using System.Linq;
using Umbraco.Core.Mapping;

namespace NullOrWhitespace.Web.Mapping.Maps
{
    public class InitViewModelMapper : BaseMapping<InitViewModelMapper, HomePage, InitViewModel>
    {
        public override void Map(HomePage src, InitViewModel dst, MapperContext ctx)
        {
            dst.SiteName = src.SiteName;
            dst.SiteDescription = src.SiteDescription;
            dst.SiteSocialLinks = src.SiteSocialLinks.Select(x => new LinkViewModel
            {
                Title = x.Name,
                Url = x.Url,
                Target = x.Target
            }).ToList();
            dst.SiteLegalText = src.SiteLegalText;
        }
    }
}
