using System.Text;
using System.Xml.Linq;
using CLI.Serialization;

namespace CLI.Model
{
    public class Adresa : ISerializable
    {
        public string ulica { get; set; }
        public string grad { get; set; }
        public string drzava { get; set; }
        public int broj { get; set; }
        public int ID { get; set; }  // ID od adrese, povezana je na profesora i studenta

        public int GetID()
        {
            return ID;
        }

        public Adresa()
        {
        }

        public Adresa(string ul, string gr, string dr, int br)
        {
            ulica = ul;
            grad = gr;
            drzava = dr;
            broj = br;
            
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            ulica,
            grad,
            drzava,
            broj.ToString(),
            ID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ulica = values[0];
            grad = values[1];
            drzava = values[2];
            broj = int.Parse(values[3]);
            ID = int.Parse(values[4]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("\nAdresa: \n");
            sb.Append($"Ulica: {ulica}, \n");
            sb.Append($"Grad: {grad}, \n");
            sb.Append($"Drzava: {drzava}, \n");
            sb.Append($"Broj: {broj}\n");
            return sb.ToString();
        }

        public string ToOneLineString()
        {
            return ulica + " " + broj.ToString() + ", "+ grad + ", " + drzava;
        }


    }
}

