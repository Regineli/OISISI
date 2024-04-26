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
    public class PredmetController
    {
        private readonly PredmetDAO _addresses;

        public PredmetController()
        {
            _addresses = new PredmetDAO();
        }

        public List<Predmet> GetAllAdresses()
        {
            return _addresses.GetAllAdresa();
        }

        public Predmet? GetAdressByID(int id)
        {
            return _addresses.GetAdresaById(id);
        }

        public Predmet AddAddress(Predmet address)
        {
            return _addresses.AddAdresa(address);
        }

        public Predmet? RemoveAddress(int addressId)
        {
            return _addresses.RemoveAdresa(addressId);
        }
        public Predmet? IzmeniAdresu(Predmet adr)
        {
            return _addresses.UpdateVehicle(adr);
        }


        public void Subscribe(IObserver observer)
        {
            _addresses.Subscribe(observer);
        }
    }
}
