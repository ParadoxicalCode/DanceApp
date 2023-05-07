using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Controls;
using Wpf.Models;
using Wpf.MVVM;

namespace Wpf
{
    public class ViewModelMainWindow : ModelBase
    {
        #region CONSTRUCTORS
        public ViewModelMainWindow()
        {
            GenerateStudents();
            VmEdit = new ViewModelEditStudent();
        }
        #endregion

        #region FIELDS
        private ObservableCollection<Student> _listStudents;
        private Student _activeStudent;
        public event EventHandler EditStudentEvent;
        private ICommand _editCommand;
        private ICommand _newCommand;
        private ICommand _deleteCommand;
        private ViewModelEditStudent _vmEdit;

        public ViewModelEditStudent VmEdit
        {
            get { return _vmEdit; }
            set { _vmEdit = value; }
        }

        #endregion

        #region PROPERTIES
        public Student ActiveStudent
        {
            get { return _activeStudent; }
            set { _activeStudent = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Student> ListStudents
        {
            get { return _listStudents; }
            set { _listStudents = value; OnPropertyChanged(); }
        }
        
        #endregion

        #region COMMANDS
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null) _editCommand = new RelayCommand(p => ShowEditWindow(ActiveStudent));
                return _editCommand;
            }
        }
        public ICommand NewCommand
        {
            get
            {
                if (_newCommand == null) _newCommand = new RelayCommand(p => ShowEditWindow(null));
                return _newCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null) _deleteCommand = new RelayCommand(p => DeleteStudent());
                return _deleteCommand;
            }
        }
        #endregion

        #region METHODS
        private void ShowEditWindow(Student student)
        {
            if (EditStudentEvent != null) EditStudentEvent.Invoke(student, EventArgs.Empty);
        }

        private void DeleteStudent()
        {
            if (ActiveStudent != null) ListStudents.Remove(ActiveStudent);
        }
        private void GenerateStudents()
        {
            ListStudents = new ObservableCollection<Student>();

            ListStudents.Add(new Student() { Name = "Ivan", Surname = "Ivanov", Number = 1 });
            ListStudents.Add(new Student() { Name = "Petr", Surname = "Petrov", Number = 2 });
            ListStudents.Add(new Student() { Name = "Sidor", Surname = "Sidorov", Number = 3 });

            ActiveStudent = ListStudents.FirstOrDefault();
        }
        #endregion
    }
}
