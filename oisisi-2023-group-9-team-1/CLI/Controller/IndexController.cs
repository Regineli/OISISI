using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class IndexController
    {
        private readonly IndeksDAO _indexes;

        public IndexController()
        {
            _indexes = new IndeksDAO();
        }

        public List<Indeks> GetAllIndexes()
        {
            return _indexes.GetAllIndexes();
        }

        public Indeks AddIndex(Indeks address)
        {
            return _indexes.AddIndex(address);
        }

        public Indeks? RemoveIndex(int addressId)
        {
            return _indexes.RemoveIndex(addressId);
        }
        public Indeks? UpdateIndex(Indeks adr)
        {
            return _indexes.UpdateIndex(adr);
        }

        public Indeks? GetIndexByID(int indID)
        {
            return _indexes.GetAdresaById(indID);
        }


        public void Subscribe(IObserver observer)
        {
            _indexes.Subscribe(observer);
        }
    }
}
