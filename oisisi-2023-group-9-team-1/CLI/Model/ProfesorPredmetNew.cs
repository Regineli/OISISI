using CLI.Controller;
using CLI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;


namespace CLI.Model
{
    public class ProfesorPredmetNew : ISerializable
    {
        public int ID;
        public int profesorID;
        public int predmetID;
        public Profesor profesor;
        public Predmet predmet;

        public ProfesorDAO _profesorDAO;
        public PredmetController _predmetController;

        public ProfesorPredmetNew()
        {
            profesor = null;
            predmet = null;

            _profesorDAO = new ProfesorDAO();
            _predmetController = new PredmetController();
        }

        public ProfesorPredmetNew(Profesor prof, Predmet predmet)
        {
            this.profesor = prof;
            this.predmet = predmet;
            this.profesorID = prof.ID;
            this.predmetID = predmet.ID;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                profesorID.ToString(),
                predmetID.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            profesorID = int.Parse(values[1]);
            predmetID = int.Parse(values[2]);

            profesor = _profesorDAO.GetAdresaById(profesorID);
            predmet = _predmetController.GetAdressByID(predmetID);
        }
    }
}
