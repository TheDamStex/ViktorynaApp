using System.Windows;

namespace ViktorynaApp
{
    public partial class MenuWindow : Window
    {
        private string _login;

        public MenuWindow(string login)
        {
            InitializeComponent();
            _login = login;
            WelcomeText.Text = _login + "!";
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            var chooseWindow = new ChooseQuizWindow(_login);
            chooseWindow.Show();
            Close();
        }

        private void MyResults_Click(object sender, RoutedEventArgs e)
        {
            var window = new MyResultsWindow(_login);
            window.Owner = this;
            window.ShowDialog();
        }

        private void TopResults_Click(object sender, RoutedEventArgs e)
        {
            var window = new TopResultsWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (App.KorystuvachSettingsService != null)
            {
                var settingsWindow = new SettingsWindow(_login);
                settingsWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Сервіс налаштувань недоступний");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}