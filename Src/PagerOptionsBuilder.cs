using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="PagerOptionsBuilder"]/*'/>
    public class PagerOptionsBuilder
    {
        private readonly PagerOptions _pagerOptions;

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Constrctor[@name="PagerOptionsBuilder"]/*'/>
        public PagerOptionsBuilder(PagerOptions pagerOptions)
        {
            _pagerOptions = pagerOptions;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetAction"]/*'/>
        public PagerOptionsBuilder SetAction(string actionName)
        {
            _pagerOptions.Action = actionName;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetController"]/*'/>
        public PagerOptionsBuilder SetController(string controllerName)
        {
            _pagerOptions.Controller = controllerName;
            return this;
        }


        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetArea"]/*'/>
        public PagerOptionsBuilder SetArea(string areaName)
        {
            _pagerOptions.Area = areaName;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="AddHtmlAttribute"]/*'/>
        public PagerOptionsBuilder AddHtmlAttribute(string key, object value)
        {
            if (_pagerOptions.HtmlAttributes == null)
            {
                _pagerOptions.HtmlAttributes=new Dictionary<string, object>();
            }
            _pagerOptions.HtmlAttributes[key] = value;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetOnPageIndexError"]/*'/>
        public PagerOptionsBuilder SetOnPageIndexError(string handler)
        {
            _pagerOptions.OnPageIndexError = handler;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="AddRouteValue"]/*'/>
        public PagerOptionsBuilder AddRouteValue(string key, object value)
        {
            if (_pagerOptions.RouteValues == null)
            {
                _pagerOptions.RouteValues=new RouteValueDictionary();
            }
            _pagerOptions.RouteValues[key] = value;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetRoute"]/*'/>
        public PagerOptionsBuilder SetRoute(string routeName)
        {
            _pagerOptions.Route = routeName;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetFirstPageRoute"]/*'/>
        public PagerOptionsBuilder SetFirstPageRoute(string routeName)
        {
            _pagerOptions.FirstPageRoute = routeName;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="DisableAutoHide"]/*'/>
        public PagerOptionsBuilder DisableAutoHide()
        {
            _pagerOptions.AutoHide = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetPageIndexOutOfRangeErrorMessage"]/*'/>
        public PagerOptionsBuilder SetPageIndexOutOfRangeErrorMessage(string msg)
        {
            _pagerOptions.PageIndexOutOfRangeErrorMessage = msg;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetHorizontalAlign"]/*'/>
        public PagerOptionsBuilder SetHorizontalAlign(string alignment)
        {
            _pagerOptions.HorizontalAlign = alignment;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetInvalidPageIndexErrorMessage"]/*'/>
        public PagerOptionsBuilder SetInvalidPageIndexErrorMessage(string msg)
        {
            _pagerOptions.InvalidPageIndexErrorMessage = msg;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetPageIndexParameterName"]/*'/>
        public PagerOptionsBuilder SetPageIndexParameterName(string prmName)
        {
            _pagerOptions.PageIndexParameterName = prmName;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetPageIndexBoxId"]/*'/>
        public PagerOptionsBuilder SetPageIndexBoxId(string id)
        {
            _pagerOptions.PageIndexBoxId = id;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetMaximumPageIndexItems"]/*'/>
        public PagerOptionsBuilder SetMaximumPageIndexItems(int itmes)
        {
            _pagerOptions.MaximumPageIndexItems = itmes;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetGoToButtonId"]/*'/>
        public PagerOptionsBuilder SetGoToButtonId(string id)
        {
            _pagerOptions.GoToButtonId = id;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetPageNumberFormatString"]/*'/>
        public PagerOptionsBuilder SetPageNumberFormatString(string format)
        {
            _pagerOptions.PageNumberFormatString = format;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetCurrentPageNumberFormatString"]/*'/>
        public PagerOptionsBuilder SetCurrentPageNumberFormatString(string format)
        {
            _pagerOptions.CurrentPageNumberFormatString = format;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetTagName"]/*'/>
        public PagerOptionsBuilder SetTagName(string tagName)
        {
            _pagerOptions.TagName = tagName;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetPagerItemTemplate"]/*'/>
        public PagerOptionsBuilder SetPagerItemTemplate(string template)
        {
            _pagerOptions.PagerItemTemplate = template;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetNumericPagerItemTemplate"]/*'/>
        public PagerOptionsBuilder SetNumericPagerItemTemplate(string template)
        {
            _pagerOptions.NumericPagerItemTemplate = template;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetCurrentPagerItemTemplate"]/*'/>
        public PagerOptionsBuilder SetCurrentPagerItemTemplate(string template)
        {
            _pagerOptions.CurrentPagerItemTemplate = template;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetNavigationPagerItemTemplate"]/*'/>
        public PagerOptionsBuilder SetNavigationPagerItemTemplate(string template)
        {
            _pagerOptions.NavigationPagerItemTemplate = template;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetMorePagerItemTemplate"]/*'/>
        public PagerOptionsBuilder SetMorePagerItemTemplate(string template)
        {
            _pagerOptions.MorePagerItemTemplate = template;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetDisabledPagerItemTemplate"]/*'/>
        public PagerOptionsBuilder SetDisabledPagerItemTemplate(string template)
        {
            _pagerOptions.DisabledPagerItemTemplate = template;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="AlwaysShowFirstLastPageNumber"]/*'/>
        public PagerOptionsBuilder AlwaysShowFirstLastPageNumber()
        {
            _pagerOptions.AlwaysShowFirstLastPageNumber = true;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetNumericPagerItemCount"]/*'/>
        public PagerOptionsBuilder SetNumericPagerItemCount(int itemCount)
        {
            _pagerOptions.NumericPagerItemCount = itemCount;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="HidePrevNext"]/*'/>
        public PagerOptionsBuilder HidePrevNext()
        {
            _pagerOptions.ShowPrevNext = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetPrevPageText"]/*'/>
        public PagerOptionsBuilder SetPrevPageText(string text)
        {
            _pagerOptions.PrevPageText = text;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetNextPageText"]/*'/>
        public PagerOptionsBuilder SetNextPageText(string text)
        {
            _pagerOptions.NextPageText = text;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="HideNumericPagerItems"]/*'/>
        public PagerOptionsBuilder HideNumericPagerItems()
        {
            _pagerOptions.ShowNumericPagerItems = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="HideFirstLast"]/*'/>
        public PagerOptionsBuilder HideFirstLast()
        {
            _pagerOptions.ShowFirstLast = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetFirstPageText"]/*'/>
        public PagerOptionsBuilder SetFirstPageText(string text)
        {
            _pagerOptions.FirstPageText = text;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetLastPageText"]/*'/>
        public PagerOptionsBuilder SetLastPageText(string text)
        {
            _pagerOptions.LastPageText = text;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="HideMorePagerItems"]/*'/>
        public PagerOptionsBuilder HideMorePagerItems()
        {
            _pagerOptions.ShowMorePagerItems = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetMorePageText"]/*'/>
        public PagerOptionsBuilder SetMorePageText(string text)
        {
            _pagerOptions.MorePageText = text;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetId"]/*'/>
        public PagerOptionsBuilder SetId(string id)
        {
            _pagerOptions.Id = id;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetCssClass"]/*'/>
        public PagerOptionsBuilder SetCssClass(string cssClass)
        {
            _pagerOptions.CssClass = cssClass;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="HideDisabledPagerItems"]/*'/>
        public PagerOptionsBuilder HideDisabledPagerItems()
        {
            _pagerOptions.ShowDisabledPagerItems = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetMaximumPageNumber"]/*'/>
        public PagerOptionsBuilder SetMaximumPageNumber(int number)
        {
            _pagerOptions.MaximumPageNumber = number;
            return this;
        }
        
        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetNavigationPagerItemsPosition"]/*'/>
        public PagerOptionsBuilder SetNavigationPagerItemsPosition(PagerItemsPosition position)
        {
            _pagerOptions.NavigationPagerItemsPosition = position;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="PagerOptionsBuilder"]/*'/>
        public PagerOptionsBuilder SetPagerItemCssClass(string cssClass)
        {
            _pagerOptions.PagerItemCssClass = cssClass;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetRouteValues"]/*'/>
        public PagerOptionsBuilder SetRouteValues(RouteValueDictionary values)
        {
            _pagerOptions.RouteValues = values;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptionsBuilder/Method[@name="SetHtmlAttributes"]/*'/>
        public PagerOptionsBuilder SetHtmlAttributes(IDictionary<string, object> attributes)
        {
            _pagerOptions.HtmlAttributes = attributes;
            return this;
        }
    }
}
