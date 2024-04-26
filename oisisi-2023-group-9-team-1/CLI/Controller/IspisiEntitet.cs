using CLI.Model;
using CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using CLI.Serialization;


using CLI.Storage;

namespace CLI.Controller
{
    public class IspisiEntitet
    {
        public static void ispisiEntitet()
        {
            int komanda = -1;
            while (komanda != 0)
            {
                try
                {
                    Console.WriteLine("\nDodaj: ");
                    Console.WriteLine("1. Ispisi Adresu ");
                    Console.WriteLine("2. Ispisi Indeks ");
                    Console.WriteLine("3. Ispisi Katedru ");
                    Console.WriteLine("4. Ispisi Ocenu ");
                    Console.WriteLine("5. Ispisi Predmet ");
                    Console.WriteLine("6. Ispisi Profesora ");
                    Console.WriteLine("7. Ispisi Studenta ");
                    Console.Write("Izaberite operaciju: ");
                    komanda = int.Parse(Console.ReadLine());
                    if (komanda == 1)
                    {
                        ispisiAdrese();

                    }
                    else if (komanda == 2)
                    {
                        ispisiIndeks();

                    }
                    else if (komanda == 3)
                    {
                        ispisiKatedru();

                    }
                    else if (komanda == 4)
                    {
                        ispisiOcenu();

                    }
                    else if (komanda == 5)
                    {
                        ispisiPredmet();

                    }
                    else if (komanda == 6)
                    {
                        ispisiProfesora();

                    }
                    else if (komanda == 7)
                    {
                        ispisiStudenta();

                    }
                    else if (komanda == 0)
                    {
                        break;
                    }
                }
                catch { Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 7!"); }
            }
        }


        private static void ispisiAdrese()
        {
            int x = 0;

            List<Adresa> adrese = new List<Adresa>();
            Storage<Adresa> adresaStorage = new Storage<Adresa>("adresa.txt");
            adrese = adresaStorage.Load();

            foreach (Adresa ad in adrese)
            {
                Console.WriteLine("\nAdresa" + x + ":");
                Console.WriteLine(ad);
                x++;
            }
        }
        public static void ispisiAdresu()
        {
            int x = 0;

            List<Adresa> adrese = new List<Adresa>();
            Storage<Adresa> adresaStorage = new Storage<Adresa>("adresa.txt");
            adrese = adresaStorage.Load();

            foreach (Adresa adr in adrese)
            {
                Console.WriteLine("\nAdresa" + x + ":");
                Console.WriteLine(adr);
                x++;
            }
        }
        public static void ispisiIndeks()
        {
            int x = 0;

            List<Indeks> indeksi = new List<Indeks>();
            Storage<Indeks> indeksStorage = new Storage<Indeks>("indeks.txt");
            indeksi = indeksStorage.Load();

            foreach (Indeks ind in indeksi)
            {
                Console.WriteLine("\nIndeks" + x + ":");
                Console.WriteLine(ind);
                x++;
            }
        }
     

        public static void ispisiKatedru()
        {
            int x = 0;

            List<Katedra> katedre = new List<Katedra>();
            Storage<Katedra> katedraStorage = new Storage<Katedra>("katedra.txt");
            katedre = katedraStorage.Load();

            foreach (Katedra ka in katedre)
            {
                Console.WriteLine("\nKatedra" + x + ":");
                Console.WriteLine(ka);
                x++;
            }
        }

        public static void ispisiOcenu()
        {
            int x = 0;

            List<Ocena> ocene = new List<Ocena>();
            Storage<Ocena> oceneStorage = new Storage<Ocena>("ocene.txt");
            ocene = oceneStorage.Load();

            foreach (Ocena oc in ocene)
            {
                Console.WriteLine("\nOcene" + x + ":");
                Console.WriteLine(oc);
                x++;
            }
        }

        public static void ispisiPredmet()
        {
           /* int x = 0;

            List<Predmet> predmeti = new List<Predmet>();
            Storage<Predmet> predmetStorage = new Storage<Predmet>("predmet.txt");
            predmeti = predmetStorage.Load();

            foreach (Predmet pr in predmeti)
            {
                Console.WriteLine("\nProfesor" + x + ":");
                Console.WriteLine(pr);
                x++;
            }*/
        }

        
        public static void ispisiProfesora()
        {
            int x = 0;

            List<Profesor> profesori = new List<Profesor>();
            Storage<Profesor> profesorStorage = new Storage<Profesor>("profesor.txt");
            profesori = profesorStorage.Load();

            foreach (Profesor pr in profesori)
            {
                Console.WriteLine("\nProfesor" + x + ":");
                Console.WriteLine(pr);
                x++;
            }
        }
        public static void ispisiStudenta()
        {
            int x = 0;

            List<Student> studenti = new List<Student>();
            Storage<Student> studentStorage = new Storage<Student>("student.txt");
            studenti = studentStorage.Load();

            foreach (Student st in studenti)
            {
                Console.WriteLine("\nStudent" + x + ":");
                Console.WriteLine(st);
                x++;
            }
        }

    }  
}
 