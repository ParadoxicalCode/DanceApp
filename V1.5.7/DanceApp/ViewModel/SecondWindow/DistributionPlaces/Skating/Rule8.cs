using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Rule8
    {
        public int[,] Calculate(ref int placeCounter, int equalSum, ref string[,] result, int pairsCount, int[,] rivals, ref int[,] rightMatrix, ref int rivalsCount, int rightMatrixColumn)
        {
            int sum = 0;
            for (int y = placeCounter; y < placeCounter + equalSum; y++)
            {
                sum += y;
            }

            double fractionalPlace = sum / (double)equalSum;

            for (int y = 0; y < equalSum; y++)
            {
                result[rivals[0, y] - 1, pairsCount] = fractionalPlace.ToString();
                placeCounter++;
                rightMatrix[rivals[0, 0] - 1, rightMatrixColumn] = 0;
                rivalsCount--;
                rivals[1, y] = 0;
                rivals[2, y] = 0;
            }
            return rivals;
        }
    }
}
