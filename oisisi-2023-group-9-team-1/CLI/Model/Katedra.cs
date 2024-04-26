using System;
using System.Text;
using System.Xml.Linq;
using CLI.Model;
using CLI.Serialization;
using CLI.Storage;

namespace CLI
{
    public class Katedra : ISerializable
    {
        public string sifra { get; set; }
        public string naziv { get; set; }
		public Profesor shef { get; set; }
        public List<Profesor> profesori { get; set; }
		public int katedraProfesoriID { get; set; }
        public KatedraProfesori katedraProfesori { get; set; }
        public int ID { get; set; }

        public int shefID { get; set; }  // SefID je ID od profesora koji je sef

        public Katedra()
        {
            shefID = -1;
            profesori = new List<Profesor>();
            KatedraProfesori kP = new KatedraProfesori();
            katedraProfesori = kP;
            katedraProfesoriID = kP.GetID();
			ID = new Random().Next();
		}

        public Katedra(string s, string n, Profesor sh)
        {
            sifra = s;
            naziv = n;
            shef = sh;
            shefID = sh.GetID();
            KatedraProfesori kP = new KatedraProfesori();
            katedraProfesori = kP;
            katedraProfesoriID = kP.GetID();
			ID = new Random().Next();
		}

        public Katedra(string s, string n)
        {
			
			sifra = s;
            naziv = n;
            shefID = -1;
            KatedraProfesori kP = new KatedraProfesori();
            katedraProfesori = kP;
            katedraProfesoriID = kP.GetID();    // Pravim poveznicu katedru sa profesorima i storujem ID
            ID = new Random().Next();
		}

        public void dodajShefa(Profesor pr)
        {
            shef = pr;
            shefID = pr.GetID(); 
        }

        public void dodajProfesora(Profesor pr)
        {
            List<KatedraProfesori> katedreP = new List<KatedraProfesori>();

            Storage<KatedraProfesori> katedraStorageP = new Storage<KatedraProfesori>("katedraProfesori.txt");

            foreach (KatedraProfesori kP in katedreP)
            {
                if (kP.GetID() == katedraProfesoriID)
                {
                    katedraProfesori =  kP;
                }
            }

            katedraProfesori.DodajProfesora(pr.GetID());    // ovde pretpostavljamo da je katedraProfesori vec ucitana i stavljena u
            katedraStorageP.Save(katedreP);                                    // katedraProfesori varijablu, svaki put kad otvaramo katedru moramo otvoriti
                                                                               // i KatedraProfesori i smestiti je u katedraProfesori
        }

        public void IzbaciProfesora (Profesor pr)
        {
            List<KatedraProfesori> katedreP = new List<KatedraProfesori>();

            Storage<KatedraProfesori> katedraStorageP = new Storage<KatedraProfesori>("katedraProfesori.txt");

            foreach (KatedraProfesori kP in katedreP)
            {
                if (kP.GetID() == katedraProfesoriID)
                {
                    katedraProfesori = kP;
                }
            }
            katedraProfesori.IzbaciProfesora(pr.GetID());

            katedraStorageP.Save(katedreP);
        }

        // Ovde pronalazimo katedraProfesori koja je vezana za katedru preko KatedraProfesoriID = Katedra.ID
        public KatedraProfesori UzmiKatedruProfesori()
        {
            List<KatedraProfesori> katedreP = new List<KatedraProfesori>();

            Storage<KatedraProfesori> katedraStorageP = new Storage<KatedraProfesori>("katedraProfesori.txt");

            foreach(KatedraProfesori kP in katedreP)
            {
                if(kP.GetID() == katedraProfesoriID)
                {
                    return kP;
                }
            }

            KatedraProfesori kp1 = new KatedraProfesori();

            return kp1;
        }

        public List<Profesor> UzmiProfesore()
        {
            KatedraProfesori kP = UzmiKatedruProfesori();
            return kP.getProfesors();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            sifra,
            naziv,
            shefID.ToString(),
            katedraProfesoriID.ToString(),            
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            sifra = values[0];
            naziv = values[1];
            shefID = int.Parse(values[2]);
            katedraProfesoriID = int.Parse(values[3]);
        }

        public override string ToString()
        {
            string sef1 = "Katedra nema sefa katedre.";
            List<Profesor> profesori = new List<Profesor>();
            Storage<Profesor> profesorStorage = new Storage<Profesor>("profesor.txt");
            profesori = profesorStorage.Load();

            foreach (Profesor p in profesori)
            {
                if (p.ID == shefID)
                {
                    sef1 = "Shef: \n" + p.ToString();
                }
            }   // ovde citamo shefa iz tabele


            StringBuilder sb = new StringBuilder();
            sb.Append($"Sifra: {sifra}, \n");
            sb.Append($"Naziv: {naziv}, \n");
            sb.Append(sef1);
            return sb.ToString(); 
        }

        
    }
}

