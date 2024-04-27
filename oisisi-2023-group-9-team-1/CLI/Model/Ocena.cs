using System;
using System.Text;
using CLI.Controller;
using CLI.Model;
using CLI.Serialization;
using CLI.Storage;
using Microsoft.VisualBasic;

namespace CLI
{
    public class Ocena : ISerializable
    {
        public Student studentPolozio;
        public Predmet predmet;

        public int StudentID { get; set; }  // ID studenta
        public int PredmetID { get; set; }  // ID predmeta koji je student polozio

        public DateOnly datumPolaganja;
        public int ocena { get; set; }
        public int ID { get; set; }

        public StudentController _studentController { get; set; }
        public PredmetController _predmetController { get; set; }

        public Ocena()
        {
            studentPolozio = null;
            predmet = null;
            datumPolaganja = DateOnly.FromDateTime(DateTime.Now);
            ocena = 0;

            _studentController = new StudentController();
            _predmetController = new PredmetController();
        }

        

        public Ocena(Predmet predmet, Student student, int ocena, DateOnly datumPolaganja)
        {
            studentPolozio = student;
            this.predmet = predmet;
            this.datumPolaganja = datumPolaganja;
            this.ocena = ocena;


            StudentID = studentPolozio.ID;
            this.PredmetID = predmet.ID;

            _studentController = new StudentController();
            _predmetController = new PredmetController();
        }


        //
        //  pocetak pisanja i citanja iz fajla
        //

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ocena.ToString(),
                StudentID.ToString(),
                PredmetID.ToString(),
                ID.ToString(),
                datumPolaganja.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ocena = int.Parse(values[0]);
            StudentID = int.Parse(values[1]);
            PredmetID = int.Parse(values[2]);
            ID = int.Parse(values[3]);
            datumPolaganja = DateOnly.Parse(values[4]);

            studentPolozio = _studentController.GetStudentByID(StudentID);
            predmet = _predmetController.GetAdressByID(PredmetID);
        }


        //
        //  kraj pisanja i citanja iz fajla
        //


        // funkciju koristimo da uzmemo studenta iz tabele koji je povezan na ocenu
        public Student GetStudent()
        {
            List<Student> studenti = new List<Student>();
            Storage<Student> studentiStorage = new Storage<Student>("student.txt");

            studenti = studentiStorage.Load();

            foreach(Student st in studenti)
            {
                if(StudentID == st.ID)
                {
                    return st;
                }
            }

            Student s1 = new Student();
            s1.ID = -1;
            return s1;
        }

        // funkciju koristimo da uzmemo predmet iz tabele koji je povezan na ocenu
        public Predmet GetPredmet()
        {
            List<Predmet> predmeti = new List<Predmet>();
            Storage<Predmet> predmetiStorage = new Storage<Predmet>("predmet.txt");

            predmeti = predmetiStorage.Load();
            //Console.WriteLine("student radi ocena 7");
            foreach (Predmet pr in predmeti)
            {
                if (PredmetID == pr.ID)
                {
                    return pr;
                }
            }
            ///Console.WriteLine("student radi ocena 8");
            Predmet p1 = new Predmet();
            p1.ID = -1;
            return p1;
        }

        public override string ToString()
        {
            //Console.WriteLine("student radi ocena 4");
            studentPolozio = GetStudent();
            //Console.WriteLine("student radi ocena 5");
            predmet = GetPredmet();
            //Console.WriteLine("student radi ocena 6");
            StringBuilder sb = new StringBuilder();
            
            sb.Append($"StudentPolozio: {studentPolozio}, \n");
            sb.Append($"Predmet: {predmet}, \n");
            sb.Append($"DatumPolaganja: {datumPolaganja}, \n");


            return sb.ToString();
        }


        public void UnesiDatumPolaganja()
        {
            string datum;

            Console.Write("Dan: ");
            datum = Console.ReadLine();

            Console.Write("Mesec: ");
            datum += "/" + Console.ReadLine();

            Console.Write("Godina: ");
            datum += "/" + Console.ReadLine();



            datumPolaganja = DateOnly.Parse(datum);
        }
    }
}

