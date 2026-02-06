// Services/ViktorynaService.cs (полная исправленная версия)
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

            if (rozdil == "Змішана")
            {
                return vsiPytannia
                    .OrderBy(x => rand.Next())
                    .Take(20)
                    .ToList();
            }

            return vsiPytannia
                .Where(p => p.Rozdil == rozdil)
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

            if (rozdil == "Усі" || string.IsNullOrEmpty(rozdil))
            {
                return vsiRezultaty
                    .OrderByDescending(r => r.KilkistPravylnyh)
                    .ThenBy(r => r.DataVykonannia)
                    .Take(20)
                    .ToList();
            }
            else
            {
                return vsiRezultaty
                    .Where(r => r.Rozdil == rozdil)
                    .OrderByDescending(r => r.KilkistPravylnyh)
                    .ThenBy(r => r.DataVykonannia)
                    .Take(20)
                    .ToList();
            }
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

        // ДОДАЄМО ЦЕЙ МЕТОД
        public List<Rezultat> OtrymatyVsiRezultaty()
        {
            if (_rezultatyService == null)
                return new List<Rezultat>();

            return _rezultatyService.Zavantazhyty();
        }
    }
}