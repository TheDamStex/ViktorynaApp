using System.Windows;
using System.Windows.Media;
using ViktorynaApp.Factories;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp
{
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel? _viewModel; // Додаємо ?
        private readonly string _login;
        private readonly IWindowFactory _windowFactory;

        public SettingsWindow(
            SettingsViewModel viewModel,
            string login,
            IWindowFactory windowFactory)
        {
            InitializeComponent();
            _login = login;
            _windowFactory = windowFactory;

            _viewModel = viewModel;
            _viewModel.OnCloseRequested += () =>
            {
                var menuWindow = _windowFactory.CreateMenuWindow(_login);
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
