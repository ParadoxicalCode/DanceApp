using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class SortingSum
    {
        [Fact]
        public void Test1()
        {
            int same = 2;

            var rivals = new int[,]
            {
                { 1, 2, 3 },
                { 7, 6, 0 },
                { 0, 0, 0 },
            };

            var expected = new int[,]
            {
                { 2, 1, 3 },
                { 6, 7, 0 },
                { 0, 0, 0 },
            };

            var x = new DanceApp.Model.Skating.Sorting();
            rivals = x.SortingSum(rivals, same);

            Assert.Equal(rivals, expected);
        }

        [Fact]
        public void Test2()
        {
            int same = 3;

            var rivals = new int[,]
            {
                { 1, 2, 3, 4, 5 },
                { 3, 7, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
            };

            var expected = new int[,]
            {
                { 3, 1, 2, 4, 5 },
                { 1, 3, 7, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Sorting();
            rivals = x.SortingSum(rivals, same);

            Assert.Equal(rivals, expected);
        }
    }
}
