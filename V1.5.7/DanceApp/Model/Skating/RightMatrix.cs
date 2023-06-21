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

            for (int i = 0; i < pairsCount; i++)
            {
                int placeCount = 1;
                for (int beginCell = 0; beginCell < pairsCount; beginCell++)
                {
                    int sum = 0;
                    for (int j = 0; j < judgesCount; j++)
                    {
                        if (leftMatrix[i, j] <= placeCount)
                        {
                            sum++;
                        }
                    }
                    rightMatrix[i, beginCell] = sum;
                    placeCount++;
                }
            }

            // Заполняем правую часть таблицы чёрточками для GUI.
            for (int i = 0; i < pairsCount; i++)
            {
                for (int j = 0; j < pairsCount; j++)
                {
                    if (rightMatrix[i, j] == 0)
                    {
                        result[i, j] = "-";
                    }
                    else
                    {
                        result[i, j] = rightMatrix[i, j].ToString();
                    }
                }
            }
            return rightMatrix;
        }
    }
}
