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

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for IzmeniProfesora.xaml
    /// </summary>
    public partial class IzmeniProfesora : Window
    {
        private ProfesorDAO profesoriDAO;
        private MainWindow mainWindow;
        private int profesorId;

        public IzmeniProfesora(ProfesorDAO profesoriDAO, MainWindow mainWindow, int profesorId)
        {
            InitializeComponent();
            this.profesoriDAO = profesoriDAO;
            this.mainWindow = mainWindow;
            this.profesorId = profesorId;

            Profesor profesor = profesoriDAO.GetAdresaById(profesorId);
            if (profesor != null)
            {
                // Popunjavamo vrednosti na poljima vrednostima profesora
                Tb1.Text = profesor.prezime;
                Tb2.Text = profesor.ime;
                Tb3.Text = profesor.datumRodjenja;
                Tb4.Text = profesor.adresaStanovanja;
                Tb5.Text = profesor.kontaktTelefon;
                Tb6.Text = profesor.eMailAdresa;
                Tb7.Text = profesor.brLicneKarte;
                Tb8.Text = profesor.zvanje;
                Tb9.Text = profesor.godineStaza;

                
            }
            else
            {
                MessageBox.Show("Profesor sa datim ID-jem nije pronađen.");
                this.Close();
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
                profesor.adresaStanovanja = Tb4.Text; 
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
    }
}
