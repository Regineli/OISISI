using CLI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class KatedraDTO : INotifyPropertyChanged
    {
        public int _id;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }


        private string _sifra;
        public string Sifra
        {

            get
            {
                return _sifra;
            }
            set
            {
                if (value != _sifra)
                {
                    _sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }
        private string _naziv;
        public string Naziv
        {
            get
            {
                return _naziv;
            }
            set
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }



        private int _shefID;
        public int ShefID
        {
            get
            {
                return _shefID;
            }
            set
            {
                if (value != _shefID)
                {
                    _shefID = value;
                    OnPropertyChanged("ShefID");
                }
            }
        }

        public Profesor _shefKatedre;

        public Profesor ShefKatedre
        {
            get
            {
                return _shefKatedre;
            }
            set
            {
                if (value != _shefKatedre)
                {
                    _shefKatedre = value;
                    OnPropertyChanged("ShefKatedre");
                }
            }
        }

        public string _imeShefa;
        public string ImeShefa
        {
            get
            {
                if(ShefKatedre != null)
                {
                    return ShefKatedre.ime + " " + ShefKatedre.prezime;
                }
                return "nan";
            }
        }


        public KatedraDTO() { }


        public KatedraDTO(Katedra katedra)
        {
            ID = katedra.ID;
            this.ShefID = katedra.shefID;
            this.Sifra = katedra.sifra;
            this.Naziv = katedra.naziv;
            this.ShefKatedre = katedra.shef;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
