using System.Collections.Generic;
using System.Linq;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public class KorystuvachService : IKorystuvachService
    {
        private readonly IDaniService<Korystuvach> _daniService;

        public KorystuvachService(IDaniService<Korystuvach> daniService)
        {
            _daniService = daniService;
        }

        public List<Korystuvach> OtrymatyVsih()
        {
            return _daniService.Zavantazhyty();
        }

        public bool Zareiestruvaty(Korystuvach novyiKorystuvach)
        {
            var vsiKorystuvachi = OtrymatyVsih();

            if (vsiKorystuvachi.Any(k => k.Login == novyiKorystuvach.Login))
                return false;

            vsiKorystuvachi.Add(novyiKorystuvach);
            _daniService.Zberehty(vsiKorystuvachi);
            return true;
        }

        public Korystuvach? Uviyty(string login, string parol)
        {
            var vsiKorystuvachi = OtrymatyVsih();
            return vsiKorystuvachi.FirstOrDefault(k =>
                k.Login == login && k.Parol == parol);
        }
    }
}
