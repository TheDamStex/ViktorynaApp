// MainWindow.xaml.cs
using System.Windows;
using ViktorynaApp.Factories;
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
        private readonly IWindowFactory _windowFactory;

        public MainWindow(
            IAuthService authService,
            IViktorynaService viktorynaService,
            IKorystuvachSettingsService korystuvachSettingsService,
            ITopResultsService topResultsService,
            IMyResultsService myResultsService,
            IWindowFactory windowFactory)
        {
            InitializeComponent();
            _authService = authService;
            _viktorynaService = viktorynaService;
            _korystuvachSettingsService = korystuvachSettingsService;
            _topResultsService = topResultsService;
            _myResultsService = myResultsService;
            _windowFactory = windowFactory;
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

            var menuWindow = _windowFactory.CreateMenuWindow(korystuvach.Login);
            menuWindow.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = _windowFactory.CreateRegisterWindow();
            registerWindow.Show();
            Close();
        }
    }
}
