using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;
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
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"{TestUtils.CreateStartTag(18)}<<<{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
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
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                FirstPageText= first,
                NextPageText=next,
                PrevPageText=prev,
                LastPageText=last
            };
            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"{TestUtils.CreateStartTag(18)}{first}{prev}{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">{next}</a><a href=\"/Home/test?pageindex=18\">{last}</a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Fact]
        public void ShowFirstLastIsFalse_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                ShowFirstLast=false                
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"{TestUtils.CreateStartTag(18)}<{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Fact]
        public void HideAllNavigationPagerItems_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                ShowPrevNext = false,
                ShowFirstLast=false,
                ShowMorePagerItems=false
            };
            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"{TestUtils.CreateStartTag(18)}{numLinks}</div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void ShowNumericPagerItemsIsFalse_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                ShowNumericPagerItems=false
            };
            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string expectedResult = $"{TestUtils.CreateStartTag(18)}<<<<a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void AutoHideIsTrue_ShouldOutputEmptyHtmlTagIfPageCountIs1()
        {
            var pagedList = Enumerable.Range(1, 8).ToPagedList(1, 10);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext
            };
            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string expectedResult = $"{TestUtils.CreateStartTag(1)}</div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void CurrentPageIndexLargeThan1_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(3, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 3, "/Home/test?pageindex={0}");
            string expectedResult = $"{TestUtils.CreateStartTag(18,currentPage:3)}<a href=\"/Home/test?pageindex=1\"><<</a><a href=\"/Home/test?pageindex=2\"><</a>{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=4\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
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
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));

            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                CurrentPageNumberFormatString= currentPageNumberFormat,
                PageNumberFormatString= numberFormat
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}",numberFormat: numberFormat,currentPageFormat: string.IsNullOrWhiteSpace(currentPageNumberFormat)?numberFormat:currentPageNumberFormat);
            string expectedResult = $"{TestUtils.CreateStartTag(18)}<<<{numLinks}<a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Fact]
        public void PagerItemTemplates_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            string template = "<span>{0}</span>";
            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                PagerItemTemplate= template
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}", template:template);
            string expectedResult = $"{TestUtils.CreateStartTag(18)}<span><<</span><span><</span>{numLinks}<span><a href=\"/Home/test?pageindex=11\">...</a></span><span><a href=\"/Home/test?pageindex=2\">></a></span><span><a href=\"/Home/test?pageindex=18\">>></a></span></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Fact]
        public void SpecificPagerItemTemplateShouldOverridePagerItemTemplate()
        {
            var pagedList = Enumerable.Range(1, 99).ToPagedList(1, 9);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            string template = "<span>{0}</span>";
            string numTemp = "<li>{0}</li>";
            string navTemp = "<div>{0}</div>";
            string curTemp = "<span class=\"active\">{0}</span>";
            string disTemp = "<button class=\"disabled\">{0}</button>";
            string moreTemp = "<span class=\"more\">{0}</span>";
            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                PagerItemTemplate = template,
                NumericPagerItemTemplate = numTemp,
                NavigationPagerItemTemplate = navTemp,
                CurrentPagerItemTemplate = curTemp,
                DisabledPagerItemTemplate = disTemp,
                MorePagerItemTemplate = moreTemp
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 1, "/Home/test?pageindex={0}", template: template,numTemp:numTemp,curTemp:curTemp);
            string expectedResult = $"{TestUtils.CreateStartTag(11)}<button class=\"disabled\"><<</button><button class=\"disabled\"><</button>{numLinks}<span class=\"more\"><a href=\"/Home/test?pageindex=11\">...</a></span><div><a href=\"/Home/test?pageindex=2\">></a></div><div><a href=\"/Home/test?pageindex=11\">>></a></div></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

        [Fact]
        public void TagNameAndPagerItemTemplateSettings_ShouldOutputCorrectContent()
        {
            var pagedList = Enumerable.Range(1, 108).ToPagedList(5, 10);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            string template = "<li>{0}</li>";
            string tagName = "ul";
            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                TagName= tagName,
                PagerItemTemplate = template
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput(tagName);
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, 10, 5, "/Home/test?pageindex={0}", template: template);
            string expectedResult = $"{TestUtils.CreateStartTag(11,tagName,5)}<li><a href=\"/Home/test?pageindex=1\"><<</a></li><li><a href=\"/Home/test?pageindex=4\"><</a></li>{numLinks}<li><a href=\"/Home/test?pageindex=11\">...</a></li><li><a href=\"/Home/test?pageindex=6\">></a></li><li><a href=\"/Home/test?pageindex=11\">>></a></li></{tagName}>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }


        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(10)]
        public void NumericPagerItemCountSetting_ShouldOutputCorrectContent(int numericPagerItemCount)
        {
            var pagedList = Enumerable.Range(1, 101).ToPagedList(1, 10);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = TestUtils.GetViewContext(httpContext, new RouteValueDictionary(new { action = "test", controller = "Home" }));
            
            var tagHelper = new MvcCorePagerTagHelper(TestUtils.GetUrlHelperFactory())
            {
                DataSource = pagedList,
                ViewContext = viewContext,
                NumericPagerItemCount= numericPagerItemCount
            };

            var tagHelperContext = TestUtils.GetTagHelperContext();
            var tagHelperOutput = TestUtils.GetTagHelperOutput("div");
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string numLinks = TestUtils.CreateNumericPageLinks(1, numericPagerItemCount, 1, "/Home/test?pageindex={0}");
            string expectedResult = $"{TestUtils.CreateStartTag(11)}<<<{numLinks}<a href=\"/Home/test?pageindex={numericPagerItemCount+1}\">...</a><a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=11\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }

    }
}
