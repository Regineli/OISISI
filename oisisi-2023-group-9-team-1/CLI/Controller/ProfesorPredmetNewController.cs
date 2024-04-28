using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class ProfesorPredmetNewController
    {
        private readonly ProfesorPredmetNewDAO _addresses;

        public ProfesorPredmetNewController()
        {
            _addresses = new ProfesorPredmetNewDAO();
        }

        public List<ProfesorPredmetNew> GetAll()
        {
            return _addresses.GetAll();
        }

        public List<ProfesorPredmetNew>? GetByID(int id)
        {
            return _addresses.GetByID(id);
        }

        public void AddNEW(ProfesorPredmetNew o)
        {
           _addresses.AddNEW(o);
        }

        public ProfesorPredmetNew AddNEW(Profesor prof, Predmet predmet)
        {
            return _addresses.AddNEW(prof, predmet);
        }

        public void UkloniPredmet(int predmetID, int profesorID)
        {
            _addresses.UkloniPredmet(predmetID, profesorID);
        }

        public List<ProfesorPredmetNew>? GetByProfesorID(int id)
        {
            return _addresses.GetByProfesorID(id);
        }

        public List<Predmet> GetPredmetiByProfesorID(int id)
        {
            List<Predmet> predmeti = new List<Predmet>();

            foreach (ProfesorPredmetNew p in _addresses.GetByProfesorID(id)){
                predmeti.Add(p.predmet);
            }

            return predmeti;
        }

        public void Subscribe(IObserver observer)
        {
            _addresses.Subscribe(observer);
        }
    }
}
