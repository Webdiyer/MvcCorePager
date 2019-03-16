# MvcCorePager | [简体中文](README.zh-CN.md)

![MvcCorePager](/Demo/wwwroot/images/MvcCorePager.gif)
MvcCorePager is a free and open source pagination component for ASP.NET Core MVC application, it expose a series of extension methods for using in ASP.NET Core MVC applications. It supports both tag helper and Html extension method syntax.

### Html extension method syntax:
```csharp
@Html.Pager(Model, new PagerOptions
{
    PageIndexParameterName = "id",
    TagName = "ul",
    CssClass = "pagination",
    CurrentPagerItemTemplate = "<li class=\"page-item active\"><a  class=\"page-link\" href=\"javascript:void(0);\">{0}</a></li>",
    DisabledPagerItemTemplate = "<li class=\"page-item disabled\"><a class=\"page-link\">{0}</a></li>",
    PagerItemTemplate = "<li class=\"page-item\">{0}</li>",
    PagerItemCssClass= "page-link",
    Id = "bootstrappager"
})
```

### TagHelper syntax:
```csharp
<mvcpager asp-model="@Model" page-index-parameter-name="id" tag-name="ul"
              class="pagination" 
              current-pager-item-template='<li class="page-item active"><a class="page-link" href="javascript:void(0);">{0}</a></li>'
              disabled-pager-item-template='<li class="page-item disabled"><a class="page-link">{0}</a></li>'
              pager-item-template='<li class="page-item">{0}</li>' pager-item-css-class="page-link" 
    id="bootstrappager2"></mvcpager>
```