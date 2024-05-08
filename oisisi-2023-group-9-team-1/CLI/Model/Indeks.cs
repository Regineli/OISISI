using System;
using System.Text;
using System.Xml.Linq;
using CLI.Storage;
using CLI.Serialization;

namespace CLI
{
    public class Indeks : ISerializable
    {
        public int brUpisa { get; set; }
        public int godinaUpisa { get; set; }
        public string oznakaSmera { get; set; }
        public int ID { get; set; }  // ID indeksa, povezana je na studenta

        public int GetID()
        {
            return ID;
        }

        public Indeks()
        {
            brUpisa = 0;
            godinaUpisa = 0;
            oznakaSmera = "";
            ID = new Random().Next();
        }

        public Indeks(int br, int god, string oznaka)
        {
            brUpisa = br;
            godinaUpisa = god;
            oznakaSmera = oznaka;
            ID = new Random().Next();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            brUpisa.ToString(),
            godinaUpisa.ToString(),
            oznakaSmera,
            ID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            brUpisa = int.Parse(values[0]);
            godinaUpisa = int.Parse(values[1]);
            oznakaSmera = values[2];
            ID = int.Parse(values[3]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Broj Upisa: {brUpisa}, \n");
            sb.Append($"Godina Upisa: {godinaUpisa}, \n");
            sb.Append($"Oznaka Smera: {oznakaSmera} \n");
            return sb.ToString();
        }

        public string ToOneLineString()
        {
            return oznakaSmera + "-" + brUpisa.ToString() + "-" + godinaUpisa.ToString();
        }
    }
}

