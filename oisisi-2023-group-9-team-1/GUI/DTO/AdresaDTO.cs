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
	public class AdresaDTO : INotifyPropertyChanged
	{
		public int ID { get; set; }

		public string ulica;
		public int broj;
		public string grad;
		public string drzava;


		public string Ulica
		{
			get { return ulica; }
			set { SetProperty(ref ulica, value, nameof(Ulica)); }
		}

		public int Broj
		{
			get { return broj; }
			set { SetProperty(ref broj, value, nameof(Broj)); }
		}

		public string Grad
		{
			get { return grad; }
			set { SetProperty(ref grad, value, nameof(Grad)); }
		}

		public string Drzava
		{
			get { return drzava; }
			set { SetProperty(ref drzava, value, nameof(Drzava)); }
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

        public Adresa ToAdress()
        {
			return new Adresa(ulica, grad, drzava, broj); 
        }


    }
}
