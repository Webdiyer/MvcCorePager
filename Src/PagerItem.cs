namespace Webdiyer.AspNetCore
{
    internal class PagerItem
    {
        public PagerItem(string text, int pageIndex, bool disabled, PagerItemType type)
        {
            Text = text;
            PageIndex = pageIndex;
            Disabled = disabled;
            Type = type;
        }

        internal string Text { get; set; }
        internal int PageIndex { get; set; }
        internal bool Disabled { get; set; }
        internal PagerItemType Type { get; set; }
    }

    internal enum PagerItemType:byte
    {
        FirstPage,
        NextPage,
        PrevPage,
        LastPage,
        MorePage,
        NumericPage
    }

}
