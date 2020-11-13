using Practice.Interface;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Practice.Service
{
    class StudentService : IStudentService
    {
        private List<Student> _data;
        public StudentService()
        {
            _data = LoadDataFromXml().Students;
        }

        private static Dataset LoadDataFromXml()
        {
            XmlSerializer reader = new XmlSerializer(typeof(Dataset));
            StreamReader file = new StreamReader(@"..\..\..\student_sample_data.xml");

            Dataset data = (Dataset)reader.Deserialize(file);
            file.Close();
            return data;
        }

        public Student Add(Student student)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllClasses()
        {
            return _data.OrderBy(s => s.Class).Select(s => s.Class).Distinct().ToList();
        }

        public void Remove(int studentId)
        {
            var stuRemove = _data.FirstOrDefault(s => s.StudentID == studentId);
            if (stuRemove != null)
            {
                _data.Remove(stuRemove);
            }
        }

        public List<Student> SearchStudent(StudentSearchCriteria criteria)
        {
            return _data.Where(s =>
                (string.IsNullOrEmpty(criteria.SearchText) ||
                s.StudentID.ToString().Contains(criteria.SearchText) ||
                s.FirstName.Contains(criteria.SearchText) ||
                s.LastName.Contains(criteria.SearchText) ||
                s.Address.Contains(criteria.SearchText) ||
                s.Email.Contains(criteria.SearchText))
                &&
                (string.IsNullOrEmpty(criteria.ClassName) ||
                s.Class.Contains(criteria.ClassName)
                )
            ).ToList();
        }

        public Student Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
