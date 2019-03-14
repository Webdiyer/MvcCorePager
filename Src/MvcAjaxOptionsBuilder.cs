namespace Webdiyer.AspNetCore
{
    ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/Classes/Class[@name="MvcAjaxOptionsBuilder"]/*'/>
    public class MvcAjaxOptionsBuilder
    {
        private readonly MvcAjaxOptions _ajaxOptions;

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Constructor[@name="MvcAjaxOptionsBuilder"]/*'/>
        public MvcAjaxOptionsBuilder(MvcAjaxOptions ajaxOptions)
        {
            _ajaxOptions = ajaxOptions;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetUpdateTargetId"]/*'/>
        public MvcAjaxOptionsBuilder SetUpdateTargetId(string targetId)
        {
            _ajaxOptions.UpdateTargetId = targetId;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetHttpMethod"]/*'/>
        public MvcAjaxOptionsBuilder SetHttpMethod(string method)
        {
            _ajaxOptions.HttpMethod = method;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetOnBegin"]/*'/>
        public MvcAjaxOptionsBuilder SetOnBegin(string name)
        {
            _ajaxOptions.OnBegin = name;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetOnSuccess"]/*'/>
        public MvcAjaxOptionsBuilder SetOnSuccess(string name)
        {
            _ajaxOptions.OnSuccess = name;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetOnComplete"]/*'/>
        public MvcAjaxOptionsBuilder SetOnComplete(string name)
        {
            _ajaxOptions.OnComplete = name;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetOnFailure"]/*'/>
        public MvcAjaxOptionsBuilder SetOnFailure(string name)
        {
            _ajaxOptions.OnFailure = name;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetLoadingElementId"]/*'/>
        public MvcAjaxOptionsBuilder SetLoadingElementId(string id)
        {
            _ajaxOptions.LoadingElementId = id;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetLoadingElementDuration"]/*'/>
        public MvcAjaxOptionsBuilder SetLoadingElementDuration(int duration)
        {
            _ajaxOptions.LoadingElementDuration = duration;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetConfirm"]/*'/>
        public MvcAjaxOptionsBuilder SetConfirm(string confirm)
        {
            _ajaxOptions.Confirm = confirm;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="EnablePartialLoading"]/*'/>
        public MvcAjaxOptionsBuilder EnablePartialLoading()
        {
            _ajaxOptions.EnablePartialLoading = true;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="SetDataFormId"]/*'/>
        public MvcAjaxOptionsBuilder SetDataFormId(string id)
        {
            _ajaxOptions.DataFormId = id;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="DisallowCache"]/*'/>
        public MvcAjaxOptionsBuilder DisallowCache()
        {
            _ajaxOptions.AllowCache = false;
            return this;
        }

        ///<include file='docs/MvcCorePagerDocs.xml' path='MvcCorePagerDocs/MvcAjaxOptionsBuilder/Method[@name="DisableHistorySupport"]/*'/>
        public MvcAjaxOptionsBuilder DisableHistorySupport()
        {
            _ajaxOptions.EnableHistorySupport = false;
            return this;
        }
    }
}
