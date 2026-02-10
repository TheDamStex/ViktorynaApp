using System.Windows;
using ViktorynaApp.Services;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class MyResultsWindow : Window
    {
        private readonly IMyResultsService _myResultsService;

        public MyResultsWindow(IMyResultsService myResultsService, string login)
        {
            InitializeComponent();
            _myResultsService = myResultsService;
            DataContext = new MyResultsViewModel(_myResultsService, login);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
