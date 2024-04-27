using CLI.Model;
using CLI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class OcenaDTO : INotifyPropertyChanged
    {
        private int id;
        private DateOnly datum;
        private int ocena;
        private Predmet predmet;
        private Student student;
        private int predmetID;
        private int studentID;

        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public int Ocena
        {
            get { return ocena; }
            set
            {
                if (value != ocena)
                {
                    ocena = value;
                    OnPropertyChanged("Ocena");
                }
            }
        }

        public int PredmetID
        {
            get { return predmetID; }
            set
            {
                if (value != predmetID)
                {
                    predmetID = value;
                    OnPropertyChanged("PredmetID");
                }
            }
        }

        public Predmet Predmet
        {
            get { return predmet; }
            set
            {
                if (value != predmet)
                {
                    predmet = value;
                    OnPropertyChanged("Predmet");
                }
            }
        }

        public int StudentID
        {
            get { return studentID; }
            set
            {
                if (value != studentID)
                {
                    studentID = value;
                    OnPropertyChanged("StudentID");
                }
            }
        }

        public Student Student
        {
            get { return student; }
            set
            {
                if (value != student)
                {
                    student = value;
                    OnPropertyChanged("Student");
                }
            }
        }

        public DateOnly Datum
        {
            get { return datum; }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }        

        public string DatumString
        {
            get { return Datum.ToString(); }
        }


        public OcenaDTO(Ocena ocena)
        {
            this.Id = ocena.ID;
            this.Student = ocena.studentPolozio;
            this.StudentID = ocena.StudentID;
            this.predmetID = ocena.PredmetID;
            this.Predmet = ocena.predmet;
            this.ocena = ocena.ocena;

            if (ocena.datumPolaganja != null)
                this.Datum = ocena.datumPolaganja;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
