using System.Windows;
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

        public MenuWindow(
            string login,
            IAuthService authService,
            IViktorynaService viktorynaService,
            IKorystuvachSettingsService korystuvachSettingsService,
            ITopResultsService topResultsService,
            IMyResultsService myResultsService)
        {
            InitializeComponent();
            _login = login;
            _authService = authService;
            _viktorynaService = viktorynaService;
            _korystuvachSettingsService = korystuvachSettingsService;
            _topResultsService = topResultsService;
            _myResultsService = myResultsService;
            WelcomeText.Text = _login + "!";
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            var chooseWindow = new ChooseQuizWindow(
                _login,
                _authService,
                _viktorynaService,
                _korystuvachSettingsService,
                _topResultsService,
                _myResultsService);
            chooseWindow.Show();
            Close();
        }

        private void MyResults_Click(object sender, RoutedEventArgs e)
        {
            var window = new MyResultsWindow(_myResultsService, _login);
            window.Owner = this;
            window.ShowDialog();
        }

        private void TopResults_Click(object sender, RoutedEventArgs e)
        {
            var window = new TopResultsWindow(_topResultsService);
            window.Owner = this;
            window.ShowDialog();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(
                _login,
                _korystuvachSettingsService,
                _authService,
                _viktorynaService,
                _topResultsService,
                _myResultsService);
            settingsWindow.Show();
            Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(
                _authService,
                _viktorynaService,
                _korystuvachSettingsService,
                _topResultsService,
                _myResultsService);
            main.Show();
            Close();
        }
    }
}
