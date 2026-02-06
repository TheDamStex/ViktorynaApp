using System;
using System.Windows.Input;
using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IKorystuvachSettingsService _settingsService;
        private readonly string _login;

        private string _staryiParol = "";
        public string StaryiParol
        {
            get => _staryiParol;
            set
            {
                SetProperty(ref _staryiParol, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _novyiParol = "";
        public string NovyiParol
        {
            get => _novyiParol;
            set
            {
                SetProperty(ref _novyiParol, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _pidtverdzhenniaParol = "";
        public string PidtverdzhenniaParol
        {
            get => _pidtverdzhenniaParol;
            set
            {
                SetProperty(ref _pidtverdzhenniaParol, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _dataNarodzhennia = "";
        public string DataNarodzhennia
        {
            get => _dataNarodzhennia;
            set
            {
                SetProperty(ref _dataNarodzhennia, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _statusMessage = "";
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private bool _isStatusSuccess;
        public bool IsStatusSuccess
        {
            get => _isStatusSuccess;
            set => SetProperty(ref _isStatusSuccess, value);
        }

        public ICommand ZminytyParolCommand { get; }
        public ICommand ZminytyDatuCommand { get; }
        public ICommand PovternutyCommand { get; }

        public SettingsViewModel(IKorystuvachSettingsService settingsService, string login)
        {
            _settingsService = settingsService;
            _login = login;

            // Використовуємо лямбда-вирази для команд
            ZminytyParolCommand = new RelayCommand(
                execute: _ => ZminytyParol(),
                canExecute: _ => CanZminytyParol());

            ZminytyDatuCommand = new RelayCommand(
                execute: _ => ZminytyDatu(),
                canExecute: _ => CanZminytyDatu());

            PovternutyCommand = new RelayCommand(_ => Povternuty());

            ZavantazhytyDaniKorystuvacha();
        }

        private void ZavantazhytyDaniKorystuvacha()
        {
            var korystuvach = _settingsService.OtrymatyKorystuvacha(_login);
            if (korystuvach != null)
            {
                DataNarodzhennia = korystuvach.DataNarodzhennia;
            }
        }

        private bool CanZminytyParol()
        {
            return !string.IsNullOrWhiteSpace(StaryiParol) &&
                   !string.IsNullOrWhiteSpace(NovyiParol) &&
                   !string.IsNullOrWhiteSpace(PidtverdzhenniaParol) &&
                   NovyiParol == PidtverdzhenniaParol;
        }

        private bool CanZminytyDatu()
        {
            return !string.IsNullOrWhiteSpace(DataNarodzhennia);
        }

        private void ZminytyParol()
        {
            if (NovyiParol != PidtverdzhenniaParol)
            {
                PokazatyStatus("Паролі не співпадають", false);
                return;
            }

            bool uspishno = _settingsService.ZminytyParol(_login, StaryiParol, NovyiParol);

            if (uspishno)
            {
                PokazatyStatus("Пароль успішно змінено", true);
                StaryiParol = "";
                NovyiParol = "";
                PidtverdzhenniaParol = "";
            }
            else
            {
                PokazatyStatus("Помилка при зміні пароля. Перевірте старий пароль.", false);
            }
        }

        private void ZminytyDatu()
        {
            bool uspishno = _settingsService.ZminytyDatuNarodzhennia(_login, DataNarodzhennia);

            if (uspishno)
            {
                PokazatyStatus("Дату народження успішно змінено", true);
            }
            else
            {
                PokazatyStatus("Помилка при зміні дати. Перевірте формат дати (дд.мм.рррр).", false);
            }
        }

        private void Povternuty()
        {
            OnCloseRequested?.Invoke();
        }

        private void PokazatyStatus(string message, bool isSuccess)
        {
            StatusMessage = message;
            IsStatusSuccess = isSuccess;
        }

        public event Action? OnCloseRequested;
    }
}