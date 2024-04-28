using CLI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
		public enum Semester
		{
			Letnji,
			Zimski
		}
	public class PredmetDTO : INotifyPropertyChanged
	{
		private int id;
		private string naziv;
		private int brESPB;
		private Semester predSemestar;
		private int godStudijaIzvodjenja;
		private string sifra;


		public PredmetDTO() { }

		public string SifraPredmeta
		{
			get { return sifra; }
			set
			{
				if (value != sifra)
				{
					sifra = value;
					OnPropertyChanged("Sifra");
				}
			}
		}

		public int ID
		{
			get { return id; }
			set
			{
				if (value != id)
				{
					id = value;
					OnPropertyChanged("ID");
				}
			}
		}

		public string Naziv
		{
			get { return naziv; }
			set
			{
				if (value != naziv)
				{
					naziv = value;
					OnPropertyChanged("Naziv");
				}
			}
		}

		public int ESPB
		{
			get { return brESPB; }
			set
			{
				if (value != brESPB)
				{
					brESPB = value;
					OnPropertyChanged("ESPB");
				}
			}
		}

		public string SemestarString
		{
			get { return predSemestar.ToString(); }
		}

		public Semester Semestar
		{
			get { return predSemestar; }
			set
			{
				if (value != predSemestar)
				{
					predSemestar = value;
					OnPropertyChanged("Semestar");
				}
			}
		}

		public int GodStudijaIzvodjenja
		{
			get { return godStudijaIzvodjenja; }
			set
			{
				if (value != godStudijaIzvodjenja)
				{
					godStudijaIzvodjenja = value;
					OnPropertyChanged("GodStudijaIzvodjenja");
				}
			}
		}

		//public PredmetDTO() { }

		public PredmetDTO(Predmet predmet)
		{
			id = predmet.ID;
			naziv = predmet.naziv;
			brESPB = predmet.brESPB;
			predSemestar = (Semester)predmet.predmetSemestar; 
			godStudijaIzvodjenja = predmet.godinaStudijaIzvodjenja;
			sifra = predmet.sifra;
		}

		
		public Predmet ToPredmet()
		{
			return new Predmet(SifraPredmeta, Naziv, GodStudijaIzvodjenja, ESPB, (CLI.Predmet.Semestar)Semestar, ID); 
		}


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
