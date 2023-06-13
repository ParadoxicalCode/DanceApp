using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Rule5
    {
        // В массиве rivals могут быть нули в любом месте.
        public int Calculate(int pairsCount, int[,] rivals, ref string[,] result, int placeCounter, int rightMatrixColumn, int unallocatedPairs)
        {
            for (int i = 0; i < pairsCount; i++)
            {
                if (rivals[1, i] != 0)
                {
                    result[i, pairsCount] = placeCounter.ToString();
                    placeCounter++;

                    // Ставим тире.
                    var x = new Clear();
                    if (unallocatedPairs == 0)
                    {
                        x.Calculate(ref result, rivals[0, i] - 1, rightMatrixColumn, pairsCount);
                    }
                    else
                    {
                        x.Calculate(ref result, rivals[0, i] - 1, rivals[2, i], pairsCount);
                    }

                    break;
                }
            }
            return placeCounter;
        }
    }
}
