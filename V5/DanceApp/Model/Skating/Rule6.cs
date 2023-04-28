using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Rule6
    {
        public int[,] Calculate(ref int [,] rightMatrix, int rightMatrixColumn, int pairsCount, int[,] rivals, ref string[,] result, ref int placeCounter)
        {
            result[rivals[0, 0] - 1, pairsCount] = placeCounter.ToString();

            // Ставим тире.
            var x = new Clear();
            x.Calculate(ref result, rivals[0, 0] - 1, rivals[2, 0], pairsCount);

            rivals[1, 0] = 0;
            rivals[2, 0] = 0;
            rightMatrix[rivals[0, 0] - 1, rightMatrixColumn] = 0;
            placeCounter++;

            return rivals;
        }
    }
}
