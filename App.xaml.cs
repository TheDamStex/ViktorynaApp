// App.xaml.cs (доповнюємо)
using System.Windows;
using ViktorynaApp.Models;
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
                var korystuvachiStorage = new JsonDaniService<Korystuvach>("korystuvachi.json");
                var pytanniaStorage = new JsonDaniService<Pytannia>("pytannia.json");
                var rezultatyStorage = new JsonDaniService<Rezultat>("rezultaty.json");

                var dataValidator = new DataValidator();

                var korystuvachService = new KorystuvachService(korystuvachiStorage);
                var viktorynaService = new ViktorynaService(pytanniaStorage, rezultatyStorage);
                var korystuvachSettingsService = new KorystuvachSettingsService(
                    korystuvachService,
                    dataValidator,
                    korystuvachiStorage);
                var topResultsService = new TopResultsService(rezultatyStorage);
                var myResultsService = new MyResultsService(rezultatyStorage);

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
