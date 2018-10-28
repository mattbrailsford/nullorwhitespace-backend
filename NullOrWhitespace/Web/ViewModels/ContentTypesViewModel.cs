using System.Collections.Generic;

namespace NullOrWhitespace.Web.ViewModels
{
    public class ContentTypesViewModel
    {
        public IEnumerable<ContentTypeViewModel> ContentTypes { get; set; }
    }

    public class ContentTypeViewModel
    {
        public int Id { get; set; }
        public string Alias { get; set; }
    }
}
