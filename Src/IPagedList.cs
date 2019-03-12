using System.Collections;
using System.Collections.Generic;

namespace Webdiyer.AspNetCore
{
    ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Interfaces/Interface[@name="IPagedList"]/*'/>
    public interface IPagedList:IEnumerable
    {
        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/IPagedList/Property[@name="CurrentPageIndex"]/*'/>
        int CurrentPageIndex { get; set; }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/IPagedList/Property[@name="PageSize"]/*'/>
        int PageSize { get; set; }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/IPagedList/Property[@name="TotalItemCount"]/*'/>
        int TotalItemCount { get; set; }
    }

    ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Interfaces/Interface[@name="IPagedList2"]/*'/>
    public interface IPagedList<T>:IEnumerable<T>,IPagedList{}
}
