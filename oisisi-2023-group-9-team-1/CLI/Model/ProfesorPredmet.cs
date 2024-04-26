using System;
using CLI.Serialization;
using CLI.Storage;

namespace CLI.Model


{
    public class ProfesorPredmet : ISerializable

    {
        public int ID { get; set; }
        public List<int> predmetID { get; set; }

        public ProfesorPredmet()
        {
            predmetID = new List<int>();
            ID = new Random().Next();
        }

        public void DodajPredmet(int predID)
        {
            predmetID.Add(predID);
        }

        public void IzbaciPredmet(int predID)
        {
            predmetID.Remove(predID);
        }

        //serijalizacija
        public string[] ToCSV()
        {
            int num = 1 + predmetID.Count();
            string[] csvValues = new string[num];
            csvValues[0] = ID.ToString();
            int x = 0;
            foreach (int id in predmetID)
            {
                csvValues[x + 1] = predmetID[x].ToString();
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
                    predmetID.Add(int.Parse(v));
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
                foreach (int id in predmetID)
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

