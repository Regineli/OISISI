using System;
using System.Xml.Linq;
using CLI.Storage;
using CLI.Serialization;

namespace CLI.Model 
{
	public class KatedraProfesori : ISerializable
    {
		public List<int> profesoriID { get; set; }
		private int ID;


		public int GetID()
		{
			return ID;
		}

		public KatedraProfesori()
		{
			profesoriID = new List<int>();
			ID = new Random().Next();
		}

		public void DodajProfesora(int prID)
		{
			profesoriID.Add(prID);
		}

		public void IzbaciProfesora(int prID)
		{
			profesoriID.Remove(prID);
		}


		//serijalizacija
        public string[] ToCSV()
        {
			//Console.WriteLine("Serijalizacija 1");
			int num = 1 + profesoriID.Count();


			string[] csvValues = new string[num];


            //Console.WriteLine("Serijalizacija 2");
            csvValues[0] = ID.ToString();
           // Console.WriteLine("Serijalizacija 3");


            int x = 0;


			foreach(int id in profesoriID)
			{
				csvValues[x+1] = profesoriID[x].ToString();
				//Console.WriteLine("csVal[" + x.ToString() + "] = " + csvValues[x]);
				x++;
			}


            //Console.WriteLine("Serijalizacija 4");
            return csvValues;
        }


		//deserijalizacija
        public void FromCSV(string[] values)
        {
			ID = int.Parse(values[0]);
            foreach(string v in values)
			{
				if(v != values[0])
				{
					profesoriID.Add(int.Parse(v));
				}
			}
        }

		public List<Profesor> getProfesors()
		{
			List<Profesor> profesoriRet = new List<Profesor>();

            List<Profesor> profesori = new List<Profesor>();

            Storage<Profesor> profesorStorage = new Storage<Profesor>("profesor.txt");
            profesori = profesorStorage.Load();

            foreach (Profesor prof in profesori)
            {
				foreach(int id in profesoriID)
				{
                    if (prof.GetID() == id)
					{
						profesoriRet.Add(prof);
					}
                }                
            }

            return profesoriRet;
		}
    }
}

