using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UnitTests
{
    public class RightMatrix
    {
        [Fact]
        public void Test1()
        {
            int[,] leftMatrix = new int[5, 7]
            {
                { 1, 5, 1, 4, 2, 5, 4 },
                { 4, 3, 2, 3, 1, 3, 2 },
                { 3, 1, 3, 2, 3, 2, 5 },
                { 5, 2, 4, 5, 4, 1, 1 },
                { 2, 4, 5, 1, 5, 4, 3 }
            };

            var x = new DanceApp.Model.Skating.RightMatrix();
            int[,] actual = x.Calculate(leftMatrix, 5, 7, out string[,] result);

            int[,] expected = new int[5,5]
            {
                { 2, 3, 3, 5, 7 },
                { 1, 3, 6, 7, 7 },
                { 1, 3, 6, 6, 7 },
                { 2, 3, 3, 5, 7 },
                { 1, 2, 3, 5, 7 },
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test2()
        {
            int[,] leftMatrix = new int[5, 3]
            {
                { 1, 1, 1 },
                { 2, 2, 2 },
                { 3, 3, 3 },
                { 5, 5, 4 },
                { 4, 4, 5 },
            };

            var x = new DanceApp.Model.Skating.RightMatrix();
            int[,] actual = x.Calculate(leftMatrix, 5, 3, out string[,] result);

            int[,] expected = new int[5, 5]
            {
                { 3, 3, 3, 3, 3 },
                { 0, 3, 3, 3, 3 },
                { 0, 0, 3, 3, 3 },
                { 0, 0, 0, 1, 3 },
                { 0, 0, 0, 2, 3 },
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test3()
        {
            int[,] leftMatrix = new int[4, 5]
            {
                { 1, 1, 2, 4, 4 },
                { 4, 3, 1, 3, 1 },
                { 3, 4, 3, 2, 2 },
                { 2, 2, 4, 1, 3 }
            };

            var x = new DanceApp.Model.Skating.RightMatrix();
            int[,] actual = x.Calculate(leftMatrix, 4, 5, out string[,] result);

            int[,] expected = new int[4, 4]
            {
                { 2, 3, 3, 5},
                { 2, 2, 4, 5},
                { 0, 2, 4, 5},
                { 1, 3, 4, 5}
            };

            Assert.Equal(actual, expected);
        }
    }
}
