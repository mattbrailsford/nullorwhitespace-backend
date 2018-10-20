using System;
using System.Web;

namespace NullOrWhitespace.Web.Extensions
{
    public static class StringExtensions
    {
        public static string ToAbsoluteUrl(this string url)
        {
            if (url == null || !url.StartsWith("/"))
                return url;

            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + url;
        }
    }
}
