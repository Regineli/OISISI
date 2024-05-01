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
using CLI.Model;
using GUI.DTO;
using System.Collections.ObjectModel;
using CLI.Observer;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GUI.Dodatni.Student;

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for IzmeniStudenta.xaml
    /// </summary>
    public partial class IzmeniStudenta : Window, IObserver, INotifyPropertyChanged
    {        
        private GradeController _gradeController;
        private StudentController _studentController;
        private AdressController _addressController;
        private IndexController _indexController;

        public StudentDTO StudentDTO { get; set; }
        public AdresaDTO AdressDTO { get; set; }
        public IndeksDTO IndexDTO { get; set; }

        private StudentDAO studentDAO;
        private MainWindow mainWindow;
        private int studentId;

        public ObservableCollection<OcenaDTO> PolozeniPredmeti { get; set; }
        public ObservableCollection<OcenaDTO> NepolozeniPredmeti { get; set; }

        public OcenaDTO? SelectedPolozenPredmet { get; set; }
        public OcenaDTO? SelectedNepolozenPredmet { get; set; }

        public IzmeniStudenta(StudentDAO studentd, MainWindow mainWindow, int id, StudentController studentController, AdressController addressController, IndexController indexController)
        {
            InitializeComponent();
            DataContext = this;

            this.studentDAO = studentd;
            this.mainWindow = mainWindow;
            this.studentId = id;

            StudentDTO = new StudentDTO();
            AdressDTO = new AdresaDTO();
            IndexDTO = new IndeksDTO();
            _addressController = addressController;
            _studentController = studentController;
            _indexController = indexController;
            _gradeController = new GradeController();

            PolozeniPredmeti = new ObservableCollection<OcenaDTO>();

            var student = _studentController.GetStudentByID(studentId);
            if (student != null)
            {
                ime.Text = student.ime;
                prezime.Text = student.prezime;
                datRodj.Text = student.datumRodjenja;
                //Tb14.Text = student.adresaStanovanja;
                telefon.Text = student.kontaktTelefon;
                email.Text = student.eMailAdresa;
                //Tb17.Text = student.brIndexa;
                AddComboBoxAdresses();
                AddComboBoxIndexes();
                Ucitaj();

            }
            else
            {
                MessageBox.Show("Student sa datim ID-jem nije pronađen.");
                this.Close();
            }
        }

        public Indeks GetIndeks()
        {
            ComboboxItem ind = new ComboboxItem();
            ind = ComboBoxSelectIndex.SelectedItem as ComboboxItem;


            int id = ind.Value;


            return GetSelectedIndeks(id);
        }

        private Indeks? GetSelectedIndeks(int indID)
        {
            return _indexController.GetIndexByID(indID);
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
                if (StudentDTO.AdresaStanovanja.ID == a.ID)
                {
                    ComboBoxSelectAdress.SelectedItem = item;
                }
            }
        }

        private void AddComboBoxIndexes()
        {
            List<Indeks> indexes = this._indexController.GetAllIndexes();

            foreach (Indeks ind in indexes)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = ind.ToOneLineString();
                item.Value = ind.ID;
                ComboBoxSelectIndex.Items.Add(item);
            }
        }

        private string adressBinding;
        public string AdressBinding
        {
            get => adressBinding;
            set
            {
                adressBinding = value;
            }
        }

        private string indexBinding;
        public string IndexBinding
        {
            get => indexBinding;
            set
            {
                indexBinding = value;
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ažuriranje podataka studenta na osnovu unetih izmena
                var student = studentDAO.GetStudentByID(studentId);
                if (student != null)
                {
                    student.ime = ime.Text;
                    student.prezime = prezime.Text;
                    student.datumRodjenja = datRodj.Text;                 
                   // student.adresaStanovanja = Tb14.Text;
                    student.kontaktTelefon = telefon.Text;
                    student.eMailAdresa = email.Text;
                    //student.brIndexa = Tb17.Text;
                    student.AddIndeks(GetIndeks());
                    student.AddAdress(GetAdress());
                    _studentController.UpdateStudent(student);

                    mainWindow.Ucitaj();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Student sa datim ID-jem nije pronađen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        // POLOZENI PREDMETI DEO

        
        public void Ucitaj()
        {                        
            
            PolozeniPredmeti.Clear();

            foreach (Ocena ocena in _gradeController.GetPolozeniPredmetiByStudentID(studentId))
            {
                PolozeniPredmeti.Add(new OcenaDTO(ocena));
            }

            OnPropertyChanged("PolozeniPredmeti");

            NepolozeniPredmeti = new ObservableCollection<OcenaDTO>();
            NepolozeniPredmeti.Clear();

            foreach (Ocena ocena in _gradeController.GetNepolozeniPredmetiByStudentID(studentId))
            {
                NepolozeniPredmeti.Add(new OcenaDTO(ocena));
            }

            OnPropertyChanged("NepolozeniPredmeti");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PonistiOcenu_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPolozenPredmet == null)
            {
                MessageBox.Show("Odaberite predmet koji zelite da ponistite!");
            }
            else
            {
                _gradeController.PonistiPredmet(SelectedPolozenPredmet.Id);
                Ucitaj();
            }
        }

        private void DodajPredmet(object sender, RoutedEventArgs e)
        {
            
            DodavanjePredmetaStudentu d = new DodavanjePredmetaStudentu(studentId);
            d.ShowDialog();

            Ucitaj();
        }

        private void UkloniPredmet_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNepolozenPredmet == null)
            {
                MessageBox.Show("Odaberite predmet koji zelite da uklonite");
            }
            else
            {
                _gradeController.Obrisi(SelectedNepolozenPredmet.Id);

                Ucitaj();
            }
        }
    }
}
