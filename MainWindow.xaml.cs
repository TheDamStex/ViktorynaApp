// MainWindow.xaml.cs
using System.Windows;

namespace ViktorynaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text) || string.IsNullOrWhiteSpace(ParolBox.Password))
            {
                MessageBox.Show("Заповніть всі поля");
                return;
            }

            // Перевіряємо чи ініціалізовані сервіси
            if (App.AuthService == null)
            {
                MessageBox.Show("Помилка ініціалізації системи");
                return;
            }

            var korystuvach = App.AuthService.Login(LoginBox.Text, ParolBox.Password);

            if (korystuvach == null)
            {
                MessageBox.Show("Невірний логін або пароль");
                return;
            }

            var menuWindow = new MenuWindow(korystuvach.Login);
            menuWindow.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }
    }
}