// See https://aka.ms/new-console-template for more information
using CLI.Model;
using CLI.Controller;

using CLI.Storage;


using CLI.Storage;
using CLI.DAO;


namespace CLI;

class Program
{
    static void Main(string[] args)
    {
        int meni = -1;
		AdresaDAO adresaDAO = new AdresaDAO();
		/*
        List<Adresa> adrese = new List<Adresa>();
        List<Indeks> indeksi = new List<Indeks>();
        List<Katedra> katedre = new List<Katedra>();
        List<Ocena> ocene = new List<Ocena>();
        List<Predmet> predmeti = new List<Predmet>();
        List<Profesor> profesori = new List<Profesor>();
        List<Student> studenti = new List<Student>();
        */

		while (meni != 0)
        {
            Console.WriteLine("\nMeni: ");
            Console.WriteLine("0. Kraj ");
            Console.WriteLine("1. Dodaj entitet ");
            Console.WriteLine("2. Izmeni entitet ");
            Console.WriteLine("3. Obrisi entitet ");
            Console.WriteLine("4. Prikaz entitet ");
            Console.Write("Izaberite operaciju: ");
         
            try
            {
                meni = int.Parse(Console.ReadLine());
                if (meni < 0 || meni > 4)
                {
                    Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 4!");
                }
                else if (meni == 1)
                {
                    DodajEntitet.DodajEntitete();                   
                }
                else if (meni == 2)
                {
                    IzmenaEntiteta.IzmeniEntitet();
                }
                else if (meni == 3)
                {
                    BrisanjeEntiteta.brisanjeEntiteta();
                }
                else if (meni == 4)
                {




                    IspisiEntitetV2.IspisiEntitetV2p(adresaDAO);


                    //Console.Write("Unesite id adrese: ");
                    //int x = int.Parse(Console.ReadLine());
                    //Console.WriteLine(adrese[x]);
                    //Console.WriteLine(indeksi[x]);




                }

            }
            catch { Console.WriteLine("Operacija mora biti ceo broj izmedju 0 i 4!"); }
        }
    }

}




