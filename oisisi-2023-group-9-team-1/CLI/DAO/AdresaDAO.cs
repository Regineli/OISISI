using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using CLI.Observer;

namespace CLI.DAO
{
	

	public class AdresaDAO : SubjectNotifier
    {
		private readonly List<Adresa>? _adrese;
		private readonly Storage<Adresa>? _storage;


		public AdresaDAO()
		{
			_storage = new Storage<Adresa>("adresa.txt");
			_adrese = _storage.Load();
		}

		private int GenerateId()
		{
			if (_adrese.Count == 0) return 0;
			return _adrese[^1].ID + 1;
		}

		public Adresa AddAdresa(Adresa adresa)
		{
			adresa.ID = GenerateId();
			_adrese.Add(adresa);
			_storage.Save(_adrese);
            NotifyObservers();
            return adresa;
		}

		public Adresa? GetAdresaById(int id)
		{
			return _adrese.Find(v => v.ID == id);
		}

		public Adresa? UpdateAddress(Adresa adresa)
		{
			Adresa? staraAdresa = GetAdresaById(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.ulica = adresa.ulica;
			staraAdresa.grad = adresa.grad;
			staraAdresa.broj = adresa.broj;
			staraAdresa.drzava = adresa.drzava;

			_storage.Save(_adrese);

            NotifyObservers();
            return staraAdresa;
		}

		public Adresa? RemoveAdresa(int id)
		{
			Adresa? adr = GetAdresaById(id);
			if (adr == null) return null;

			_adrese.Remove(adr);
			_storage.Save(_adrese);
            NotifyObservers();
            return adr;
		}

		public List<Adresa> GetAllAdresa()
		{
			return _adrese;
		}
	}

}
