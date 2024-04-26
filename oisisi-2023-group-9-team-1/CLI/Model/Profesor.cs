using System;
using System.Text;
using CLI.Model;
using CLI.Serialization;
using CLI.Storage;

namespace CLI
{
    public class Profesor : ISerializable
    {
        public string prezime { get; set; }
        public string ime { get; set; }
        public string datumRodjenja { get; set; }
        public string adresaStanovanja { get; set; }
        public string kontaktTelefon { get; set; }
        public string eMailAdresa { get; set; }
        public string brLicneKarte { get; set; }
        public string zvanje { get; set; }
        public string godineStaza { get; set; }
        public List<Predmet> predmeti;
        public int ID { get; set; }  /// ID od profesora, povezano je na sefa katedre i listi profesora od katedre
        public int AdresaID { get; set; }   // ID od adrese

        public int ProfesorPredmetiID { get; set; }

        public Profesor()
        {
            predmeti = new List<Predmet>();
            ID = new Random().Next();
        }

        public int GetID()
        {
            return ID;
        }

        /*public Profesor(string prezime, string ime, DateTime datum, string konta, string email, string licna, string zvanje, string godSt)
        {
            this.prezime = prezime;
            this.ime = ime;
            datumRodjenja = datum;
            kontaktTelefon = konta;
            eMailAdresa = email;
            brLicneKarte = licna;
            this.zvanje = zvanje;
            godineStaza = godSt;
        }*/

        public Profesor(string prezime, string ime, string dat, string konta, string email, string licna, string zvanje, string godSt, int id)
        {
            this.prezime = prezime;
            this.ime = ime;
            kontaktTelefon = konta;
            eMailAdresa = email;
            brLicneKarte = licna;
            this.zvanje = zvanje;
            godineStaza = godSt;
            this.datumRodjenja = dat;
            ID = id;
            predmeti = new List<Predmet>();
        }

        public Profesor(string prezime, string ime, string konta, string email, string licna, string zvanje, string godSt)
        {
            this.prezime = prezime;
            this.ime = ime;
            //datumRodjenja = datum;
            kontaktTelefon = konta;
            eMailAdresa = email;
            brLicneKarte = licna;
            this.zvanje = zvanje;
            godineStaza = godSt;
            ID = new Random().Next();
        }

        public void dodajAdresu(string adr)
        {
            this.adresaStanovanja = adr;
        }

        public void dodajPredmet(Predmet pr)
        {
            predmeti.Add(pr);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            prezime,
            ime,
            datumRodjenja,

            kontaktTelefon,
            eMailAdresa,
            brLicneKarte,
            zvanje,
            godineStaza,
            ID.ToString(),
            AdresaID.ToString(),
            ProfesorPredmetiID.ToString()

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            prezime = values[0];
           // Console.WriteLine("P1");
            ime = values[1];
            //Console.WriteLine("P2");

            datumRodjenja = values[2];

            kontaktTelefon = values[3];
            //Console.WriteLine("P3");
            eMailAdresa = values[4];
            //Console.WriteLine("P4");
            brLicneKarte = values[5];
            //Console.WriteLine("P5");
            zvanje = values[6];
            godineStaza = values[7];
            //Console.WriteLine("P8");
            ID = int.Parse(values[8]);      // zasto ne radi?
            AdresaID = int.Parse(values[9]);
            ProfesorPredmetiID = int.Parse(values[10]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Prezime: {prezime}, \n");
            sb.Append($"Ime: {ime}, \n");
            sb.Append($"DatumRodjenja: {datumRodjenja}, \n");
            sb.Append($"AdresaStanovanja: {adresaStanovanja}\n");
            sb.Append($"KontaktTelefon: {kontaktTelefon}, \n");
            sb.Append($"EMailAdresa: {eMailAdresa}, \n");
            sb.Append($"BrLicneKarte: {brLicneKarte}, \n");
            sb.Append($"Zvanje: {zvanje}\n");
            sb.Append($"GodineStaza: {godineStaza}, \n");

            return sb.ToString();
        }


        public void UnesiDatumRodjenja()
        {
            string datum;

            Console.Write("Dan: ");
            datum = Console.ReadLine();

            Console.Write("Mesec: ");
            datum += "/" + Console.ReadLine();

            Console.Write("Godina: ");
            datum += "/" + Console.ReadLine();

            datumRodjenja = datum;
        }


        public List<Predmet> getPredmeti()
        {
            List<ProfesorPredmet> studentOcene = new List<ProfesorPredmet>();

            Storage<ProfesorPredmet> sOStorage = new Storage<ProfesorPredmet>("profesorPredmet.txt");
            studentOcene = sOStorage.Load();

            foreach (ProfesorPredmet sO in studentOcene)
            {
                if (sO.ID == ProfesorPredmetiID)
                {
                    return sO.getPredmeti();
                }
            }
   
            Predmet ocena1 = new Predmet();
            ocena1.ID = -1;
            List<Predmet> oc = new List<Predmet>();
            oc.Add(ocena1);
            return oc;
        }

        public Adresa getAdresa()
        {
            List<Adresa> studentOcene = new List<Adresa>();

            Storage<Adresa> sOStorage = new Storage<Adresa>("adresa.txt");
            studentOcene = sOStorage.Load();

            foreach (Adresa sO in studentOcene)
            {
                if (sO.ID == AdresaID)
                {
                    return sO;
                }
            }

            Adresa ocena1 = new Adresa();
            return ocena1;
        }

    }
}

