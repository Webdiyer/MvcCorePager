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
using System.Text.Encodings.Web;
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
            string numLinks = CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
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
            string numLinks = CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
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
            string numLinks = CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
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
            string numLinks = CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\">{numLinks}</div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void ShowNumericPagerItemsIsFalse_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                ShowNumericPagerItems=false
            };
            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><<<<a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
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
            string numLinks = CreateNumericPageLinks(1, 10, 3, "/Home/test?pageindex={0}");
            string expectedResult = $"<div data-current-page=\"3\" data-first-page=\"/Home/test?pageindex=1\" data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><a href=\"/Home/test?pageindex=1\"><<</a><a href=\"/Home/test?pageindex=2\"><</a>{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=4\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Theory]
        [InlineData("[{0}]", null)]
        [InlineData("[{0}]", "[{0}]")]
        [InlineData("【{0}】", null)]
        [InlineData("-{0}-", "［{0}］")]
        [InlineData("【{0}】", "-{0}-")]
        public void FormatingPageNumber_ShouldOutputCorrectContent(string numberFormat,string currentPageNumberFormat)
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                CurrentPageNumberFormatString= currentPageNumberFormat,
                PageNumberFormatString= numberFormat
            };

            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}",numberFormat: numberFormat,currentPageFormat: string.IsNullOrWhiteSpace(currentPageNumberFormat)?numberFormat:currentPageNumberFormat);
            string expectedResult = $"<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><<<{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void PagerItemTemplates_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            string template = "<span>{0}</span>";
            var tagHelper = new MvcCorePagerTagHelper(GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                PagerItemTemplate= template
            };

            var tagHelperContext = GetTagHelperContext();
            var tagHelperOutput = GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}", template:template);
            string expectedResult = $"{CreateStartTag(18)}<span><<</span><span><</span>{numLinks}<span><a href=\"/Home/test?pageindex=11\">...</a></span><span><a href=\"/Home/test?pageindex=2\">></a></span><span><a href=\"/Home/test?pageindex=18\">>></a></span></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        string CreateStartTag(int pageCount, int currentPage = 1,string piparam= "pageindex",string firstPageUrl="/Home/test?pageindex=1", string urlFormat= "/Home/test?pageindex=__pageindex__",string ivperror= "Invalid page index",string orerror= "Page index out of range")
        {
            if (currentPage == 1)
            {
                return $"<div data-invalid-page-error=\"{ivperror}\" data-out-range-error=\"{orerror}\" data-page-count=\"{pageCount}\" data-page-parameter=\"{piparam}\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"{urlFormat}\">";
            }
            else
            {
                return $"<div data-current-page=\"{currentPage}\" data-first-page=\"{firstPageUrl}\" data-invalid-page-error=\"{ivperror}\" data-out-range-error=\"{orerror}\" data-page-count=\"{pageCount}\" data-page-parameter=\"{piparam}\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"{urlFormat}\">";
            }
        }

        string CreateNumericPageLinks(int startIndex,int endIndex,int currentIndex,string urlFormat,string cssClass=null,string numberFormat="{0}",string currentPageFormat="{0}",string template="{0}")
        {
            var sbuilder = new StringBuilder();
            for(var i = startIndex; i <= endIndex; i++)
            {
                var sb = new StringBuilder();
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
                if (!string.IsNullOrWhiteSpace(template))
                {
                    sbuilder.AppendFormat(template, sb);
                }
                else
                {
                    sbuilder.Append(sb);
                }
            }
            return sbuilder.ToString();
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
