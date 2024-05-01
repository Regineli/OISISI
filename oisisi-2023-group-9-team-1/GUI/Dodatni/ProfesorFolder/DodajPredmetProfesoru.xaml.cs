using CLI.Controller;
using CLI;
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
using CLI.DAO;
using CLI.Model;

namespace GUI.Dodatni.ProfesorFolder
{
    /// <summary>
    /// Interaction logic for DodajPredmetProfesoru.xaml
    /// </summary>
    public partial class DodajPredmetProfesoru : Window
    {
        private ProfesorPredmetNewController _PPController;
        private ProfesorDAO _ProfesorDAO;
        private PredmetController _predmetController;

        public ObservableCollection<PredmetDTO> Predmeti { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        public int profesorID { get; set; }

        public DodajPredmetProfesoru(int profesorID)
        {
            InitializeComponent();

            DataContext = this;


            this.profesorID = profesorID;

            _PPController = new ProfesorPredmetNewController();
            _ProfesorDAO = new ProfesorDAO();
            _predmetController = new PredmetController();


            Ucitaj();
        }

        public List<Predmet> UcitajPredmeteKojeProfesorNema()
        {
            List<ProfesorPredmetNew> predmetiProfesora = _PPController.GetByProfesorID(profesorID);
            List<int> predmetIDs = new List<int>();
            foreach (ProfesorPredmetNew o in predmetiProfesora)
            {
                predmetIDs.Add(o.predmetID);
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

            foreach (Predmet predmet in UcitajPredmeteKojeProfesorNema())
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

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajPredmet_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPredmet == null)
            {
                MessageBox.Show("Izaberite predmet koji zelite da dodate!");
            }
            else
            {
                ProfesorPredmetNew noviPredmet = new ProfesorPredmetNew(profesorID, SelectedPredmet.ID);

                _PPController.AddNEW(noviPredmet);

                this.Close();
            }

            
        }
    }
}
