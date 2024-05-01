using CLI.Controller;
using CLI.DAO;
using CLI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class KatedraProfesor : ISerializable
    {
        public int profesorID;
        public int katedraID;
        public Profesor profesor;
        public Katedra katedra;

        public int ID;

        public ProfesorDAO _profesorDAO;
        public KatedraDAO _katedraDAO;

        public KatedraProfesor() {
            _profesorDAO = new ProfesorDAO();
            _katedraDAO = new KatedraDAO();
        }

        public KatedraProfesor(Profesor prof, Katedra kat)
        {
            profesor = prof;
            katedra = kat;
            profesorID = prof.ID;
            katedraID = kat.ID;
        }

        



        public string[] ToCSV()
        {
            string[] csvValues =
            {
            profesorID.ToString(),
            katedraID.ToString(),
            ID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            profesorID = int.Parse(values[0]);
            katedraID = int.Parse(values[1]);
            ID = int.Parse(values[2]);
        }
    }
}
