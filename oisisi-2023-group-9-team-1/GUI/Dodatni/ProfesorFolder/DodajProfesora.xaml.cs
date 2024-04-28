using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
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

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for DodajProfesora.xaml
    /// </summary>
    public partial class DodajProfesora : Window
    {
        public ProfesorDTO ProfesorDTO { get; set; }
        public ProfesorDAO profesorDAO;
        public MainWindow mainWindow;

        private AdressController _addressController;
        public AdresaDTO AdressDTO { get; set; }    

        public DodajProfesora(ProfesorDAO profesoriDAO, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            ProfesorDTO = new ProfesorDTO();
            this.profesorDAO = profesoriDAO;
            this.mainWindow = mainWindow;

            _addressController = new AdressController();
            AdressDTO = new AdresaDTO();

            AddComboBoxAdresses();
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

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb1.Text) && !string.IsNullOrEmpty(tb2.Text) && !string.IsNullOrEmpty(tb3.Text)
                && !string.IsNullOrEmpty(tb5.Text) && !string.IsNullOrEmpty(tb6.Text) && !string.IsNullOrEmpty(tb7.Text) && !string.IsNullOrEmpty(tb8.Text) && !string.IsNullOrEmpty(tb9.Text))
            {
                potvrdi.IsEnabled = true;
            }
            else
            {
                potvrdi.IsEnabled = false;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ProfesorDTO.Adresa = GetAdress();
            profesorDAO.AddAdresa(ProfesorDTO.ToProfessor());
            mainWindow.Ucitaj();
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
