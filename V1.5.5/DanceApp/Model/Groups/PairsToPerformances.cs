using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Groups
{
    public class PairsToPerformances
    {
        public void Distribution(List<ClassPairs> selectedPairs)
        {
            // Для этого надо узнать количество заходов: количество пар в группе / вместимость площадки
            // А после нужно поделить пары поровну +- 1 между заходами
            // Для этого существует два сценария:
            // Если остаток от деления пар на вместимость площадки != 0, то отнимаем от количества пар 1, делим чётное число пар по заходам и в первый заход дописываем 1 пару
            // Иначе делим чётное число пар по заходам

            // А если
        }
    }
}
