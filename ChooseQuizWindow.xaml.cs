using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ViktorynaApp.Factories;
using ViktorynaApp.Services;

namespace ViktorynaApp
{
    public partial class ChooseQuizWindow : Window
    {
        private readonly string _login;
        private readonly IAuthService _authService;
        private readonly IViktorynaService _viktorynaService;
        private readonly IKorystuvachSettingsService _korystuvachSettingsService;
        private readonly ITopResultsService _topResultsService;
        private readonly IMyResultsService _myResultsService;
        private readonly IWindowFactory _windowFactory;
        private List<string> _categories = new List<string>(); // Ініціалізуємо

        public ChooseQuizWindow(
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
            ZavantazhytyKategorii();
        }

        private void ZavantazhytyKategorii()
        {
            _categories = new List<string>
            {
                "Історія",
                "Географія",
                "Біологія",
                "Фізика",
                "Хімія",
                "Література",
                "Змішана"
            };

            // Можна також отримати категорії з сервісу
            var rozdily = _viktorynaService.OtrymatyVsiRozdily();
            if (rozdily != null && rozdily.Any())
            {
                _categories = rozdily;
                _categories.Add("Змішана");
            }

            CategoriesList.ItemsSource = _categories;

            // Вибираємо першу категорію за замовчуванням
            if (_categories.Any())
                CategoriesList.SelectedIndex = 0;
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesList.SelectedItem == null)
            {
                MessageBox.Show("Оберіть категорію");
                return;
            }

            string selectedCategory = CategoriesList.SelectedItem.ToString()!;
            var quizWindow = _windowFactory.CreateViktorynaWindow(_login, selectedCategory);
            quizWindow.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var menuWindow = _windowFactory.CreateMenuWindow(_login);
            menuWindow.Show();
            Close();
        }
    }
}
