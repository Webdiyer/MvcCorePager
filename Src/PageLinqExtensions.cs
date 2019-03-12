using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdiyer.AspNetCore
{
    ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="PageLinqExtensions"]/*'/>
    public static class PageLinqExtensions
    {
        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PageLinqExtensions/Method[@name="ToPagedList1"]/*'/>
        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int pageIndex,
                int pageSize
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex-1) * pageSize;
            var totalItemCount = allItems.Count();
            while (totalItemCount <= itemIndex&&pageIndex>1)
            {
                itemIndex = (--pageIndex - 1) * pageSize;
            }
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PageLinqExtensions/Method[@name="ToPagedList2"]/*'/>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            return allItems.AsQueryable().ToPagedList(pageIndex, pageSize);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PageLinqExtensions/Method[@name="ToPagedListAsync1"]/*'/>
        public static async Task<PagedList<T>> ToPagedListAsync<T>
        (
            this IQueryable<T> allItems,
            int pageIndex,
            int pageSize
        )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = allItems.Count();
            while (totalItemCount <= itemIndex && pageIndex > 1)
            {
                itemIndex = (--pageIndex - 1) * pageSize;
            }
            var query = allItems.Skip(itemIndex).Take(pageSize);
            var pageOfItems = await Task.Factory.StartNew(query.ToList);
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PageLinqExtensions/Method[@name="ToPagedListAsync2"]/*'/>
        public static Task<PagedList<T>> ToPagedListAsync<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            return allItems.AsQueryable().ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
