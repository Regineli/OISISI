using CLI.Controller;
using CLI.DAO;
using CLI.Model;
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

namespace GUI.Dodatni.Katedra
{
    /// <summary>
    /// Interaction logic for DodajProfesoraKatedri.xaml
    /// </summary>
    public partial class DodajProfesoraKatedri : Window
    {
        private KatedraProfesorDAO _katedraProfDAO;
        private ProfesorDAO _ProfesorDAO;
        private KatedraDAO _katedraDAO;

        public ObservableCollection<ProfesorDTO> Profesori { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }

        public int katedraID { get; set; }

        public DodajProfesoraKatedri(int katedraID)
        {
            InitializeComponent();

            DataContext = this;


            this.katedraID = katedraID;

            _katedraProfDAO = new KatedraProfesorDAO();
            _ProfesorDAO = new ProfesorDAO();
            _katedraDAO = new KatedraDAO();


            Ucitaj();
        }

        public List<Profesor> UcitajProfesoreKojiNisuNaKatedri()
        {
            List<KatedraProfesor> profNaKatedri = _katedraProfDAO.GetProfesorsByKatedraID(this.katedraID);
            List<int> profIDs = new List<int>();
            foreach (KatedraProfesor p in profNaKatedri)
            {
                profIDs.Add(p.profesorID);
            }

            List<Profesor> profesori = new List<Profesor>();

            foreach (Profesor p in _ProfesorDAO.GetAllAdresa())
            {
                if (!profIDs.Contains(p.ID))
                {
                    profesori.Add(p);
                }
            }

            return profesori;
        }

        public void Ucitaj()
        {
            Profesori = new ObservableCollection<ProfesorDTO>();
            Profesori.Clear();

            foreach (Profesor pr in UcitajProfesoreKojiNisuNaKatedri())
            {
                Profesori.Add(new ProfesorDTO(pr));
            }

            OnPropertyChanged("Profesori");
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

        private void DodajProfesora_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfesor == null)
            {
                MessageBox.Show("Izaberite predmet koji zelite da dodate!");
            }
            else
            {
                CLI.Katedra k = _katedraDAO.GetAdresaById(katedraID);
                Profesor p = _ProfesorDAO.GetAdresaById(SelectedProfesor.ID);


                KatedraProfesor noviProf = new KatedraProfesor(p, k);

                _katedraProfDAO.AddNEW(noviProf);

                this.Close();
            }


        }
        
    }
}
