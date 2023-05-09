using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Shift
    {
        public int[,] LeftShift(int pairsCount, int[,] rivals)
        {
            int[,] copy = new int[3, pairsCount];

            int begin = 0;
            for (int i = 0; i < pairsCount; i++)
            {
                if (rivals[1, i] != 0)
                {
                    begin = i;
                    break;
                }
            }

            if (begin != 0)
            {
                for (int l = 0; l < begin; l++)
                {
                    copy[0, pairsCount - begin + l] = rivals[0, l];
                }

                int i = 0;
                for (; begin < pairsCount; begin++)
                {
                    copy[0, i] = rivals[0, begin];
                    copy[1, i] = rivals[1, begin];
                    copy[2, i] = rivals[2, begin];
                    rivals[0, begin] = 0;
                    rivals[1, begin] = 0;
                    rivals[2, begin] = 0;
                    i++;
                }

                for (int v = 0; v < pairsCount; v++)
                {
                    rivals[0, v] = copy[0, v];
                    rivals[1, v] = copy[1, v];
                    rivals[2, v] = copy[2, v];
                }
            }
            return rivals;
        }
    }
}
