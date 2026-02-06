// Services/MyResultsService.cs
using System.Collections.Generic;
using System.Linq;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public class MyResultsService : IMyResultsService
    {
        private readonly IDaniService<Rezultat> _rezultatyService;

        public MyResultsService(IDaniService<Rezultat> rezultatyService)
        {
            _rezultatyService = rezultatyService;
        }

        public List<Rezultat> OtrymatyRezultatyKorystuvacha(string login)
        {
            var vsiRezultaty = _rezultatyService.Zavantazhyty();
            return vsiRezultaty
                .Where(r => r.Login == login)
                .OrderByDescending(r => r.DataVykonannia)
                .ToList();
        }

        public StatystykaKorystuvacha OtrymatyStatystyku(string login)
        {
            var rezultaty = OtrymatyRezultatyKorystuvacha(login);

            if (rezultaty.Count == 0)
            {
                return new StatystykaKorystuvacha
                {
                    Login = login,
                    ZagalnaKilkistViktoryn = 0,
                    ZagalnaKilkistPravylnyh = 0,
                    SeredniyProcent = 0,
                    NajkrashyiRozdil = "Немає результатів",
                    NajkrashyiRezultat = 0
                };
            }

            // Загальна кількість правильних відповідей
            int zagalnaPravylnyh = rezultaty.Sum(r => r.KilkistPravylnyh);

            // Середній процент
            double seredniyProcent = rezultaty.Average(r => (r.KilkistPravylnyh * 100.0) / 20.0);

            // Найкращий результат
            var najkrashyi = rezultaty.OrderByDescending(r => r.KilkistPravylnyh).First();

            // Найкращий розділ (розділ з найвищим середнім)
            var rozdilyStat = rezultaty
                .GroupBy(r => r.Rozdil)
                .Select(g => new
                {
                    Rozdil = g.Key,
                    Seredniy = g.Average(r => r.KilkistPravylnyh),
                    Kilkist = g.Count()
                })
                .OrderByDescending(x => x.Seredniy)
                .FirstOrDefault();

            return new StatystykaKorystuvacha
            {
                Login = login,
                ZagalnaKilkistViktoryn = rezultaty.Count,
                ZagalnaKilkistPravylnyh = zagalnaPravylnyh,
                SeredniyProcent = seredniyProcent,
                NajkrashyiRozdil = rozdilyStat?.Rozdil ?? "Немає",
                NajkrashyiRezultat = najkrashyi.KilkistPravylnyh
            };
        }
    }
}