using System.Windows;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class MyResultsWindow : Window
    {
        public MyResultsWindow(MyResultsViewModel viewModel)
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
