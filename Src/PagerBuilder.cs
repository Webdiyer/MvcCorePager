using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Webdiyer.AspNetCore
{
    internal class PagerBuilder
    {
        private readonly ViewContext _viewContext;
        private readonly int _totalPageCount = 1;
        private readonly int _pageIndex;
        private readonly PagerOptions _pagerOptions;
        private readonly int _startPageIndex = 1;
        private readonly int _endPageIndex = 1;
        private readonly bool _ajaxPagingEnabled;
        private readonly MvcAjaxOptions _ajaxOptions;
        private IUrlHelper _urlHelper;

        //html pager builder
        internal PagerBuilder(ViewContext viewContext,IUrlHelper urlHelper, int totalPageCount, int pageIndex, PagerOptions pagerOptions)
        {
            _ajaxPagingEnabled = false;
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            _urlHelper = urlHelper;
            _viewContext = viewContext ?? throw new ArgumentNullException("viewContext");
            if (pagerOptions.MaximumPageNumber == 0 || pagerOptions.MaximumPageNumber > totalPageCount)
                _totalPageCount = totalPageCount;
            else
                _totalPageCount = pagerOptions.MaximumPageNumber;
            _pageIndex = pageIndex;
            _pagerOptions = pagerOptions;

            // start page index
            _startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount / 2);
            if (_startPageIndex + pagerOptions.NumericPagerItemCount > _totalPageCount)
                _startPageIndex = _totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
            if (_startPageIndex < 1)
                _startPageIndex = 1;

            // end page index
            _endPageIndex = _startPageIndex + _pagerOptions.NumericPagerItemCount - 1;
            if (_endPageIndex > _totalPageCount)
                _endPageIndex = _totalPageCount;
        }
        //Ajax pager builder
        internal PagerBuilder(ViewContext viewContext, IUrlHelper urlHelper, int totalPageCount, int pageIndex, PagerOptions pagerOptions,
            MvcAjaxOptions ajaxOptions)
        {
            _ajaxPagingEnabled =true;
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            _urlHelper = urlHelper;
            _viewContext = viewContext ?? throw new ArgumentNullException("viewContext");
            if (pagerOptions.MaximumPageNumber == 0 || pagerOptions.MaximumPageNumber > totalPageCount)
                _totalPageCount = totalPageCount;
            else
                _totalPageCount = pagerOptions.MaximumPageNumber;
            _pageIndex = pageIndex;
            _pagerOptions = pagerOptions;
            _ajaxOptions = ajaxOptions ?? new MvcAjaxOptions();

            // start page index
            _startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount / 2);
            if (_startPageIndex + pagerOptions.NumericPagerItemCount > _totalPageCount)
                _startPageIndex = _totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
            if (_startPageIndex < 1)
                _startPageIndex = 1;

            // end page index
            _endPageIndex = _startPageIndex + _pagerOptions.NumericPagerItemCount - 1;
            if (_endPageIndex > _totalPageCount)
                _endPageIndex = _totalPageCount;
        }


        private void AddPrevious(ICollection<PagerItem> results)
        {
            var item = new PagerItem(_pagerOptions.PrevPageText, _pageIndex - 1, _pageIndex == 1, PagerItemType.PrevPage);
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }
        private void AddFirst(ICollection<PagerItem> results)
        {
            var item = new PagerItem(_pagerOptions.FirstPageText, 1, _pageIndex == 1, PagerItemType.FirstPage);
            //Add pager item when PagerItem is not disabled or PagerItem is disabled but PagerOptions.ShowDisabledPagerItems is true
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }


        private void AddMoreBefore(ICollection<PagerItem> results)
        {
            if (_startPageIndex > 1 && _pagerOptions.ShowMorePagerItems)
            {
                var index = _startPageIndex - 1;
                if (index < 1) index = 1;
                var item = new PagerItem(_pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<PagerItem> results)
        {
            for (var pageIndex = _startPageIndex; pageIndex <= _endPageIndex; pageIndex++)
            {
                var text = pageIndex.ToString(CultureInfo.InvariantCulture);
                if (pageIndex == _pageIndex && !string.IsNullOrEmpty(_pagerOptions.CurrentPageNumberFormatString))
                    text = String.Format(_pagerOptions.CurrentPageNumberFormatString, text);
                else if (!string.IsNullOrEmpty(_pagerOptions.PageNumberFormatString))
                    text = String.Format(_pagerOptions.PageNumberFormatString, text);
                var item = new PagerItem(text, pageIndex, false, PagerItemType.NumericPage);
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results)
        {
            if (_endPageIndex < _totalPageCount)
            {
                var index = _startPageIndex + _pagerOptions.NumericPagerItemCount;
                if (index > _totalPageCount) { index = _totalPageCount; }
                var item = new PagerItem(_pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<PagerItem> results)
        {
            var item = new PagerItem(_pagerOptions.NextPageText, _pageIndex + 1, _pageIndex >= _totalPageCount, PagerItemType.NextPage);
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddLast(ICollection<PagerItem> results)
        {
            var item = new PagerItem(_pagerOptions.LastPageText, _totalPageCount, _pageIndex >= _totalPageCount, PagerItemType.LastPage);
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        
        /// <summary>
        /// Generate pagination url using page index
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Pagination url</returns>
        private string GenerateUrl(int pageIndex)
        {
            //IUrlHelperFactory urlHelperFactory = (IUrlHelperFactory)_viewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            //var urlHelper = _urlHelperFactory.GetUrlHelper(_viewContext);
            ////Return null if page index is larger than total page count or page index equals current page index
            if (pageIndex > _totalPageCount || pageIndex == _pageIndex)
                return null;
            var routeValues = new RouteValueDictionary(_viewContext.RouteData.Values);
            AddQueryStringToRouteValues(routeValues, _viewContext);
            if (_pagerOptions.RouteValues != null && _pagerOptions.RouteValues.Count > 0)
            {
                foreach (var de in _pagerOptions.RouteValues)
                {
                    if (!routeValues.ContainsKey(de.Key))
                    {
                        routeValues.Add(de.Key, de.Value);
                    }
                    else
                    {
                        routeValues[de.Key] = de.Value; //RouteValues that added manually have higher privilege
                    }
                }
            }
            var pageValue = _viewContext.RouteData.Values[_pagerOptions.PageIndexParameterName];
            IRouteConstraint constraintValue = null;

            Route currentRoute = null;
            if (!string.IsNullOrWhiteSpace(_pagerOptions.Route) && _viewContext.RouteData.Routers.Count > 0)
            {
                var rc = _viewContext.RouteData.Routers[0] as RouteCollection;
                for (int i = 0; i < rc.Count; i++)
                {
                    INamedRouter nr = rc[i] as INamedRouter;
                    if (nr != null && nr.Name == _pagerOptions.Route)
                    {
                        currentRoute = nr as Route;
                        break;
                    }
                }
            }
            else if (_viewContext.RouteData.Routers.Count > 1)
            {
                currentRoute = _viewContext.RouteData.Routers[1] as Route;
            }
            //Generate url format string when pageIndex is 0
            if (pageIndex == 0)
            {
                //Check if constraint is applied to page index parameter in route definition 
                if (currentRoute != null && currentRoute.Constraints != null &&
                    currentRoute.Constraints.ContainsKey(_pagerOptions.PageIndexParameterName))
                {
                    //Remove constraint applied to page index parameter in route definition temporarily in order to generate paging url format string, otherwise it maybe failed because page index in paging url format is a string value
                    constraintValue = currentRoute.Constraints[_pagerOptions.PageIndexParameterName];
                    currentRoute.Constraints.Remove(_pagerOptions.PageIndexParameterName);
                }
                routeValues[_pagerOptions.PageIndexParameterName] = "__" + _pagerOptions.PageIndexParameterName + "__";
            }
            else
            {
                if (pageIndex == 1)
                {
                    //Remove page index parameter from route values when page index parameter is optional and no constraint is applied to it in route definition
                    if (currentRoute != null &&
                            (currentRoute.ParsedTemplate.Parameters.Any(p => p.Name == _pagerOptions.PageIndexParameterName && p.IsOptional)))
                    {
                        routeValues.Remove(_pagerOptions.PageIndexParameterName);
                        //Remove page index parameter from route value
                        _viewContext.RouteData.Values.Remove(_pagerOptions.PageIndexParameterName);
                    }
                    else
                    {
                        routeValues[_pagerOptions.PageIndexParameterName] = pageIndex;
                    }
                    //}
                }
                else
                {
                    routeValues[_pagerOptions.PageIndexParameterName] = pageIndex;
                }
            }

            string url;
            if (!string.IsNullOrWhiteSpace(_pagerOptions.Route))
            {
                url = _urlHelper.RouteUrl(_pagerOptions.Route, routeValues);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_pagerOptions.Controller))
                {
                    routeValues["controller"] = _pagerOptions.Controller;
                }
                var actionName = _pagerOptions.Action;
                if (string.IsNullOrWhiteSpace(actionName))
                {
                    actionName = (string)routeValues["action"];
                }
                url = _urlHelper.Action(actionName, routeValues);
            }

            if (pageValue != null)
                _viewContext.RouteData.Values[_pagerOptions.PageIndexParameterName] = pageValue;
            if (constraintValue != null && currentRoute != null && !currentRoute.Constraints.ContainsKey(_pagerOptions.PageIndexParameterName))
                //Add constraint back
                currentRoute.Constraints.Add(_pagerOptions.PageIndexParameterName, constraintValue);
            return url;
        }

        /// <summary>
        /// Generate final html code
        /// </summary>
        /// <returns></returns>
        public string GenerateHtml()
        {
            var htmlAttributes = _pagerOptions.HtmlAttributes;
            if ((_pageIndex > _totalPageCount && _totalPageCount > 0) || _pageIndex < 1)
            {
                if (_ajaxPagingEnabled)
                {
                    return $"<div data-ajax=\"true\" data-ajax-update=\"{EscapeIdSelector(_ajaxOptions.UpdateTargetId)}\" data-invalidpageerrmsg=\"{_pagerOptions.InvalidPageIndexErrorMessage}\" data-outrangeerrmsg=\"{_pagerOptions.PageIndexOutOfRangeErrorMessage}\" data-pagerid=\"Webdiyer.MvcPager\" style=\"color:red;font-weight:bold\">{_pagerOptions.PageIndexOutOfRangeErrorMessage}</div>";

                }
                return $"<div data-invalidpageerrmsg=\"{_pagerOptions.InvalidPageIndexErrorMessage}\" data-outrangeerrmsg=\"{_pagerOptions.PageIndexOutOfRangeErrorMessage}\" data-pagerid=\"Webdiyer.MvcPager\" style=\"color:red;font-weight:bold\">{_pagerOptions.PageIndexOutOfRangeErrorMessage}</div>";
            }

            var tb = new TagBuilder(_pagerOptions.TagName);
            if (!string.IsNullOrEmpty(_pagerOptions.Id))
                tb.GenerateId(_pagerOptions.Id, "_");
            if (!string.IsNullOrEmpty(_pagerOptions.HorizontalAlign))
            {
                string strAlign = "text-align:" + _pagerOptions.HorizontalAlign.ToLower();
                MergeStyleAttribute(ref htmlAttributes, strAlign);
            }
            tb.MergeAttributes(htmlAttributes, true);
            if (!string.IsNullOrEmpty(_pagerOptions.CssClass))
                tb.AddCssClass(_pagerOptions.CssClass);
            IDictionary<string, object> attrs = null;
            if (_ajaxPagingEnabled)
            {
                if (string.IsNullOrWhiteSpace(_ajaxOptions.UpdateTargetId))
                {
                    throw new ArgumentException("UpdateTargetId can not be null or empty", "UpdateTargetId");
                }
                attrs = _ajaxOptions.ToUnobtrusiveHtmlAttributes();
            }
            if (attrs == null)
            {
                attrs = new Dictionary<string, object>();
            }            
            AddDataAttributes(attrs);
            tb.MergeAttributes(attrs, true);
            if (_totalPageCount > 1 ||!_pagerOptions.AutoHide)
            {
                var pagerItems = new List<PagerItem>();
                if (_pagerOptions.NavigationPagerItemsPosition == PagerItemsPosition.Left ||
                    _pagerOptions.NavigationPagerItemsPosition == PagerItemsPosition.BothSide)
                {
                    //First page
                    if (_pagerOptions.ShowFirstLast)
                        AddFirst(pagerItems);

                    // Prev page
                    if (_pagerOptions.ShowPrevNext)
                        AddPrevious(pagerItems);
                    if (_pagerOptions.NavigationPagerItemsPosition == PagerItemsPosition.Left)
                    {
                        // Next page
                        if (_pagerOptions.ShowPrevNext)
                            AddNext(pagerItems);

                        //Last page
                        if (_pagerOptions.ShowFirstLast)
                            AddLast(pagerItems);
                    }
                }

                if (_pagerOptions.ShowNumericPagerItems)
                {
                    if (_pagerOptions.AlwaysShowFirstLastPageNumber && _startPageIndex > 1)
                        pagerItems.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));

                    // more page before numeric page buttons
                    if (_pagerOptions.ShowMorePagerItems &&
                        ((!_pagerOptions.AlwaysShowFirstLastPageNumber && _startPageIndex > 1) ||
                         (_pagerOptions.AlwaysShowFirstLastPageNumber && _startPageIndex > 2)))
                        AddMoreBefore(pagerItems);

                    // numeric page
                    AddPageNumbers(pagerItems);

                    // more page after numeric page buttons
                    if (_pagerOptions.ShowMorePagerItems &&
                        ((!_pagerOptions.AlwaysShowFirstLastPageNumber && _endPageIndex < _totalPageCount) ||
                         (_pagerOptions.AlwaysShowFirstLastPageNumber && _totalPageCount > _endPageIndex + 1)))
                        AddMoreAfter(pagerItems);

                    if (_pagerOptions.AlwaysShowFirstLastPageNumber && _endPageIndex < _totalPageCount)
                        pagerItems.Add(new PagerItem(_totalPageCount.ToString(CultureInfo.InvariantCulture),
                            _totalPageCount, false,
                            PagerItemType.NumericPage));
                }
                if (_pagerOptions.NavigationPagerItemsPosition == PagerItemsPosition.Right ||
                    _pagerOptions.NavigationPagerItemsPosition == PagerItemsPosition.BothSide)
                {
                    if (_pagerOptions.NavigationPagerItemsPosition == PagerItemsPosition.Right)
                    {
                        //First page
                        if (_pagerOptions.ShowFirstLast)
                            AddFirst(pagerItems);

                        // Prev page
                        if (_pagerOptions.ShowPrevNext)
                            AddPrevious(pagerItems);
                    }
                    // Next page
                    if (_pagerOptions.ShowPrevNext)
                        AddNext(pagerItems);

                    //Last page
                    if (_pagerOptions.ShowFirstLast)
                        AddLast(pagerItems);
                }

                var sb = new StringBuilder();
                if (_ajaxPagingEnabled)
                {
                    foreach (PagerItem item in pagerItems)
                    {
                        sb.Append(GenerateAjaxPagerElement(item));
                    }
                }
                else
                {
                    foreach (PagerItem item in pagerItems)
                    {
                        sb.Append(GeneratePagerElement(item));
                    }
                }
                tb.InnerHtml.AppendHtml(sb.ToString());
            }
            using (var sw = new StringWriter())
            {
                tb.WriteTo(sw, HtmlEncoder.Default);
                return sw.ToString();
            }
        }

        internal static string EscapeIdSelector(string id)
        {
            var reg=new Regex(@"[.:[\]]");
            return '#' + reg.Replace(id, @"\$&");
        }

        internal static void AddToDictionaryIfNotEmpty(IDictionary<string, object> dictionary, string name, string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                dictionary.Add(name, value);
            }
        }

        private static void MergeStyleAttribute(ref IDictionary<string, object> attributes, string style)
        {
            if (attributes == null)
            {
                attributes = new Dictionary<string, object> {{"style", style}};
            }
            else
            {
                if (attributes.ContainsKey("style"))
                {
                    attributes["style"] += ";" + style;
                }
                else
                {
                    attributes.Add("style", style);
                }
            }
        }

        private void AddDataAttributes(IDictionary<string, object> attrs)
        {
            if (string.IsNullOrWhiteSpace(_pagerOptions.PageIndexParameterName))
            {
                throw new ArgumentNullException("PageIndexParameterName can not be null or empty!");
            }
            attrs.Add("data-url-format", GenerateUrl(0));
            attrs.Add("data-pager-type", "Webdiyer.MvcPager");
            if (_pageIndex > 1)
                attrs.Add("data-current-page", _pageIndex);
            if (_pageIndex > 1)
                attrs.Add("data-first-page", GenerateUrl(1));
            AddToDictionaryIfNotEmpty(attrs, "data-onerror", _pagerOptions.OnPageIndexError);
            attrs.Add("data-page-parameter", _pagerOptions.PageIndexParameterName);
            attrs.Add("data-page-count", _totalPageCount);
            if (!string.IsNullOrWhiteSpace(_pagerOptions.PageIndexBoxId))
            {
                attrs.Add("data-page-index-box", EscapeIdSelector(_pagerOptions.PageIndexBoxId));
                AddToDictionaryIfNotEmpty(attrs, "data-goto-button", EscapeIdSelector(_pagerOptions.GoToButtonId));

                if (_pagerOptions.MaximumPageIndexItems != 20)
                {
                    attrs.Add("data-max-items", _pagerOptions.MaximumPageIndexItems);
                }
            }
            AddToDictionaryIfNotEmpty(attrs, "data-out-range-error", _pagerOptions.PageIndexOutOfRangeErrorMessage);
            AddToDictionaryIfNotEmpty(attrs, "data-invalid-page-error", _pagerOptions.InvalidPageIndexErrorMessage);
        }

        private string GenerateAjaxAnchor(PagerItem item)
        {
            string url = GenerateUrl(item.PageIndex);
            if (string.IsNullOrWhiteSpace(url))
                return HtmlEncoder.Default.Encode(item.Text);
            var tag = new TagBuilder("a");
            tag.InnerHtml.AppendHtml(item.Text);
            tag.MergeAttribute("href", url);
            tag.MergeAttribute("data-page-index", item.PageIndex.ToString(CultureInfo.InvariantCulture));
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        private string GeneratePagerElement(PagerItem item)
        {
            //pager item link
            string url = GenerateUrl(item.PageIndex);
            if (item.Disabled) //first,last,next or previous pager items, don't encode content
                return CreateWrappedPagerElement(item, item.Text);
            //var link = HtmlEncoder.Default.Encode(item.Text);
            string link;
            if (string.IsNullOrEmpty(url))
            {
                link = item.Text;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_pagerOptions.PagerItemCssClass))
                {
                    link = $"<a class=\"{_pagerOptions.PagerItemCssClass}\" href=\"{url}\">{item.Text}</a>";
                }
                else
                {
                    link = $"<a href=\"{url}\">{item.Text}</a>";
                }
            }
            return CreateWrappedPagerElement(item,link);
        }

        private string GenerateAjaxPagerElement(PagerItem item)
        {
            if (item.Disabled)
                return CreateWrappedPagerElement(item, item.Text);
            return CreateWrappedPagerElement(item, GenerateAjaxAnchor(item));
        }


        private string CreateWrappedPagerElement(PagerItem item, string el)
        {
            if (item.Disabled)
            {
                if ((!string.IsNullOrEmpty(_pagerOptions.DisabledPagerItemTemplate) ||
                     !string.IsNullOrEmpty(_pagerOptions.NavigationPagerItemTemplate) ||
                     !string.IsNullOrEmpty(_pagerOptions.PagerItemTemplate)))
                {
                    return
                            string.Format(
                                _pagerOptions.DisabledPagerItemTemplate ??
                                (_pagerOptions.NavigationPagerItemTemplate ??
                                 _pagerOptions.PagerItemTemplate), el);
                }
                return el;
            }
            string navStr = el;
            switch (item.Type)
            {
                case PagerItemType.FirstPage:
                case PagerItemType.LastPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                    if ((!string.IsNullOrEmpty(_pagerOptions.NavigationPagerItemTemplate) ||
                         !string.IsNullOrEmpty(_pagerOptions.PagerItemTemplate)))
                        navStr =
                            string.Format(
                                _pagerOptions.NavigationPagerItemTemplate ??
                                _pagerOptions.PagerItemTemplate, el);
                    break;
                case PagerItemType.MorePage:
                    if ((!string.IsNullOrEmpty(_pagerOptions.MorePagerItemTemplate) ||
                         !string.IsNullOrEmpty(_pagerOptions.PagerItemTemplate)))
                        navStr =
                            string.Format(
                                _pagerOptions.MorePagerItemTemplate ??
                                _pagerOptions.PagerItemTemplate, el);
                    break;
                case PagerItemType.NumericPage:
                    if (item.PageIndex == _pageIndex &&
                        (!string.IsNullOrEmpty(_pagerOptions.CurrentPagerItemTemplate) ||
                         !string.IsNullOrEmpty(_pagerOptions.PagerItemTemplate))) //current page
                        navStr =
                            string.Format(
                                _pagerOptions.CurrentPagerItemTemplate ??
                                _pagerOptions.PagerItemTemplate, el);
                    else if (!string.IsNullOrEmpty(_pagerOptions.NumericPagerItemTemplate) ||
                             !string.IsNullOrEmpty(_pagerOptions.PagerItemTemplate))
                        navStr =
                            string.Format(
                                _pagerOptions.NumericPagerItemTemplate ??
                                _pagerOptions.PagerItemTemplate, el);
                    break;
            }
            return navStr;
        }

        private void AddQueryStringToRouteValues(RouteValueDictionary routeValues, ActionContext actionContext)
        {
            if (routeValues == null)
                routeValues = new RouteValueDictionary();
            var rq = actionContext.HttpContext.Request.Query;
            if (rq != null && rq.Count > 0)
            {
                var invalidParams = new[]
                {"x-requested-with", "xmlhttprequest", _pagerOptions.PageIndexParameterName.ToLower()};
                foreach (string key in rq.Keys)
                {
                    //Add url parameter to route values
                    if (!string.IsNullOrEmpty(key) && Array.IndexOf(invalidParams, key.ToLower()) < 0)
                    {
                        var kv = rq[key];
                        routeValues[key] = kv;
                    }
                }
            }
        }
    }
}