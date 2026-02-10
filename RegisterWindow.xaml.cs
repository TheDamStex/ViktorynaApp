using System;
using System.Windows;
using ViktorynaApp.Models;
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

        public RegisterWindow(
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
                MainWindow main = new MainWindow(
                    _authService,
                    _viktorynaService,
                    _korystuvachSettingsService,
                    _topResultsService,
                    _myResultsService);
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
