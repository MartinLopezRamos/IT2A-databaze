using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace databaze
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public MainWindow()
        {
            InitializeComponent();

            Movies.Add(new Movie { Name = "Inception", Genre = "Sci-Fi", Length = 148, Rating = 90 });
            Movies.Add(new Movie { Name = "Titanic", Genre = "Romantický", Length = 195, Rating = 85 });
            Movies.Add(new Movie { Name = "Avengers", Genre = "Akční", Length = 143, Rating = 88 });

            DataContext = this;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Movies.Add(new Movie
            {
                Name = tbName.Text,
                Genre = tbGenre.Text,
                Length = int.Parse(tbLength.Text),
                Rating = int.Parse(tbRating.Text)
            });
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (dgFilmy.SelectedItem is Movie m)
            {
                tbName.Text = m.Name;
                tbGenre.Text = m.Genre;
                tbLength.Text = m.Length.ToString();
                tbRating.Text = m.Rating.ToString();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dgFilmy.SelectedItem is Movie m)
            {
                m.Name = tbName.Text;
                m.Genre = tbGenre.Text;
                m.Length = int.Parse(tbLength.Text);
                m.Rating = int.Parse(tbRating.Text);
                dgFilmy.Items.Refresh();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgFilmy.SelectedItem is Movie m)
                Movies.Remove(m);
        }

        private void dgFilmy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}