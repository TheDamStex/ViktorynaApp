using System;
using System.Windows;
using ViktorynaApp.Models;

namespace ViktorynaApp
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
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

            // Використовуємо сервіс з App
            if (App.AuthService != null)
            {
                bool uspishno = App.AuthService.Register(novyiKorystuvach);

                if (uspishno)
                {
                    MessageBox.Show("Реєстрація успішна!");
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Помилка реєстрації. Можливо, логін вже зайнятий.");
                }
            }
            else
            {
                MessageBox.Show("Помилка ініціалізації сервісу");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}