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
            var pagedList = Enumerable.Range(1, 88).ToPagedList(1, 5);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = new QueryString();
            var viewContext = new ViewContext()
            {
                RouteData = new RouteData(
                new RouteValueDictionary(new { action = "test", controller = "Home" })),                
                HttpContext=httpContext
            };
            
            var tagHelper = new MvcCorePagerTagHelper(urlHelperFactory.Object)
            {
                DataSource=pagedList,ViewContext=viewContext
            };


            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(), 
                new Dictionary<object, object>(), 
                Guid.NewGuid().ToString("N"));
            var tagHelperOutput = new TagHelperOutput("div", 
                new TagHelperAttributeList(),(result, encoder) =>{
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            tagHelper.Process(tagHelperContext, tagHelperOutput);
            string expectedResult = "<div data-invalid-page-error=\"Invalid page index\" data-out-range-error=\"Page index out of range\" data-page-count=\"18\" data-page-parameter=\"pageindex\" data-pager-type=\"Webdiyer.MvcPager\" data-url-format=\"/Home/test?pageindex=__pageindex__\"><<<1<a href=\"/Home/test?pageindex=2\">2</a><a href=\"/Home/test?pageindex=3\">3</a><a href=\"/Home/test?pageindex=4\">4</a><a href=\"/Home/test?pageindex=5\">5</a><a href=\"/Home/test?pageindex=6\">6</a><a href=\"/Home/test?pageindex=7\">7</a><a href=\"/Home/test?pageindex=8\">8</a><a href=\"/Home/test?pageindex=9\">9</a><a href=\"/Home/test?pageindex=10\">10</a><a href=\"/Home/test?pageindex=11\">...</a><a href=\"/Home/test?pageindex=2\">></a><a href=\"/Home/test?pageindex=18\">>></a></div>";
            Assert.Equal(expectedResult, tagHelperOutput.Content.GetContent());
        }
    }
}
