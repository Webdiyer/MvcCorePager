# MvcCorePager | [English](README.md)

MvcCorePager是一个免费开源的 ASP.NET Core MVC 分页组件,是原MvcPager的.net core升级版，支持TagHelper 和 Html扩展方法两种语法：

### Html扩展方法语法：
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

### TagHelper 语法：
```csharp
<mvcpager asp-model="@Model" page-index-parameter-name="id" tag-name="ul"
              class="pagination" 
              current-pager-item-template='<li class="page-item active"><a class="page-link" href="javascript:void(0);">{0}</a></li>'
              disabled-pager-item-template='<li class=\"page-item disabled\"><a class="page-link">{0}</a></li>'
              pager-item-template='<li class="page-item">{0}</li>' pager-item-css-class="page-link" 
    id="bootstrappager2"></mvcpager>
```