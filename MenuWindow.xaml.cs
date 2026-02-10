using System.Windows;
using ViktorynaApp.Factories;
using ViktorynaApp.Services;

namespace ViktorynaApp
{
    public partial class MenuWindow : Window
    {
        private readonly string _login;
        private readonly IAuthService _authService;
        private readonly IViktorynaService _viktorynaService;
        private readonly IKorystuvachSettingsService _korystuvachSettingsService;
        private readonly ITopResultsService _topResultsService;
        private readonly IMyResultsService _myResultsService;
        private readonly IWindowFactory _windowFactory;

        public MenuWindow(
            string login,
            IAuthService authService,
            IViktorynaService viktorynaService,
            IKorystuvachSettingsService korystuvachSettingsService,
            ITopResultsService topResultsService,
            IMyResultsService myResultsService,
            IWindowFactory windowFactory)
        {
            InitializeComponent();
            _login = login;
            _authService = authService;
            _viktorynaService = viktorynaService;
            _korystuvachSettingsService = korystuvachSettingsService;
            _topResultsService = topResultsService;
            _myResultsService = myResultsService;
            _windowFactory = windowFactory;
            WelcomeText.Text = _login + "!";
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            var chooseWindow = _windowFactory.CreateChooseQuizWindow(_login);
            chooseWindow.Show();
            Close();
        }

        private void MyResults_Click(object sender, RoutedEventArgs e)
        {
            var window = _windowFactory.CreateMyResultsWindow(_login);
            window.Owner = this;
            window.ShowDialog();
        }

        private void TopResults_Click(object sender, RoutedEventArgs e)
        {
            var window = _windowFactory.CreateTopResultsWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = _windowFactory.CreateSettingsWindow(_login);
            settingsWindow.Show();
            Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = _windowFactory.CreateMainWindow();
            main.Show();
            Close();
        }
    }
}
