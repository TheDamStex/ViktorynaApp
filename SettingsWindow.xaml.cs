using System.Windows;
using System.Windows.Media;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel? _viewModel; // Додаємо ?

        public SettingsWindow(string login)
        {
            InitializeComponent();

            if (App.KorystuvachSettingsService == null)
            {
                MessageBox.Show("Помилка ініціалізації сервісу налаштувань");
                Close();
                return;
            }

            _viewModel = new SettingsViewModel(App.KorystuvachSettingsService, login);
            _viewModel.OnCloseRequested += () =>
            {
                var menuWindow = new MenuWindow(login);
                menuWindow.Show();
                Close();
            };

            DataContext = _viewModel;

            // Прив'язка PasswordBox до ViewModel
            StaryiParolBox.PasswordChanged += (s, e) =>
            {
                if (_viewModel != null)
                    _viewModel.StaryiParol = StaryiParolBox.Password;
            };

            NovyiParolBox.PasswordChanged += (s, e) =>
            {
                if (_viewModel != null)
                    _viewModel.NovyiParol = NovyiParolBox.Password;
            };

            PidtverdzhenniaParolBox.PasswordChanged += (s, e) =>
            {
                if (_viewModel != null)
                    _viewModel.PidtverdzhenniaParol = PidtverdzhenniaParolBox.Password;
            };

            // Підписка на зміну статусу для оновлення кольору
            if (_viewModel != null)
            {
                _viewModel.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(SettingsViewModel.StatusMessage) ||
                        e.PropertyName == nameof(SettingsViewModel.IsStatusSuccess))
                    {
                        UpdateStatusBorder();
                    }
                };
            }
        }

        private void UpdateStatusBorder()
        {
            if (_viewModel == null || string.IsNullOrWhiteSpace(_viewModel.StatusMessage))
            {
                StatusBorder.Visibility = Visibility.Collapsed;
                return;
            }

            StatusBorder.Visibility = Visibility.Visible;
            StatusBorder.Background = _viewModel.IsStatusSuccess
                ? new SolidColorBrush(Color.FromRgb(76, 175, 80))  // Зелений
                : new SolidColorBrush(Color.FromRgb(244, 67, 54)); // Червоний
        }
    }
}