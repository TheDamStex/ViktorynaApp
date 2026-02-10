// App.xaml.cs (доповнюємо)
using System.Windows;
using ViktorynaApp.Services;
using ViktorynaApp.Validators;

namespace ViktorynaApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitsializuvatyTaPokazatyGolovneVikno();
        }

        private void InitsializuvatyTaPokazatyGolovneVikno()
        {
            try
            {
                var korystuvachService = ServiceFactory.CreateKorystuvachService();
                var viktorynaService = ServiceFactory.CreateViktorynaService();
                var korystuvachSettingsService = ServiceFactory.CreateKorystuvachSettingsService();
                var topResultsService = ServiceFactory.CreateTopResultsService();
                var myResultsService = ServiceFactory.CreateMyResultsService();

                var dataValidator = new DataValidator();
                var validator = new KorystuvachValidator(dataValidator);
                var authService = new AuthService(korystuvachService, validator);

                var mainWindow = new MainWindow(
                    authService,
                    viktorynaService,
                    korystuvachSettingsService,
                    topResultsService,
                    myResultsService);

                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критична помилка: {ex.Message}");
                Shutdown();
            }
        }
    }
}
