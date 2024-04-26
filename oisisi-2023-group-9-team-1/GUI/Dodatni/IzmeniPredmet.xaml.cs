using CLI;
using CLI.DAO;
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
using static CLI.Predmet;

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for IzmeniPredmet.xaml
    /// </summary>
    public partial class IzmeniPredmet : Window
    {
        private PredmetDAO predmetiDAO;
        private MainWindow mainWindow;
        private int predmetId;

        public IzmeniPredmet(PredmetDAO predmetiiDAO, MainWindow mainWindow, int predmetId)
        {
            InitializeComponent();
            this.predmetiDAO = predmetiiDAO;
            this.mainWindow = mainWindow;
            this.predmetId = predmetId;

            
            if (predmetiDAO == null)
            {
                MessageBox.Show("SubjectDAO je null.");
                this.Close();
                return;
            }

            
            Predmet predmet = predmetiDAO.GetAdresaById(predmetId);
            if (predmet != null)
            {

                Tb1.Text = predmet.sifra;
                Tb2.Text = predmet.naziv;
                Tb3.Text = predmet.brESPB.ToString();
                ComboBoxGodStudija.SelectedItem = predmet.godinaStudijaIzvodjenja;
                ComboBoxSemestar.SelectedItem = predmet.predmetSemestar;

                
            }
            else
            {
                MessageBox.Show("Predmet sa datim ID-jem nije pronađen.");
                this.Close();
            }
        }

        

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            
            Predmet predmet = predmetiDAO.GetAdresaById(predmetId);
            if (predmet != null)
            {
                try
                {
                    predmet.sifra = Tb1.Text;
                    predmet.naziv = Tb2.Text;
                    predmet.brESPB = int.Parse(Tb3.Text);

                    if (ComboBoxGodStudija.SelectedItem is ComboBoxItem selectedItemGodinaStudija)
                    {
                        predmet.godinaStudijaIzvodjenja = int.Parse(selectedItemGodinaStudija.Content.ToString());
                    }

                    if (ComboBoxSemestar.SelectedItem is ComboBoxItem selectedItemSemestar)
                    {
                        predmet.predmetSemestar = (Semestar)Enum.Parse(typeof(Semestar), selectedItemSemestar.Content.ToString());
                    }

                    mainWindow.Ucitaj();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Došlo je do greške prilikom čuvanja izmena: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Predmet sa datim ID-jem nije pronađen.");
            }
        }



        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
