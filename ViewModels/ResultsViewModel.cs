// ViewModels/ResultsViewModel.cs
using System.Collections.ObjectModel;
using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.ViewModels
{
    public class ResultsViewModel : BaseViewModel
    {
        private readonly IViktorynaService _viktorynaService;
        private string _login;
        private bool _showTopResults;

        private ObservableCollection<Rezultat> _results = new ObservableCollection<Rezultat>();
        public ObservableCollection<Rezultat> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        private string _windowTitle;
        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }

        public ResultsViewModel(IViktorynaService viktorynaService, string login, bool showTopResults = false)
        {
            _viktorynaService = viktorynaService;
            _login = login;
            _showTopResults = showTopResults;

            LoadResults();
        }

        private void LoadResults()
        {
            if (_showTopResults)
            {
                WindowTitle = "Топ-20 результатів";
                // Тут буде завантаження топ-20
                // Поки що завантажуємо всі результати
                var allResults = _viktorynaService.OtrymatyTop20("Усі");
                Results = new ObservableCollection<Rezultat>(allResults);
            }
            else
            {
                WindowTitle = "Мої результати";
                var userResults = _viktorynaService.OtrymatyRezultatyKorystuvacha(_login);
                Results = new ObservableCollection<Rezultat>(userResults);
            }
        }
    }
}