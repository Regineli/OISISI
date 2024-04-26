using System;
using CLI.Model;
using CLI.Controller;
using CLI.Storage;

namespace CLI.Controller
{
	public class DodajEntitet
	{
        public static void DodajEntitete()
        {
            int meni = -1;
            while (meni != 0)
            {
                Console.WriteLine("\nDodaj: ");
                Console.WriteLine("0. Nazad ");
                Console.WriteLine("1. Dodaj Adresu ");
                Console.WriteLine("2. Dodaj Indeks ");
                Console.WriteLine("3. Dodaj Katedru ");
                Console.WriteLine("4. Dodaj Ocenu ");
                Console.WriteLine("5. Dodaj Predmet ");
                Console.WriteLine("6. Dodaj Profesora ");
                Console.WriteLine("7. Dodaj Studenta ");
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
                        DodajAdresu();
                    }
                    else if (meni == 2)
                    {
                        DodajIndeks();
                    }
                    else if (meni == 3)
                    {
                        DodajKatedru();
                    }
                    else if (meni == 4)
                    {
                        DodajOcenu();
                    }
                    else if (meni == 5)
                    {
                        DodajPredmet();
                    }
                    else if (meni == 6)
                    {
                        DodajProfesora();
                    }
                    else if (meni == 7)
                    {
                        DodajStudenta();
                    }

                }
                catch { Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 7!"); }
            }
        }


        //
        //      Indeks
        //      Profesor
        //      Adresa
        //      Katedra
        //


        public static void DodajIndeks()
        {
            string oznakaSmera;
            int brojUpisa, godinaUpisa;
            Console.WriteLine("\nUnesite Indeks:");
            Console.Write("Broj Upisa: ");
            brojUpisa = int.Parse(Console.ReadLine());
            Console.Write("Godina Upisa: ");
            godinaUpisa = int.Parse(Console.ReadLine());
            Console.Write("Oznaka Smera: ");
            oznakaSmera = Console.ReadLine();

            Indeks i = new Indeks(brojUpisa, godinaUpisa, oznakaSmera);     

            List<Indeks> indeksi = new List<Indeks>();

            Storage<Indeks> indeksStorage = new Storage<Indeks>("indeks.txt");
            indeksi = indeksStorage.Load();
       
            indeksi.Add(i);
            indeksStorage.Save(indeksi);
        }

        public static void DodajProfesora()
        {
            Console.Write("Prezime: ");
            string prezime = Console.ReadLine();
            Console.Write("Ime: ");
            string ime = Console.ReadLine();
            //DateTime datumRodjenja { get; set; }
            //Adresa adresaStanovanja { get; set; }
            Console.Write("Kontakt Telefon: ");
            string kontaktTelefon = Console.ReadLine();
            Console.Write("E-Mail Adresa: ");
            string eMailAdresa = Console.ReadLine();
            Console.Write("Broj Licne Karte: ");
            string brLicneKarte = Console.ReadLine();
            Console.Write("Zvanje: ");
            string zvanje = Console.ReadLine();
            Console.Write("Godine Staza: ");
            string godineStaza = Console.ReadLine();
            //List<Predmet> predmeti;

            
            Profesor p = new Profesor(prezime, ime, kontaktTelefon, eMailAdresa, brLicneKarte, zvanje, godineStaza);
            p.UnesiDatumRodjenja();


            Adresa adresa = IzaberiEntitet.IzaberiAdresu();
            //Console.WriteLine("Profesor radi 1");

            p.AdresaID = adresa.ID;


            //
            //  Biramo predmete koji student slusa a nije polozio, 
            //
            List<ProfesorPredmet> stPredmeti = new List<ProfesorPredmet>();

            Storage<ProfesorPredmet> studentPStorage = new Storage<ProfesorPredmet>("profesorPredmet.txt");

            ProfesorPredmet studentPredmet = new ProfesorPredmet();

            p.ProfesorPredmetiID = studentPredmet.ID;  // povezujemo predmet i predmetStudent


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


            //
            //
            //







            List<Profesor> profesori = new List<Profesor>();

            

            Storage<Profesor> profesorStorage = new Storage<Profesor>("profesor.txt");
            profesori = profesorStorage.Load();
            //Console.WriteLine("Profesor radi 2");
            profesori.Add(p);
            //Console.WriteLine("Profesor radi 3");
            profesorStorage.Save(profesori);
        }

        public static void DodajAdresu()
        {
            string ulica, grad, drzava;
            int broj;
            Console.WriteLine("\nUnesite Adresu:");
            Console.Write("Ulica: ");
            ulica = Console.ReadLine();
            Console.Write("Grad: ");
            grad = Console.ReadLine();
            Console.Write("Drzava: ");
            drzava = Console.ReadLine();
            Console.Write("Broj: ");
            broj = int.Parse(Console.ReadLine());
            Adresa a = new Adresa(ulica, grad, drzava, broj);

            List<Adresa> adrese = new List<Adresa>();           
            
            Storage<Adresa> adresaStorage = new Storage<Adresa>("adresa.txt");

            adrese = adresaStorage.Load();
            adrese.Add(a);
            adresaStorage.Save(adrese);
        }

        public static void DodajKatedru()
        {
            string sifra, naziv;         
            Console.WriteLine("\nUnesite Katedru:");
            Console.Write("Sifra: ");
            sifra = Console.ReadLine();
            Console.Write("Naziv: ");
            naziv = Console.ReadLine();

            Katedra k = new Katedra(sifra, naziv);

            Console.WriteLine("Shef: ");
            Profesor pr = new Profesor();


            Console.WriteLine("==============================");
            Console.WriteLine("\nIzaberite profesora za sefa katedre: \n");

            pr = IzaberiEntitet.IzaberiProfesora();
            
            if(pr.ime != "-1")
            {
                
                k.dodajShefa(pr);
            }


            Console.WriteLine("\n==============================\n");
            do
            {
                Console.WriteLine("==============================");
                Console.WriteLine("\nIzaberite Profesore Katedre: \n");
                pr = IzaberiEntitet.IzaberiProfesora();
                if (pr.ime != "-1")
                {
                    //Console.WriteLine("Pocinjemo da dodajemo profesora");
                    k.dodajProfesora(pr);

                }
                Console.WriteLine("==============================");
            } while (pr.ime != "-1");
            
            // ovde ubacujemo katedru u listu i cuvamo katedru 
            List<Katedra> katedre = new List<Katedra>();

            Storage<Katedra> katedraStorage = new Storage<Katedra>("katedra.txt");

            katedre = katedraStorage.Load();
            katedre.Add(k);
            katedraStorage.Save(katedre);
            
            // Ovde moramo da sacuvamo i katedraProfesori sto se napravilo
            List<KatedraProfesori> katedreP = new List<KatedraProfesori>();

            Storage<KatedraProfesori> katedraStorageP = new Storage<KatedraProfesori>("katedraProfesori.txt");

            katedreP = katedraStorageP.Load();
            katedreP.Add(k.katedraProfesori);
            katedraStorageP.Save(katedreP);
        }


        //  
        //    Student
        //    Predmet
        //    Ocena
        //


        public static void DodajStudenta()
        {
            Console.WriteLine("\nUnesite studenta: ");
            Console.Write("Prezime: ");
            string prezime = Console.ReadLine();
            Console.Write("Ime: ");
            string ime = Console.ReadLine();

            //DateTime datumRodjenja { get; set; }
            //Adresa adresaStanovanja { get; set; }
            Console.Write("Kontakt Telefon: ");
            string kontaktTelefon = Console.ReadLine();
            Console.Write("E-Mail Adresa: ");
            string eMailAdresa = Console.ReadLine();
            Console.Write("Broj Indeksa: ");
            string brojIndeksa = Console.ReadLine();


            Console.Write("Trenutna Godina Studija: ");
            string godinaStudija = Console.ReadLine();

            string status = "";
            while(status != "B" && status != "S")
            {
                Console.Write("Status(B za budzet, S za samofinansiranje): ");
                status = Console.ReadLine();
            }


            //Student st = new Student(ime, prezime, kontaktTelefon, eMailAdresa, brojIndeksa, godinaStudija, status);
            Student st = new Student();


            //
            // biramo indeks za studenta i dodajemo ID indeksa
            //

            Indeks ind = IzaberiEntitet.IzaberiIndeks();

            if (ind.brUpisa == -1)
            {
                st.IndeksID = -1;
            }
            else
            {
                st.IndeksID = ind.GetID();
            }
            

            //
            // biramo adresu studenta i dodajemo ID adrese
            //

            
            Adresa adr = IzaberiEntitet.IzaberiAdresu();
            if (adr.broj == -1)
            {
                st.AdresaID = -1;
            }
            else
            {
                st.AdresaID = adr.GetID();
            }

            //
            //  Biramo predmete koje je student polozio
            //
            List<StudentOcena> SPO = new List<StudentOcena>();

            Storage<StudentOcena> SPOStorage = new Storage<StudentOcena>("studentOcena.txt");

            StudentOcena spoStudenta = new StudentOcena();

            st.StudentOcenaID = spoStudenta.ID;  // povezujemo studenta i StudentPredmetOcena

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

            st.StudentPredmetiID = studentPredmet.ID;  // povezujemo predmet i predmetStudent


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


            List<Student> studenti = new List<Student>();

            Storage<Student> studentStorage = new Storage<Student>("student.txt");

            //Console.WriteLine("Procitao studente");

            studenti = studentStorage.Load();
            //Console.WriteLine("Procitao studente2");
            studenti.Add(st);
            //Console.WriteLine("Procitao studente3");
            studentStorage.Save(studenti);

        }

        //
        //  Dodavanja predmeta, predmet sadrzi ID od Profesora i ID od StudentiPolozili StudentiNPolozili
        //

        public static void DodajPredmet()
        {
            Console.WriteLine("\nUnesite predmet: ");            

            string sifra, naziv, semestar = "";
            int godina, esp;

            // unosimo polja predmeta

            Console.Write("Sifra predmeta: ");
            sifra = Console.ReadLine();
            Console.Write("Naziv predmeta: ");
            naziv = Console.ReadLine();
            while (semestar != "L" && semestar != "Z")
            {
                Console.Write("Semestar predmeta(L za letnji, Z za zimski): ");
                semestar = Console.ReadLine();
            }
            Console.Write("Godina studija izvodjenja predmeta: ");
            godina = int.Parse(Console.ReadLine());
            Console.Write("ESP bodovi predmeta: ");
            esp = int.Parse(Console.ReadLine());

            // kraj unosa polja predmeta

            Predmet predmet = new Predmet(sifra, naziv, godina, esp, semestar);

            //Console.WriteLine("predmet radi 1");




            //
            //  Biramo studente koji su polozili predmet, tj ocene
            //
            List<PredmetOcena> PredmetOcene = new List<PredmetOcena>();

            Storage<PredmetOcena> POStorage = new Storage<PredmetOcena>("predmetOcena.txt");

            PredmetOcena spoStudenta = new PredmetOcena();

            predmet.PredmetOcenaID = spoStudenta.ID;  // povezujemo predmet i predmetocena

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

            predmet.PredmetStudentiID = predmetStudent.ID;  // povezujemo predmet i predmetStudent

     
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


            //
            //
            //




            








            List<Predmet> predmeti = new List<Predmet>();

            Profesor profesor = IzaberiEntitet.IzaberiProfesora();

            predmet.ProfesorID = profesor.ID;       // ako ne izaberemo profesora onda ce ID biti -1

            Storage<Predmet> predmetiStorage = new Storage<Predmet>("predmet.txt");
            predmeti = predmetiStorage.Load();

            predmeti.Add(predmet);
            predmetiStorage.Save(predmeti);
        }

        //
        //  Dodavanje ocene, ocena sadrzi ID od studenta i profesora
        //


        public static void DodajOcenu()
        {
            Console.WriteLine("\nUnesite ocenu: ");

            int ocena = 0;

            while (ocena <= 5 || ocena > 10)
            {
                Console.Write("Unesite ocenu(ocena mora biti broj od 6 do 10): ");
                ocena = int.Parse(Console.ReadLine());
            }

            Ocena oc = new Ocena(ocena);
            // unosimo predmet i studenta
            oc.UnesiDatumPolaganja();


            Predmet pr = IzaberiEntitet.IzaberiPredmet();
            oc.PredmetID = pr.ID;
            Student st = IzaberiEntitet.IzaberiStudenta();
            oc.StudentID = st.ID;

            List<Ocena> ocene = new List<Ocena>();

            Storage<Ocena> oceneStorage = new Storage<Ocena>("ocena.txt");
            ocene = oceneStorage.Load();

            ocene.Add(oc);
            oceneStorage.Save(ocene);
        }


    }
}

