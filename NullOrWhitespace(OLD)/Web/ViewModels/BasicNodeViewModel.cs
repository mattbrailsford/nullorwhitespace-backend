
using System.Collections.Generic;

namespace NullOrWhitespace.Web.ViewModels
{
    public class BasicNodeViewModel
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Type { get; set; }

        public IEnumerable<BasicNodeViewModel> Children { get; set; }
    }
}
