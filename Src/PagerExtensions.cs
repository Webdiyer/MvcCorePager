using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Webdiyer.AspNetCore
{
    ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="PagerExtensions"]/*'/>
    public static class PagerExtensions
    {
        #region Html Pager

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="HtmlPager1"]/*'/>
        public static HtmlPager Pager(this IHtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, PagerOptions pagerOptions)
        {
            return new HtmlPager
                (
                    helper,
                    totalItemCount,pageSize,
                    pageIndex,
                    pagerOptions
                );
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="HtmlPager2"]/*'/>
        public static HtmlPager Pager(this IHtmlHelper helper, int totalItemCount, int pageSize, int pageIndex)
        {
            return new HtmlPager
                (
                    helper,
                    totalItemCount, pageSize,
                    pageIndex
                );
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="HtmlPager3"]/*'/>
        public static HtmlPager Pager(this IHtmlHelper helper, IPagedList pagedList)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException("pagedList");
            }
            return new HtmlPager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="HtmlPager4"]/*'/>
        public static HtmlPager Pager(this IHtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException("pagedList");
            }
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, pagerOptions);
        }


        #endregion
        
        #region Ajax Pager

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="AjaxPager1"]/*'/>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            return new AjaxPager(helper, totalItemCount, pageSize, pageIndex, pagerOptions, ajaxOptions);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="AjaxPager2"]/*'/>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, IPagedList pagedList)
        {
            return new AjaxPager(helper, pagedList, null,null);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="AjaxPager3"]/*'/>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            return AjaxPager(helper, pagedList, pagerOptions, null);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerExtensions/Method[@name="AjaxPager4"]/*'/>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException("pagedList");
            }
            return AjaxPager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,pagerOptions, ajaxOptions);
        }

        #endregion
    }
}