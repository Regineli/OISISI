using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
	public class PredmetDAO
	{
		private readonly List<Predmet>? _adrese;
		private readonly Storage<Predmet>? _storage;

		public PredmetDAO()
		{
			_storage = new Storage<Predmet>("predmet.txt");
			_adrese = _storage.Load();
		}

		private int GenerateId()
		{
			if (_adrese.Count == 0) return 0;
			return _adrese[^1].ID + 1;
		}

		public Predmet AddAdresa(Predmet adresa)
		{
			adresa.ID = GenerateId();
			_adrese.Add(adresa);
			_storage.Save(_adrese);
			return adresa;
		}

		public Predmet? GetAdresaById(int id)
		{
			return _adrese.Find(v => v.ID == id);
		}

		public Predmet? UpdateVehicle(Predmet adresa)
		{
			Predmet? staraAdresa = GetAdresaById(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.sifra = adresa.sifra;
			staraAdresa.naziv = adresa.naziv;
			staraAdresa.godinaStudijaIzvodjenja = adresa.godinaStudijaIzvodjenja;

			staraAdresa.predmetniProfesor = adresa.predmetniProfesor;
			staraAdresa.studentiPolozili = adresa.studentiPolozili;
			staraAdresa.studentiPali = adresa.studentiPali;
			staraAdresa.brESPB = adresa.brESPB;
			staraAdresa.predmetSemestar = adresa.predmetSemestar;
			staraAdresa.ProfesorID = adresa.ProfesorID;


			_storage.Save(_adrese);
			return staraAdresa;
		}

		public Predmet? RemoveAdresa(int id)
		{
			Predmet? adr = GetAdresaById(id);
			if (adr == null) return null;

			_adrese.Remove(adr);
			_storage.Save(_adrese);
			return adr;
		}

		public List<Predmet> GetAllAdresa()
		{
			return _adrese;
		}

        internal void Subscribe(IObserver observer)
        {
            throw new NotImplementedException();
        }
    }
}
