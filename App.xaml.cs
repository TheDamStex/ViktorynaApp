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
                IDataStorage<Korystuvach> korystuvachiDataStorage = new JsonStorage<Korystuvach>("korystuvachi.json");
                IDataStorage<Pytannia> pytanniaDataStorage = new JsonStorage<Pytannia>("pytannia.json");
                IDataStorage<Rezultat> rezultatyDataStorage = new JsonStorage<Rezultat>("rezultaty.json");

                var korystuvachiStorage = new JsonDaniService<Korystuvach>(korystuvachiDataStorage);
                var pytanniaStorage = new JsonDaniService<Pytannia>(pytanniaDataStorage);
                var rezultatyStorage = new JsonDaniService<Rezultat>(rezultatyDataStorage);

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
