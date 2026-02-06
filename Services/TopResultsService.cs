// Services/TopResultsService.cs
using System.Collections.Generic;
using System.Linq;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public class TopResultsService : ITopResultsService
    {
        private readonly IDaniService<Rezultat> _rezultatyService;

        public TopResultsService(IDaniService<Rezultat> rezultatyService)
        {
            _rezultatyService = rezultatyService;
        }

        public List<Rezultat> OtrymatyTop20(string rozdil)
        {
            var vsiRezultaty = _rezultatyService.Zavantazhyty();

            if (rozdil == "Усі")
            {
                return vsiRezultaty
                    .OrderByDescending(r => r.KilkistPravylnyh)
                    .ThenBy(r => r.DataVykonannia)
                    .Take(20)
                    .ToList();
            }

            return vsiRezultaty
                .Where(r => r.Rozdil == rozdil)
                .OrderByDescending(r => r.KilkistPravylnyh)
                .ThenBy(r => r.DataVykonannia)
                .Take(20)
                .ToList();
        }

        public List<string> OtrymatyVsiRozdily()
        {
            var vsiRezultaty = _rezultatyService.Zavantazhyty();
            var rozdily = vsiRezultaty
                .Select(r => r.Rozdil)
                .Distinct()
                .OrderBy(r => r)
                .ToList();

            rozdily.Insert(0, "Усі");
            return rozdily;
        }
    }
}