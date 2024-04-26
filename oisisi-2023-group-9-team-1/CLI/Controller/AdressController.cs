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
    public class AdressController
    {
        private readonly AdresaDAO _addresses;

        public AdressController()
        {
            _addresses = new AdresaDAO();
        }

        public List<Adresa> GetAllAdresses()
        {
            return _addresses.GetAllAdresa();
        }

        public Adresa? GetAdressByID(int id)
        {
            return _addresses.GetAdresaById(id);
        }

        public Adresa AddAddress(Adresa address)
        {
            return _addresses.AddAdresa(address);
        }

        public Adresa? RemoveAddress(int addressId)
        {
            return _addresses.RemoveAdresa(addressId);
        }
        public Adresa? IzmeniAdresu(Adresa adr)
        {
            return _addresses.UpdateAddress(adr);
        }


        public void Subscribe(IObserver observer)
        {
            _addresses.Subscribe(observer);
        }
    }
}
