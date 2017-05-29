using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.Core;

namespace TravelAgency.UI
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        ToursRepository toursRepository;
        int[] twentyEightDays = new int[28], thirtyDays = new int[30], thirtyOneDays = new int[31], months = new int[12], years = new int[] { 2017, 2018 };

        double[] ratings = new double[] { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };

        public AddWindow(ToursRepository toursRepository)
        {
            InitializeComponent();

            this.toursRepository = toursRepository;

            for (int i = 0; i < 31; i++)
            {
                if (i < 12)
                    months[i] = i + 1;
                if (i < 28)
                    twentyEightDays[i] = i + 1;
                if (i < 30)
                    thirtyDays[i] = i + 1;
                thirtyOneDays[i] = i + 1;
            }

            dayToComboBox.ItemsSource = thirtyOneDays;
            monthToComboBox.ItemsSource = months;
            yearToComboBox.ItemsSource = years;
            dayFromComboBox.ItemsSource = thirtyOneDays;
            monthFromComboBox.ItemsSource = months;
            yearFromComboBox.ItemsSource = years;
            ratingComboBox.ItemsSource = ratings;
        }

        private void priceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;

            e.Handled = !(key >= 34 && key <= 43 ||
                key >= 74 && key <= 83 || key == 2);
        }

        private void addTourButton_Click(object sender, RoutedEventArgs e)
        {
            Tour tour = null;
            try
            {
                tour = new Tour()
                {
                    Direction = directionTextBox.Text,
                    ToDate = new DateTime((int)yearToComboBox.SelectedItem, (int)monthToComboBox.SelectedItem, (int)dayToComboBox.SelectedItem),
                    FromDate = new DateTime((int)yearFromComboBox.SelectedItem, (int)monthFromComboBox.SelectedItem, (int)dayFromComboBox.SelectedItem),
                    Hotel = hotelTextBox.Text,
                    Rating = (double)ratingComboBox.SelectedItem,
                    Price = int.Parse(priceTextBox.Text)
                };

                if (tour.ToDate >= tour.FromDate)
                    throw new Exception();
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Необходимо корректно заполнить все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    return;
            }

            toursRepository.AddTour(tour);

            MainWindow mainWindow = (MainWindow)DataContext;
            mainWindow.toursGrid.ItemsSource = null;
            mainWindow.toursGrid.ItemsSource = toursRepository.GetTours();
            Close();

        }

        private void monthToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedMonth = (int)monthToComboBox.SelectedItem;

            if (selectedMonth < 8)
            {
                if (selectedMonth % 2 == 0)
                {
                    if (selectedMonth == 2)
                        dayToComboBox.ItemsSource = twentyEightDays;
                    else dayToComboBox.ItemsSource = thirtyDays;
                }
                else dayToComboBox.ItemsSource = thirtyOneDays;
            }
            else
            {
                if (selectedMonth % 2 == 0)
                    dayToComboBox.ItemsSource = thirtyOneDays;
                else dayToComboBox.ItemsSource = thirtyDays;
            }
        }

        private void monthFromComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            int selectedMonth = (int)monthFromComboBox.SelectedItem;

            if (selectedMonth < 8)
            {
                if (selectedMonth % 2 == 0)
                {
                    if (selectedMonth == 2)
                        dayFromComboBox.ItemsSource = twentyEightDays;
                    else dayFromComboBox.ItemsSource = thirtyDays;
                }
                else dayFromComboBox.ItemsSource = thirtyOneDays;
            }
            else
            {
                if (selectedMonth % 2 == 0)
                    dayFromComboBox.ItemsSource = thirtyOneDays;
                else dayFromComboBox.ItemsSource = thirtyDays;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
