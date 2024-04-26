using System;
using CLI.Serialization;
using CLI.Storage;

namespace CLI.Model


{

    //
    //	Ovo je klasa koja ce sadrzati ID-jeve od ocena
    //  Pristup ovoj klasi imaju predmet i student koji su povezani preko ocene
    //


    public class PredmetOcena : ISerializable

    {
        public int ID { get; set; }
        public List<int> oceneID { get; set; }

        public PredmetOcena()
        {
            oceneID = new List<int>();
            ID = new Random().Next();
        }

        public void DodajOcenu(int ocenaID)
        {
            oceneID.Add(ocenaID);
        }

        public void IzbaciOcenu(int ocenaID)
        {
            oceneID.Remove(ocenaID);
        }

        //serijalizacija
        public string[] ToCSV()
        {
            int num = 1 + oceneID.Count();
            string[] csvValues = new string[num];
            csvValues[0] = ID.ToString();
            int x = 0;
            foreach (int id in oceneID)
            {
                csvValues[x + 1] = oceneID[x].ToString();
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
                    oceneID.Add(int.Parse(v));
                }
            }
        }

        public List<Ocena> getOcene()
        {
            List<Ocena> profesoriRet = new List<Ocena>();

            List<Ocena> profesori = new List<Ocena>();

            Storage<Ocena> profesorStorage = new Storage<Ocena>("ocena.txt");
            profesori = profesorStorage.Load();

            foreach (Ocena prof in profesori)
            {
                foreach (int id in oceneID)
                {
                    if (prof.ID == id)
                    {
                        profesoriRet.Add(prof);
                    }
                }
            }

            return profesoriRet;
            // ovde sve radi sa ocenama, nisam menjao imena varijabla
        }
    }
}

