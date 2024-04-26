using System;
using CLI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
