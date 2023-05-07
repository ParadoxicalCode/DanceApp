using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Models;
using Wpf.MVVM;

namespace Wpf.Controls
{
    public class ViewModelEditStudent : ModelBase
    {
        private bool? _dialogResult;
        private ICommand _closeCommand;
        private ICommand _saveCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null) _closeCommand = new RelayCommand(p => DialogResult = false);
                return _closeCommand;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null) _saveCommand = new RelayCommand(p => DialogResult = true);
                return _saveCommand;
            }
        }
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set { _dialogResult = value; OnPropertyChanged(); }
        }
        
        public ViewModelEditStudent(Student stud = null)
        {
            if (stud != null) ActiveStudent = stud;
            else ActiveStudent = new Student();
        }

        private Student _activeStudent;

        public Student ActiveStudent
        {
            get { return _activeStudent; }
            set
            {
                _activeStudent = value;
                OnPropertyChanged();
            }
        }
    }
}
