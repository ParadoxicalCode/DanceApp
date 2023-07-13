using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Clear
    {
        public void Calculate(ref string[,] result, int i, int j, int pairsCount)
        {
            j++;
            for (; j < pairsCount; j++)
            {
                result[i, j] = "-";
            }
        }
    }
}
