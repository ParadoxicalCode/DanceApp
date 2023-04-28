using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Skating
{
    public class Rule7
    {
        // Проходимся по значениям БГС в правой части таблицы, записываем конкурирующие пары в rivals. Сортируем массив по убыванию. 
        // Первую строку rivals нужно сохранить, так как она содержит места пар.

        // Создание массива, содержащего строки правой части таблицы для конкурирующих за место пар.
        public int[,] DetachedRightMatrix(int[,] rightMatrix, int[,] rivals, int pairsCount, int same)
        {
            var detachedRightMatrix = new int[same, pairsCount];

            for (int i = 0; i < same; i++)
            {
                for (int j = 0; j < pairsCount; j++)
                {
                    detachedRightMatrix[i, j] = rightMatrix[rivals[0, i] - 1, j];
                }
            }
            return detachedRightMatrix;
        }

        public int[,] PlacesSum(int same, int judgesCount, int[,] leftMatrix, int rightMatrixColumn, ref string[,] result, int[,] rivals)
        {
            for (int i = 0; i < same; i++)
            {
                int sum = 0;
                for (int j = 0; j < judgesCount; j++)
                {
                    if (leftMatrix[rivals[0, i] - 1, j] <= rightMatrixColumn + 1)
                    {
                        sum += leftMatrix[rivals[0, i] - 1, j];
                    }
                }
                rivals[1, i] = sum;
                rivals[2, i] = rightMatrixColumn;

                // Проверка на длину нужна чтобы в ячейку не продублировалась сумма мест.
                if (result[rivals[0, i] - 1, rightMatrixColumn].Length < 5)
                {
                    result[rivals[0, i] - 1, rightMatrixColumn] += $" ({sum})";
                }
            }
            return rivals;
        }

        // Подсчёт количества пар с подряд идущими числами и удаление тех пар, которые различны с первыми двумя.
        public int[,] Equal(int[,] rivals, out int same, int pairsCount)
        {
            same = 1;
            for (int i = 0; i < pairsCount - 1; i++)
            {
                if (rivals[1, 0] == rivals[1, i + 1])
                {
                    same++;
                }
                else
                {
                    rivals[1, i + 1] = 0;
                }
            }
            return rivals;
        }
    }
}
