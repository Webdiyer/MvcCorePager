using System;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Webdiyer.AspNetCore
{
    [HtmlTargetElement("mvcpager")]
    public partial class MvcCorePagerTagHelper:TagHelper
    {
        private const string ActionAttributeName = "asp-action";
        private const string AreaAttributeName = "asp-area";
        private const string ControllerAttributeName = "asp-controller";
        private const string RouteAttributeName = "asp-route";
        private const string RouteValuesDictionaryName = "asp-all-route-data";
        private const string RouteValuesPrefix = "asp-route-";
        
        private int _totalPageCount = 1;
        private int _pageIndex;


        IUrlHelperFactory _urlHelperFactory;
        public MvcCorePagerTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //if(!int.TryParse((string)ViewContext.RouteData.Values[PageIndexParameterName],out _pageIndex)){
            //    _pageIndex = 1;
            //}
                        
            if(DataSource==null)
                throw new ArgumentNullException(nameof(DataSource));
            int actualPageCount = (int) Math.Ceiling(DataSource.TotalItemCount/(double) DataSource.PageSize);
            _pageIndex = DataSource.CurrentPageIndex;
            if (MaximumPageNumber == 0 || MaximumPageNumber >actualPageCount)
                _totalPageCount = actualPageCount;
            else
                _totalPageCount = MaximumPageNumber;

            PagerBuilder pb;
            if (AjaxEnabled)
            {
                pb = new PagerBuilder(ViewContext,_urlHelperFactory.GetUrlHelper(ViewContext), _totalPageCount, _pageIndex, Options, AjaxOptions);
            }
            else
            {
                pb = new PagerBuilder(ViewContext, _urlHelperFactory.GetUrlHelper(ViewContext), _totalPageCount, _pageIndex, Options);
            }
            var content=pb.GenerateHtml();
            output.TagName = string.Empty;
            output.Content = new DefaultTagHelperContent().SetHtmlContent(content);

            //output.PreElement.SetHtmlContent(_copyrightText);
            //output.TagName = TagName;
            //_content = new DefaultTagHelperContent();
            //output.Content = _content.SetHtmlContent(buildPager(output));
            //output.PostElement.SetHtmlContent(_copyrightText);
        }
        
    }
}
