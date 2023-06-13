using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class Rule5
    {
        [Fact]
        public void Test1()
        {
            int pairsCount = 5;
            int placeCounter = 1;
            int rightMatrixColumn = 0;
            int unallocatedPairs = 0;
            var BGSPairs = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 4, 0 }
            };

            var result = new string[,]
            {
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "0" }
            };

            var x = new DanceApp.Model.Skating.Rule5();
            placeCounter = x.Calculate(pairsCount, BGSPairs, ref result, placeCounter, rightMatrixColumn, unallocatedPairs);

            var expected = new string[,]
            {
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "0" },
                { "0", "0", "0", "0", "0", "1" },
                { "0", "0", "0", "0", "0", "0" }
            };

            Assert.Equal(result, expected);
        }
    }
}
