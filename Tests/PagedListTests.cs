using System.Linq;
using Webdiyer.AspNetCore;
using Xunit;

namespace Webdiyer.MvcCorePagerTests
{
    public class PagedListTests
    {
        [Theory]
        [InlineData(88,3,8,17,24)]
        [InlineData(100,1,10,1,10)]
        [InlineData(100,8,10,71,80)]
        [InlineData(88,4,5,16,20)]
        [InlineData(88,3,5,11,15)]
        public void StartItemIndexAndEndItemIndex_ShouldBeCorrect(int total,int pageIndex,int pageSize,int expectedStartIndex,int expectedEndIndex)
        {
            var list = new PagedList<int>(Enumerable.Range(1, total), pageIndex, pageSize);
            Assert.Equal(list.StartItemIndex, expectedStartIndex);
            Assert.Equal(list.EndItemIndex, expectedEndIndex);
        }

        [Theory]
        [InlineData(88, 8, 8, 0, 57)]
        [InlineData(88, 8, 8, 7, 64)]
        [InlineData(100, 8, 10, 5, 76)]
        [InlineData(88, 3, 8, 0, 17)]
        public void PagedListItem_ShouldBeCorrect(int total, int pageIndex, int pageSize, int arrayIndex, int expectedValue)
        {
            var list = new PagedList<int>(Enumerable.Range(1, total), pageIndex, pageSize);
            Assert.Equal(list[arrayIndex], expectedValue);
        }

        [Theory]
        [InlineData(88,2,8,0,9)]
        [InlineData(88,8,8,0,57)]
        [InlineData(88,8,8,7,64)]
        public void EnumerablePagedListItem_ShouldBeCorrect(int total,int pageIndex,int pageSize,int arrayIndex,int expectedValue)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            var list = new PagedList<int>(Enumerable.Range(startIndex, startIndex + pageSize), pageIndex, pageSize, total);
            Assert.Equal(list[arrayIndex], expectedValue);
        }
        
        [Theory]
        [InlineData(88,3,5,18)]
        [InlineData(100,1,10,10)]
        [InlineData(100,2,18,6)]
        public void TotalPageCount_ShouldBeCorrect(int total,int pageIndex,int pageSize,int expectedPageCount)
        {
            var list = new PagedList<int>(Enumerable.Range(1, total), pageIndex,pageSize);
            Assert.Equal(list.TotalPageCount, expectedPageCount);
        }
    }
}
