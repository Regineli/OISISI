using CLI;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
	internal class OcenaDAO : SubjectNotifier
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
			return staraAdresa;
		}

		public Ocena? DeleteGrade(int id)
		{
			Ocena? adr = GetAdresaById(id);
			if (adr == null) return null;

            _grades.Remove(adr);
			_storage.Save(_grades);
			return adr;
		}

		public List<Ocena> GetAllAdresa()
		{
			return _grades;
		}
	}
}


