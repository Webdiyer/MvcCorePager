using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Webdiyer.AspNetCore
{
    public class UrlFormatHelper
    {
        static Regex _pattern = new Regex(@"\{([^\}]+)\}");
        public static string FormatUrl(string urlFormat, RouteValueDictionary routeValues)
        {
            var maches = _pattern.Matches(urlFormat);
            foreach (Match item in maches)
            {
                if (item.Success && routeValues.ContainsKey(item.Groups[1].Value))
                {
                    urlFormat = urlFormat.Replace(item.Value, routeValues[item.Groups[1].Value].ToString());
                }
            }
            return urlFormat;
        }
    }
}
