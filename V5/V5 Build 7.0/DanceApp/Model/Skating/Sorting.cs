using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Sorting
    {
        // Сортировка по убыванию БГС.
        public int[,] SortingBGS(int[,] rivals, int pairsCount)
        {
            for (int i = 0; i < pairsCount; i++)
            {
                for (int j = 0; j < pairsCount - 1; j++)
                {
                    if (rivals[1, j] < rivals[1, j + 1])
                    {
                        int temp1 = rivals[0, j];
                        rivals[0, j] = rivals[0, j + 1];
                        rivals[0, j + 1] = temp1;

                        int temp2 = rivals[1, j];
                        rivals[1, j] = rivals[1, j + 1];
                        rivals[1, j + 1] = temp2;

                        int temp3 = rivals[2, j];
                        rivals[2, j] = rivals[2, j + 1];
                        rivals[2, j + 1] = temp3;
                    }
                }
            }
            return rivals;
        }

        // Сортировка по возрастанию сумм мест.
        public int[,] SortingSum(int[,] rivals, int same)
        {
            for (int i = 0; i < same; i++)
            {
                for (int j = 0; j < same - 1; j++)
                {
                    if (rivals[1, j] > rivals[1, j + 1])
                    {
                        int temp1 = rivals[0, j];
                        rivals[0, j] = rivals[0, j + 1];
                        rivals[0, j + 1] = temp1;

                        int temp2 = rivals[1, j];
                        rivals[1, j] = rivals[1, j + 1];
                        rivals[1, j + 1] = temp2;

                        int temp3 = rivals[2, j];
                        rivals[2, j] = rivals[2, j + 1];
                        rivals[2, j + 1] = temp3;
                    }
                }
            }
            return rivals;
        }
    }
}
