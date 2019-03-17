using System.Linq;
using System.Threading.Tasks;
using Webdiyer.AspNetCore;
using Xunit;

namespace Webdiyer.MvcCorePagerTest
{
    public class ToPagedListTests
    {
        [Theory]
        [InlineData(88, 3, 8, 17, 24)]
        [InlineData(100, 1, 10, 1, 10)]
        [InlineData(100, 8, 10, 71, 80)]
        [InlineData(88, 4, 5, 16, 20)]
        [InlineData(88, 3, 5, 11, 15)]
        public void StartItemIndexAndEndItemIndex_ShouldBeCorrect(int total, int pageIndex, int pageSize, int expectedStartIndex, int expectedEndIndex)
        {
            var pagedList = Enumerable.Range(1, total).ToList().ToPagedList(pageIndex, pageSize);
            Assert.Equal(pagedList.StartItemIndex, expectedStartIndex);
            Assert.Equal(pagedList.EndItemIndex, expectedEndIndex);
        }

        [Theory]
        [InlineData(88, 3, 8, 17, 24)]
        [InlineData(100, 1, 10, 1, 10)]
        [InlineData(100, 8, 10, 71, 80)]
        [InlineData(88, 4, 5, 16, 20)]
        [InlineData(88, 3, 5, 11, 15)]
        public async Task StartItemIndexAndEndItemIndex_ShouldBeCorrect_Async(int total, int pageIndex, int pageSize, int expectedStartIndex, int expectedEndIndex)
        {
            var pagedList =await Enumerable.Range(1, total).ToPagedListAsync(pageIndex, pageSize);
            Assert.Equal(pagedList.StartItemIndex, expectedStartIndex);
            Assert.Equal(pagedList.EndItemIndex, expectedEndIndex);
        }        

        [Theory]
        [InlineData(88, 8, 8, 0, 57)]
        [InlineData(88, 8, 8, 7, 64)]
        [InlineData(100, 8, 10, 5, 76)]
        [InlineData(88, 3, 8, 0, 17)]
        public void PagedListItem_ShouldBeCorrect(int total, int pageIndex, int pageSize, int arrayIndex, int expectedValue)
        {
            var pagedList = Enumerable.Range(1, total).ToPagedList(pageIndex, pageSize);
            Assert.Equal(pagedList[arrayIndex], expectedValue);
        }

        [Theory]
        [InlineData(88, 8, 8, 0, 57)]
        [InlineData(88, 8, 8, 7, 64)]
        [InlineData(100, 8, 10, 5, 76)]
        [InlineData(88, 3, 8, 0, 17)]
        public async Task PagedListItem_ShouldBeCorrect_Async(int total, int pageIndex, int pageSize, int arrayIndex, int expectedValue)
        {
            var pagedList = await Enumerable.Range(1, total).ToPagedListAsync(pageIndex, pageSize);
            Assert.Equal(pagedList[arrayIndex], expectedValue);
        }

        [Theory]
        [InlineData(88, 3, 5, 18)]
        [InlineData(100, 1, 10, 10)]
        [InlineData(100, 2, 18, 6)]
        public void TotalPageCount_ShouldBeCorrect(int total, int pageIndex, int pageSize, int expectedPageCount)
        {
            var pagedList = Enumerable.Range(1, total).ToPagedList(pageIndex, pageSize);
            Assert.Equal(pagedList.TotalPageCount, expectedPageCount);
        }

        [Theory]
        [InlineData(88, 3, 5, 18)]
        [InlineData(100, 1, 10, 10)]
        [InlineData(100, 2, 18, 6)]
        public async Task TotalPageCount_ShouldBeCorrect_Async(int total, int pageIndex, int pageSize, int expectedPageCount)
        {
            var pagedList = await Enumerable.Range(1, total).ToPagedListAsync(pageIndex, pageSize);
            Assert.Equal(pagedList.TotalPageCount, expectedPageCount);
        }

        [Theory]
        [InlineData(9,1,8,0,1)]
        [InlineData(9,2,8,0,9)]
        [InlineData(9,5,8,0,9)]
        [InlineData(20,3,10,0,11)]
        public void PageIndexOutOfRange_ShouldReturnDataOfTheLastPage(int total,int pageIndex,int pageSize,int arrayIndex,int expectedValue)
        {
            PagedList<int> list = Enumerable.Range(1, total).ToPagedList(pageIndex,pageSize);
            Assert.Equal(list[arrayIndex], expectedValue);
        }

        [Theory]
        [InlineData(9, 1, 8, 0, 1)]
        [InlineData(9, 2, 8, 0, 9)]
        [InlineData(9, 5, 8, 0, 9)]
        [InlineData(20, 3, 10, 0, 11)]
        public async Task PageIndexOutOfRange_ShouldReturnDataOfTheLastPage_Async(int total, int pageIndex, int pageSize, int arrayIndex, int expectedValue)
        {
            PagedList<int> list =await Enumerable.Range(1, total).ToPagedListAsync(pageIndex, pageSize);
            Assert.Equal(list[arrayIndex], expectedValue);
        }
    }
}
