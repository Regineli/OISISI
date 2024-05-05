using CLI;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
	public class OcenaDAO : SubjectNotifier
	{
		private readonly List<Ocena>? _grades;
		private readonly Storage<Ocena>? _storage;


		public OcenaDAO()
		{
			_storage = new Storage<Ocena>("ocena.txt");
            _grades = _storage.Load();
		}

		private int GenerateId()
		{
			if (_grades.Count == 0) return 0;
			return _grades[^1].ID + 1;
		}

		public List<Ocena>? GetPolozeniPredmetiByStudentID(int id)
		{
			return _grades.FindAll(v => v.ocena != 5 &&  v.StudentID == id);
		}

        public List<Ocena>? GetNepolozeniPredmetiByStudentID(int id)
        {
            return _grades.FindAll(v => v.ocena == 5 && v.StudentID == id);
        }

        public void AddGrade(Ocena o)
		{
			o.ID = GenerateId();
			_grades.Add(o);
			_storage.Save(_grades);
            NotifyObservers();
		}

        public List<Ocena>? GetStudentiPoloziliPredmet(int predmetID)
        {
            return _grades.FindAll(v => v.ocena != 5 && v.PredmetID == predmetID);
        }

        public bool StudentNijePolozioPredmet(int studentID, int predmetID)
        {
            return _grades.Find(v => v.PredmetID == predmetID && v.StudentID == studentID).ocena ==5;
        }

        public bool StudentSlusaPredmet(int studentID, int predmetID)
		{
            return _grades.Find(v => v.PredmetID == predmetID && v.StudentID==studentID) != null;
        }

        public List<Ocena> GetOceneByPredmetID(int pID)
        {
            return _grades.FindAll(v => v.PredmetID == pID);
        }

        public List<Ocena> GetOcenePredmeta(Predmet p)
		{
			return _grades.FindAll(v=>v.PredmetID == p.ID);
		}

        public Ocena AddGrade(Student student, Predmet predmet, int ocena, DateOnly datum)
		{
            Ocena newGrade = new Ocena();
            newGrade.ID = GenerateId();
            newGrade.datumPolaganja = datum;
            newGrade.ocena = ocena;
            newGrade.studentPolozio = student;
            newGrade.predmet = predmet;
            _grades.Add(newGrade);
            _storage.Save(_grades);
            NotifyObservers();
            return newGrade;
        }

		public void PonistiPredmet(int ocenaID)
		{
			Ocena o = _grades.Find(v => v.ID == ocenaID);
			o.ocena = 5;
			_storage.Save(_grades);
            NotifyObservers();
        }

		public Ocena? GetAdresaById(int id)
		{
			return _grades.Find(v => v.ID == id);
		}

        public List<Ocena>? GetGradesByStudentID(int id)
        {
            return _grades.FindAll(v => v.StudentID == id);
        }

        public Ocena? UpdateVehicle(Ocena adresa)
		{
			Ocena? staraAdresa = GetAdresaById(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.studentPolozio = adresa.studentPolozio;
			staraAdresa.predmet = adresa.predmet;
			staraAdresa.StudentID = adresa.StudentID;
			staraAdresa.PredmetID = adresa.PredmetID;
			staraAdresa.datumPolaganja = adresa.datumPolaganja;
			staraAdresa.ocena = adresa.ocena;		

			_storage.Save(_grades);
            NotifyObservers();
            return staraAdresa;
		}

		public Ocena? DeleteGrade(int id)
		{
			Ocena? adr = GetAdresaById(id);
			if (adr == null) return null;

            _grades.Remove(adr);
			_storage.Save(_grades);
            NotifyObservers();
            return adr;
		}

		public List<Ocena> GetAllAdresa()
		{
			return _grades;
		}
	}
}


