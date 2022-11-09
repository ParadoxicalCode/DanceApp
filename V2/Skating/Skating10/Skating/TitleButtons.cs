using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Skating
{
    internal class TitleButtons
    {
        static bool isMax = false;
        static Point old_loc, default_loc;
        static Size old_size, default_size;

        public static void SetIntials(Window win)
        {
            old_size = new Size(win.Width, win.Height);
            old_loc = new Point(win.Top, win.Left);
            default_size = new Size(win.Width, win.Height);
            default_loc = new Point(win.Top, win.Left);
        }

        public static void DoMaximize(Window win, Button btn)
        {
            // Если приложение не развёрнуто, то сделать окно больше.
            if (isMax == false)
            {
                old_size = new Size(win.Width, win.Height);
                old_loc = new Point(win.Top, win.Left);
                Maximize(win);
                isMax = true;
                btn.Content = "2";
            }

            // Иначе сделать окно меньше.
            else
            {
                if (old_size.Width >= SystemParameters.WorkArea.Width ||
                    old_size.Height >= SystemParameters.WorkArea.Height)
                {
                    win.Top = default_loc.Y;
                    win.Left = default_loc.X;
                    win.Width = default_size.Width;
                    win.Height = default_size.Height;
                }

                else
                {
                    win.Top = old_loc.Y;
                    win.Left = old_loc.X;
                    win.Width = old_size.Width;
                    win.Height = old_size.Height;
                }
                isMax = false;
                btn.Content = "1";
            }
        }

        static void Maximize(Window win)
        {
            double x = SystemParameters.WorkArea.Width;
            double y = SystemParameters.WorkArea.Height;
            win.WindowState = WindowState.Normal;
            win.Top = 0;
            win.Left = 0;
            win.Width = x;
            win.Height = y;
        }
    }
}
