using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
	public class KatedraDAO
	{
		private readonly List<Katedra>? _adrese;
		private readonly Storage<Katedra>? _storage;


		public KatedraDAO()
		{
			_storage = new Storage<Katedra>("katedra.txt");
			_adrese = _storage.Load();
		}

		private int GenerateId()
		{
			if (_adrese.Count == 0) return 0;
			return _adrese[^1].ID + 1;
		}

		public Katedra AddAdresa(Katedra adresa)
		{
			adresa.ID = GenerateId();
			_adrese.Add(adresa);
			_storage.Save(_adrese);
			return adresa;
		}

		public Katedra? GetAdresaById(int id)
		{
			return _adrese.Find(v => v.ID == id);
		}

		public void PostaviSefaKatedre(int katedraID, Profesor profesor)
		{
			Katedra k = GetAdresaById(katedraID);
			k.shef = profesor;
			k.shefID = profesor.ID;
			_storage.Save(_adrese);

        }

		public Katedra? UpdateVehicle(Katedra adresa)
		{
			Katedra? staraAdresa = GetAdresaById(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.sifra = adresa.sifra;
			staraAdresa.naziv = adresa.naziv;
			staraAdresa.shef = adresa.shef;
			staraAdresa.profesori = adresa.profesori;
			staraAdresa.ID = adresa.ID;
			staraAdresa.shefID = adresa.shefID;


			_storage.Save(_adrese);
			return staraAdresa;
		}

		public Katedra? RemoveAdresa(int id)
		{
			Katedra? adr = GetAdresaById(id);
			if (adr == null) return null;

			_adrese.Remove(adr);
			_storage.Save(_adrese);
			return adr;
		}

		public List<Katedra> GetAllAdresa()
		{
			return _adrese;
		}
	}
}
