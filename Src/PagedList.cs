using System;
using System.Collections.Generic;
using System.Linq;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="PagedList"]/*'/>
    public class PagedList<T> : List<T>,IPagedList<T>
    {
        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Constructor[@name="PagedList1"]/*'/>
        public PagedList(IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            var items = allItems as IList<T> ?? allItems.ToList();
            TotalItemCount = items.Count();
            CurrentPageIndex = pageIndex;
            AddRange(items.Skip(StartItemIndex - 1).Take(pageSize));
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Constructor[@name="PagedList2"]/*'/>
        public PagedList(IEnumerable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(currentPageItems);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Constructor[@name="PagedList3"]/*'/>
        public PagedList(IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            int startIndex = (pageIndex - 1)*pageSize;
            AddRange(allItems.Skip(startIndex).Take(pageSize));
            TotalItemCount = allItems.Count();
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Constructor[@name="PagedList4"]/*'/>
        public PagedList(IQueryable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(currentPageItems);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Property[@name="CurrentPageIndex"]/*'/>
        public int CurrentPageIndex { get; set; }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Property[@name="PageSize"]/*'/>
        public int PageSize { get; set; }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Property[@name="TotalItemCount"]/*'/>
        public int TotalItemCount { get; set; }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Property[@name="TotalPageCount"]/*'/>
        public int TotalPageCount { get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); } }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Property[@name="StartItemIndex"]/*'/>
        public int StartItemIndex { get { return (CurrentPageIndex - 1) * PageSize + 1; } }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagedList/Property[@name="EndItemIndex"]/*'/>
        public int EndItemIndex { get { return TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount; } }
    }
}
