using CLI;
using CLI.DAO;
using CLI.Model;
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

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for Katedra.xaml
    /// </summary>
    public partial class Katedra : Window, IObserver
    {
        public int katedraID;

        public KatedraDAO _katedraDAO;
        public ProfesorDAO _profesorDAO;
        public KatedraProfesorDAO _katProfDAO;

        public ObservableCollection<ProfesorDTO> ProfesoriKatedre;
        public ProfesorDTO SelectedProfesorKatedre;

        public Katedra(int katedraID)
        {
            InitializeComponent();
            DataContext = this;

            this.katedraID = katedraID;

            _katedraDAO = new KatedraDAO();
            _profesorDAO = new ProfesorDAO();
            _katProfDAO = new KatedraProfesorDAO();
            ProfesoriKatedre = new ObservableCollection<ProfesorDTO>();
            
        }

        public void Ucitaj()
        {
            var katedra = _katedraDAO.GetAdresaById(katedraID);

            List<KatedraProfesor> kprof = _katProfDAO.GetProfesorsByKatedraID(katedraID);

            foreach(KatedraProfesor kp in kprof)
            {
                ProfesoriKatedre.Add(new ProfesorDTO(kp.profesor));
            }

            OnPropertyChanged("ProfesoriKatedre");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SefKatedre_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedProfesorKatedre == null)
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
            return;
        }
    }
}
