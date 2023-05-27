using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Controls;
using Wpf.Models;

namespace Wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelMainWindow _vm;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new ViewModelMainWindow();

            _vm.EditStudentEvent += Vm_EditStudentEvent;
            this.DataContext = _vm;
        }

        private void Vm_EditStudentEvent(object sender, EventArgs e)
        {
            _vm.VmEdit = new ViewModelEditStudent(sender as Student);

            var win = new EditStudent(_vm.VmEdit);

            if (sender != null) _vm.VmEdit.ActiveStudent = sender as Student;

            var res = win.ShowDialog();

            if (res == true)
            {
                if (sender == null) _vm.ListStudents.Add(_vm.VmEdit.ActiveStudent);
            }
        }
    }
}
