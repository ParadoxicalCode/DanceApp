using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace UnitTests
{
    public class Equal
    {
        [Fact]
        public void Test1()
        {
            int pairsCount = 7;
            int equalSum = 1;
            int expectedSum = 3;

            var rivals = new int[,]
            {
                { 1, 2, 3, 4, 5, 6, 7 },
                { 3, 3, 3, 4, 4, 5, 10 }
            };

            var expected = new int[,]
            {
                { 1, 2, 3, 4, 5, 6, 7 },
                { 3, 3, 3, 0, 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Rule7();
            rivals = x.Equal(rivals, out equalSum, pairsCount);

            Assert.Equal(rivals, expected);
            Assert.Equal(equalSum, expectedSum);
        }

        [Fact]
        public void Test2()
        {
            int pairsCount = 7;
            int equalSum = 1;
            int expectedSum = 2;

            var rivals = new int[,]
            {
                { 1, 2, 3, 4, 5, 6, 7 },
                { 1, 1, 3, 4, 4, 5, 10 }
            };

            var expected = new int[,]
            {
                { 1, 2, 3, 4, 5, 6, 7 },
                { 1, 1, 0, 0, 0, 0, 0 }
            };

            var x = new DanceApp.Model.Skating.Rule7();
            rivals = x.Equal(rivals, out equalSum, pairsCount);

            Assert.Equal(rivals, expected);
            Assert.Equal(equalSum, expectedSum);
        }

        [Fact]
        public void Test3()
        {
            int pairsCount = 5;
            int equalSum = 1;
            int expectedSum = 5;

            var rivals = new int[,]
            {
                { 1, 2, 3, 4, 5},
                { 1, 1, 1, 1, 1}
            };

            var expected = new int[,]
            {
                { 1, 2, 3, 4, 5 },
                { 1, 1, 1, 1, 1 }
            };

            var x = new DanceApp.Model.Skating.Rule7();
            rivals = x.Equal(rivals, out equalSum, pairsCount);

            Assert.Equal(rivals, expected);
            Assert.Equal(equalSum, expectedSum);
        }
    }
}
