using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DanceApp.Model.Skating
{
    public class DistributionOfPlaces
    {
        public string[,] Distribution(int[,] leftMatrix, int pairsCount, int judgesCount)
        {
            var rm = new RightMatrix();
            var sbgs = new SearchBGS();
            var sort = new Sorting();
            var shift = new Shift();
            var rule5 = new Rule5();
            var rule6 = new Rule6();
            var rule7 = new Rule7();
            var rule8 = new Rule8();
            int placeCounter = 1;

            // Расчёт правой части таблицы.
            int[,] rightMatrix = rm.Calculate(leftMatrix, pairsCount, judgesCount, out string[,] result);

            for (int rightMatrixColumn = 0; placeCounter - 1 != pairsCount; rightMatrixColumn++)
            {
                int[,] rivals = new int[3, pairsCount]; // 1 строка - координата пары по вертикали, 2 строка - БГС или сумма мест, 3 строка - координата по горизонтали, когда стало понятно, что надо присуждать место.

                rule5:

                // Поиск пар, претендующих на место.
                rivals = sbgs.Search(rivals, rightMatrix, rightMatrixColumn, result, pairsCount, judgesCount, out int rivalsCount, out int unallocatedPairs);

                if (rivalsCount == 1)
                {
                    // Правило 5.
                    placeCounter = rule5.Calculate(pairsCount, rivals, ref result, placeCounter, rightMatrixColumn, unallocatedPairs);
                }

                else if (rivalsCount > 1)
                {
                    rivals = sort.SortingBGS(rivals, pairsCount);

                    bool rule7Switch = true; // Переменная-переключатель. Сначала сравниваются БГС, потом суммы мест.

                    for (int rightMatrixColumn78 = rightMatrixColumn; rivalsCount > 0;)
                    {
                        if (rivals[1, 0] != rivals[1, 1] && rivals[1, 0] != 0)
                        {
                            // Правило 6.
                            rivals = rule6.Calculate(ref rightMatrix, rightMatrixColumn, pairsCount, rivals, ref result, ref placeCounter);
                            rivals = shift.LeftShift(pairsCount, rivals);
                            rivalsCount--;
                        }

                        // Правило 7.
                        else if (rivals[1, 0] == rivals[1, 1] && rivals[1, 0] != 0)
                        {
                            // Подсчёт количества пар с подряд идущими числами и удаление всех пар после, если серия прерывается.
                            rivals = rule7.Equal(rivals, out int same, pairsCount);

                            if (rule7Switch == true && rightMatrixColumn78 != pairsCount - 1)
                            {
                                // Подсчёт сумм мест в текущем столбце и вывод их в GUI для дальнейшего сравнения сумм мест.
                                rivals = rule7.PlacesSum(same, judgesCount, leftMatrix, rightMatrixColumn78, ref result, rivals);
                                rivals = sort.SortingSum(rivals, same);

                                rule7Switch = false;
                            }

                            else if (rightMatrixColumn78 != pairsCount - 1)
                            {
                                // Выписываем строки правой части таблицы для конкурирующих за место пар.
                                int[,] DetachedRightMatrix = rule7.DetachedRightMatrix(rightMatrix, rivals, pairsCount, same);

                                // Запись БГС конкурирующих за место пар следующего столбца и координат следующего столбца.
                                rivals = sbgs.DetachedSearchBGS(DetachedRightMatrix, rightMatrixColumn78, rivals, same);

                                rivals = sort.SortingBGS(rivals, pairsCount);

                                rule7Switch = true;
                                rightMatrixColumn78++;
                            }
                            else
                            {
                                // Подсчёт сумм мест и вывод их в GUI в последнем столбце.
                                rivals = rule7.PlacesSum(same, judgesCount, leftMatrix, rightMatrixColumn78, ref result, rivals);

                                // Правило 8.
                                rivals = rule8.Calculate(ref placeCounter, same, ref result, pairsCount, rivals, ref rightMatrix, ref rivalsCount, rightMatrixColumn);
                            }
                        }

                        // Если в текущем столбце ещё остались пары, то перейти к правилу 5.
                        else if (rivalsCount > 0 && rivals[1, 0] == 0)
                        {
                            goto rule5;
                        }
                    }
                }
            }
            return result;
        }
    }
}
