using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class Shift
    {
        [Fact]
        public void Test1()
        {
            int pairsCount = 5;
            int[,] array = new int[,]
            {
                { 0, 0, 0, 0, 0},
                { 8, 5, 7, 9, 1},
                { 0, 0, 0, 0, 0}
            };
            
            var x = new DanceApp.Model.Skating.Shift();
            int[,] actual = x.LeftShift(pairsCount, array);

            int[,] expected = new int[,]
            {
                { 0, 0, 0, 0, 0},
                { 8, 5, 7, 9, 1},
                { 0, 0, 0, 0, 0}
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test2()
        {
            int pairsCount = 5;
            int[,] array = new int[,]
            {
                { 1, 2, 3, 4, 5},
                { 0, 0, 0, 0, 1},
                { 0, 0, 0, 0, 0}
            };

            var x = new DanceApp.Model.Skating.Shift();
            int[,] actual = x.LeftShift(pairsCount, array);

            int[,] expected = new int[,]
            {
                { 5, 1, 2, 3, 4},
                { 1, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0}
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test3()
        {
            int pairsCount = 5;
            int[,] array = new int[,]
            {
                { 1, 3, 5, 4, 2},
                { 0, 0, 4, 0, 0},
                { 0, 0, 0, 0, 0}
            };

            var x = new DanceApp.Model.Skating.Shift();
            int[,] actual = x.LeftShift(pairsCount, array);

            int[,] expected = new int[,]
            {
                { 5, 4, 2, 1, 3},
                { 4, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0}
            };

            Assert.Equal(actual, expected);
        }
    }
}
