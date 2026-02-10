using System;
using System.Windows;
using ViktorynaApp.Models;
using ViktorynaApp.Factories;
using ViktorynaApp.Services;

namespace ViktorynaApp
{
    public partial class RegisterWindow : Window
    {
        private readonly IAuthService _authService;
        private readonly IViktorynaService _viktorynaService;
        private readonly IKorystuvachSettingsService _korystuvachSettingsService;
        private readonly ITopResultsService _topResultsService;
        private readonly IMyResultsService _myResultsService;
        private readonly IWindowFactory _windowFactory;

        public RegisterWindow(
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка на пусті поля
            if (string.IsNullOrWhiteSpace(LoginBox.Text) ||
                string.IsNullOrWhiteSpace(ParolBox.Password) ||
                DataPicker.SelectedDate == null)
            {
                MessageBox.Show("Заповніть всі поля");
                return;
            }

            var novyiKorystuvach = new Korystuvach
            {
                Login = LoginBox.Text,
                Parol = ParolBox.Password,
                // Беремо дату з DatePicker
                DataNarodzhennia = DataPicker.SelectedDate.Value.ToString("dd.MM.yyyy")
            };

            bool uspishno = _authService.Register(novyiKorystuvach);

            if (uspishno)
            {
                MessageBox.Show("Реєстрація успішна!");
                MainWindow main = _windowFactory.CreateMainWindow();
                main.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Помилка реєстрації. Можливо, логін вже зайнятий.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = _windowFactory.CreateMainWindow();
            main.Show();
            Close();
        }
    }
}
