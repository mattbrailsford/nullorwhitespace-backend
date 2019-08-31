using System.Collections.Generic;

namespace NullOrWhitespace.Web.ViewModels
{
    public class InitViewModel
    {
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }
        public string SiteLegalText { get; set; }
        public IEnumerable<LinkViewModel> SiteSocialLinks { get; set; }
    }
}
