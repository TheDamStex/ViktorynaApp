// MainWindow.xaml.cs
using System.Windows;
using ViktorynaApp.Services;

namespace ViktorynaApp
{
    public partial class MainWindow : Window
    {
        private readonly IAuthService _authService;
        private readonly IViktorynaService _viktorynaService;
        private readonly IKorystuvachSettingsService _korystuvachSettingsService;
        private readonly ITopResultsService _topResultsService;
        private readonly IMyResultsService _myResultsService;

        public MainWindow(
            IAuthService authService,
            IViktorynaService viktorynaService,
            IKorystuvachSettingsService korystuvachSettingsService,
            ITopResultsService topResultsService,
            IMyResultsService myResultsService)
        {
            InitializeComponent();
            _authService = authService;
            _viktorynaService = viktorynaService;
            _korystuvachSettingsService = korystuvachSettingsService;
            _topResultsService = topResultsService;
            _myResultsService = myResultsService;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text) || string.IsNullOrWhiteSpace(ParolBox.Password))
            {
                MessageBox.Show("Заповніть всі поля");
                return;
            }

            var korystuvach = _authService.Login(LoginBox.Text, ParolBox.Password);

            if (korystuvach == null)
            {
                MessageBox.Show("Невірний логін або пароль");
                return;
            }

            var menuWindow = new MenuWindow(
                korystuvach.Login,
                _authService,
                _viktorynaService,
                _korystuvachSettingsService,
                _topResultsService,
                _myResultsService);
            menuWindow.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(
                _authService,
                _viktorynaService,
                _korystuvachSettingsService,
                _topResultsService,
                _myResultsService);
            registerWindow.Show();
            Close();
        }
    }
}
