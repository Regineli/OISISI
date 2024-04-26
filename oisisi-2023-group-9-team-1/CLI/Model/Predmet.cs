using System;
using System.ComponentModel.Design;
using System.Text;
using CLI.Model;
using CLI.Serialization;
using CLI.Storage;

namespace CLI
{
    public class Predmet : ISerializable
    {
        public string sifra { get; set; }
        public string naziv { get; set; }
        public int godinaStudijaIzvodjenja { get; set; }
        public Profesor predmetniProfesor { get; set; }
        public List<Ocena> studentiPolozili { get; set; }
        public List<Student> studentiPali { get; set; }
        public int brESPB { get; set; }
        public enum Semestar { Letnji, Zimski }
        public Semestar predmetSemestar { get; set; }
        public int ID { get; set; }  // ID predmeta, povezan je na oceni
        public int ProfesorID { get; set; } // ID profesora koji drzi predmet

        public int PredmetOcenaID { get; set; }     // ovo je isto sto i studentiPolozili
        public int PredmetStudentiID { get; set; }  // ovo je isto sto i studentiPali

        public Predmet()
        {
            studentiPolozili = new List<Ocena>();
            studentiPali = new List<Student>();
            ID = new Random().Next();
        }

		public Predmet(string sifr, string naz, int god, int esp, Semestar sem, int id)
		{
			sifra = sifr;
			naziv = naz;
			godinaStudijaIzvodjenja = god;
			brESPB = esp;
            predmetSemestar = sem;
            ID = id;
		}

		public Predmet(string sifr, string naz, int god, int esp, string sem)
        {
            sifra = sifr;
            naziv = naz;
            godinaStudijaIzvodjenja = god;
            brESPB = esp;
            if (sem == "L")
            {
                predmetSemestar = Semestar.Letnji;
            }
            else
            {
                predmetSemestar = Semestar.Zimski;
            }
            ID = new Random().Next();
        }

        public void IzmeniSemestar(string sem)
        {
            if(sem == "L")
            {
                predmetSemestar = Semestar.Letnji;
            }
            else
            {
                predmetSemestar = Semestar.Zimski;
            }
        }

        //
        // citanje i pisanje u fajl
        //

        public string[] ToCSV()
        {
            string semestar1;
            if(predmetSemestar == Semestar.Letnji)
            {
                semestar1 = "L";
            }
            else
            {
                semestar1 = "Z";
            }
            string[] csvValues =
            {
            sifra,
            naziv,
            semestar1,
            godinaStudijaIzvodjenja.ToString(),
            brESPB.ToString(),
            ID.ToString(),
            ProfesorID.ToString(),
            PredmetOcenaID.ToString(),
            PredmetStudentiID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            sifra = values[0];
            naziv = values[1];
            string semestar1 = values[2];
            if(semestar1 == "L")
            {
                this.predmetSemestar = Semestar.Letnji;
            }
            else
            {
                this.predmetSemestar = Semestar.Zimski;
            }

            godinaStudijaIzvodjenja = int.Parse(values[3]);
            brESPB = int.Parse(values[4]);
            ID = int.Parse(values[5]);
            ProfesorID = int.Parse(values[6]);
            PredmetOcenaID = int.Parse(values[7]);
            PredmetStudentiID = int.Parse(values[8]);
        }

        //
        //  Kraj citanja i pisanja u fajl
        //

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Sifra: {sifra}, \n");
            sb.Append($"Naziv: {naziv}, \n");
            sb.Append($"GodinaStudijaIzvodjenja: {godinaStudijaIzvodjenja}, \n");
            sb.Append($"PredmetniProfesor: {predmetniProfesor}, \n");
            //sb.Append($"StudentiPolozili: {studentiPolozili}, ");
            //sb.Append($"StudentiPali: {studentiPali}, ");
            sb.Append($"Sifra: {sifra}, \n");
            sb.Append($"BrESPB: {brESPB}, \n");
            string semestar ;
            if (predmetSemestar ==  Semestar.Letnji)
                semestar = "Letnji";
            else semestar = "Zimski";
            sb.Append("Semestar: semestar, \n");






            return sb.ToString();
        }


        public List<Ocena> getStudentiPolozili()
        {
            List<PredmetOcena> studentOcene = new List<PredmetOcena>();

            Storage<PredmetOcena> sOStorage = new Storage<PredmetOcena>("predmetOcena.txt");
            studentOcene = sOStorage.Load();

            foreach (PredmetOcena sO in studentOcene)
            {
                if (sO.ID == PredmetOcenaID)
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

        public List<Student> getStudentiPali()
        {
            List<PredmetStudenti> studentOcene = new List<PredmetStudenti>();

            Storage<PredmetStudenti> sOStorage = new Storage<PredmetStudenti>("predmetStudenti.txt");
            studentOcene = sOStorage.Load();

            foreach (PredmetStudenti sO in studentOcene)
            {
                if (sO.ID == PredmetStudentiID)
                {
                    return sO.getStudenti();
                }
            }

            // ovaj dole deo se nikad nece desiti jer ce uvek postojati StudentOcena za studenta
            // ali da ne bi bacao errore sam dodao
            Student ocena1 = new Student();
            ocena1.ID = -1;
            List<Student> oc = new List<Student>();
            oc.Add(ocena1);
            return oc;
        }
    }
}

