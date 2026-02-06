// Services/ViktorynaService.cs (повна виправлена версія)
using System;
using System.Collections.Generic;
using System.Linq;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public class ViktorynaService : IViktorynaService
    {
        private readonly IDaniService<Pytannia>? _pytanniaService;
        private readonly IDaniService<Rezultat>? _rezultatyService;

        public ViktorynaService()
        {
            _pytanniaService = new JsonDaniService<Pytannia>("pytannia.json");
            _rezultatyService = new JsonDaniService<Rezultat>("rezultaty.json");
        }

        public ViktorynaService(
            IDaniService<Pytannia> pytanniaService,
            IDaniService<Rezultat> rezultatyService)
        {
            _pytanniaService = pytanniaService;
            _rezultatyService = rezultatyService;
        }

        public List<Pytannia> OtrymatyPytannia(string rozdil)
        {
            if (_pytanniaService == null)
                return new List<Pytannia>();

            var vsiPytannia = _pytanniaService.Zavantazhyty();
            var rand = new Random();
            var filtr = vsiPytannia.AsEnumerable();

            if (rozdil != "Змішана")
            {
                filtr = filtr.Where(p => p.Rozdil == rozdil);
            }

            return filtr
                .OrderBy(x => rand.Next())
                .Take(20)
                .ToList();
        }

        public List<string> OtrymatyVsiRozdily()
        {
            if (_pytanniaService == null)
                return new List<string>();

            var vsiPytannia = _pytanniaService.Zavantazhyty();
            return vsiPytannia
                .Select(p => p.Rozdil)
                .Distinct()
                .OrderBy(r => r)
                .ToList();
        }

        public void DodatyRezultat(Rezultat rezultat)
        {
            if (_rezultatyService == null)
                return;

            var vsiRezultaty = _rezultatyService.Zavantazhyty();
            vsiRezultaty.Add(rezultat);
            _rezultatyService.Zberehty(vsiRezultaty);
        }

        public List<Rezultat> OtrymatyTop20(string rozdil)
        {
            if (_rezultatyService == null)
                return new List<Rezultat>();

            var vsiRezultaty = _rezultatyService.Zavantazhyty();
            var filtr = vsiRezultaty.AsEnumerable();

            if (!string.IsNullOrEmpty(rozdil) && rozdil != "Усі")
            {
                filtr = filtr.Where(r => r.Rozdil == rozdil);
            }

            return filtr
                .OrderByDescending(r => r.KilkistPravylnyh)
                .ThenBy(r => r.DataVykonannia)
                .Take(20)
                .ToList();
        }

        public List<Rezultat> OtrymatyRezultatyKorystuvacha(string login)
        {
            if (_rezultatyService == null)
                return new List<Rezultat>();

            var vsiRezultaty = _rezultatyService.Zavantazhyty();
            return vsiRezultaty
                .Where(r => r.Login == login)
                .OrderByDescending(r => r.DataVykonannia)
                .ToList();
        }

        // Додаємо цей метод для зручного доступу до всіх результатів.
        public List<Rezultat> OtrymatyVsiRezultaty()
        {
            if (_rezultatyService == null)
                return new List<Rezultat>();

            return _rezultatyService.Zavantazhyty();
        }
    }
}
