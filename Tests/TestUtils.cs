using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webdiyer.MvcCorePagerTests
{
    internal static class TestUtils
    {
       internal static string CreateStartTag(int pageCount,string tagName="div", int currentPage = 1, string piparam = "pageindex", string firstPageUrl = "/Home/test?pageindex=1", string urlFormat = "/Home/test?pageindex=__pageindex__", string ivperror = "Invalid page index", string orerror = "Page index out of range")
        {
            if (currentPage == 1)
            {
                return $"<{tagName} data-invalid-page-error=\"{ivperror}\" data-out-range-error=\"{orerror}\" data-page-count=\"{pageCount}\" data-page-parameter=\"{piparam}\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"{urlFormat}\">";
            }
            else
            {
                return $"<{tagName} data-current-page=\"{currentPage}\" data-first-page=\"{firstPageUrl}\" data-invalid-page-error=\"{ivperror}\" data-out-range-error=\"{orerror}\" data-page-count=\"{pageCount}\" data-page-parameter=\"{piparam}\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"{urlFormat}\">";
            }
        }

        internal static string CreateNumericPageLinks(int startIndex, int endIndex, int currentIndex, string urlFormat, string cssClass = null, string numberFormat = "{0}", string currentPageFormat = "{0}", string template = null,string numTemp=null,string curTemp=null)
        {
            var sbuilder = new StringBuilder();
            var tpl = template;
            for (var i = startIndex; i <= endIndex; i++)
            {
                var sb = new StringBuilder();
                if (i == currentIndex)
                {
                    if (!string.IsNullOrWhiteSpace(curTemp))
                    {
                        tpl = curTemp;
                    }
                    sb.AppendFormat(currentPageFormat, i);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(numTemp))
                    {
                        tpl = numTemp;
                    }
                    sb.Append("<a");
                    if (!string.IsNullOrWhiteSpace(cssClass))
                    {
                        sb.AppendFormat(" class=\"{0}\"", cssClass);
                    }
                    sb.Append(" href=\"");
                    sb.Append(string.Format(urlFormat, i));
                    sb.Append("\">").AppendFormat(numberFormat, i).Append("</a>");
                }
                if (!string.IsNullOrWhiteSpace(tpl))
                {
                    sbuilder.AppendFormat(tpl, sb);
                }
                else
                {
                    sbuilder.Append(sb);
                }
            }
            return sbuilder.ToString();
        }

        internal static ViewContext GetViewContext(HttpContext httpContext, RouteValueDictionary routeValues)
        {
            return new ViewContext()
            {
                RouteData = new RouteData(routeValues),
                HttpContext = httpContext
            };
        }

        internal static TagHelperContext GetTagHelperContext()
        {
            return new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
        }

        internal static TagHelperOutput GetTagHelperOutput(string tagName)
        {
            var tagHelperOutput = new TagHelperOutput(tagName,
                new TagHelperAttributeList(), (result, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            return tagHelperOutput;
        }

        internal static IUrlHelperFactory GetUrlHelperFactory()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns<UrlActionContext>(s => {
                    var prms = "";
                    var rv = new RouteValueDictionary(s.Values);
                    foreach (var kv in rv)
                    {
                        if (kv.Key.ToLower() != "action" && kv.Key.ToLower() != "controller")
                        {
                            prms += "&" + kv.Key + "=" + kv.Value;
                        }
                    }
                    return $"/{rv["controller"]}/{s.Action}?{prms.Trim('&')}";
                });

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f =>
                    f.GetUrlHelper(It.IsAny<ActionContext>()))
                        .Returns(urlHelper.Object);
            return urlHelperFactory.Object;
        }
    }
}
