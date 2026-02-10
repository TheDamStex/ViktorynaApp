// ViewModels/MyResultsViewModel.cs (обновляем)
using System.Collections.ObjectModel;
using System.Linq;
using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.ViewModels
{
    public class MyResultsViewModel : BaseViewModel
    {
        private readonly IMyResultsService _service;
        private readonly string _login;

        public ObservableCollection<MyResultDisplay> Rezultaty { get; } = new ObservableCollection<MyResultDisplay>();
        public StatystykaKorystuvacha Statystyka { get; private set; } = new StatystykaKorystuvacha();
        public int ZagalnaKilkistViktoryn => Statystyka.ZagalnaKilkistViktoryn;
        public int ZagalnaKilkistPravylnyh => Statystyka.ZagalnaKilkistPravylnyh;
        public double SeredniyProcent => Statystyka.SeredniyProcent;

        public MyResultsViewModel(IMyResultsService service, string login)
        {
            _service = service;
            _login = login;
            ZavantazhytyDani();
        }

        private void ZavantazhytyDani()
        {
            // Завантажуємо результати
            var rezultaty = _service.OtrymatyRezultatyKorystuvacha(_login);

            Rezultaty.Clear();
            foreach (var rezultat in rezultaty)
            {
                Rezultaty.Add(new MyResultDisplay
                {
                    Rozdil = rezultat.Rozdil,
                    KilkistPravylnyh = rezultat.KilkistPravylnyh,
                    TotalQuestions = rezultat.EffectiveTotalQuestions,
                    Procent = rezultat.ProcentPravylnyh,
                    DataVykonannia = rezultat.DataVykonannia.ToString("dd.MM.yyyy HH:mm")
                });
            }

            // Завантажуємо статистику
            Statystyka = _service.OtrymatyStatystyku(_login);
            OnPropertyChanged(nameof(Statystyka));
            OnPropertyChanged(nameof(ZagalnaKilkistViktoryn));
            OnPropertyChanged(nameof(ZagalnaKilkistPravylnyh));
            OnPropertyChanged(nameof(SeredniyProcent));
        }
    }
}
