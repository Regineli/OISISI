using System;
using CLI.Serialization;
using CLI.Storage;

namespace CLI.Model


{

    //
    //	Ovo je klasa koja ce sadrzati ID-jeve od ocena
    //  Pristup ovoj klasi imaju predmet i student koji su povezani preko ocene
    //


    public class PredmetStudenti : ISerializable

    {
        public int ID { get; set; }
        public List<int> studentiID { get; set; }

        public PredmetStudenti()
        {
            studentiID = new List<int>();
            ID = new Random().Next();
        }

        public void DodajStudenta(int studID)
        {
            studentiID.Add(studID);
        }

        public void IzbaciStudenta(int studID)
        {
            studentiID.Remove(studID);
        }

        //serijalizacija
        public string[] ToCSV()
        {
            int num = 1 + studentiID.Count();
            string[] csvValues = new string[num];
            csvValues[0] = ID.ToString();
            int x = 0;
            foreach (int id in studentiID)
            {
                csvValues[x + 1] = studentiID[x].ToString();
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
                    studentiID.Add(int.Parse(v));
                }
            }
        }

        public List<Student> getStudenti()
        {
            List<Student> profesoriRet = new List<Student>();

            List<Student> profesori = new List<Student>();

            Storage<Student> profesorStorage = new Storage<Student>("student.txt");
            profesori = profesorStorage.Load();

            foreach (Student prof in profesori)
            {
                foreach (int id in studentiID)
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
