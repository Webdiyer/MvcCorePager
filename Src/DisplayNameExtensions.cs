using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="DisplayNameExtensions"]/*'/>
    public static class DisplayNameExtensions
    {
        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/DisplayNameExtensions/Method[@name="DisplayNameFor"]/*'/>
        public static string DisplayNameFor<TModel, TValue>(this IHtmlHelper<PagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DisplayNameForInnerType<TModel, TValue>(expression);
        }
    }
}
