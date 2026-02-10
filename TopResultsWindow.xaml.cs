using System.Windows;
using ViktorynaApp.Services;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class TopResultsWindow : Window
    {
        private readonly ITopResultsService _topResultsService;

        public TopResultsWindow(ITopResultsService topResultsService)
        {
            InitializeComponent();
            _topResultsService = topResultsService;
            DataContext = new TopResultsViewModel(_topResultsService);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
