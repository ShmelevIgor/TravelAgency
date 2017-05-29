using System.Windows;
using System.Windows.Controls;
using TravelAgency.Core;

namespace TravelAgency.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ToursRepository toursRepository;

        public MainWindow()
        {
            InitializeComponent();

            toursRepository = new ToursRepository();

            toursGrid.ItemsSource = toursRepository.GetTours();
        }

        private void toursGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (toursGrid.SelectedItem != null)
            {
                UpdateButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }
            else
            {
                UpdateButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow(toursRepository);
            searchWindow.DataContext = this;
            searchWindow.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(toursRepository);
            addWindow.DataContext = this;
            addWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateWindow updateWindow = new UpdateWindow(toursRepository, (Tour)toursGrid.SelectedItem);
            updateWindow.DataContext = this;
            updateWindow.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Tour tour = (Tour)toursGrid.SelectedItem;
            toursRepository.DeleteTour(tour.Id);
            toursGrid.ItemsSource = null;
            toursGrid.ItemsSource = toursRepository.GetTours();
        }

        private void showAllButton_Click(object sender, RoutedEventArgs e)
        {
            toursGrid.ItemsSource = toursRepository.GetTours();
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            toursRepository.SaveChanges();
            MessageBoxResult result = MessageBox.Show("Все изменения сохранены!", "", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
                return;
        }
    }
}
