using CLI.DAO;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class StudentController
    {
        private readonly StudentDAO _students;

        public StudentController()
        {
            _students = new StudentDAO();
        }

        public List<Student> SearchStudentByThreeWords(string lastName, string firstName, string index)
        {
            return _students.SearchStudentByThreeWords(lastName, firstName, index);
        }

        public List<Student> SearchStudentByLastAndFirstName(string lastName, string firstName)
        {
            return _students.SearchStudentByLastAndFirstName(lastName, firstName);
        }

        public List<Student> SearchStudentByLastName(string lastName)
        {
            return _students.SearchStudentByLastName(lastName);
        }

        public List<Student> GetAllStudents()
        {
            return _students.GetAllStudents();
        }

        public Student AddStudent(Student student)
        {
            return _students.AddStudent(student);
        }

        public Student? DeleteStudent(int studentId)
        {
            return _students.DeleteStudent(studentId);
        }

        public Student? UpdateStudent(Student stud)
        {
            return _students.UpdateStudent(stud);
        }

        public Student? GetStudentByID(int Id)
        {
            return _students.GetStudentByID(Id);
        }

        public void DodajNepolozeni(int studentId, Predmet predmet)
        {
            _students.DodajNepolozenPredmet(studentId, predmet);
        }

        public void ObrisiNepolozenPredmet(int studentId, Predmet predmet)
        {
            _students.ObrisiNepolozenPredmet(studentId, predmet);
        }

        public void DodajPolozeni(int studentId, Predmet predmet)
        {
            _students.DodajPolozenPredmet(studentId, predmet);
        }

        public void ObrisiPolozeni(int studentId, Predmet predmet)
        {
            _students.ObrisiPolozenPredmet(studentId, predmet);
        }

        public void Subscribe(IObserver observer)
        {
            _students.Subscribe(observer);
        }
    }

}
