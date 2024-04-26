using System;
using CLI.Model;
using CLI.Storage;


namespace CLI.Controller

{
	public class IzaberiEntitet
	{

		// Uzimanje profesora iz liste

		public static Profesor IzaberiProfesora()
		{

			int x = 0;

            List<Profesor> profesori = new List<Profesor>();

            Storage<Profesor> profesorStorage = new Storage<Profesor>("profesor.txt");
            profesori = profesorStorage.Load();

            Console.WriteLine("Izaberite profesora: ");
            foreach (Profesor prof in profesori)
			{
				Console.WriteLine("\nProfesor " + x + ": ");
				Console.WriteLine(prof);
				x++;
			}
            

            Console.Write("Izaberite profesora(unesite -1 ako ne zelite da izaberete profesora): ");
			x = int.Parse(Console.ReadLine());
			
			if(x == -1)
			{
				Profesor prof1 = new Profesor();
				prof1.ime = "-1";
                prof1.ID = -1;
				return prof1;
			}
			return profesori[x];
		}



		// Uzimanje Indeksa iz liste

		public static Indeks IzaberiIndeks()
		{
            int x = 0;

            List<Indeks> indeksi = new List<Indeks>();

            Storage<Indeks> indeksStorage = new Storage<Indeks>("indeks.txt");            
            indeksi = indeksStorage.Load();
            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite indeks: ");
            foreach (Indeks ind in indeksi)
            {
                Console.WriteLine("\nIndeks " + x + ": ");
                Console.WriteLine(ind);
                x++;
            }


            Console.Write("Izaberite indeks(unesite -1 ako ne zelite da izaberete indeks): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");
            

            if (x == -1)
            {
                Indeks ind1 = new Indeks();
                ind1.brUpisa = -1;
                return ind1;
            }
            return indeksi[x];
        }


        // Uzimanje Adrese iz liste

        public static Adresa IzaberiAdresu()
        {
            int x = 0;

            List<Adresa> adrese = new List<Adresa>();

            Storage<Adresa> adresaStorage = new Storage<Adresa>("adresa.txt");
            adrese = adresaStorage.Load();
            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite adresu: ");
            foreach (Adresa adr in adrese)
            {
                Console.WriteLine("\nAdresa " + x + ": ");
                Console.WriteLine(adr);
                x++;
            }


            Console.Write("Izaberite adresu(unesite -1 ako ne zelite da izaberete adresu): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");
            //Console.WriteLine("Uneo je broj adrese: ");

            if (x == -1)
            {
                Adresa adr1 = new Adresa();
                adr1.broj = -1;
                adr1.ID = -1;
                return adr1;
            }
           // Console.WriteLine("Adresa: " + adrese[x]);
           // Console.WriteLine("vraca adresu");
            return adrese[x];
        }

        // Uzimanje iz liste Predmeta

        public static Predmet IzaberiPredmet()
        {
            int x = 0;

            List<Predmet> predmeti = new List<Predmet>();

            Storage<Predmet> predmetStorage = new Storage<Predmet>("predmet.txt");
            predmeti = predmetStorage.Load();
            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite predmet: ");
            foreach (Predmet pred in predmeti)
            {
                Console.WriteLine("\nPredmet " + x + ": ");
                Console.WriteLine(pred);
                x++;
            }


            Console.Write("Izaberite predmet(unesite -1 ako ne zelite da izaberete predmet): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");
            //Console.WriteLine("Uneo je broj adrese: ");

            if (x == -1)
            {
                Predmet pred1 = new Predmet();
                pred1.ID = -1;
                return pred1;
            }
            return predmeti[x];
        }


        // Uzimanje iz liste Studenta

        public static Student IzaberiStudenta()
        {
            int x = 0;

            List<Student> studenti = new List<Student>();

            Storage<Student> studentStorage = new Storage<Student>("student.txt");
            studenti = studentStorage.Load();

            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite studenta: ");
            foreach (Student st in studenti)
            {
                Console.WriteLine("\nStudent " + x + ": ");
                Console.WriteLine(st);
                x++;
            }


            Console.Write("Izaberite studenta(unesite -1 ako ne zelite da izaberete studenta): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");
            //Console.WriteLine("Uneo je broj adrese: ");

            if (x == -1)
            {
                Student stud1 = new Student();
                stud1.ID = -1;
                return stud1;
            }
            return studenti[x];
        }


        // uzimanje iz liste ocena

        public static int IzaberiOcenu()
        {
            int x = 0;

            List<Ocena> ocene = new List<Ocena>();

            Storage<Ocena> oceneStorage = new Storage<Ocena>("ocena.txt");
            ocene = oceneStorage.Load();

            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite ocenu: ");
            foreach (Ocena oc in ocene)
            {
                Console.WriteLine("\nOcena " + x + ": ");
                Console.WriteLine(oc);
                x++;
            }


            Console.Write("Izaberite ocenu(unesite -1 ako ne zelite da izaberete ocenu): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");

            if (x == -1)
            {
                return x;
            }
            return ocene[x].ID;
        }
    }
}

