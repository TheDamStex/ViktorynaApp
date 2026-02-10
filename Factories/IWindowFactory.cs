using ViktorynaApp.ViewModels;

namespace ViktorynaApp.Factories
{
    public interface IWindowFactory
    {
        MainWindow CreateMainWindow();
        MenuWindow CreateMenuWindow(string login);
        RegisterWindow CreateRegisterWindow();
        ChooseQuizWindow CreateChooseQuizWindow(string login);
        ViktorynaWindow CreateViktorynaWindow(string login, string category);
        SettingsWindow CreateSettingsWindow(string login);
        TopResultsWindow CreateTopResultsWindow();
        MyResultsWindow CreateMyResultsWindow(string login);

        // Фабричні методи для ViewModel'ів, щоб одразу передати вікнам DataContext.
        SettingsViewModel CreateSettingsViewModel(string login);
        TopResultsViewModel CreateTopResultsViewModel();
        MyResultsViewModel CreateMyResultsViewModel(string login);
    }
}
