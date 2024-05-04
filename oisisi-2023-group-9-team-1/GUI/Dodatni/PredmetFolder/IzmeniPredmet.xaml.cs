using CLI;
using CLI.Controller;
using CLI.DAO;
using CLI.Observer;
using GUI.Dodatni.Predmet;
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
    public partial class IzmeniPredmet : Window, IObserver
    {
        public GradeController _gradeController { get; set; }

        private PredmetDAO predmetiDAO;
        private MainWindow mainWindow;
        private int predmetId;

        public PredmetDTO SelectedPredmet { get; set; }
        public List<PredmetDTO> Predmeti { get; set; }


        public IzmeniPredmet(PredmetDAO predmetiiDAO, MainWindow mainWindow, int predmetId) 
        {
            InitializeComponent();
            this.predmetiDAO = predmetiiDAO;
            this.mainWindow = mainWindow;
            this.predmetId = predmetId;

            Predmeti = new List<PredmetDTO>();
            _gradeController = new GradeController();



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
                Ucitaj();
                
            }
            else
            {
                MessageBox.Show("Predmet sa datim ID-jem nije pronađen.");
                this.Close();
            }
        }

        public void Ucitaj()
        {
            Predmeti.Clear();
            foreach (Predmet p in predmetiDAO.GetAllAdresa())
            {
                Predmeti.Add(new PredmetDTO(p));
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

        private void StudentSlusaOba_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPredmet == null)
            {
                MessageBox.Show("Odaberite predmet");
            }
            else
            {
                _student



                PrikazStudenata pstud = new PrikazStudenata();
            }
        }
    }
}
