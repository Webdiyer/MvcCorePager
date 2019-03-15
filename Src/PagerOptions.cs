using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="PagerOptions"]/*'/>
    public class PagerOptions
    {
        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Constructor[@name="PagerOptions"]/*'/>
        public PagerOptions()
        {
            AutoHide = true;
            PageIndexParameterName = "pageindex";
            NumericPagerItemCount = 10;
            AlwaysShowFirstLastPageNumber = false;
            ShowPrevNext = true;
            PrevPageText = "<"; 
            NextPageText = ">";
            ShowNumericPagerItems = true;
            ShowFirstLast = true;
            FirstPageText = "<<";
            LastPageText = ">>";
            ShowMorePagerItems = true;
            MorePageText = "...";
            ShowDisabledPagerItems = true;
            MaximumPageIndexItems = 20;
            TagName = "div";
            InvalidPageIndexErrorMessage = "Invalid page index";// MvcCorePagerResources.InvalidPageIndexErrorMessage;
            PageIndexOutOfRangeErrorMessage = "Page index out of range"; //MvcCorePagerResources.PageIndexOutOfRangeErrorMessage;
            MaximumPageNumber = 0;
            FirstPageRoute = null;
        }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="Action"]/*'/>
        public string Action { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="Controller"]/*'/>
        public string Controller { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="Area"]/*'/>
        public string Area { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="Route"]/*'/>
        public string Route { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="OverwriteUrlFormat"]/*'/>
        public string OverwriteUrlFormat { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="RouteValues"]/*'/>
        public RouteValueDictionary RouteValues { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="HtmlAttributes"]/*'/>
        public IDictionary<string,object> HtmlAttributes { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="FirstPageRoute"]/*'/>
        public string FirstPageRoute { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="AutoHide"]/*'/>
        public bool AutoHide { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageIndexOutOfRangeErrorMessage"]/*'/>
        public string PageIndexOutOfRangeErrorMessage { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="InvalidPageIndexErrorMessage"]/*'/>
        public string InvalidPageIndexErrorMessage { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageIndexParameterName"]/*'/>
        public string PageIndexParameterName { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageIndexBoxId"]/*'/>
        public string PageIndexBoxId { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="GoToButtonId"]/*'/>
        public string GoToButtonId { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MaximumPageIndexItems"]/*'/>
        public int MaximumPageIndexItems { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageNumberFormatString"]/*'/>
        public string PageNumberFormatString { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="CurrentPageNumberFormatString"]/*'/>
        public string CurrentPageNumberFormatString { get; set; }

        private string _tagName;


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="TagName"]/*'/>
        public string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentNullException(nameof(TagName));// MvcCorePagerResources.TagNameCannotBeNull);
                _tagName = value;
            }
        }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PagerItemTemplate"]/*'/>
        public string PagerItemTemplate { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NumericPagerItemTemplate"]/*'/>
        public string NumericPagerItemTemplate { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="CurrentPagerItemTemplate"]/*'/>
        public string CurrentPagerItemTemplate { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NavigationPagerItemTemplate"]/*'/>
        public string NavigationPagerItemTemplate { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MorePagerItemTemplate"]/*'/>
        public string MorePagerItemTemplate { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="DisabledPagerItemTemplate"]/*'/>
        public string DisabledPagerItemTemplate { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="AlwaysShowFirstLastPageNumber"]/*'/>
        public bool AlwaysShowFirstLastPageNumber { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NumericPagerItemCount"]/*'/>
        public int NumericPagerItemCount { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowPrevNext"]'/>
        public bool ShowPrevNext { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PrevPageText"]/*'/>
        public string PrevPageText { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NextPageText"]/*'/>
        public string NextPageText { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowNumericPagerItems"]'/>
        public bool ShowNumericPagerItems { get; set; } = true;

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowFirstLast"]'/>
        public bool ShowFirstLast { get; set; } = true;


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="FirstPageText"]/*'/>
        public string FirstPageText { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="LastPageText"]/*'/>
        public string LastPageText { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowMorePagerItems"]/*'/>
        public bool ShowMorePagerItems { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MorePageText"]/*'/>
        public string MorePageText { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="Id"]/*'/>
        public string Id { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="HorizontalAlign"]/*'/>
        public string HorizontalAlign { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="CssClass"]/*'/>
        public string CssClass { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowDisabledPagerItems"]/*'/>
        public bool ShowDisabledPagerItems { get; set; }


        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MaximumPageNumber"]/*'/>
        public int MaximumPageNumber { get; set; }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="HidePagerItems"]/*'/>
        public bool HidePagerItems { get; set; }

        private PagerItemsPosition _navPagerItemsPosition = PagerItemsPosition.BothSide;

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NavigationPagerItemsPosition"]/*'/>
        public PagerItemsPosition NavigationPagerItemsPosition { get { return _navPagerItemsPosition; } set{_navPagerItemsPosition = value;} }

        /// <include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="OnPageIndexError"]/*'/>
        public string OnPageIndexError { get; set; }

        public string PagerItemCssClass { get; set; }
    }
}