using System;
using System.Text;
using System.Xml.Linq;
using CLI.DAO;
using CLI.Model;
using CLI.Serialization;
using CLI.Storage;

namespace CLI
{
    public enum Status { B, S }

    public class Student : ISerializable
    {
        public Adresa adresaStanovanja { get; set; }
        public int ID { get; set; }          // ID Studenta
        public string prezime { get; set; }
        public string ime { get; set; }
        public string datumRodjenja { get; set; }
        public string kontaktTelefon { get; set; }
        public string eMailAdresa { get; set; }
        public Indeks index { get; set; }
        public string godinaStudija { get; set; }
        public float prosecnaOcena { get; set; }
        public List<Predmet> polozeniPredmeti { get; set; }
        public List<Predmet> nepolozeniPredmeti { get; set; }
        
        public Status statusStudenta { get; set; }

        // ID Indeksa i Adrese
        public int IndeksID { get; set; }
        public int AdresaID { get; set; }

        // ID klase koja ima ocene koje student ima
        public int StudentOcenaID { get; set; }
        public int StudentPredmetiID { get; set; }

        public AdresaDAO adresaDAO { get; set; }
        public IndeksDAO indexDAO { get; set; }

        public Student()
        {
            //adresaStanovanja = new Adresa();
            polozeniPredmeti = new List<Predmet>();
            nepolozeniPredmeti = new List<Predmet>();
            ID = new Random().Next();

            adresaDAO = new AdresaDAO();
            indexDAO = new IndeksDAO();
        }

        public void PromeniStatus(string status)
        {
            if (status == "B")
            {
                statusStudenta = Status.B;
            }
            else
            {
                statusStudenta = Status.S;
            }
        }

        /*
        public Student(string ime, string prezime, string tel, string email, Indeks brInd, string god, string status)
        {
            ID = new Random().Next();
            
            this.ime = ime;
            this.prezime = prezime;
            //datumRodjenja = datR;
            
            kontaktTelefon = tel;
            eMailAdresa = email;
            index = brInd;
            godinaStudija = god;
            //this.polozeniPredmeti = polozeniPredmeti;
            //this.nepolozeniPredmeti = nepolozeniPredmeti;
            //adresaStanovanja = addr;
            if(status == "B")
            {
                statusStudenta = Status.B;
            }
            else
            {
                statusStudenta = Status.S;
            }

            polozeniPredmeti = new List<Predmet>();
            nepolozeniPredmeti = new List<Predmet>();
        } */

        public Student(string ime, string prezime, string dat, Adresa adr, string tel, string email, Indeks brInd, string god, Status status)
        {
            ID = new Random().Next();
            adresaStanovanja = adr;

            AdresaID = adresaStanovanja.ID;

            this.ime = ime;
            this.prezime = prezime;
            datumRodjenja = dat;
            kontaktTelefon = tel;
            eMailAdresa = email;

            index = brInd;
            IndeksID = index.ID;

            godinaStudija = god;
            statusStudenta = status;

            polozeniPredmeti = new List<Predmet>();
            nepolozeniPredmeti = new List<Predmet>();

            adresaDAO = new AdresaDAO();
            indexDAO = new IndeksDAO();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append($"AdresaStanovanja: {adresaStanovanja}, ");
            //sb.Append($"ID: {ID}, ");
            sb.Append($"Prezime: {prezime}, \n");
            sb.Append($"Ime: {ime}\n");
            //sb.Append($"DatumRodjenja: {datumRodjenja}, ");
            sb.Append($"KontaktTelefon: {kontaktTelefon}, \n");
            sb.Append($"EMailAdresa: {eMailAdresa}, \n");
            sb.Append($"GodinaStudija: {godinaStudija}, \n");
            //sb.Append($"ProsecnaOcena: {prosecnaOcena}, ");
            //sb.Append($"PolozeniPredmeti: {polozeniPredmeti}");
            //sb.Append($"NepolozeniPredmeti: {nepolozeniPredmeti}");
            return sb.ToString();
        }

        /*
        public void studentProsek()
        {
            int sum = 0;
            int n = 0;
            foreach (Predmet oc in polozeniPredmeti)
            {
                sum += oc.ocena;
                n++;
            }
            prosecnaOcena = sum / n;
        }*/

        //
        // Pocetak citanja i pisanja u fajl
        //

        public string[] ToCSV()
        {
            string statusStudija;
            if(statusStudenta == Status.B)
            {
                statusStudija = "B";
            } else
            {
                statusStudija = "S";
            }

            // u fajlu cuvamo ime, prezime, datum rodjenja, telefon, email, broj indexa, godina studija, status, ID, ID indeksa i ID adrese
            // ocenu cemo uvek racunati preko klase OCENA pa nema potrebe da je cuvamo
            // 


            string[] csvValues =
            {
            ime,
            prezime,
            kontaktTelefon,
            eMailAdresa,
            godinaStudija,
            datumRodjenja,
            statusStudija,
            ID.ToString(),
            index.ID.ToString(),
            adresaStanovanja.ID.ToString(),
            StudentOcenaID.ToString(),
            StudentPredmetiID.ToString()
            };
            return csvValues;
        }

        
        public void FromCSV(string[] values)
        {
            ime = values[0];
            prezime = values[1];
            kontaktTelefon = values[2];
            eMailAdresa = values[3];
            godinaStudija = values[4];
            datumRodjenja = values[5];
            string statusStudija = values[6];
            if(statusStudija == "B")
            {
                statusStudenta = Status.B;
            }
            else
            {
                statusStudenta = Status.S;
            }
            ID = int.Parse(values[7]);
            IndeksID = int.Parse(values[8]);
            AdresaID = int.Parse(values[9]);

            this.adresaStanovanja = adresaDAO.GetAdresaById(AdresaID);
            this.index = indexDAO.GetAdresaById(IndeksID);

            StudentOcenaID = int.Parse(values[10]);
            StudentPredmetiID = int.Parse(values[11]);
        }

        public void AddAdress(Adresa a)
        {
            this.adresaStanovanja = a;
            this.AdresaID = a.ID;
        }

        public void AddIndeks(Indeks i)
        {
            this.index = i;
            this.IndeksID = i.ID;
        }

        //
        //  Kraj pisanja i citanja u fajl
        //




        /*
        public List<Ocena> getPolozeniPredmeti()
        {
            List<StudentOcena> studentOcene = new List<StudentOcena>();

            Storage<StudentOcena> sOStorage = new Storage<StudentOcena>("studentOcena.txt");
            studentOcene = sOStorage.Load();

            foreach (StudentOcena sO in studentOcene)
            {
                if (sO.ID == StudentOcenaID)
                {
                    return sO.getOcene();
                }
            }

            // ovaj dole deo se nikad nece desiti jer ce uvek postojati StudentOcena za studenta
            // ali da ne bi bacao errore sam dodao
            Ocena ocena1 = new Ocena();
            ocena1.ID = -1;
            List<Ocena> oc = new List<Ocena>();
            oc.Add(ocena1);
            return oc;
        }

        public List<Predmet> getNepolozeniPredmeti()
        {
            List<StudentPredmeti> studentOcene = new List<StudentPredmeti>();

            Storage<StudentPredmeti> sOStorage = new Storage<StudentPredmeti>("studentPredmeti.txt");
            studentOcene = sOStorage.Load();

            foreach (StudentPredmeti sO in studentOcene)
            {
                if (sO.ID == StudentOcenaID)
                {
                    return sO.getPredmeti();
                }
            }

            // ovaj dole deo se nikad nece desiti jer ce uvek postojati StudentPredmet za studenta
            // ali da ne bi bacao errore sam dodao
            Predmet ocena1 = new Predmet();
            ocena1.ID = -1;
            List<Predmet> oc = new List<Predmet>();
            oc.Add(ocena1);
            return oc;
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

        public Indeks getIndeks()
        {
            List<Indeks> studentOcene = new List<Indeks>();

            Storage<Indeks> sOStorage = new Storage<Indeks>("indeks.txt");
            studentOcene = sOStorage.Load();

            foreach (Indeks sO in studentOcene)
            {
                if (sO.ID == IndeksID)
                {
                    return sO;
                }
            }

            Indeks ocena1 = new Indeks();
            return ocena1;
        } */

    }
}

