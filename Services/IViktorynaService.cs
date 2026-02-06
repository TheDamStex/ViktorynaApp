// Services/IViktorynaService.cs
using ViktorynaApp.Models;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface IViktorynaService
    {
        List<Pytannia> OtrymatyPytannia(string rozdil);
        void DodatyRezultat(Rezultat rezultat);
        List<Rezultat> OtrymatyTop20(string rozdil);
        List<Rezultat> OtrymatyRezultatyKorystuvacha(string login);
        List<string> OtrymatyVsiRozdily();
        List<Rezultat> OtrymatyVsiRezultaty(); // Новий метод
    }
}