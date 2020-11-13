using Practice.Command;
using Practice.Interface;
using Practice.Models;
using Practice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Practice.ViewModels
{
    class MainWindowVM : BaseVM
    {
        private string _ContentSearchBox, _ClassSelected;
        public string ContentSearchBox
        {
            get => _ContentSearchBox;
            set
            {
                _ContentSearchBox = value;
                OnPropertyChanged(nameof(ContentSearchBox));
            }
        }
        public string ClassSelected
        {
            get => _ClassSelected;
            set
            {
                _ClassSelected = value;
                OnPropertyChanged(nameof(ClassSelected));
            }
        }
        public IStudentService StudentService { get; set; }
        public ObservableCollection<Student> StudentList { get; set; }
        public ObservableCollection<string> ClassList { get; set; }
        public MainWindowVM()
        {
            ButtonSearch = new RelayCommand(parameter => Search(), parameter => !string.IsNullOrEmpty(ContentSearchBox) || !string.IsNullOrEmpty(ClassSelected));
            ButtonReset = new RelayCommand(parameter => Reset(), parameter => !string.IsNullOrEmpty(ContentSearchBox) || !string.IsNullOrEmpty(ClassSelected));

            NewStudentMenu = new RelayCommand(o => OpenNS());

            // get data
            StudentService = new StudentService();
            StudentList = new ObservableCollection<Student>(StudentService.SearchStudent(new StudentSearchCriteria()));
            ClassList = new ObservableCollection<string>(StudentService.GetAllClasses());
        }

        public void ShowData(string ContentSearch = null, string ClassN = null)
        {
            StudentList.Clear();
            var result = StudentService.SearchStudent(new StudentSearchCriteria { SearchText = ContentSearch, ClassName = ClassN});
            foreach (var item in result)
            {
                StudentList.Add(item);
            }
        }

        #region Command
        // Menu
        public ICommand NewStudentMenu { get; set; }
        public void OpenNS()
        {
            Window window = new NewStudent();
            window.ShowDialog();
        }
        //Reset Button
        public ICommand ButtonReset { get; set; }
        public void Reset()
        {
            ContentSearchBox = null;
            ClassSelected = null;
            ShowData();
          
        }
        //Search Button
        public ICommand ButtonSearch { get; set; }
        public void Search()
        {
            ShowData(ContentSearchBox, ClassSelected);
            ContentSearchBox = null;
            ClassSelected = null;
        }
        #endregion
    }
}
