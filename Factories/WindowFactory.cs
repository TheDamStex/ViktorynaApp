using ViktorynaApp.Services;
using ViktorynaApp.ViewModels;

namespace ViktorynaApp.Factories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAuthService _authService;
        private readonly IViktorynaService _viktorynaService;
        private readonly IKorystuvachSettingsService _korystuvachSettingsService;
        private readonly ITopResultsService _topResultsService;
        private readonly IMyResultsService _myResultsService;

        public WindowFactory(
            IAuthService authService,
            IViktorynaService viktorynaService,
            IKorystuvachSettingsService korystuvachSettingsService,
            ITopResultsService topResultsService,
            IMyResultsService myResultsService)
        {
            _authService = authService;
            _viktorynaService = viktorynaService;
            _korystuvachSettingsService = korystuvachSettingsService;
            _topResultsService = topResultsService;
            _myResultsService = myResultsService;
        }

        public MainWindow CreateMainWindow()
        {
            return new MainWindow(_authService, _viktorynaService, _korystuvachSettingsService, _topResultsService, _myResultsService, this);
        }

        public MenuWindow CreateMenuWindow(string login)
        {
            return new MenuWindow(login, _authService, _viktorynaService, _korystuvachSettingsService, _topResultsService, _myResultsService, this);
        }

        public RegisterWindow CreateRegisterWindow()
        {
            return new RegisterWindow(_authService, _viktorynaService, _korystuvachSettingsService, _topResultsService, _myResultsService, this);
        }

        public ChooseQuizWindow CreateChooseQuizWindow(string login)
        {
            return new ChooseQuizWindow(login, _authService, _viktorynaService, _korystuvachSettingsService, _topResultsService, _myResultsService, this);
        }

        public ViktorynaWindow CreateViktorynaWindow(string login, string category)
        {
            return new ViktorynaWindow(login, category, _authService, _viktorynaService, _korystuvachSettingsService, _topResultsService, _myResultsService, this);
        }

        public SettingsWindow CreateSettingsWindow(string login)
        {
            var viewModel = CreateSettingsViewModel(login);
            return new SettingsWindow(viewModel, login, this);
        }

        public TopResultsWindow CreateTopResultsWindow()
        {
            var viewModel = CreateTopResultsViewModel();
            return new TopResultsWindow(viewModel);
        }

        public MyResultsWindow CreateMyResultsWindow(string login)
        {
            var viewModel = CreateMyResultsViewModel(login);
            return new MyResultsWindow(viewModel);
        }

        public SettingsViewModel CreateSettingsViewModel(string login)
        {
            return new SettingsViewModel(_korystuvachSettingsService, login);
        }

        public TopResultsViewModel CreateTopResultsViewModel()
        {
            return new TopResultsViewModel(_topResultsService);
        }

        public MyResultsViewModel CreateMyResultsViewModel(string login)
        {
            return new MyResultsViewModel(_myResultsService, login);
        }
    }
}
