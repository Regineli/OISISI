using CLI.Model;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
	public class IndeksDAO : SubjectNotifier
	{
		private readonly List<Indeks>? _indexes;
		private readonly Storage<Indeks>? _storage;


		public IndeksDAO()
		{
			_storage = new Storage<Indeks>("indeks.txt");
			_indexes = _storage.Load();
		}

		private int GenerateId()
		{
			if (_indexes.Count == 0) return 0;
			return _indexes[^1].ID + 1;
		}

		public Indeks AddIndex(Indeks ind)
		{
			ind.ID = GenerateId();
            _indexes.Add(ind);
			_storage.Save(_indexes);
			return ind;
		}

		public Indeks? GetAdresaById(int id)
		{
			return _indexes.Find(v => v.ID == id);
		}

		public Indeks? UpdateIndex(Indeks adresa)
		{
			Indeks? staraAdresa = GetAdresaById(adresa.ID);
			if (staraAdresa is null) return null;

			staraAdresa.brUpisa = adresa.brUpisa;
			staraAdresa.godinaUpisa = adresa.godinaUpisa;
			staraAdresa.oznakaSmera = adresa.oznakaSmera;
			

			_storage.Save(_indexes);
            NotifyObservers();
            return staraAdresa;
		}

		public Indeks? RemoveIndex(int id)
		{
			Indeks? adr = GetAdresaById(id);
			if (adr == null) return null;

            _indexes.Remove(adr);
			_storage.Save(_indexes);
			return adr;
		}

		public List<Indeks> GetAllIndexes()
		{
			return _indexes;
		}
	}
}
