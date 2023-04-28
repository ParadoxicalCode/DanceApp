using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class RightMatrix
    {
        public int[,] Calculate(int[,] leftMatrix, int pairsCount, int judgesCount, out string[,] result)
        {
            result = new string[pairsCount, pairsCount + 1];
            int[,] rightMatrix = new int[pairsCount, pairsCount];

            for (int j = 0; j < pairsCount; j++)
            {
                int placeCount = 1;
                for (int i = 0; i < pairsCount; i++)
                {
                    int sum = 0;
                    for (int counter = 0; counter < judgesCount; counter++)
                    {
                        if (leftMatrix[j, counter] <= placeCount)
                        {
                            sum++;
                        }
                    }
                    rightMatrix[j, i] = sum;
                    placeCount++;
                }
            }

            // Заполняем правую часть таблицы для GUI.
            for (int a = 0; a < pairsCount; a++)
            {
                for (int b = 0; b < pairsCount; b++)
                {
                    if (rightMatrix[a, b] == 0)
                    {
                        result[a, b] = "-";
                    }
                    else
                    {
                        result[a, b] = rightMatrix[a, b].ToString();
                    }
                }
            }
            return rightMatrix;
        }
    }
}
