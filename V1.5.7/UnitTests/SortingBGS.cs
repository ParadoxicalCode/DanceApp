using DanceApp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UnitTests
{
    public class SortingBGS
    {
        [Fact]
        public void Test1()
        {
            int pairsCount = 7;
            int[,] array = new int[,]
            {
                { 1, 2, 3, 4, 5, 6, 7 },
                { 7, 5, 3, 0, 8, 9, 1 },
                { 0, 0, 0, 0, 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Sorting();
            int[,] actual = x.SortingBGS(array, pairsCount);

            int[,] expected = new int[,]
            {
                { 6, 5, 1, 2, 3, 7, 4},
                { 9, 8, 7, 5, 3, 1, 0},
                { 0, 0, 0, 0, 0, 0, 0 }
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test2()
        {
            int pairsCount = 5;
            int[,] array = new int[,]
            {
                { 1, 2, 3, 4, 5 },
                { 4, 5, 7, 5, 2 },
                { 0, 0, 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Sorting();
            int[,] actual = x.SortingBGS(array, pairsCount);

            int[,] expected = new int[,]
            {
                { 3, 2, 4, 1, 5 },
                { 7, 5, 5, 4, 2 },
                { 0, 0, 0, 0, 0 }
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test3()
        {
            int pairsCount = 3;
            int[,] array = new int[,]
            {
                { 1, 2, 3 },
                { 7, 5, 8 },
                { 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Sorting();
            int[,] actual = x.SortingBGS(array, pairsCount);

            int[,] expected = new int[,]
            {
                { 3, 1, 2 },
                { 8, 7, 5 },
                { 0, 0, 0 }
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test4()
        {
            int pairsCount = 3;
            int[,] array = new int[,]
            {
                { 1, 2, 3 },
                { 8, 5, 7 },
                { 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Sorting();
            int[,] actual = x.SortingBGS(array, pairsCount);

            int[,] expected = new int[,]
            {
                { 1, 3, 2 },
                { 8, 7, 5 },
                { 0, 0, 0 }
            };

            Assert.Equal(actual, expected);
        }
    }
}