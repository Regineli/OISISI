using CLI.DAO;
using CLI.Model;
using CLI.Observer;
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

namespace GUI.Dodatni.KatedraFolder
{
    /// <summary>
    /// Interaction logic for KatedraInfoNew.xaml
    /// </summary>
    public partial class KatedraInfoNew : Window, IObserver, INotifyPropertyChanged
    {
        public int katedraID;

        public KatedraDAO _katedraDAO;
        public ProfesorDAO _profesorDAO;
        public KatedraProfesorDAO _katProfDAO;

        public ObservableCollection<ProfesorDTO> ProfKatedre { get; set; }
        public ProfesorDTO SelectedProfesorKatedre { get; set; }

        public string SifraTextBox { get; set; }
        public string NazivTextBox { get; set; }

        public KatedraInfoNew(int katedraID)
        {
            InitializeComponent();
            DataContext = this;

            this.katedraID = katedraID;

            _katedraDAO = new KatedraDAO();
            _profesorDAO = new ProfesorDAO();
            _katProfDAO = new KatedraProfesorDAO();
            ProfKatedre = new ObservableCollection<ProfesorDTO>();


            Ucitaj();

            ProfKatedre.Add(new ProfesorDTO(_profesorDAO.GetAdresaById(335797952)));
            OnPropertyChanged("ProfKatedre");
        }

        public void Ucitaj()
        {
            var katedra = _katedraDAO.GetAdresaById(katedraID);

            SifraTextBox = katedra.sifra;
            NazivTextBox = katedra.naziv;

            //OnPropertyChanged("SifraTextBox");
            //OnPropertyChanged("NazivTextBox");


            List<KatedraProfesor> kprof = _katProfDAO.GetProfesorsByKatedraID(katedraID);

            ProfKatedre.Clear();
            foreach (KatedraProfesor kp in kprof)
            {
                ProfKatedre.Add(new ProfesorDTO(kp.profesor));
            }

            OnPropertyChanged("ProfKatedre");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SefKatedre_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfesorKatedre == null)
            {
                MessageBox.Show("Odaberite profesora kog zelite da stavite za sefa katedre");
            }
            else
            {
                Profesor sef = _profesorDAO.GetAdresaById(SelectedProfesorKatedre.ID);
                _katedraDAO.PostaviSefaKatedre(katedraID, sef);
            }
        }

        private void DodajProfesora_Click(object sender, RoutedEventArgs e)
        {
            Dodatni.Katedra.DodajProfesoraKatedri pk = new Dodatni.Katedra.DodajProfesoraKatedri(katedraID);
            pk.ShowDialog();

            Ucitaj();
        }
    }
}
