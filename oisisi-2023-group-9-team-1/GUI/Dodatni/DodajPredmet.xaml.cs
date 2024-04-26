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

namespace GUI.Dodatni
{
    /// <summary>
    /// Interaction logic for DodajPredmet.xaml
    /// </summary>
    public partial class DodajPredmet : Window
    {
        public PredmetDTO Predmet { get; set; }

        private PredmetDAO predmetiDAO;
        private MainWindow mainWindow;

        public DodajPredmet(PredmetDAO predmetiDAO, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Predmet = new PredmetDTO();
            this.predmetiDAO = predmetiDAO;
            this.mainWindow = mainWindow;
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb1.Text) && !string.IsNullOrEmpty(tb2.Text) && !string.IsNullOrEmpty(tb3.Text) && cb1.SelectedIndex != -1 && cb2.SelectedIndex != -1)
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
            predmetiDAO.AddAdresa(Predmet.ToPredmet());
            mainWindow.Ucitaj();
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
