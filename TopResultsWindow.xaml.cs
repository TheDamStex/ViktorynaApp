using System.Windows;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class TopResultsWindow : Window
    {
        public TopResultsWindow()
        {
            InitializeComponent();

            if (App.TopResultsService != null)
            {
                DataContext = new TopResultsViewModel(App.TopResultsService);
            }
            else
            {
                MessageBox.Show("Сервіс недоступний");
                Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}