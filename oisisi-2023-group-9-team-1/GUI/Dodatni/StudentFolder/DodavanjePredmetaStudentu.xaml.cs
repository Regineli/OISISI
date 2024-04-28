using CLI;
using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.Dodatni.Student
{
    /// <summary>
    /// Interaction logic for DodavanjePredmetaStudentu.xaml
    /// </summary>
    public partial class DodavanjePredmetaStudentu : Window, IObserver, INotifyPropertyChanged
    {
        private GradeController _gradeController;
        private StudentController _studentController;
        private PredmetController _predmetController;

        public ObservableCollection<PredmetDTO> Predmeti { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        public int studentID { get; set; }


        public DodavanjePredmetaStudentu(int studentId)
        {
            InitializeComponent();
            DataContext = this;
            

            this.studentID = studentId;

            _gradeController = new GradeController();
            _studentController = new StudentController();
            _predmetController = new PredmetController();


            Ucitaj();
            
        }

        public List<Predmet> UcitajPredmeteKojeStudentNema() {
            List<Ocena> oceneStudenta = _gradeController.GetGradesByStudentID(studentID);
            List<int> predmetIDs = new List<int>();
            foreach (Ocena o in oceneStudenta)
            {
                predmetIDs.Add(o.PredmetID);
            }

            List<Predmet> predmeti = new List<Predmet>();

            foreach (Predmet p in _predmetController.GetAllAdresses())
            {
                if (!predmetIDs.Contains(p.ID))
                {
                    predmeti.Add(p);
                }
            }

            return predmeti;
        }

        public void Ucitaj()
        {
            Predmeti = new ObservableCollection<PredmetDTO>();
            Predmeti.Clear();                    

            foreach (Predmet predmet in UcitajPredmeteKojeStudentNema())
            {
                Predmeti.Add(new PredmetDTO(predmet));
            }

            OnPropertyChanged("Predmeti");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DodajPredmet(object sender, RoutedEventArgs e)
        {
            Ocena noviPredmet = new Ocena();
            
            var st = this._studentController.GetStudentByID(studentID);
            noviPredmet.StudentID = st.ID;
            noviPredmet.studentPolozio = st;
            noviPredmet.PredmetID = SelectedPredmet.ID;
            noviPredmet.predmet = SelectedPredmet.ToPredmet();
            noviPredmet.ocena = 5;

            _gradeController.AddGrade(noviPredmet);
            
            Close();
        }
    }
}
