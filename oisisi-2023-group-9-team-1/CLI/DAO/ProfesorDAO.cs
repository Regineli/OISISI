using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
	public class ProfesorDAO
	{
		private readonly List<Profesor>? _adrese;
		private readonly Storage<Profesor>? _storage;


		public ProfesorDAO()
		{
			_storage = new Storage<Profesor>("profesor.txt");
			_adrese = _storage.Load();
		}

		private int GenerateId()
		{
			if (_adrese.Count == 0) return 0;
			return _adrese[^1].ID + 1;
		}

		public Profesor AddAdresa(Profesor adresa)
		{
			adresa.ID = GenerateId();
			_adrese.Add(adresa);
			_storage.Save(_adrese);
			return adresa;
		}

		public Profesor? GetAdresaById(int id)
		{
			return _adrese.Find(v => v.ID == id);
		}

		public Profesor? UpdateVehicle(Profesor adresa)
		{
			Profesor? staraAdresa = GetAdresaById(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.adresaStanovanja = adresa.adresaStanovanja;
			staraAdresa.prezime = adresa.prezime;
			staraAdresa.ime = adresa.ime;
			staraAdresa.datumRodjenja = adresa.datumRodjenja;
			staraAdresa.kontaktTelefon = adresa.kontaktTelefon;
			staraAdresa.eMailAdresa = adresa.eMailAdresa;
			staraAdresa.brLicneKarte = adresa.brLicneKarte;

			staraAdresa.zvanje = adresa.zvanje;
			staraAdresa.godineStaza = adresa.godineStaza;
			staraAdresa.predmeti = adresa.predmeti;
			staraAdresa.AdresaID = adresa.AdresaID;
			staraAdresa.ProfesorPredmetiID = adresa.ProfesorPredmetiID;
			staraAdresa.adresa = adresa.adresa;

		_storage.Save(_adrese);
			return staraAdresa;
		}

		public Profesor? RemoveAdresa(int id)
		{
			Profesor? adr = GetAdresaById(id);
			if (adr == null) return null;

			_adrese.Remove(adr);
			_storage.Save(_adrese);
			return adr;
		}

		public List<Profesor> GetAllAdresa()
		{
			return _adrese;
		}
	}
}
