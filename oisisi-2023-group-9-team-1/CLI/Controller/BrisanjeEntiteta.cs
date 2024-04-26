using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class BrisanjeEntiteta

    {
        public static void brisanjeEntiteta()
        {
            int meni = -1;
            while (meni != 0)
            {
                Console.WriteLine("\nDodaj: ");
                Console.WriteLine("0. Nazad ");
                Console.WriteLine("1. Izbrisi Adresu ");
                Console.WriteLine("2. Izbrisi Indeks ");
                Console.WriteLine("3. Izbrisi Katedru ");
                Console.WriteLine("4. Izbrisi Ocenu ");
                Console.WriteLine("5. Izbrisi Predmet ");
                Console.WriteLine("6. Izbrisi Profesora ");
                Console.WriteLine("7. Izbrisi Studenta ");
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
                        izbrisiAdresu();
                    }
                    else if (meni == 2)
                    {
                        izbrisiIndeks();
                    }
                    else if (meni == 3)
                    {
                        izbrisiKatedru();
                    }
                    else if (meni == 4)
                    {
                        izbrisiOcenu();
                    }
                    else if (meni == 5)
                    {
                        izbrisiPredmet();
                    }
                    else if (meni == 6)
                    {
                        izbrisiProfesora();
                    }
                    else
                    {
                        izbrisiStudenta();
                    }

                }
                catch { Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 7!"); }
            }

        }


        public static void izbrisiAdresu()
        {
            Console.WriteLine("Unesite redni broj adrese koju zelite da obrisete: ");
            int index = int.Parse(Console.ReadLine());
            List<Adresa> adrese = new List<Adresa>();
            Storage<Adresa> adresaStorage = new Storage<Adresa>("adresa.txt");
            adrese = adresaStorage.Load();

            foreach (Adresa adr in adrese)
            {
                if (index >= 0 && index < adrese.Count)
                {
                    // Brisanje adrese iz kolekcije
                    adrese.RemoveAt(index);

                    // Ažuriranje fajla
                    adresaStorage.Save(adrese);

                    Console.WriteLine("Adresa uspešno obrisana.");
                }
                else 
                {
                    Console.WriteLine("Ne postoji redni broj unete adrese");
                }
            }

        }

        public static void izbrisiIndeks()
        {
            Console.WriteLine("Unesite redni proj indeksa koji zelite da obrisete.");
            int index = int.Parse(Console.ReadLine());
            List<Indeks> indeksi = new List<Indeks>();
            Storage<Indeks> indeksStorage = new Storage<Indeks>("indeks.txt");
            indeksi = indeksStorage.Load();

            foreach (Indeks ind in indeksi)
            {
                if (index >= 0 && index < indeksi.Count())
                {
                    indeksi.RemoveAt(index);
                    indeksStorage.Save(indeksi);

                    Console.WriteLine("Indeks uspešno obrisana.");
                }
                else
                {
                    Console.WriteLine("Ne postoji redni broj unetog indeksa");
                }
            }

        }

        public static void izbrisiKatedru()
        {
            Console.WriteLine("Unesite redni proj katedre koju zelite da obrisete.");
            int index = int.Parse(Console.ReadLine());
            List<Katedra> katedre = new List<Katedra>();
            Storage<Katedra> katedraStorage = new Storage<Katedra>("katedra.txt");
            katedre = katedraStorage.Load();

            foreach (Katedra kat in katedre)
            {
                if (index >= 0 && index < katedre.Count())
                {
                    katedre.RemoveAt(index);
                    katedraStorage.Save(katedre);

                    Console.WriteLine("Katedra uspešno obrisana.");
                }
                else
                {
                    Console.WriteLine("Ne postoji redni broj unete katedre");
                }
            }
        }
        public static void izbrisiPredmet()
        {
            Console.WriteLine("Unesite redni proj predmeta koji zelite da obrisete.");
            int index = int.Parse(Console.ReadLine());
            List<Predmet> predmeti = new List<Predmet>();
            Storage<Predmet> predmetStorage = new Storage<Predmet>("predmet.txt");
            predmeti = predmetStorage.Load();

            foreach (Predmet pre in predmeti)
            {
                if (index >= 0 && index < predmeti.Count())
                {
                    predmeti.RemoveAt(index);
                    predmetStorage.Save(predmeti);

                    Console.WriteLine("Predmet uspešno obrisana.");
                }
                else
                {
                    Console.WriteLine("Ne postoji redni broj unetog predmeta");
                }
            }
        }
        public static void izbrisiOcenu()
        {
            Console.WriteLine("Unesite redni proj ocene koju zelite da obrisete.");
            int index = int.Parse(Console.ReadLine());
            List<Ocena> ocene = new List<Ocena>();
            Storage<Ocena> ocenaStorage = new Storage<Ocena>("ocena.txt");
            ocene = ocenaStorage.Load();

            foreach (Ocena oce in ocene)
            {
                if (index >= 0 && index < ocene.Count())
                {
                    ocene.RemoveAt(index);
                    ocenaStorage.Save(ocene);

                    Console.WriteLine("Ocena uspešno obrisana.");
                }
                else
                {
                    Console.WriteLine("Ne postoji redni broj unete ocene");
                }
            }
        }
        public static void izbrisiProfesora()
        {
            Console.WriteLine("Unesite redni proj profesora kojeg zelite da obrisete.");
            int index = int.Parse(Console.ReadLine());
            List<Profesor> profesori = new List<Profesor>();
            Storage<Profesor> profesorStorage = new Storage<Profesor>("profesor.txt");
            profesori = profesorStorage.Load();

            foreach (Profesor prof in profesori)
            {
                if (index >= 0 && index < profesori.Count())
                {
                    profesori.RemoveAt(index);
                    profesorStorage.Save(profesori);

                    Console.WriteLine("Profesor uspešno obrisana.");
                }
                else
                {
                    Console.WriteLine("Ne postoji redni broj unetog profesora");
                }
            }

        }
        public static void izbrisiStudenta()
        {
            Console.WriteLine("Unesite redni proj studneta koji zelite da obrisete.");
            int index = int.Parse(Console.ReadLine());
            List<Student> studenti = new List<Student>();
            Storage<Student> studentStorage = new Storage<Student>("stundet.txt");
            studenti = studentStorage.Load();

            foreach (Student stud in studenti)
            {
                if (index >= 0 && index < studenti.Count())
                {
                    studenti.RemoveAt(index);
                    studentStorage.Save(studenti);

                    Console.WriteLine("Student uspešno obrisana.");
                }
                else
                {
                    Console.WriteLine("Ne postoji redni broj unetog studenta");
                }
            }
        }
    }
}
