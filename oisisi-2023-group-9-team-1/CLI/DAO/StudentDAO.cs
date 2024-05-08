using CLI.Model;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
	public class StudentDAO : SubjectNotifier
    {
		private readonly List<Student>? _students;
		private readonly Storage<Student>? _storage;


		public StudentDAO()
		{
			_storage = new Storage<Student>("student.txt");
            _students = _storage.Load();
		}

		private int GenerateId()
		{
			if (_students.Count == 0) return 0;
			return _students[^1].ID + 1;
		}

        public List<Student> SearchStudentByThreeWords(string lastName, string firstName, string index)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return GetAllStudents();
            }

            return _students.FindAll(v => v.prezime.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0 
                                    && v.ime.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0
                                    && v.index.ToOneLineString().IndexOf(index, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public List<Student> SearchStudentByLastAndFirstName(string lastName, string firstName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return GetAllStudents();
            }

            return _students.FindAll(v => v.prezime.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0 && v.ime.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public List<Student> SearchStudentByLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return GetAllStudents();
            }

            return _students.FindAll(v => v.prezime.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public Student AddStudent(Student st)
		{
			st.ID = GenerateId();
            _students.Add(st);
			_storage.Save(_students);
            NotifyObservers();
            return st;
		}

		public Student? GetStudentByID(int id)
		{
			return _students.Find(v => v.ID == id);
		}

		public Student? UpdateStudent(Student adresa)
		{
			Student? staraAdresa = GetStudentByID(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.adresaStanovanja = adresa.adresaStanovanja;
			staraAdresa.prezime = adresa.prezime;
			staraAdresa.ime = adresa.ime;
			staraAdresa.datumRodjenja = adresa.datumRodjenja;
			staraAdresa.kontaktTelefon = adresa.kontaktTelefon;
			staraAdresa.eMailAdresa = adresa.eMailAdresa;
			staraAdresa.godinaStudija = adresa.godinaStudija;
			staraAdresa.prosecnaOcena = adresa.prosecnaOcena;
			staraAdresa.polozeniPredmeti = adresa.polozeniPredmeti;
			staraAdresa.nepolozeniPredmeti = adresa.nepolozeniPredmeti;
			staraAdresa.statusStudenta = adresa.statusStudenta;
			staraAdresa.IndeksID = adresa.IndeksID;
			staraAdresa.AdresaID = adresa.AdresaID;
			staraAdresa.StudentOcenaID = adresa.StudentOcenaID;
			staraAdresa.StudentPredmetiID = adresa.StudentPredmetiID;
			staraAdresa.index = adresa.index;

			_storage.Save(_students);

            NotifyObservers();
            return staraAdresa;
		}

		public Student? DeleteStudent(int id)
		{
			Student? st = GetStudentByID(id);
			if (st == null) return null;

            _students.Remove(st);
			_storage.Save(_students);
            NotifyObservers();
            return st;
		}

		public List<Student> GetAllStudents()
		{
			return _students;
		}

        public void DodajNepolozenPredmet(int id, Predmet predmet)
        {
            Student? student = GetStudentByID(id);
            if (student is null) return;

            student.nepolozeniPredmeti.Add(predmet);
            _storage.Save(_students);
            NotifyObservers();
        }

        public void ObrisiNepolozenPredmet(int id, Predmet predmet)
        {
            Student? student = GetStudentByID(id);
            if (student is null) return;

            student.nepolozeniPredmeti.Remove(predmet);
            _storage.Save(_students);
            NotifyObservers();
        }

        public void DodajPolozenPredmet(int id, Predmet predmet)
        {
            Student? student = GetStudentByID(id);
            if (student is null) return;

            student.polozeniPredmeti.Add(predmet);
            _storage.Save(_students);
            NotifyObservers();
        }

        public void ObrisiPolozenPredmet(int id, Predmet predmet)
        {
            Student? student = GetStudentByID(id);
            if (student is null) return;

            student.polozeniPredmeti.Remove(predmet);
            _storage.Save(_students);
            NotifyObservers();
        }

    }
}
