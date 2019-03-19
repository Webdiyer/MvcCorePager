using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.AspNetCore;
using Xunit;

namespace Webdiyer.MvcCorePagerTests
{
    public class TagHelperTests
    {
        [Fact]
        public void DefaultSettings_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext
            };

            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = GenerateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><<<{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Theory]
        [InlineData("first","prev","next","last")]
        [InlineData("首页","上页","下页","尾页")]
        [InlineData("First Page","Prev Page","Next Page","Last Page")]
        public void NavigationPagerItemText_ShouldOutputCorrectContent(string first,string prev,string next,string last)
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                FirstPageText= first,
                NextPageText=next,
                PrevPageText=prev,
                LastPageText=last
            };
            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = GenerateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\">{first}{prev}{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">{next}</a><a href=\"/Home/test?pageindex=18\">{last}</a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Fact]
        public void ShowFirstLastIsFalse_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                ShowFirstLast=false                
            };

            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = GenerateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Fact]
        public void HideAllNavigationPagerItems_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                ShowPrevNext = false,
                ShowFirstLast=false,
                ShowMorePagerItems=false
            };
            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = GenerateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\">{numLinks}</div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void AutoHideIsTrue_ShouldOutputEmptyHtmlTagIfPageCountIs1()
        {
            var pagedList = Enumerable.Range(1, 8).ToPagedList(1, 10);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext
            };
            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string expectedResult = "<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"1\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void CurrentPageIndexLargeThan1_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(3, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext
            };

            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = GenerateNumericPageLinks(1, 10, 3, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-current-page=\"3\" data-first-page=\"/Home/test?pageindex=1\" data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><a href=\"/Home/test?pageindex=1\"><<</a><a href=\"/Home/test?pageindex=2\"><</a>{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=4\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        string GenerateNumericPageLinks(int startIndex,int endIndex,int currentIndex,string urlFormat,string cssClass=null,string numberFormat="{0}",string currentPageFormat="{0}")
        {
            var sb = new StringBuilder();
            for(var i = startIndex; i <= endIndex; i++)
            {
                if (i == currentIndex)
                {
                    sb.AppendFormat(currentPageFormat, i);
                }
                else
                {
                    sb.Append("<a");
                    if (!string.IsNullOrWhiteSpace(cssClass))
                    {
                        sb.AppendFormat(" class=\"{0}\"", cssClass);
                    }
                    sb.Append(" href=\"");
                    sb.Append(string.Format(urlFormat, i));
                    sb.Append("\">").AppendFormat(numberFormat,i).Append("</a>");
                }
            }
            return sb.ToString();
        }

        ViewContext GetViewContext(HttpContext httpContext, RouteValueDictionary routeValues)
        {
            return new ViewContext()
            {
                RouteData = new RouteData(routeValues),
                HttpContext = httpContext
            };
        }

        TagHelperContext GetTagHelperContext()
        {
            return new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
        }

        TagHelperOutput GetTagHelperOutput(string tagName)
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

        private IUrlHelperFactory GetUrlHelperFactory()
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
