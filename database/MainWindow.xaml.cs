using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;

namespace database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Film> Filmy { get; set; } = new ObservableCollection<Film>();
        private Film vybranyFilm = null;
        private int nextId = 1;

        public MainWindow()
        {
            InitializeComponent();
            dgFilmy.ItemsSource = Filmy;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dgFilmy.ItemsSource);
            view.Filter = FilterFilmy;
        }
        private bool FilterFilmy(object item)
        {
            if (string.IsNullOrEmpty(tbFilter.Text))
                return true;

            Film film = item as Film;

            return film.Nazev.ToLower().Contains(tbFilter.Text.ToLower());
        }
        private void tbFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgFilmy.ItemsSource).Refresh();
        }
        public class Film
        {
            public int Id { get; set; }
            public string Nazev { get; set; }
            public string Reziser { get; set; }
            public int Rok { get; set; }
            public string Zanr { get; set; }
            public int Pocet { get; set; }
            public bool Dostupny { get; set; }
        }

        private void BtnUlozit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNazev.Text))
            {
                MessageBox.Show("Název je povinný");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbReziser.Text))
            {
                MessageBox.Show("Režisér je povinný");
                return;
            }
            if (!int.TryParse(tbRok.Text, out int rok) || rok < 1800)
            {
                MessageBox.Show("Neplatný rok");
                return;
            }
            if (!int.TryParse(tbPocet.Text, out int pocet) || pocet < 0)
            {
                MessageBox.Show("Neplatný počet kusů");
                return;
            }
            if (vybranyFilm == null)
            {
                Filmy.Add(new Film
                {
                    Id = nextId++,
                    Nazev = tbNazev.Text,
                    Reziser = tbReziser.Text,
                    Rok = rok,
                    Zanr = tbZanr.Text,
                    Pocet = pocet,
                    Dostupny = cbDostupny.IsChecked == true
                });
            }
            else
            {
                vybranyFilm.Nazev = tbNazev.Text;
                vybranyFilm.Reziser = tbReziser.Text;
                vybranyFilm.Rok = rok;
                vybranyFilm.Zanr = tbZanr.Text;
                vybranyFilm.Pocet = pocet;
                vybranyFilm.Dostupny = cbDostupny.IsChecked == true;

                dgFilmy.Items.Refresh();
            }
            VymazFormular();
        }
        private void BtnSmazat_Click(object sender, RoutedEventArgs e)
        {
            if (vybranyFilm == null)
            {
                MessageBox.Show("Vyber film");
                return;
            }

            if (MessageBox.Show("Opravdu smazat?", "Potvrzení", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Filmy.Remove(vybranyFilm);
                VymazFormular();
            }
        }
        private void dgFilmy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            vybranyFilm = dgFilmy.SelectedItem as Film;

            if (vybranyFilm != null)
            {
                tbNazev.Text = vybranyFilm.Nazev;
                tbReziser.Text = vybranyFilm.Reziser;
                tbRok.Text = vybranyFilm.Rok.ToString();
                tbZanr.Text = vybranyFilm.Zanr;
                tbPocet.Text = vybranyFilm.Pocet.ToString();
                cbDostupny.IsChecked = vybranyFilm.Dostupny;
            }
        }
        private void VymazFormular()
        {
            tbNazev.Text = "";
            tbReziser.Text = "";
            tbRok.Text = "";
            tbZanr.Text = "";
            tbPocet.Text = "";
            cbDostupny.IsChecked = false;
            vybranyFilm = null;
            dgFilmy.SelectedItem = null;
        }
    }
}