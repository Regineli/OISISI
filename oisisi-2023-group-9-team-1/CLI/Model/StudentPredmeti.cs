using System;
using CLI.Serialization;
using CLI.Storage;

namespace CLI.Model


{

    //
    //	Ovo je klasa koja ce sadrzati ID-jeve od ocena
    //  Pristup ovoj klasi imaju predmet i student koji su povezani preko ocene
    //


    public class StudentPredmeti : ISerializable

    {
        public int ID { get; set; }
        public List<int> predmetiID { get; set; }

        public StudentPredmeti()
        {
            predmetiID = new List<int>();
            ID = new Random().Next();
        }

        public void DodajPredmet(int predID)
        {
            predmetiID.Add(predID);
        }

        public void IzbaciPredmet(int predID)
        {
            predmetiID.Remove(predID);
        }

        //serijalizacija
        public string[] ToCSV()
        {
            int num = 1 + predmetiID.Count();
            string[] csvValues = new string[num];
            csvValues[0] = ID.ToString();
            int x = 0;
            foreach (int id in predmetiID)
            {
                csvValues[x + 1] = predmetiID[x].ToString();
                x++;
            }
            return csvValues;
        }


        //deserijalizacija
        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            foreach (string v in values)
            {
                if (v != values[0])
                {
                    predmetiID.Add(int.Parse(v));
                }
            }
        }

        public List<Predmet> getPredmeti()
        {
            List<Predmet> profesoriRet = new List<Predmet>();

            List<Predmet> profesori = new List<Predmet>();

            Storage<Predmet> profesorStorage = new Storage<Predmet>("predmet.txt");
            profesori = profesorStorage.Load();

            foreach (Predmet prof in profesori)
            {
                foreach (int id in predmetiID)
                {
                    if (prof.ID == id)
                    {
                        profesoriRet.Add(prof);
                    }
                }
            }

            return profesoriRet;
            // ovde sve radi sa predmetima, nisam menjao imena varijabla
        }
    }
}
