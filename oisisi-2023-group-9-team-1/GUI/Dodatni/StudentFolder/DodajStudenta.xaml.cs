using CLI;
using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DodajStudenta.xaml
    /// </summary>
    /// 


    class ComboboxItem
    {

        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public partial class DodajStudenta : Window, INotifyPropertyChanged
    {
        private StudentController _studentController;
        private AdressController _addressController;
        private IndexController _indexController;

        public StudentDTO Student { get; set; }
        public AdresaDTO Adress { get; set; }
        public IndeksDTO Index { get; set; }


        public DodajStudenta(StudentController studentController, AdressController addressController, IndexController indexController)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            Adress = new AdresaDTO();
            Index = new IndeksDTO();
            _addressController = addressController;
            _studentController = studentController;
            _indexController = indexController;

            AddComboBoxAdresses();
            AddComboBoxIndexes();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CLI.Student s = Student.ToStudent();

            Adresa a = GetAdress();

            s.AddAdress(a);

            Indeks ind = GetIndeks();
            s.AddIndeks(ind);

            _studentController.AddStudent(s);
            //MainWindow main = new MainWindow();
            //main.Show();
            //Close();
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

        private int _selectedAdressID;
        public int SelectedAdressID
        {
            get { return _selectedAdressID;  }
            set
            {
                _selectedAdressID = value;
                OnPropertyChanged(nameof(SelectedAdressID));
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

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            
            // Proveri da li TextBox ima tekst
            
            if (!string.IsNullOrEmpty(TB1.Text) && !string.IsNullOrEmpty(TB2.Text) && !string.IsNullOrEmpty(TB3.Text)
                && !string.IsNullOrEmpty(TB5.Text) && !string.IsNullOrEmpty(TB6.Text) && SelectedAdressID != null)
            {
                // Omogući dugme ako TextBox ima tekst
                Potvrdi.IsEnabled = true;
            }
            else
            {
                // Onemogući dugme ako TextBox nema tekst
                Potvrdi.IsEnabled = false;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
    
