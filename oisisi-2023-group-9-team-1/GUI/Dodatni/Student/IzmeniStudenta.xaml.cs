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

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for IzmeniStudenta.xaml
    /// </summary>
    public partial class IzmeniStudenta : Window
    {

        
        private GradeController _gradeController;
        private StudentController _studentController;
        private AdressController _addressController;
        private IndexController _indexController;

        public StudentDTO Student { get; set; }
        public AdresaDTO Adress { get; set; }
        public IndeksDTO Index { get; set; }

        private StudentDAO studentDAO;
        private MainWindow mainWindow;
        private int studentId;

        public ObservableCollection<Ocena> Grades { get; set; }

        public IzmeniStudenta(StudentDAO studentd, MainWindow mainWindow, int id, StudentController studentController, AdressController addressController, IndexController indexController)
        {
            InitializeComponent();
            this.studentDAO = studentd;
            this.mainWindow = mainWindow;
            this.studentId = id;

            Student = new StudentDTO();
            Adress = new AdresaDTO();
            Index = new IndeksDTO();
            _addressController = addressController;
            _studentController = studentController;
            _indexController = indexController;
            _gradeController = new GradeController();

            Student student = _studentController.GetStudentByID(studentId);
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
                LoadPolozeni();

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
                if (Student.AdresaStanovanja.ID == a.ID)
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
                Student student = studentDAO.GetStudentByID(studentId);
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

        
        public void LoadPolozeni()
        {
            Grades = new ObservableCollection<Ocena>(_gradeController.GetGradesByStudentID(studentId));
        }

    }
}
