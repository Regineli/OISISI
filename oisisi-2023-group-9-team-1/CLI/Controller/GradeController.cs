using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class GradeController
    {
        private readonly OcenaDAO _grades;

        public GradeController()
        {
            _grades = new OcenaDAO();
        }

        public List<Ocena> GetAllGrades()
        {
            return _grades.GetAllAdresa();
        }

        public Ocena AddGrade(Student student, Predmet predmet, int ocena, DateOnly datum)
        {
            return _grades.AddGrade(student, predmet, ocena, datum);
        }

        public Ocena? Obrisi(int gradeId)
        {
            return _grades.DeleteGrade(gradeId);
        }

        public Ocena? Izmeni(Ocena ocena)
        {
            return _grades.UpdateVehicle(ocena);
        }

        public List<Ocena> GetGradesByStudentID(int studentId)
        {
            return _grades.GetGradesByStudentID(studentId);
        }

        public void Subscribe(IObserver observer)
        {
            _grades.Subscribe(observer);
        }
    }
}
