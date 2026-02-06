// Services/KorystuvachSettingsService.cs
using System.Linq;
using ViktorynaApp.Models;
using ViktorynaApp.Validators; // Цей using має бути!

namespace ViktorynaApp.Services
{
    public class KorystuvachSettingsService : IKorystuvachSettingsService
    {
        private readonly IKorystuvachService _korystuvachService;
        private readonly IDataValidator _validator;

        public KorystuvachSettingsService(IKorystuvachService korystuvachService, IDataValidator validator)
        {
            _korystuvachService = korystuvachService;
            _validator = validator;
        }

        public Korystuvach? OtrymatyKorystuvacha(string login)
        {
            var vsiKorystuvachi = _korystuvachService.OtrymatyVsih();
            return vsiKorystuvachi.FirstOrDefault(k => k.Login == login);
        }

        public bool ZminytyParol(string login, string staryiParol, string novyiParol)
        {
            var korystuvach = OtrymatyKorystuvacha(login);
            if (korystuvach == null)
                return false;

            // Перевіряємо старий пароль
            if (!korystuvach.ParolSpivpadaye(staryiParol))
                return false;

            // Валідуємо новий пароль
            if (!_validator.ChyParolValidnyi(novyiParol))
                return false;

            // Змінюємо пароль
            korystuvach.ZminytyParol(novyiParol);

            // Зберігаємо зміни
            return ZberehtyZminy(korystuvach);
        }

        public bool ZminytyDatuNarodzhennia(string login, string novaData)
        {
            var korystuvach = OtrymatyKorystuvacha(login);
            if (korystuvach == null)
                return false;

            // Валідуємо дату
            if (!_validator.ChyDataValidna(novaData))
                return false;

            // Змінюємо дату
            korystuvach.ZminytyDatuNarodzhennia(novaData);

            // Зберігаємо зміни
            return ZberehtyZminy(korystuvach);
        }

        private bool ZberehtyZminy(Korystuvach updatedKorystuvach)
        {
            var vsiKorystuvachi = _korystuvachService.OtrymatyVsih();

            // Знаходимо користувача у списку
            for (int i = 0; i < vsiKorystuvachi.Count; i++)
            {
                if (vsiKorystuvachi[i].Login == updatedKorystuvach.Login)
                {
                    vsiKorystuvachi[i] = updatedKorystuvach;

                    // Оновлюємо через KorystuvachService
                    // Створюємо новий сервіс для збереження
                    var daniService = new JsonDaniService<Korystuvach>("korystuvachi.json");
                    daniService.Zberehty(vsiKorystuvachi);
                    return true;
                }
            }

            return false;
        }
    }
}