using System;
using System.Linq;
using Webdiyer.AspNetCore;
using Xunit;

namespace Webdiyer.MvcCorePagerTest
{
    public class PagedListTests
    {
        [Theory]
        [InlineData(88,3,8,17,24)]
        [InlineData(100,1,10,1,10)]
        [InlineData(100,8,10,71,80)]
        [InlineData(88,4,5,16,20)]
        [InlineData(88,3,5,11,15)]
        public void StartRecordIndexAndEndRecordIndex_ShouldReturnCorrectValue(int total,int pageIndex,int pageSize,int startIndex,int endIndex)
        {
            var list = new PagedList<int>(Enumerable.Range(1, total), pageIndex, pageSize);
            Assert.Equal(list.StartItemIndex, startIndex);
            Assert.Equal(list.EndItemIndex, endIndex);
        }

        [Theory]
        [InlineData(88, 8, 8, 0, 57)]
        [InlineData(88, 8, 8, 7, 64)]
        [InlineData(100, 8, 10, 5, 76)]
        [InlineData(88, 3, 8, 0, 17)]
        public void PagedListItem_ShouldReturnCorrectValue(int total, int pageIndex, int pageSize, int arrayIndex, int value)
        {
            var list = new PagedList<int>(Enumerable.Range(1, total), pageIndex, pageSize);
            Assert.Equal(list[arrayIndex], value);
        }

        [Theory]
        [InlineData(88,2,8,0,9)]
        [InlineData(88,8,8,0,57)]
        [InlineData(88,8,8,7,64)]
        public void EnumerablePagedListItem_ShouldReturnCorrectValue(int total,int pageIndex,int pageSize,int arrayIndex,int value)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            var list = new PagedList<int>(Enumerable.Range(startIndex, startIndex + pageSize), pageIndex, pageSize, total);
            Assert.Equal(list[arrayIndex], value);
        }

        [Theory]
        [InlineData(9,1,8,0,1)]
        [InlineData(9,2,8,0,9)]
        [InlineData(9,5,8,0,9)]
        [InlineData(20,3,10,0,11)]
        public void ToPagedList_PageIndexOutOfRange_ShouldReturnDataOfTheLastPage(int total,int pageIndex,int pageSize,int arrayIndex,int value)
        {
            var items = Enumerable.Range(1, total).ToList();
            PagedList<int> list = items.ToPagedList(pageIndex,pageSize);
            Assert.Equal(list[arrayIndex], value);
        }

        [Theory]
        [InlineData(88,3,5,18)]
        [InlineData(100,1,10,10)]
        [InlineData(100,2,18,6)]
        public void TotalPageCount_ShouldBeCorrect(int total,int pageIndex,int pageSize,int pageCount)
        {
            var list = new PagedList<int>(Enumerable.Range(1, total), pageIndex,pageSize);
            Assert.Equal(list.TotalPageCount, pageCount);
        }
    }
}
