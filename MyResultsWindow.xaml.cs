using System.Windows;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class MyResultsWindow : Window
    {
        public MyResultsWindow(string login)
        {
            InitializeComponent();

            if (App.MyResultsService != null)
            {
                DataContext = new MyResultsViewModel(App.MyResultsService, login);
            }
            else
            {
                MessageBox.Show("Сервіс результатів недоступний");
                Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}