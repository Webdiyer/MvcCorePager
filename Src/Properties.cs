using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace Webdiyer.AspNetCore
{
    public partial class MvcCorePagerTagHelper
    {
        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="FirstPageRoute"]/*'/>
        public string FirstPageRouteName { get { return Options.FirstPageRoute; } set { Options.FirstPageRoute = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="AutoHide"]/*'/>
        public bool AutoHide { get { return Options.AutoHide; } set { Options.AutoHide = value; } }
        
        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageIndexOutOfRangeErrorMessage"]/*'/>
        public string PageIndexOutOfRangeErrorMessage { get { return Options.PageIndexOutOfRangeErrorMessage; } set { Options.PageIndexOutOfRangeErrorMessage = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="InvalidPageIndexErrorMessage"]/*'/>
        public string InvalidPageIndexErrorMessage { get { return Options.InvalidPageIndexErrorMessage; } set { Options.InvalidPageIndexErrorMessage = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageIndexParameterName"]/*'/>
        public string PageIndexParameterName { get { return Options.PageIndexParameterName; } set { Options.PageIndexParameterName = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PageNumberFormatString"]/*'/>
        public string PageNumberFormatString { get { return Options.PageNumberFormatString; } set { Options.PageNumberFormatString = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="CurrentPageNumberFormatString"]/*'/>
        public string CurrentPageNumberFormatString { get { return Options.CurrentPageNumberFormatString; } set { Options.CurrentPageNumberFormatString = value; } }
        
        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="TagName"]/*'/>
        public string TagName{get { return Options.TagName; } set { Options.TagName = value; }}

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PagerItemTemplate"]/*'/>
        public string PagerItemTemplate { get { return Options.PagerItemTemplate; } set { Options.PagerItemTemplate = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NumericPagerItemTemplate"]/*'/>
        public string NumericPagerItemTemplate { get { return Options.NumericPagerItemTemplate; } set { Options.NumericPagerItemTemplate = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="CurrentPagerItemTemplate"]/*'/>
        public string CurrentPagerItemTemplate { get { return Options.CurrentPagerItemTemplate; } set { Options.CurrentPagerItemTemplate = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NavigationPagerItemTemplate"]/*'/>
        public string NavigationPagerItemTemplate { get { return Options.NavigationPagerItemTemplate; } set { Options.NavigationPagerItemTemplate = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MorePagerItemTemplate"]/*'/>
        public string MorePagerItemTemplate { get { return Options.MorePagerItemTemplate; } set { Options.MorePagerItemTemplate = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="DisabledPagerItemTemplate"]/*'/>
        public string DisabledPagerItemTemplate { get { return Options.DisabledPagerItemTemplate; } set { Options.DisabledPagerItemTemplate = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="AlwaysShowFirstLastPageNumber"]/*'/>
        public bool AlwaysShowFirstLastPageNumber { get { return Options.AlwaysShowFirstLastPageNumber; } set { Options.AlwaysShowFirstLastPageNumber = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NumericPagerItemCount"]/*'/>
        public int NumericPagerItemCount { get { return Options.NumericPagerItemCount; } set { Options.NumericPagerItemCount = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowPrevNext"]'/>
        public bool ShowPrevNext { get { return Options.ShowPrevNext; } set { Options.ShowPrevNext = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="PrevPageText"]/*'/>
        public string PrevPageText { get { return Options.PrevPageText; } set { Options.PrevPageText = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NextPageText"]/*'/>
        public string NextPageText { get { return Options.NextPageText; } set { Options.NextPageText = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowNumericPagerItems"]'/>
        public bool ShowNumericPagerItems { get { return Options.ShowNumericPagerItems; } set { Options.ShowNumericPagerItems = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowFirstLast"]'/>
        public bool ShowFirstLast { get { return Options.ShowFirstLast; } set { Options.ShowFirstLast = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="FirstPageText"]/*'/>
        public string FirstPageText { get { return Options.FirstPageText; } set { Options.FirstPageText = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="LastPageText"]/*'/>
        public string LastPageText { get { return Options.LastPageText; } set { Options.LastPageText = value; } } 


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowMorePagerItems"]/*'/>
        public bool ShowMorePagerItems { get { return Options.ShowMorePagerItems; } set { Options.ShowMorePagerItems = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MorePageText"]/*'/>
        public string MorePageText { get { return Options.MorePageText; } set { Options.MorePageText = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="Id"]/*'/>
        public string Id { get { return Options.Id; } set { Options.Id = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="HorizontalAlign"]/*'/>
        //public string HorizontalAlign { get; set; }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="CssClass"]/*'/>
        [HtmlAttributeName("class")]
        public string CssClass { get { return Options.CssClass; } set { Options.CssClass = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="ShowDisabledPagerItems"]/*'/>
        public bool ShowDisabledPagerItems { get { return Options.ShowDisabledPagerItems; } set { Options.ShowDisabledPagerItems = value; } }


        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="MaximumPageNumber"]/*'/>
        public int MaximumPageNumber { get { return Options.MaximumPageNumber; } set { Options.MaximumPageNumber = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="HidePagerItems"]/*'/>
        public bool HidePagerItems { get { return Options.HidePagerItems; } set { Options.HidePagerItems = value; } }

        public string PageIndexBoxId { get { return Options.PageIndexBoxId; } set { Options.PageIndexBoxId = value; } }

        public string GoToButtonId { get { return Options.GoToButtonId; } set { Options.GoToButtonId = value; } }

        public int MaximumPageIndexItems { get { return Options.MaximumPageIndexItems; } set { Options.MaximumPageIndexItems = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="NavigationPagerItemsPosition"]/*'/>
        public PagerItemsPosition NavigationPagerItemsPosition { get { return Options.NavigationPagerItemsPosition; } set { Options.NavigationPagerItemsPosition = value; } }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="OnPageIndexError"]/*'/>
        public string OnPageIndexError { get { return Options.OnPageIndexError; } set { Options.OnPageIndexError = value; } }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        /// <summary>
        /// The name of the action method.
        /// </summary>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get { return Options.Action; } set { Options.Action = value; } }

        /// <summary>
        /// The name of the controller.
        /// </summary>
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get { return Options.Controller; } set { Options.Controller = value; } }

        /// <summary>
        /// The name of the area.
        /// </summary>
        [HtmlAttributeName(AreaAttributeName)]
        public string Area { get { return Options.Area; } set { Options.Area = value; } }

        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get { return Options.Route; } set { Options.Route = value; } }

        /// <summary>
        /// The HTTP method to use.
        /// </summary>
        /// <remarks>Passed through to the generated HTML in all cases.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Method { get; set; }

        /// <include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/PagerOptions/Property[@name="RouteValues"]/*'/>
        /// <summary>
        /// Additional parameters for the route.
        /// </summary>
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                return Options.RouteValues.ToDictionary(r => r.Key, r => r.Value == null ? null : r.Value.ToString());
            }
            set { Options.RouteValues = new RouteValueDictionary(value); }
        }

        [HtmlAttributeName("asp-model")]
        public IPagedList DataSource { get; set; }

        #region Ajax settings

        [HtmlAttributeName("ajax-enabled")]
        public bool AjaxEnabled { get; set; }

        [HtmlAttributeName("ajax-update-target")]
        public string AjaxUpdateTarget { get { return AjaxOptions.UpdateTargetId; } set { AjaxOptions.UpdateTargetId = value; } }

        [HtmlAttributeName("ajax-method")]
        public string AjaxMethod { get { return AjaxOptions.HttpMethod; } set { AjaxOptions.HttpMethod = value; } }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="EnablePartialLoading"]/*'/>
        [HtmlAttributeName("ajax-partial-loading")]
        public bool AjaxPartialLoading { get { return AjaxOptions.EnablePartialLoading; } set { AjaxOptions.EnablePartialLoading = value; } }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="DataFormId"]/*'/>
        [HtmlAttributeName("ajax-search-form")]
        public string AjaxDataFormId { get { return AjaxOptions.DataFormId; } set { AjaxOptions.DataFormId = value; } }


        [HtmlAttributeName("ajax-allow-cache")]
        public bool AjaxAllowCache { get { return AjaxOptions.AllowCache; } set { AjaxOptions.AllowCache = value; } }

        ///<include file='MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="EnableHistorySupport"]/*'/>
        [HtmlAttributeName("ajax-history-support")]
        public bool AjaxHistorySupport { get { return AjaxOptions.EnableHistorySupport; } set { AjaxOptions.EnableHistorySupport = value; } }


        private PagerOptions _options;
        private MvcAjaxOptions _ajaxOptions;
        public PagerOptions Options
        {
            get
            {
                if (_options == null) { _options = new PagerOptions(); }
                return _options;
            }
            set { if (value != null) { _options = value; } }
        }
        
        public MvcAjaxOptions AjaxOptions
        {
            get
            {
                if (_ajaxOptions == null) { _ajaxOptions = new MvcAjaxOptions(); }
                return _ajaxOptions;
            }
            set { if (value != null) { _ajaxOptions = value; } }
        }
        #endregion
    }
}
