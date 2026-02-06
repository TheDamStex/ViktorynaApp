using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ViktorynaApp.Models;

namespace ViktorynaApp
{
    public partial class ViktorynaWindow : Window
    {
        private string _login;
        private string _category;
        private List<Pytannia> _questions = new List<Pytannia>(); // Ініціалізуємо
        private int _currentQuestionIndex = 0;
        private List<List<bool>> _userAnswers = new List<List<bool>>(); // Ініціалізуємо

        public ViktorynaWindow(string login, string category)
        {
            InitializeComponent();
            _login = login;
            _category = category;
            PochatyViktorynu();
        }

        private void PochatyViktorynu()
        {
            if (App.ViktorynaService == null)
            {
                MessageBox.Show("Помилка завантаження питань");
                Close();
                return;
            }

            _questions = App.ViktorynaService.OtrymatyPytannia(_category);

            if (_questions == null || _questions.Count == 0)
            {
                MessageBox.Show("Немає питань для обраної категорії");
                Close();
                return;
            }

            // Ініціалізуємо масив для зберігання відповідей
            _userAnswers = new List<List<bool>>();
            foreach (var question in _questions)
            {
                _userAnswers.Add(new List<bool>(new bool[question.Varianty.Count]));
            }

            TitleText.Text = $"Вікторина: {_category}";
            PokazatyPytannia(0);
        }

        private void PokazatyPytannia(int index)
        {
            if (index < 0 || index >= _questions.Count)
                return;

            _currentQuestionIndex = index;
            var question = _questions[index];

            QuestionText.Text = question.Tekst;
            QuestionCounter.Text = $"Питання {index + 1} з {_questions.Count}";

            // Очищаємо список варіантів
            AnswersList.Items.Clear();

            // Додаємо варіанти відповідей
            for (int i = 0; i < question.Varianty.Count; i++)
            {
                var checkBox = new CheckBox
                {
                    Content = question.Varianty[i],
                    IsChecked = _userAnswers[index][i],
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                int answerIndex = i; // Для замикання
                checkBox.Checked += (s, e) => _userAnswers[index][answerIndex] = true;
                checkBox.Unchecked += (s, e) => _userAnswers[index][answerIndex] = false;

                AnswersList.Items.Add(checkBox);
            }

            // Оновлюємо кнопки
            PrevButton.IsEnabled = (index > 0);
            NextButton.Content = (index == _questions.Count - 1) ? "Завершити" : "Наступне";
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            ZberehtyVidpovidi();
            PokazatyPytannia(_currentQuestionIndex - 1);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            ZberehtyVidpovidi();

            if (_currentQuestionIndex == _questions.Count - 1)
            {
                ZakinchytyViktorynu();
            }
            else
            {
                PokazatyPytannia(_currentQuestionIndex + 1);
            }
        }

        private void ZberehtyVidpovidi()
        {
            // Зберігаємо відповіді з CheckBox'ів
            for (int i = 0; i < AnswersList.Items.Count; i++)
            {
                if (AnswersList.Items[i] is CheckBox checkBox)
                {
                    _userAnswers[_currentQuestionIndex][i] = checkBox.IsChecked ?? false;
                }
            }
        }

        private void ZakinchytyViktorynu()
        {
            ZberehtyVidpovidi();

            // Підраховуємо правильні відповіді
            int correctCount = 0;

            for (int i = 0; i < _questions.Count; i++)
            {
                if (ChyVidpovidPravylna(i))
                    correctCount++;
            }

            // Зберігаємо результат
            var result = new Rezultat
            {
                Login = _login,
                Rozdil = _category,
                KilkistPravylnyh = correctCount,
                TotalQuestions = _questions.Count,
                DataVykonannia = DateTime.Now // Додаємо поточну дату
            };

            if (App.ViktorynaService != null)
            {
                App.ViktorynaService.DodatyRezultat(result);
            }

            // Показуємо результат
            MessageBox.Show($"Вікторину завершено!\n" +
                          $"Правильних відповідей: {correctCount} з {_questions.Count}\n" +
                          $"Ваш результат збережено.");

            // Повертаємось у меню
            var menuWindow = new MenuWindow(_login);
            menuWindow.Show();
            Close();
        }

        private bool ChyVidpovidPravylna(int questionIndex)
        {
            var question = _questions[questionIndex];
            var userAnswer = _userAnswers[questionIndex];

            // Перевіряємо чи всі правильні відповіді обрані
            for (int i = 0; i < question.Varianty.Count; i++)
            {
                bool isCorrectAnswer = question.PravylnyIndex.Contains(i);
                bool isUserSelected = userAnswer[i];

                // Якщо користувач не обрав правильну відповідь або обрав неправильну
                if (isCorrectAnswer != isUserSelected)
                    return false;
            }

            return true;
        }
    }
}
