using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class SearchBGS
    {
        // Поиск пар, набравших БГС.
        public int[,] Search(int[,] rivals, int[,] rightMatrix, int rightMatrixColumn, string[,] result, int pairsCount, int judgesCount, out int rivalsCount, out int unallocatedPairs)
        {
            rivalsCount = 0;
            unallocatedPairs = 0;

            // Проверка на нераспределённые пары в текущем столбце.
            for (int i = 0; i < pairsCount; i++)
            {
                if (rivals[2, i] > 0)
                {
                    unallocatedPairs++;
                    rivals[1, i] = rightMatrix[rivals[0, i] - 1, rightMatrixColumn]; rivalsCount++;
                }
            }

            if (unallocatedPairs == 0)
            {
                // Выставление индексов пар по возрастанию.
                for (int i = 0; i < pairsCount; i++)
                {
                    rivals[0, i] = i + 1;
                }

                for (int i = 0; i < pairsCount; i++)
                {
                    // Если участник набрал необходимое количество БГС и он не занял какое-либо место.
                    if (rightMatrix[i, rightMatrixColumn] >= (judgesCount + 1) / 2 && result[i, pairsCount] == null)
                    {
                        rivals[1, i] = rightMatrix[i, rightMatrixColumn]; rivalsCount++;
                        rivals[2, i] = rightMatrixColumn;
                    }
                }
            }
            else
            {
                for (int i = 0; i < pairsCount; i++)
                {
                    // Если участник набрал необходимое количество БГС и он не занял какое-либо место.
                    if (rightMatrix[rivals[0, i] - 1, rightMatrixColumn] >= (judgesCount + 1) / 2 && result[rivals[0, i] - 1, pairsCount] == null)
                    {
                        rivals[1, i] = rightMatrix[rivals[0, i] - 1, rightMatrixColumn]; rivalsCount++;
                        if (rivals[2, i] == 0)
                        {
                            rivals[2, i] = rightMatrixColumn;
                        }
                    }
                }
            }
            return rivals;
        }

        // Запись БГС конкурирующих за место пар.
        public int[,] DetachedSearchBGS(int[,] DetachedRightMatrix, int rightMatrixColumn78, int[,] rivals, int same)
        {
            // Очистка.
            for (int i = 0; i < same; i++)
            {
                rivals[1, i] = 0;
                rivals[2, i] = 0;
            }

            // Запись в rivals БГС пар.
            for (int i = 0; i < same; i++)
            {
                rivals[1, i] = DetachedRightMatrix[i, rightMatrixColumn78 + 1];
                rivals[2, i] = rightMatrixColumn78 + 1;
            }
            return rivals;
        }
    }
}
