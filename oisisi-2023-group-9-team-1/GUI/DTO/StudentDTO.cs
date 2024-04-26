using CLI;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
	public enum Status {S, B}

	public class StudentDTO : INotifyPropertyChanged
	{
		private int id;
		private string ime;
		private string prezime;
		private string datumRodjenja;
		private Adresa adresaStanovanja;
		private string kontaktTelefon;
		private string email;
		private Indeks index;
		private string godinaStudija;
		private Status statusStudenta;
		private float prosek;
		private List<Predmet> polozeniPredmeti;
		private List<Predmet> nepolozeniPredmeti;

		public int Id
		{
			get { return id; }
			set
			{
				if (value != id)
				{
					id = value;
					OnPropertyChanged("Id");
				}
			}
		}

		public string Ime
		{
			get { return ime; }
			set
			{
				if (value != ime)
				{
					ime = value;
					OnPropertyChanged("Ime");
				}
			}
		}

		public string Prezime
		{
			get { return prezime; }
			set
			{
				if (value != prezime)
				{
					prezime = value;
					OnPropertyChanged("Prezime");
				}
			}
		}

		public string DatumRodjenja
		{
			get { return datumRodjenja; }
			set
			{
				if (value != datumRodjenja)
				{
					datumRodjenja = value;
					OnPropertyChanged("DatumRodjenja");
				}
			}
		}

		public String BrojIndeksaString
		{
			get
			{
				if(Index != null)
				{
					return Index.ToOneLineString();
                }
				return "nan";
			}
		}


        public String AdresaStanovanjaString
		{
			get { 
				if (AdresaStanovanja != null) 
					return AdresaStanovanja.ToOneLineString();
				return "nan";
			}
		}

		public Adresa AdresaStanovanja
		{
			get { return adresaStanovanja; }
			set
			{
				if (value != adresaStanovanja)
				{
					adresaStanovanja = value;
					OnPropertyChanged("AdresaStanovanja");
				}
			}
		}

		public string KontaktTelefon
		{
			get { return kontaktTelefon; }
			set
			{
				if (value != kontaktTelefon)
				{
					kontaktTelefon = value;
					OnPropertyChanged("KontaktTelefon");
				}
			}
		}

		public string Email
		{
			get { return email; }
			set
			{
				if (value != email)
				{
					email = value;
					OnPropertyChanged("Email");
				}
			}
		}

		public Indeks Index
		{
			get { return index; }
			set
			{
				if (value != index)
				{
                    index = value;
					OnPropertyChanged("index");
				}
			}
		}

		public string GodinaStudija
		{
			get { return godinaStudija; }
			set
			{
				if (value != godinaStudija)
				{
					godinaStudija = value;
					OnPropertyChanged("GodinaStudija");
				}
			}
		}



		public Status StatusStudenta
		{
			get { return statusStudenta; }
			set
			{
				if (value != statusStudenta)
				{
					statusStudenta = value;
					OnPropertyChanged("StatusStudenta");
				}
			}
		}



		public float Prosek
		{
			get { return prosek; }
			set
			{
				if (value != prosek)
				{
					prosek = value;
					OnPropertyChanged("Prosek");
				}
			}
		}

		public List<Predmet> PolozeniPredmeti
		{
			get { return polozeniPredmeti; }
			set
			{
				if (value != polozeniPredmeti)
				{
					polozeniPredmeti = value;
					OnPropertyChanged("PolozeniPredmeti");
				}
			}
		}

		public List<Predmet> NepolozeniPredmeti
		{
			get { return nepolozeniPredmeti; }
			set
			{
				if (value != nepolozeniPredmeti)
				{
					nepolozeniPredmeti = value;
					OnPropertyChanged("NepolozeniPredmeti");
				}
			}
		}

        public StudentDTO() {
			AdresaStanovanja = new Adresa();
			Index = new Indeks();
			PolozeniPredmeti = new List<Predmet>();
			NepolozeniPredmeti = new List<Predmet>();
		}


        public StudentDTO(Student student)
		{
			Id = student.ID;
			Ime = student.ime;
			Prezime = student.prezime;
			DatumRodjenja = student.datumRodjenja; 
			KontaktTelefon = student.kontaktTelefon;
			Email = student.eMailAdresa;
			Index = student.index;
			GodinaStudija = student.godinaStudija;
			AdresaStanovanja = student.adresaStanovanja;
			
			StatusStudenta = (Status)student.statusStudenta;

			Prosek = student.prosecnaOcena;
			PolozeniPredmeti = student.polozeniPredmeti;
			NepolozeniPredmeti = student.nepolozeniPredmeti;

		}

        public Student ToStudent()
        {
            return new Student(Ime, Prezime, DatumRodjenja, AdresaStanovanja, KontaktTelefon, Email, index, GodinaStudija, (CLI.Status)StatusStudenta);
        }

        public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				OnPropertyChanged(propertyName);
				return true;
			}
			return false;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
