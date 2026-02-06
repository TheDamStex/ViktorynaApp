using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ViktorynaApp
{
    public partial class ChooseQuizWindow : Window
    {
        private string _login;
        private List<string> _categories = new List<string>(); // Ініціалізуємо

        public ChooseQuizWindow(string login)
        {
            InitializeComponent();
            _login = login;
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
            if (App.ViktorynaService != null)
            {
                var rozdily = App.ViktorynaService.OtrymatyVsiRozdily();
                if (rozdily != null && rozdily.Any())
                {
                    _categories = rozdily;
                    _categories.Add("Змішана");
                }
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
            var quizWindow = new ViktorynaWindow(_login, selectedCategory);
            quizWindow.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var menuWindow = new MenuWindow(_login);
            menuWindow.Show();
            Close();
        }
    }
}