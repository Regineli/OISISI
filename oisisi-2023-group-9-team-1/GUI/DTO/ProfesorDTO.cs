using System;
using CLI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Controller;
using CLI.Model;

namespace GUI.DTO
{
    public class ProfesorDTO : INotifyPropertyChanged
    {
        public int ID { get; set; }

        private string prezime;
        private string ime;
        private string datumRodjenja;
        private int adresaId;
        private string kontakTelefont;
        private string email;
        private string brojLicneKarte;
        private string zvanje;
        private string godineStaza;

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

        public string AdresaString
        {
            get { 
                if(Adresa != null)
                    return Adresa.ToOneLineString();
                return "nan";
            }
        }
       
        public int AdresaId
        {
            get { return adresaId; }
            set
            {
                if (value != adresaId)
                {
                    adresaId = value;
                    OnPropertyChanged("AdresaId");
                }
            }
        }

        private Adresa _adresa;
        public Adresa Adresa
        {
            get { return _adresa; }
            set
            {
                if (value != _adresa)
                {
                    _adresa = value;
                    OnPropertyChanged("Adresa");
                }
            }
        }

        public string Kontakt
        {
            get { return kontakTelefont; }
            set
            {
                if (value != kontakTelefont)
                {
                    kontakTelefont = value;
                    OnPropertyChanged("Kontakt");
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
        
        public string BrojLicneKarte
        {
            get { return brojLicneKarte; }
            set
            {
                if (value != brojLicneKarte)
                {
                    brojLicneKarte = value;
                    OnPropertyChanged("BrojLicneKarte");
                }
            }
        }
        
        public string Zvanje
        {
            get { return zvanje; }
            set
            {
                if (value != zvanje)
                {
                    zvanje = value;
                    OnPropertyChanged("Zvanje");
                }
            }
        }
        
        public string GodineStaza
        {
            get { return godineStaza; }
            set
            {
                if (value != godineStaza)
                {
                    godineStaza = value;
                    OnPropertyChanged("GodineStaza");
                }
            }
        }

        public Profesor ToProfessor()
        {
            Profesor p = new Profesor(Prezime, Ime, DatumRodjenja, Kontakt, Email, BrojLicneKarte, Zvanje, GodineStaza, ID);
            return p;
        }
        public ProfesorDTO() { }

        public ProfesorDTO(Profesor profesor)
        {
            ID = profesor.ID;
            Prezime = profesor.prezime;
            Ime = profesor.ime;
            DatumRodjenja = profesor.datumRodjenja;
            AdresaId = profesor.AdresaID;
            Kontakt = profesor.kontaktTelefon;
            Email = profesor.eMailAdresa;
            BrojLicneKarte = profesor.brLicneKarte;
            Zvanje = profesor.zvanje;
            GodineStaza = profesor.godineStaza;
            Adresa = profesor.adresa;
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
