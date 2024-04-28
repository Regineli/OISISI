using CLI.Model;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
    public class ProfesorPredmetNewDAO : SubjectNotifier
    {
        private readonly List<ProfesorPredmetNew>? _PPs;
        private readonly Storage<ProfesorPredmetNew>? _storage;


        public ProfesorPredmetNewDAO()
        {
            _storage = new Storage<ProfesorPredmetNew>("profesorPredmetNEW.txt");
            _PPs = _storage.Load();
        }

        private int GenerateId()
        {
            if (_PPs.Count == 0) return 0;
            return _PPs[^1].ID + 1;
        }

        public List<ProfesorPredmetNew>? GetByID(int id)
        {
            return _PPs.FindAll(v => v.ID == id);
        }


        public void AddNEW(ProfesorPredmetNew o)
        {
            _PPs.Add(o);
            _storage.Save(_PPs);
            NotifyObservers();
        }

        public ProfesorPredmetNew AddNEW(Profesor prof, Predmet predmet)
        {
            ProfesorPredmetNew p = new ProfesorPredmetNew(prof, predmet);
            p.ID = GenerateId();
            _PPs.Add(p);
            _storage.Save(_PPs);
            NotifyObservers();
            return p;
        }

        public void UkloniPredmet(int predmetID, int profesorID)
        {
            ProfesorPredmetNew o = _PPs.Find(v => v.predmetID == predmetID && v.profesorID == profesorID);
            _PPs.Remove(o);
            _storage.Save(_PPs);
            NotifyObservers();
        }

        public List<ProfesorPredmetNew>? GetByProfesorID(int id)
        {
            return _PPs.FindAll(v => v.profesorID == id);
        }

        public List<ProfesorPredmetNew> GetAll()
        {
            return _PPs;
        }
    }
}
