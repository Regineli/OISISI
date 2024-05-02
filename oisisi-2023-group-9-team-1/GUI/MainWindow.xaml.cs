using CLI;
using GUI.Dodatni;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CLI.DAO;
using System.ComponentModel;
using CLI.Controller;
using CLI.Observer;
using GUI.Dodatni.KatedraFolder;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private readonly AdressController _addressController;
        private readonly StudentController _studentController;
        private readonly IndexController _indexController;
        private readonly KatedraDAO _katedraDAO;


        public ObservableCollection<StudentDTO> Studenti { get; set; }
		public StudentDTO? SelectedStudent { get; set; }
		public StudentDAO studentiDAO { get; set; }

        public ObservableCollection<ProfesorDTO> Profesori { get; set; }
        public ProfesorDTO? SelectedProfesor { get; set; }
        public ProfesorDAO profesorDAO { get; set; }

		public ObservableCollection<PredmetDTO> Predmeti { get; set; }
		public PredmetDTO? SelectedPredmet { get; set; }
		public PredmetDAO predmetDAO { get; set; }

        public ObservableCollection<KatedraDTO> Katedre { get; set; }
        public KatedraDTO SelectedKatedra { get; set; }
        public KatedraDAO katedraDAO;

        public string trenutniTab;

        public String AdresaStanovanja { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            //this.Width = SystemParameters.PrimaryScreenWidth * 0.75;
            //this.Height = SystemParameters.PrimaryScreenHeight * 0.75;

            DataContext = this;
            Studenti = new ObservableCollection<StudentDTO>();
            studentiDAO = new StudentDAO();

            Profesori = new ObservableCollection<ProfesorDTO>();
            profesorDAO = new ProfesorDAO();

            Predmeti = new ObservableCollection<PredmetDTO>();
            predmetDAO = new PredmetDAO();

            Katedre = new ObservableCollection<KatedraDTO>();
            katedraDAO = new KatedraDAO();

            _studentController = new StudentController();
            _studentController.Subscribe(this);

            _addressController = new AdressController();
            _addressController.Subscribe(this);

            _indexController = new IndexController();
            _indexController.Subscribe(this);

            Ucitaj();

        }

        public void Ucitaj()
        {
            Studenti.Clear();
            foreach (Student student in studentiDAO.GetAllStudents())
            {
                Studenti.Add(new StudentDTO(student));
            }

            Profesori.Clear();
            foreach (Profesor p in profesorDAO.GetAllAdresa())
            {
                Profesori.Add(new ProfesorDTO(p));
            }

            Predmeti.Clear();
            foreach (Predmet p in predmetDAO.GetAllAdresa())
            {
                Predmeti.Add(new PredmetDTO(p));
            }

            Katedre.Clear();
            foreach(CLI.Katedra k in katedraDAO.GetAllAdresa())
            {
                Katedre.Add(new KatedraDTO(k));
            }


            ICollectionView view = CollectionViewSource.GetDefaultView(Studenti);
            trenutniTab = "Student";

            if (view != null)
            {
                view.Refresh();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (trenutniTab == "Student")
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Odaberite studenta kog zelite da obrisete!");
                }
                else
                {
                    studentiDAO.DeleteStudent(SelectedStudent.Id);
                    Ucitaj();
                }
            }
            if (trenutniTab == "Profesor")
            {
                if (SelectedProfesor == null)
                {
                    MessageBox.Show("Odaberite profesora kog zelite da obrisete!");
                }
                else
                {
                    profesorDAO.RemoveAdresa(SelectedProfesor.ID);
                    Ucitaj();
                }
            }
            else if (trenutniTab == "Predmet")
            {
                if (SelectedPredmet == null)
                {
                    MessageBox.Show("Odaberite predmet koji zelite da obrisete!");
                }
                else
                {
                    predmetDAO.RemoveAdresa(SelectedPredmet.ID);
                    Ucitaj();
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (trenutniTab == "Student")
            {
                if (SelectedStudent != null)
                {
                    int studentId = SelectedStudent.Id;
                    IzmeniStudenta noviStudent = new IzmeniStudenta(studentiDAO, this, studentId, _studentController, _addressController, _indexController);
                    noviStudent.ShowDialog();  // Koristi ShowDialog kako bi sačekao da se prozor zatvori pre nego što nastavi
                } 
                else
                {
                    MessageBox.Show("Nije odabran student.");
                }
            } else if(trenutniTab == "Profesor")
            {
                if (SelectedProfesor != null)
                {
                    int profesorId = SelectedProfesor.ID;
                    IzmeniProfesora noviProf = new IzmeniProfesora(profesorDAO, this, profesorId);
                    noviProf.ShowDialog();  
                }
                else
                {
                    MessageBox.Show("Nije odabran profesor.");
                }
            }
            else if (trenutniTab == "Predmet")
            {
                if (SelectedPredmet != null)
                {
                    int predmetId = SelectedPredmet.ID;
                    IzmeniPredmet noviPred = new IzmeniPredmet(predmetDAO, this, predmetId);
                    noviPred.ShowDialog();  
                }
                else
                {
                    MessageBox.Show("Nije odabran predmet.");
                }
            }

            else
            {
                MessageBox.Show("Odaberite tab");
            }
        }

        private void Click_New(object sender, RoutedEventArgs e)
        {
            if (trenutniTab == "Profesor")
            {
                DodajProfesora noviProf = new DodajProfesora(profesorDAO, this);
                noviProf.ShowDialog();
            } else if(trenutniTab == "Student")
            {
                DodajStudenta noviStudent = new DodajStudenta(_studentController, _addressController, _indexController);
                noviStudent.ShowDialog();
            } else if(trenutniTab == "Predmet")
            {
                DodajPredmet noviStudent = new DodajPredmet(predmetDAO, this);
                noviStudent.ShowDialog();
            }
            else
            {
                MessageBox.Show("Izaberi studenta");
            }
            
        }

        private void TabControlChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl != null && tabControl.SelectedItem != null)
            {
                string selectedTabHeader = (tabControl.SelectedItem as TabItem)?.Header.ToString();

                trenutniTab = selectedTabHeader;
            }
        }

        private void CommandBindingNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Timer_Tick(object sender, EventArgs e)
        {
            // Status Bar datum i vreme
            Vreme.Content = DateTime.Now.ToLongTimeString();
            Datum.Content = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void KatedraInfo_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedKatedra == null)
            {
                MessageBox.Show("Izaberite katedru");               
            }
            else
            {
                KatedraInfoNew katedraInfo = new KatedraInfoNew(SelectedKatedra.ID);
                katedraInfo.ShowDialog();
            }
        }
    }
}
