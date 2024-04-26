using System;
using CLI.DAO;
using CLI.Model;
using CLI.Serialization;
using CLI.Storage;

namespace CLI.Controller
{
	public class IspisiEntitetV2
	{

		public static void IspisiEntitetV2p(AdresaDAO _adrDAO)
		{
			List<Adresa> adrese = _adrDAO.GetAllAdresa();

			int meni = -1;
            while (meni != 0)
            {
                Console.WriteLine("\nIspisi: ");
                Console.WriteLine("0. Nazad ");
                Console.WriteLine("1. Ispisi Adresu ");
                Console.WriteLine("2. Ispisi Indeks ");
                Console.WriteLine("3. Ispisi Katedru ");
                Console.WriteLine("4. Ispisi Ocenu ");
                Console.WriteLine("5. Ispisi Predmet ");
                Console.WriteLine("6. Ispisi Profesora ");
                Console.WriteLine("7. Ispisi Studenta ");
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
                        IspisiAdresu(adrese);
                    }
                    else if (meni == 2)
                    {
                        IspisiIndeks();
                    }
                    else if (meni == 3)
                    {
                        IspisiKatedru();
                    }
                    else if (meni == 4)
                    {
                        IspisiOcenu();
                    }
                    else if (meni == 5)
                    {
                        IspisiPredmet();
                    }
                    else if (meni == 6)
                    {
                        IspisiProfesora();
                    }
                    else
                    {
                        IspisiStudenta();
                    }

                }
                catch { Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 7!"); }
            }
        }


        public static void IspisiAdresu(List<Adresa> adrese)
        {
            int x = 0;
			
			foreach (Adresa ad in adrese)
            {
                Console.WriteLine("\nAdresa" + x + ":");
                Console.WriteLine(ad);
                x++;
            }
        }

        public static void IspisiIndeks()
        {
            int x = 0;

            List<Indeks> adrese = new List<Indeks>();
            Storage<Indeks> adresaStorage = new Storage<Indeks>("indeks.txt");
            adrese = adresaStorage.Load();

            foreach (Indeks ad in adrese)
            {
                Console.WriteLine("\nIndeks" + x + ":");
                Console.WriteLine(ad);
                x++;
            }
        }

        public static void IspisiOcenu()
        {
            int x = 0;

            List<Ocena> adrese = new List<Ocena>();
            Storage<Ocena> adresaStorage = new Storage<Ocena>("ocena.txt");
            adrese = adresaStorage.Load();

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
        }

        public static void IspisiPredmet()
        {
            int x = 0;

            List<Predmet> adrese = new List<Predmet>();
            Storage<Predmet> adresaStorage = new Storage<Predmet>("predmet.txt");
            adrese = adresaStorage.Load();

            foreach (Predmet ad in adrese)
            {
                Console.WriteLine("\nPredmet" + x + ":");
                Console.WriteLine(ad);

                List<Ocena> ocene = ad.getStudentiPolozili();

                Console.WriteLine("Studenti Polozili: ");
                foreach(Ocena ocena in ocene)
                {
                    Console.WriteLine(ocena);
                }


                List<Student> studenti = ad.getStudentiPali();
                Console.WriteLine("Studenti Pali: ");
                foreach (Student student in studenti)
                {
                    Console.WriteLine(student);
                }

                x++;
            }        
        }

        public static void IspisiStudenta()
        {
            int x = 0;

            List<Student> adrese = new List<Student>();
            Storage<Student> adresaStorage = new Storage<Student>("student.txt");
            adrese = adresaStorage.Load();

            foreach (Student ad in adrese)
            {
                Console.WriteLine("\nStudent" + x + ":");
                Console.WriteLine(ad);

               // List<Ocena> ocene = ad.getPolozeniPredmeti();

                Console.WriteLine("Indeks studenta: ");
                //Console.WriteLine(ad.getIndeks());

                Console.WriteLine("Adresa studenta: ");
                //Console.WriteLine(ad.getAdresa());

                Console.WriteLine("Polozeni predmeti: ");
             /*   foreach (Ocena ocena in ocene)
                {
                    Console.WriteLine(ocena);
                } */


               // List<Predmet> predmeti = ad.getNepolozeniPredmeti();
                Console.WriteLine("Nepolozeni predmeti: ");
                //foreach (Predmet predmet in predmeti)
                //{
                 //   Console.WriteLine(predmet);
               // }

                x++;
            }
        }


        public static void IspisiKatedru()
        {
            int x = 0;

            List<Katedra> adrese = new List<Katedra>();
            Storage<Katedra> adresaStorage = new Storage<Katedra>("katedra.txt");
            adrese = adresaStorage.Load();

            foreach (Katedra ad in adrese)
            {
                Console.WriteLine("\nKatedra" + x + ":");
                Console.WriteLine(ad);

                List<Profesor> ocene = ad.UzmiProfesore();

                Console.WriteLine("Profesori na katedru: ");
                foreach (Profesor ocena in ocene)
                {
                    Console.WriteLine(ocena);
                }               
                x++;
            }
        }

        public static void IspisiProfesora()
        {
            int x = 0;

            List<Profesor> adrese = new List<Profesor>();
            Storage<Profesor> adresaStorage = new Storage<Profesor>("profesor.txt");
            adrese = adresaStorage.Load();



            foreach (Profesor ad in adrese)
            {
                Console.WriteLine("\nProfesor" + x + ":");
                Console.WriteLine(ad);

                Console.WriteLine("\nAdresa profesora" + x + ":");
                Console.WriteLine(ad.getAdresa());

                List<Predmet> ocene = ad.getPredmeti();

                Console.WriteLine("Predmeti koje profesor drzi: ");
                foreach (Predmet ocena in ocene)
                {
                    Console.WriteLine(ocena);
                }


                x++;
            }
        }

    }
}

