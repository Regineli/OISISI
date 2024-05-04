using CLI.DAO;
using CLI;
using System;
using System.Collections.Generic;
using System.Linq;
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
using CLI.Controller;
using GUI.DTO;
using CLI.Model;
using System.Collections.ObjectModel;
using CLI.Observer;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GUI.Dodatni.ProfesorFolder;

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for IzmeniProfesora.xaml
    /// </summary>
    public partial class IzmeniProfesora : Window, IObserver, INotifyPropertyChanged
    {
        private ProfesorDAO profesoriDAO;
        private MainWindow mainWindow;
        private int profesorId;

        private AdressController _addressController;
        public AdresaDTO AdressDTO { get; set; }

        private ProfesorPredmetNewController _PPController;
        public PredmetDTO PredmetDTO { get; set; }

        public ObservableCollection<PredmetDTO> ProfesorPredmeti { get; set; }
        public PredmetDTO SelectedProfesorPredmet { get; set; }

        public ObservableCollection<StudentDTO> StudentiProfesora { get; set; }
        public StudentController _studentController { get; set; }

        public OcenaDAO _ocenaDAO { get; set; }
        public List<int> studentiProfesoraIDs { get; set; }

        public IzmeniProfesora(ProfesorDAO profesoriDAO, MainWindow mainWindow, int profesorId)
        {
            InitializeComponent();
            DataContext = this;
            this.profesoriDAO = profesoriDAO;
            this.mainWindow = mainWindow;
            this.profesorId = profesorId;

            _ocenaDAO = new OcenaDAO();
            studentiProfesoraIDs = new List<int>();

            _studentController = new StudentController();
            StudentiProfesora = new ObservableCollection<StudentDTO>();

            _addressController = new AdressController();
            AdressDTO = new AdresaDTO();

            _PPController = new ProfesorPredmetNewController();
            PredmetDTO = new PredmetDTO();

            ProfesorPredmeti = new ObservableCollection<PredmetDTO>();
            AddComboBoxAdresses();

            Profesor profesor = profesoriDAO.GetAdresaById(profesorId);
            if (profesor != null)
            {
                // Popunjavamo vrednosti na poljima vrednostima profesora
                Tb1.Text = profesor.prezime;
                Tb2.Text = profesor.ime;
                Tb3.Text = profesor.datumRodjenja;
                
                Tb5.Text = profesor.kontaktTelefon;
                Tb6.Text = profesor.eMailAdresa;
                Tb7.Text = profesor.brLicneKarte;
                Tb8.Text = profesor.zvanje;
                Tb9.Text = profesor.godineStaza;
                Ucitaj();


            }
            else
            {
                MessageBox.Show("Profesor sa datim ID-jem nije pronađen.");
                this.Close();
            }
        }
        public Adresa GetAdress()
        {
            ComboboxItem adr = new ComboboxItem();
            adr = ComboBoxSelectAdress.SelectedItem as ComboboxItem;


            int id = adr.Value;

            return GetSelectedAdress(id);
        }

        private Adresa? GetSelectedAdress(int adrID)
        {
            return _addressController.GetAdressByID(adrID);
        }

        private void AddComboBoxAdresses()
        {
            List<Adresa> adresses = this._addressController.GetAllAdresses();

            foreach (Adresa a in adresses)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = a.ToOneLineString();
                item.Value = a.ID;
                ComboBoxSelectAdress.Items.Add(item);
            }
        }

        // Potvrdi izmene
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            // Azuriranje podataka profesora na osnovu izmena
            Profesor profesor = profesoriDAO.GetAdresaById(profesorId);
            if (profesor != null)
            {
                profesor.prezime = Tb1.Text;
                profesor.ime = Tb2.Text;
                profesor.datumRodjenja = Tb3.Text;
                profesor.adresa = GetAdress();
                profesor.kontaktTelefon = Tb5.Text;
                profesor.eMailAdresa = Tb6.Text;
                profesor.brLicneKarte = Tb7.Text;
                profesor.zvanje = Tb8.Text;
                profesor.godineStaza = Tb9.Text;

                // Azuriramo listu profesora na main view posle izmene
                mainWindow.Ucitaj();
                this.Close();
            }
            else
            {
                MessageBox.Show("Profesor sa datim ID-jem nije pronađen.");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // PREDMETI TAB

        public void DodajStudentaProfesoruGrid(Predmet p)
        {
            List<Ocena> ocenePredmeta = _ocenaDAO.GetOcenePredmeta(p);

            foreach(Ocena o in ocenePredmeta)
            {
                if(!studentiProfesoraIDs.Contains(o.studentPolozio.ID))
                {
                    StudentiProfesora.Add(new StudentDTO(o.studentPolozio));
                    studentiProfesoraIDs.Add(o.studentPolozio.ID);
                }
            }
        }

        public void Ucitaj()
        {
            StudentiProfesora.Clear();
            ProfesorPredmeti.Clear();
            foreach (Predmet p in _PPController.GetPredmetiByProfesorID(profesorId))
            {
                ProfesorPredmeti.Add(new PredmetDTO(p));
                DodajStudentaProfesoruGrid(p);
            }
            OnPropertyChanged("ProfesorPredmeti");
            OnPropertyChanged("StudentiProfesora");


            
            // Ocena -> Student, Predmet
            // ProfesorPredmetNEW -> Profesor, Predmet
            // Profesor -> Predmeti, Predmet->Ocena, Ocena->Student
            // Dodati uslov da se ne ponavlja isti student
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DodajPredmet_Click(object sender, RoutedEventArgs e)
        {
            DodajPredmetProfesoru pr = new DodajPredmetProfesoru(profesorId);
            pr.ShowDialog();

            Ucitaj();
        }

        private void UkloniPredmet_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedProfesorPredmet == null)
            {
                MessageBox.Show("Odaberite predmet koji zelite da uklonite");
            }
            else
            {
                _PPController.UkloniPredmet(SelectedProfesorPredmet.ID, profesorId);

                Ucitaj();
            }
        }
    }
}
