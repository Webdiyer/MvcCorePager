using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.IO;
using System.Text.Encodings.Web;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="AjaxPager"]/*'/>
    public class AjaxPager : IHtmlContent
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly int _currentPageIndex;
        private readonly int _pageSize;
        private readonly int _totalItemCount;
        private PagerOptions _pagerOptions;
        private MvcAjaxOptions _ajaxOptions;

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/AjaxPager/Constructor[@name="AjaxPager1"]/*'/>
        public AjaxPager(IHtmlHelper html, int totalItemCount, int pageSize, int pageIndex,PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            _htmlHelper = html;
            _totalItemCount = totalItemCount;
            _pageSize = pageSize;
            _currentPageIndex = pageIndex;
            _pagerOptions = pagerOptions;
            _ajaxOptions = ajaxOptions;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/AjaxPager/Constructor[@name="AjaxPager2"]/*'/>
        public AjaxPager(IHtmlHelper ajax, IPagedList pagedList,PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
            : this(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,pagerOptions, ajaxOptions) { }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/AjaxPager/Method[@name="Options"]/*'/>
        public AjaxPager Options(Action<PagerOptionsBuilder> builder)
        {
            if (_pagerOptions == null)
            {
                _pagerOptions = new PagerOptions();
            }
            builder(new PagerOptionsBuilder(_pagerOptions));
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/AjaxPager/Method[@name="AjaxOptions"]/*'/>
        public AjaxPager AjaxOptions(Action<MvcAjaxOptionsBuilder> builder)
        {
            if (_ajaxOptions == null)
            {
                _ajaxOptions = new MvcAjaxOptions();
            }
            builder(new MvcAjaxOptionsBuilder(_ajaxOptions));
            return this;
        }
        
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var totalPageCount = (int)Math.Ceiling(_totalItemCount / (double)_pageSize);
            writer.Write(new PagerBuilder(_htmlHelper.ViewContext,new UrlHelper(_htmlHelper.ViewContext), totalPageCount, _currentPageIndex, _pagerOptions,_ajaxOptions).GenerateHtml());
        }
    }
}
