using System;
using CLI.Controller;
using CLI.Model;
using CLI.Storage;
using static CLI.Predmet;

namespace CLI.Controller
{
	public class IzmenaEntiteta
	{
		public static void IzmeniEntitet()
		{
            int meni = -1;
            while (meni != 0)
            {
                Console.WriteLine("\nIzmeni: ");
                Console.WriteLine("0. Nazad ");
                Console.WriteLine("1. Izmeni Adresu ");
                Console.WriteLine("2. Izmeni Indeks ");
                Console.WriteLine("3. Izmeni Katedru ");
                Console.WriteLine("4. Izmeni Ocenu ");
                Console.WriteLine("5. Izmeni Predmet ");
                Console.WriteLine("6. Izmeni Profesora ");
                Console.WriteLine("7. Izmeni Studenta ");
                Console.Write("Izaberite operaciju: ");
                try
                {
                    meni = int.Parse(Console.ReadLine());
                    if (meni < 0 || meni > 7)
                    {
                        Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 7!");
                    }
                    else if (meni == 1)
                    {
                        IzmeniAdresu();
                    }
                    else if (meni == 2)
                    {
                        IzmeniIndeks();
                    }
                    else if (meni == 3)
                    {
                        IzmeniKatedru();
                    }
                    else if (meni == 4)
                    {
                        IzmeniOcenu();
                    }
                    else if (meni == 5)
                    {
                        IzmeniPredmet();
                    }
                    else if (meni == 6)
                    {                        
                        IzmeniProfesora();
                    }
                    else
                    {
                        IzmeniStudenta();
                    }

                }
                catch { Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 7!"); }
            }
        }


        public static void IzmeniPredmet ()
        {
            int x = 0;

            List<Predmet> adrese = new List<Predmet>();
            Storage<Predmet> adresaStorage = new Storage<Predmet>("predmet.txt");
            adrese = adresaStorage.Load();

            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite predmet: ");
            foreach (Predmet ad in adrese)
            {
                Console.WriteLine("\nPredmet" + x + ":");
                Console.WriteLine(ad);               
                x++;
            }
            Console.Write("Izaberite predmet(unesite -1 ako ne zelite da izaberete predmet): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");


            if(x != -1)
            {
                string semestar = "";
                Console.Write("Sifra predmeta: ");
                adrese[x].sifra = Console.ReadLine();
                Console.Write("Naziv predmeta: ");
                adrese[x].naziv = Console.ReadLine();
                while (semestar != "L" && semestar != "Z")
                {
                    Console.Write("Semestar predmeta(L za letnji, Z za zimski): ");
                    semestar = Console.ReadLine();
                }
                Console.Write("Godina studija izvodjenja predmeta: ");
                adrese[x].godinaStudijaIzvodjenja = int.Parse(Console.ReadLine());
                Console.Write("ESP bodovi predmeta: ");
                adrese[x].brESPB = int.Parse(Console.ReadLine());
                adrese[x].IzmeniSemestar(semestar);

                //
                //  Biramo studente koji su polozili predmet, tj ocene
                //
                List<PredmetOcena> PredmetOcene = new List<PredmetOcena>();

                Storage<PredmetOcena> POStorage = new Storage<PredmetOcena>("predmetOcena.txt");

                PredmetOcena spoStudenta = new PredmetOcena();

                adrese[x].PredmetOcenaID = spoStudenta.ID;  // povezujemo predmet i predmetocena

                //Console.WriteLine("student radi ocena 1");
                int ocenaID = 0;
                do
                {
                    //Console.WriteLine("==============================");
                    Console.WriteLine("\nIzaberite studenta koji je polozio predmet: \n");
                    ocenaID = IzaberiEntitet.IzaberiOcenu();
                    if (ocenaID != -1)
                    {
                        spoStudenta.DodajOcenu(ocenaID);
                        //Console.WriteLine("student radi ocena 2");

                    }
                    //Console.WriteLine("==============================");
                } while (ocenaID != -1);

                PredmetOcene.Add(spoStudenta);   // dodajemo novog clana u tabelu predmetOcena
                POStorage.Save(PredmetOcene);   // sacuvali smo updated tabelu Predmet Ocena


                //
                //
                //



                //
                //  Biramo studente koji nisu polozili predmet
                //
                List<PredmetStudenti> predmetStudenti = new List<PredmetStudenti>();

                Storage<PredmetStudenti> PSStorage = new Storage<PredmetStudenti>("predmetStudenti.txt");

                PredmetStudenti predmetStudent = new PredmetStudenti();

                adrese[x].PredmetStudentiID = predmetStudent.ID;  // povezujemo predmet i predmetStudent


                int studentID = 0;
                do
                {
                    //Console.WriteLine("==============================");
                    Console.WriteLine("\nIzaberite studenta koji nije polozio predmet: \n");
                    studentID = IzaberiEntitet.IzaberiStudenta().ID;
                    if (ocenaID != -1)
                    {
                        predmetStudent.DodajStudenta(studentID);


                    }
                    //Console.WriteLine("==============================");
                } while (studentID != -1);

                predmetStudenti.Add(predmetStudent);   // dodajemo novog clana u tabelu predmetStudenti
                PSStorage.Save(predmetStudenti);   // sacuvali smo updated tabelu Predmet Ocena

                adresaStorage.Save(adrese);
                //
                //
                //
            }
        }


        public static void IzmeniProfesora()
        {
            int x = 0;

            List<Profesor> adrese = new List<Profesor>();
            Storage<Profesor> adresaStorage = new Storage<Profesor>("profesor.txt");
            adrese = adresaStorage.Load();


            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite ocenu: ");
            foreach (Profesor ad in adrese)
            {
                Console.WriteLine("\nProfesor" + x + ":");
                Console.WriteLine(ad);
                x++;
            }
            Console.Write("Izaberite profesora(unesite -1 ako ne zelite da izaberete profesora): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");

            if(x != -1)
            {
                Console.Write("Prezime: ");
                adrese[x].prezime = Console.ReadLine();
                Console.Write("Ime: ");
                adrese[x].ime = Console.ReadLine();
                //DateTime datumRodjenja { get; set; }
                //Adresa adresaStanovanja { get; set; }
                Console.Write("Kontakt Telefon: ");
                adrese[x].kontaktTelefon = Console.ReadLine();
                Console.Write("E-Mail Adresa: ");
                adrese[x].eMailAdresa = Console.ReadLine();
                Console.Write("Broj Licne Karte: ");
                adrese[x].brLicneKarte = Console.ReadLine();
                Console.Write("Zvanje: ");
                adrese[x].zvanje = Console.ReadLine();
                Console.Write("Godine Staza: ");
                adrese[x].godineStaza = Console.ReadLine();

                adrese[x].UnesiDatumRodjenja();


                Adresa adresa = IzaberiEntitet.IzaberiAdresu();
                //Console.WriteLine("Profesor radi 1");

                adrese[x].AdresaID = adresa.ID;

                List<ProfesorPredmet> stPredmeti = new List<ProfesorPredmet>();

                Storage<ProfesorPredmet> studentPStorage = new Storage<ProfesorPredmet>("profesorPredmet.txt");

                ProfesorPredmet studentPredmet = new ProfesorPredmet();

                adrese[x].ProfesorPredmetiID = studentPredmet.ID;  // povezujemo predmet i predmetStudent


                int predmetID = 0;
                do
                {
                    //Console.WriteLine("==============================");
                    Console.WriteLine("\nIzaberite predmet koji profesor drzi: \n");
                    predmetID = IzaberiEntitet.IzaberiPredmet().ID;
                    if (predmetID != -1)
                    {
                        studentPredmet.DodajPredmet(predmetID);


                    }
                    //Console.WriteLine("==============================");
                } while (predmetID != -1);

                stPredmeti.Add(studentPredmet);   // dodajemo novog clana u tabelu predmetStudenti
                studentPStorage.Save(stPredmeti);   // sacuvali smo updated tabelu Predmet Ocena
                adresaStorage.Save(adrese);
            }


        }




        public static void IzmeniOcenu()
        {

            int x = 0;

            List<Ocena> adrese = new List<Ocena>();
            Storage<Ocena> adresaStorage = new Storage<Ocena>("ocena.txt");
            adrese = adresaStorage.Load();
            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite ocenu: ");
            foreach (Ocena ad in adrese)
            {
                Console.WriteLine("\nOcena" + x + ":");
                Console.WriteLine(ad);

                Console.WriteLine("Predmet: ");
                Console.WriteLine(ad.GetPredmet());

                Console.WriteLine("Student: ");
                Console.WriteLine(ad.GetStudent());
                x++;
            }
            Console.Write("Izaberite ocenu(unesite -1 ako ne zelite da izaberete ocenu): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");

            if (x != -1)
            {
                int ocena = 0;

                while (ocena <= 5 || ocena > 10)
                {
                    Console.Write("Unesite ocenu(ocena mora biti broj od 6 do 10): ");
                    ocena = int.Parse(Console.ReadLine());
                }


                // unosimo predmet i studenta
                adrese[x].UnesiDatumPolaganja();


                Predmet pr = IzaberiEntitet.IzaberiPredmet();
                adrese[x].PredmetID = pr.ID;
                Student st = IzaberiEntitet.IzaberiStudenta();
                adrese[x].StudentID = st.ID;

  
                adresaStorage.Save(adrese);
            }
        }


        public static void IzmeniStudenta()
        {
            int x = 0;

            List<Student> adrese = new List<Student>();
            Storage<Student> adresaStorage = new Storage<Student>("student.txt");
            adrese = adresaStorage.Load();

            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite studenta: ");
            foreach (Student ad in adrese)
            {
                Console.WriteLine("\nStudent" + x + ":");
                Console.WriteLine(ad);
                x++;
            }
            Console.Write("Izaberite studenta(unesite -1 ako ne zelite da izaberete studenta): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");

            if(x != -1)
            {
                Console.Write("Prezime: ");
                adrese[x].prezime = Console.ReadLine();
                Console.Write("Ime: ");
                adrese[x].ime = Console.ReadLine();

                //DateTime datumRodjenja { get; set; }
                //Adresa adresaStanovanja { get; set; }
                Console.Write("Kontakt Telefon: ");
                adrese[x].kontaktTelefon = Console.ReadLine();
                Console.Write("E-Mail Adresa: ");
                adrese[x].eMailAdresa = Console.ReadLine();



                Console.Write("Trenutna Godina Studija: ");
                adrese[x].godinaStudija = Console.ReadLine();

                string status = "";
                while (status != "B" && status != "S")
                {
                    Console.Write("Status(B za budzet, S za samofinansiranje): ");
                    status = Console.ReadLine();
                }
                adrese[x].PromeniStatus(status);



                Indeks ind = IzaberiEntitet.IzaberiIndeks();

                if (ind.brUpisa == -1)
                {
                    adrese[x].IndeksID = -1;
                }
                else
                {
                    adrese[x].IndeksID = ind.GetID();
                }


                //
                // biramo adresu studenta i dodajemo ID adrese
                //


                Adresa adr = IzaberiEntitet.IzaberiAdresu();
                if (adr.broj == -1)
                {
                    adrese[x].AdresaID = -1;
                }
                else
                {
                    adrese[x].AdresaID = adr.GetID();
                }

                //
                //  Biramo predmete koje je student polozio
                //
                List<StudentOcena> SPO = new List<StudentOcena>();

                Storage<StudentOcena> SPOStorage = new Storage<StudentOcena>("studentOcena.txt");

                StudentOcena spoStudenta = new StudentOcena();

                adrese[x].StudentOcenaID = spoStudenta.ID;  // povezujemo studenta i StudentPredmetOcena

                //Console.WriteLine("student radi ocena 1");
                int ocenaID = 0;
                do
                {
                    //Console.WriteLine("==============================");
                    Console.WriteLine("\nIzaberite predmete koje je student polozio: \n");
                    ocenaID = IzaberiEntitet.IzaberiOcenu();
                    if (ocenaID != -1)
                    {
                        spoStudenta.DodajOcenu(ocenaID);
                        //Console.WriteLine("student radi ocena 2");

                    }
                    //Console.WriteLine("==============================");
                } while (ocenaID != -1);

                SPO.Add(spoStudenta);   // dodajemo novog clana u tabelu StudentPredmetOcena
                SPOStorage.Save(SPO);   // sacuvali smo updated tabelu Student Predmet Ocena


                //
                //
                //


                //
                //  Biramo predmete koji student slusa a nije polozio, 
                //
                List<StudentPredmeti> stPredmeti = new List<StudentPredmeti>();

                Storage<StudentPredmeti> studentPStorage = new Storage<StudentPredmeti>("studentPredmeti.txt");

                StudentPredmeti studentPredmet = new StudentPredmeti();

                adrese[x].StudentPredmetiID = studentPredmet.ID;  // povezujemo predmet i predmetStudent


                int predmetID = 0;
                do
                {
                    //Console.WriteLine("==============================");
                    Console.WriteLine("\nIzaberite predmet koji student nije polozio: \n");
                    predmetID = IzaberiEntitet.IzaberiPredmet().ID;
                    if (ocenaID != -1)
                    {
                        studentPredmet.DodajPredmet(predmetID);


                    }
                    //Console.WriteLine("==============================");
                } while (predmetID != -1);

                stPredmeti.Add(studentPredmet);   // dodajemo novog clana u tabelu predmetStudenti
                studentPStorage.Save(stPredmeti);   // sacuvali smo updated tabelu Predmet Ocena


                //
                //
                //

                adresaStorage.Save(adrese);
            }
        }




        public static void IzmeniAdresu()
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
           

            if(x != -1)
            {
                Console.WriteLine("\nUnesite nove vrednosti adrese:");
                int meni = 0;
                while (meni != -1)
                {
                    Console.WriteLine("0. Izmeni ulicu: ");
                    Console.WriteLine("1. Izmeni broj ");
                    Console.WriteLine("2. Izmeni grad ");
                    Console.WriteLine("3. Izmeni drzavu ");
                    meni = int.Parse(Console.ReadLine());

                    if(meni == 0)
                    {
                        Console.Write("Ulica: ");
                        adrese[x].ulica = Console.ReadLine();
                    }
                    else if (meni == 1)
                    {
                        Console.Write("Broj: ");
                        adrese[x].broj = int.Parse(Console.ReadLine());
                        
                    }
                    else if (meni == 2)
                    {
                        Console.Write("Grad: ");
                        adrese[x].grad = Console.ReadLine();
                        
                    }
                    else if (meni == 3)
                    {
                        Console.Write("Drzava: ");
                        adrese[x].drzava = Console.ReadLine();
                    }
                }

                //Console.WriteLine("Izmena Adrese radi 4");
                adresaStorage.Save(adrese);

               // Console.WriteLine("Izmena Adrese radi 5");
            }
        }

        public static void IzmeniIndeks()
        {
            int x = 0;

            List<Indeks> adrese = new List<Indeks>();

            Storage<Indeks> adresaStorage = new Storage<Indeks>("indeks.txt");
            adrese = adresaStorage.Load();
            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite indeks: ");
            foreach (Indeks adr in adrese)
            {
                Console.WriteLine("\nIndeks " + x + ": ");
                Console.WriteLine(adr);
                x++;
            }


            Console.Write("Izaberite indeks(unesite -1 ako ne zelite da izaberete indeks): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");


            if(x != -1)
            {                
                Console.WriteLine("\nUnesite Indeks:");
                Console.Write("Broj Upisa: ");
                adrese[x].brUpisa = int.Parse(Console.ReadLine());
                Console.Write("Godina Upisa: ");
                adrese[x].godinaUpisa = int.Parse(Console.ReadLine());
                Console.Write("Oznaka Smera: ");
                adrese[x].oznakaSmera = Console.ReadLine();
                adresaStorage.Save(adrese);
            }
        }


        public static void IzmeniKatedru()
        {
            int x = 0;

            List<Katedra> adrese = new List<Katedra>();
            Storage<Katedra> adresaStorage = new Storage<Katedra>("katedra.txt");
            adrese = adresaStorage.Load();
            Console.WriteLine("\n==============================\n");
            Console.WriteLine("Izaberite katedru: ");
            foreach (Katedra ad in adrese)
            {
                Console.WriteLine("\nKatedra" + x + ":");
                Console.WriteLine(ad);

               /* List<Profesor> ocene = ad.UzmiProfesore();

                Console.WriteLine("Profesori na katedru: ");
                foreach (Profesor ocena in ocene)
                {
                    Console.WriteLine(ocena);
                }*/
                x++;
            }
            Console.Write("Izaberite indeks(unesite -1 ako ne zelite da izaberete indeks): ");
            x = int.Parse(Console.ReadLine());
            Console.WriteLine("\n==============================\n");

            if (x != -1)
            {



                Console.Write("Sifra: ");
                adrese[x].sifra = Console.ReadLine();
                Console.Write("Naziv: ");
                adrese[x].naziv = Console.ReadLine();

                

                Console.WriteLine("Shef: ");
                Profesor pr = new Profesor();


                Console.WriteLine("==============================");
                Console.WriteLine("\nIzaberite profesora za sefa katedre: \n");

                pr = IzaberiEntitet.IzaberiProfesora();

                if (pr.ime != "-1")
                {

                    adrese[x].dodajShefa(pr);
                }
                List<Profesor> ocene = adrese[x].UzmiProfesore();
                Console.WriteLine("Profesori na katedri: ");
                foreach (Profesor ocena in ocene)
                {
                    Console.WriteLine(ocena);
                }
                x++;
                Console.WriteLine("\n==============================\n");
                do
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine("\nDodajte Profesora na katedru: \n");
                    pr = IzaberiEntitet.IzaberiProfesora();
                    if (pr.ime != "-1")
                    {
                        //Console.WriteLine("Pocinjemo da dodajemo profesora");
                        adrese[x].dodajProfesora(pr);

                    }
                    Console.WriteLine("==============================");
                } while (pr.ime != "-1");

                do
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine("\nSklonite Profesora sa katedre: \n");
                    pr = IzaberiEntitet.IzaberiProfesora();
                    if (pr.ime != "-1")
                    {
                        //Console.WriteLine("Pocinjemo da dodajemo profesora");
                        adrese[x].IzbaciProfesora(pr);
                    }
                    Console.WriteLine("==============================");
                } while (pr.ime != "-1");
            }

        }



    }
}

