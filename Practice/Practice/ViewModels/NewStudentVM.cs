using Practice.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Practice.ViewModels
{
    class NewStudentVM : BaseVM, IDataErrorInfo
    {
        private int _StudentID;
        private string _FirstName;
        private string _LastName;
        private DateTime _Birthdate = DateTime.Now;
        private string _Gender;
        private string _Address;
        private string _Email;
        private string _class;

        #region Get/Set
        public int StudentID { get => _StudentID; set => _StudentID = value; }
        public string FirstName { get => _FirstName; set => _FirstName = value; }
        public string LastName { get => _LastName; set => _LastName = value; }
        public DateTime Birthdate { get => _Birthdate; set => _Birthdate = value; }
        public string Gender { get => _Gender; set => _Gender = value; }
        public string Address { get => _Address; set => _Address = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Class { get => _class; set => _class = value; }
        #endregion

        public NewStudentVM()
        {
            ButtonSave = new RelayCommand(o => Save(), o => !(StudentID <= 0)
             && !string.IsNullOrEmpty(FirstName)
             && !string.IsNullOrEmpty(LastName)
             && !(Birthdate > DateTime.Now));
        }

        #region Bao Do
        public string this[string columnName]
        {
            get
            {
                #region Student ID box
                string result = string.Empty;
                if (nameof(StudentID) == columnName)
                {
                    if (StudentID <= 0)
                    {
                        result = "Student ID is mandatory & not Negaive and Different";
                    }
                }
                #endregion

                #region First Name box
                if (nameof(FirstName) == columnName)
                {
                    if (string.IsNullOrEmpty(FirstName))
                    {
                        result = "First Name is mandatory";
                    }
                }
                #endregion

                #region Last Name box
                if (nameof(LastName) == columnName)
                {
                    if (string.IsNullOrEmpty(LastName))
                    {
                        result = "Last Name is mandatory";
                    }
                }
                #endregion

                #region Birthdate box
                if (nameof(Birthdate) == columnName)
                {
                    if (Birthdate > DateTime.Now)
                    {
                        result = "Please select day is less than date now";
                    }
                }
                #endregion

                return result;
            }
        }
        public string Error => throw new NotImplementedException();
        #endregion

        #region Command
        public ICommand ButtonSave { get; set; }
        public void Save()
        {
        }

        public ICommand ButtonCancel { get; set; }
        public void Cancel()
        {

        }
        #endregion

    }
}
