// ViewModels/TopResultsViewModel.cs (обновляем)
using System.Collections.ObjectModel;
using System.Linq;
using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.ViewModels
{
    public class TopResultsViewModel : BaseViewModel
    {
        private readonly ITopResultsService _service;

        public ObservableCollection<TopResultDisplay> Rezultaty { get; } = new ObservableCollection<TopResultDisplay>();
        public ObservableCollection<string> Rozdily { get; } = new ObservableCollection<string>();

        private string _vybranyiRozdil = "Усі";
        public string VybranyiRozdil
        {
            get => _vybranyiRozdil;
            set
            {
                if (SetProperty(ref _vybranyiRozdil, value))
                {
                    ZavantazhytyRezultaty();
                }
            }
        }

        public TopResultsViewModel(ITopResultsService service)
        {
            _service = service;
            ZavantazhytyRozdily();
            ZavantazhytyRezultaty();
        }

        private void ZavantazhytyRozdily()
        {
            Rozdily.Clear();
            var rozdily = _service.OtrymatyVsiRozdily();

            foreach (var rozdil in rozdily)
            {
                Rozdily.Add(rozdil);
            }
        }

        private void ZavantazhytyRezultaty()
        {
            Rezultaty.Clear();
            var rezultaty = _service.OtrymatyTop20(VybranyiRozdil);

            int position = 1;
            foreach (var rezultat in rezultaty)
            {
                Rezultaty.Add(new TopResultDisplay
                {
                    Position = position++,
                    Login = rezultat.Login,
                    Rozdil = rezultat.Rozdil,
                    KilkistPravylnyh = rezultat.KilkistPravylnyh,
                    DataVykonannia = rezultat.DataVykonannia.ToString("dd.MM.yyyy HH:mm")
                });
            }
        }
    }
}