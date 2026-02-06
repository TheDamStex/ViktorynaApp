// Services/ITopResultsService.cs
using ViktorynaApp.Models;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface ITopResultsService
    {
        List<Rezultat> OtrymatyTop20(string rozdil);
        List<string> OtrymatyVsiRozdily();
    }
}