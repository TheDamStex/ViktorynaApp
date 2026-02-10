using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
        private List<string> _categories = new List<string>(); // Ініціалізуємо

        public ChooseQuizWindow(
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
            var quizWindow = new ViktorynaWindow(
                _login,
                selectedCategory,
                _authService,
                _viktorynaService,
                _korystuvachSettingsService,
                _topResultsService,
                _myResultsService);
            quizWindow.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var menuWindow = new MenuWindow(
                _login,
                _authService,
                _viktorynaService,
                _korystuvachSettingsService,
                _topResultsService,
                _myResultsService);
            menuWindow.Show();
            Close();
        }
    }
}
