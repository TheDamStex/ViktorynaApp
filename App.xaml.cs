// App.xaml.cs (доповнюємо)
using System.Windows;
using ViktorynaApp.Factories;
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
                var dataValidator = new DataValidator();

                // Абстрактна фабрика створює пов'язані сервіси даних.
                IAppServicesFactory appServicesFactory = new JsonAppServicesFactory();

                var korystuvachService = appServicesFactory.CreateKorystuvachService();
                var viktorynaService = appServicesFactory.CreateViktorynaService();
                var topResultsService = appServicesFactory.CreateTopResultsService();
                var myResultsService = appServicesFactory.CreateMyResultsService();
                var korystuvachSettingsService = new KorystuvachSettingsService(
                    korystuvachService,
                    dataValidator,
                    appServicesFactory.CreateKorystuvachDaniService());

                var validator = new KorystuvachValidator(dataValidator);
                var authService = new AuthService(korystuvachService, validator);

                // Фабрика вікон одразу підставляє залежності та DataContext.
                IWindowFactory windowFactory = new WindowFactory(
                    authService,
                    viktorynaService,
                    korystuvachSettingsService,
                    topResultsService,
                    myResultsService);

                var mainWindow = windowFactory.CreateMainWindow();
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
