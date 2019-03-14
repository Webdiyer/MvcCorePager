using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.IO;
using System.Text.Encodings.Web;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="HtmlPager"]/*'/>
    public class HtmlPager:IHtmlContent
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly int _currentPageIndex;
        private readonly int _pageSize;
        private readonly int _totalItemCount;
        private PagerOptions _pagerOptions;

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/HtmlPager/Constructor[@name="HtmlPager1"]/*'/>
        public HtmlPager(IHtmlHelper html, int totalItemCount, int pageSize, int pageIndex,PagerOptions pagerOptions)
        {
            _htmlHelper = html;
            _totalItemCount = totalItemCount;
            _pageSize = pageSize;
            _currentPageIndex = pageIndex;
            _pagerOptions = pagerOptions;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/HtmlPager/Constructor[@name="HtmlPager2"]/*'/>
        public HtmlPager(IHtmlHelper html, int totalItemCount, int pageSize, int pageIndex):this(html,totalItemCount,pageSize,pageIndex,null){}

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/HtmlPager/Constructor[@name="HtmlPager3"]/*'/>
        public HtmlPager(IHtmlHelper html, IPagedList pagedList,PagerOptions pagerOptions) : this(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,pagerOptions) { }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/HtmlPager/Constructor[@name="HtmlPager4"]/*'/>
        public HtmlPager(IHtmlHelper html, IPagedList pagedList):this(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex){}


        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/HtmlPager/Method[@name="Options"]/*'/>
        public HtmlPager Options(Action<PagerOptionsBuilder> builder)
        {
            if (_pagerOptions == null)
            {
                _pagerOptions = new PagerOptions();
            }
            builder(new PagerOptionsBuilder(_pagerOptions));
            return this;
        }

        
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var totalPageCount = (int)Math.Ceiling(_totalItemCount / (double)_pageSize);
            writer.Write(new PagerBuilder(_htmlHelper.ViewContext,new UrlHelper(_htmlHelper.ViewContext), totalPageCount, _currentPageIndex, _pagerOptions).GenerateHtml());
        }
    }

}
