using System.Windows;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class TopResultsWindow : Window
    {
        public TopResultsWindow(TopResultsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
