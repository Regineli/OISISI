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
            DataContext = this;
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

            
            CLI.Predmet predmet = predmetiDAO.GetAdresaById(predmetId);
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
            foreach (CLI.Predmet p in predmetiDAO.GetAllAdresa())
            {
                Predmeti.Add(new PredmetDTO(p));
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            
            CLI.Predmet predmet = predmetiDAO.GetAdresaById(predmetId);
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
                List<StudentDTO> studenti = new List<StudentDTO>();
                List<int> studentIds = new List<int>();

                foreach(Ocena o in _gradeController.GetPredmetGrades(SelectedPredmet.ID)) //svi koji slusaju jedan predmet, ima duplikata studenta
                {                                                                    // ako student slusa i drugi predmet i nije u listi dodaj ga
                    if (_gradeController.StudentSlusaPredmet(o.StudentID, predmetId) && !(studentIds.Contains(o.StudentID)))    
                    {
                        studenti.Add(new StudentDTO(o.studentPolozio));
                        studentIds.Add(o.StudentID);
                    }
                }

                PrikazStudenata pstud = new PrikazStudenata(studenti);
                pstud.ShowDialog();
            }
        }

        private void StudentPolozeioPrvi_Click(object sender, RoutedEventArgs e)
        {
            List<StudentDTO> studenti = new List<StudentDTO>();
            List<int> studentIds = new List<int>();

            foreach (Ocena o in _gradeController.GetStudentiPoloziliPredmet(predmetId)) //svi koji su polozili prvi
            {                                                                    // ako student slusa i drugi predmet i nije u listi dodaj ga
                if (_gradeController.StudentNijePolozioPredmet(o.StudentID, SelectedPredmet.ID) && !(studentIds.Contains(o.StudentID)))
                {
                    studenti.Add(new StudentDTO(o.studentPolozio));
                    studentIds.Add(o.StudentID);
                }
            }

            PrikazStudenata pstud = new PrikazStudenata(studenti);
            pstud.ShowDialog();
        }

        private void StudentPolozioDrugi_Click(object sender, RoutedEventArgs e)
        {
            List<StudentDTO> studenti = new List<StudentDTO>();
            List<int> studentIds = new List<int>();

            foreach (Ocena o in _gradeController.GetStudentiPoloziliPredmet(SelectedPredmet.ID)) //svi koji su polozili drugi
            {                                                                    // ako student slusa i drugi predmet i nije u listi dodaj ga
                if (_gradeController.StudentNijePolozioPredmet(o.StudentID, predmetId) && !(studentIds.Contains(o.StudentID)))
                {
                    studenti.Add(new StudentDTO(o.studentPolozio));
                    studentIds.Add(o.StudentID);
                }
            }

            PrikazStudenata pstud = new PrikazStudenata(studenti);
            pstud.ShowDialog();
        }
    }
}
