using System;
using System.Collections.Generic;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="MvcAjaxOptions"]/*'/>
    public class MvcAjaxOptions
    {
        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="EnablePartialLoading"]/*'/>
        public bool EnablePartialLoading { get; set; }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="DataFormId"]/*'/>
        public string DataFormId { get; set; }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="AllowCache"]/*'/>
        public bool AllowCache { get; set; } = true;


        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptions/Property[@name="EnableHistorySupport"]/*'/>
        public bool EnableHistorySupport { get; set; } = true;

        public string Confirm { get; set; }

        public string HttpMethod { get; set; }

        public int LoadingElementDuration { get; set; }

        public string LoadingElementId { get; set; }

        public string OnBegin { get; set; } = string.Empty;

        public string OnComplete { get; set; } = string.Empty;

        public string OnFailure { get; set; } = string.Empty;

        public string OnSuccess { get; set; } = string.Empty;

        public string UpdateTargetId { get; set; }

        public IDictionary<string, object> ToUnobtrusiveHtmlAttributes()
        {
            var result = new Dictionary<string, object>
            {
                { "data-ajax", "true" },
            };

            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-method", HttpMethod);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-confirm", Confirm);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-begin", OnBegin);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-complete", OnComplete);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-failure", OnFailure);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-success", OnSuccess);

            if (!string.IsNullOrWhiteSpace(DataFormId))
            {
                result.Add("data-ajax-search-form", PagerBuilder.EscapeIdSelector(DataFormId));
            }
            if (!String.IsNullOrWhiteSpace(LoadingElementId))
            {
                result.Add("data-ajax-loading", PagerBuilder.EscapeIdSelector(LoadingElementId.Trim('#')));

                if (LoadingElementDuration > 0)
                {
                    result.Add("data-ajax-loading-duration", LoadingElementDuration);
                }
            }
            if (!String.IsNullOrWhiteSpace(UpdateTargetId))
            {
                result.Add("data-ajax-update", PagerBuilder.EscapeIdSelector(UpdateTargetId.Trim('#')));
            }
            if (EnablePartialLoading)
            {
                result.Add("data-ajax-partial-loading", "true");
            }
            if (!AllowCache)
            {
                result.Add("data-ajax-cache", "false");
            }
            if (!EnableHistorySupport)
                result.Add("data-ajax-history", "false");

            return result;
        }
        

    }
}
