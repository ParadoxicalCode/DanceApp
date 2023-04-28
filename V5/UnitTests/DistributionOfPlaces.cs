using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class DistributionOfPlaces
    {
        [Fact]
        public void Test1()
        {
            int[,] leftMatrix = new int[5, 7]
            {
               // Левая часть таблицы        Правая часть таблицы

   // Количество конкурирующих за место пар: 0   0   2   3   -           Места

                { 1, 5, 1, 4, 2, 5, 4 }, //  2 | 3 | 3 | 5(12) | 7       3,5
                { 4, 3, 2, 3, 1, 3, 2 }, //  1 | 3 | 6 | 7     | 7       1
                { 3, 1, 3, 2, 3, 2, 5 }, //  1 | 3 | 6 | 6     | 7       2
                { 5, 2, 4, 5, 4, 1, 1 }, //  2 | 3 | 3 | 5(12) | 7       3,5
                { 2, 4, 5, 1, 5, 4, 3 }, //  1 | 2 | 3 | 5(14) | 7       5
            };

            var x = new DanceApp.Model.Skating.DistributionOfPlaces();
            string[,] array = x.Distribution(leftMatrix, 5, 7);
            string[] actual = new string[5];

            for (int i = 0; i < 5; i++)
            {
                actual[i] = array[i, 5];
            }

            string[] expected = { "3,5", "1", "2", "3,5", "5"};

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test2()
        {
            int[,] leftMatrix = new int[3, 7]
            {
                { 1, 3, 2, 3, 2, 1, 2 },
                { 2, 2, 3, 1, 1, 2, 3 },
                { 3, 1, 1, 2, 3, 3, 1 }
            };

            var x = new DanceApp.Model.Skating.DistributionOfPlaces();
            string[,] array = x.Distribution(leftMatrix, 3, 7);
            string[] actual = new string[3];

            for (int i = 0; i < 3; i++)
            {
                actual[i] = array[i, 3];
            }

            string[] expected = { "1,5", "1,5", "3" };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Test3()
        {
            int pairsCount = 5;
            int judgesCount = 3;

            int[,] leftMatrix = new int[5, 3]
            {
                { 3, 4, 1 },
                { 4, 2, 2 },
                { 2, 3, 4 },
                { 5, 1, 5 },
                { 1, 5, 3 }
            };

            var x = new DanceApp.Model.Skating.DistributionOfPlaces();
            string[,] array = x.Distribution(leftMatrix, pairsCount, judgesCount);
            string[] actual = new string[5];

            for (int i = 0; i < pairsCount; i++)
            {
                actual[i] = array[i, pairsCount];
            }

            string[] expected = { "2", "1", "4", "5", "3" };

            Assert.Equal(actual, expected);
        }

        /*
        [Fact]
        public void Test4()
        {
            int pairsCount = 10;
            int judgesCount = 7;

            int[,] leftMatrix = new int[10, 7]
            {
                { 6,  2,  5,  4,  10, 5,  1 },
                { 1,  4,  7,  8,  4,  1,  6 },
                { 8,  1,  4,  10, 3,  2,  7 },
                { 7,  9,  8,  3,  6,  3,  5 },
                { 5,  5,  9,  9,  7,  4,  2 },
                { 3,  10, 6,  5,  2,  7,  10 },
                { 4,  6,  1,  7,  1,  9,  3 },
                { 10, 3,  2,  6,  8,  10, 4 },
                { 9,  7,  3,  1,  9,  8,  8 },
                { 2,  8,  10, 2,  5,  6,  9 }
            };

            string[,] expected = new string[10, 11]
            {
                { "1", "2", "2", "3"    , "5"    , "-"    , "-"    , "-"    , "-", "-", "4"  },
                { "2", "2", "2", "4(10)", "4(10)", "5"    , "-"    , "-"    , "-", "-", "2"  },
                { "1", "2", "3", "4(10)", "4(10)", "4"    , "-"    , "-"    , "-", "-", "3"  },
                { "-", "-", "2", "2"    , "3"    , "4(17)", "-"    , "-"    , "-", "-", "9"  },
                { "-", "1", "1", "2"    , "4"    , "-"    , "-"    , "-"    , "-", "-", "5"  },
                { "-", "1", "2", "2"    , "3"    , "4(16)", "-"    , "-"    , "-", "-", "8"  },
                { "2", "2", "3", "4(9)" , "-"    , "-"    , "-"    , "-"    , "-", "-", "1"  },
                { "-", "1", "2", "3"    , "3"    , "4(15)", "4(15)", "5(23)", "5", "-", "7"  },
                { "1", "1", "2", "2"    , "2"    , "2"    , "3"    , "5"    , "-", "-", "10" },
                { "-", "2", "2", "2"    , "3"    , "4(15)", "4(15)", "5(23)", "6", "-", "6"  }
            };

            var x = new DanceApp.Model.Skating.DistributionOfPlaces();
            string[,] actual = x.Distribution(leftMatrix, pairsCount, judgesCount);

            Assert.Equal(actual, expected);
        }
        */
    }
}
