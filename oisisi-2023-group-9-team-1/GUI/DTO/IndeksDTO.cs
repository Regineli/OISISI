using CLI.Model;
using CLI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class IndeksDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }


        private int brojUpisa;
        public int BrojUpisa
        {

            get
            {
                return brojUpisa;
            }
            set
            {
                if (value != brojUpisa)
                {
                    brojUpisa = value;
                    OnPropertyChanged("BrojUpisa");
                }
            }
        }
        private int godinaUpisa;
        public int GodinaUpisa
        {
            get
            {
                return godinaUpisa;
            }
            set
            {
                if (value != godinaUpisa)
                {
                    godinaUpisa = value;
                    OnPropertyChanged("GodinaUpisa");
                }
            }
        }
        


        private string oznakaSmera;
        public string OznakaSmera
        {
            get
            {
                return oznakaSmera;
            }
            set
            {
                if (value != oznakaSmera)
                {
                    oznakaSmera = value;
                    OnPropertyChanged("OznakaSmera");
                }
            }
        }

        
        public IndeksDTO() {}


        public IndeksDTO(Indeks index)
        {
            Id = index.ID;
            BrojUpisa = index.brUpisa;
            GodinaUpisa = index.godinaUpisa;
            OznakaSmera = index.oznakaSmera;
        }

        public Indeks ToIndeks()
        {
            Indeks i = new Indeks();
            i.ID = this.Id;
            return i;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
