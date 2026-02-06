// Services/IMyResultsService.cs
using ViktorynaApp.Models;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface IMyResultsService
    {
        List<Rezultat> OtrymatyRezultatyKorystuvacha(string login);
        StatystykaKorystuvacha OtrymatyStatystyku(string login);
    }

    public class StatystykaKorystuvacha
    {
        public string Login { get; set; } = "";
        public int ZagalnaKilkistViktoryn { get; set; }
        public int ZagalnaKilkistPravylnyh { get; set; }
        public double SeredniyProcent { get; set; }
        public string NajkrashyiRozdil { get; set; } = "";
        public int NajkrashyiRezultat { get; set; }
    }
}