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
    public class KatedraProfesorDAO : SubjectNotifier
    {
        private readonly List<KatedraProfesor>? _katedraProf;
        private readonly Storage<KatedraProfesor>? _storage;


        public KatedraProfesorDAO()
        {
            _storage = new Storage<KatedraProfesor>("katedraProfesor.txt");
            _katedraProf = _storage.Load();
        }

        private int GenerateId()
        {
            if (_katedraProf.Count == 0) return 0;
            return _katedraProf[^1].ID + 1;
        }

        public List<KatedraProfesor>? GetByID(int id)
        {
            return _katedraProf.FindAll(v => v.ID == id);
        }


        public void AddNEW(KatedraProfesor o)
        {
            _katedraProf.Add(o);
            _storage.Save(_katedraProf);
            NotifyObservers();
        }

        public KatedraProfesor AddNEW(Profesor prof, Katedra katedra)
        {
            KatedraProfesor p = new KatedraProfesor(prof, katedra);
            p.ID = GenerateId();
            _katedraProf.Add(p);
            _storage.Save(_katedraProf);
            NotifyObservers();
            return p;
        }

        public void UkloniProfesora(int katedraID, int profesorID)
        {
            KatedraProfesor o = _katedraProf.Find(v => v.katedraID == katedraID && v.profesorID == profesorID);
            _katedraProf.Remove(o);
            _storage.Save(_katedraProf);
            NotifyObservers();
        }

        public List<KatedraProfesor>? GetProfesorsByKatedraID(int id)
        {
            return _katedraProf.FindAll(v => v.katedraID == id);
        }

        public List<KatedraProfesor> GetAll()
        {
            return _katedraProf;
        }
    }
}
