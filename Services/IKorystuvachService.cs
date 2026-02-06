using ViktorynaApp.Models;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface IKorystuvachService
    {
        bool Zareiestruvaty(Korystuvach korystuvach);
        Korystuvach? Uviyty(string login, string parol); // Додаємо ?
        List<Korystuvach> OtrymatyVsih();
    }
}