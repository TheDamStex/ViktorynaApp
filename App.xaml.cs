// App.xaml.cs (доповнюємо)
using System.Windows;
using ViktorynaApp.Services;
using ViktorynaApp.Validators;

namespace ViktorynaApp
{
    public partial class App : Application
    {
        public static IKorystuvachService? KorystuvachService { get; private set; }
        public static IViktorynaService? ViktorynaService { get; private set; }
        public static IQuizManager? QuizManager { get; private set; }
        public static AuthService? AuthService { get; private set; }
        public static IKorystuvachSettingsService? KorystuvachSettingsService { get; private set; } // НОВЕ
        public static ITopResultsService? TopResultsService { get; private set; }
        public static IMyResultsService? MyResultsService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitsializuvatySeryvisy();
        }

        private void InitsializuvatySeryvisy()
        {
            try
            {
                KorystuvachService = ServiceFactory.CreateKorystuvachService();
                ViktorynaService = ServiceFactory.CreateViktorynaService();
                QuizManager = ServiceFactory.CreateQuizManager();
                KorystuvachSettingsService = ServiceFactory.CreateKorystuvachSettingsService(); // НОВЕ
                TopResultsService = ServiceFactory.CreateTopResultsService();
                MyResultsService = ServiceFactory.CreateMyResultsService();

                var validator = new KorystuvachValidator();
                AuthService = new AuthService(KorystuvachService, validator);

                if (KorystuvachService == null || ViktorynaService == null ||
                    AuthService == null || KorystuvachSettingsService == null) // Додали перевірку
                {
                    MessageBox.Show("Помилка ініціалізації сервісів");
                    Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критична помилка: {ex.Message}");
                Shutdown();
            }
        }
    }
}