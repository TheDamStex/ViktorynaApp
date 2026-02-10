using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.Factories
{
    public class JsonAppServicesFactory : IAppServicesFactory
    {
        private readonly IDaniService<Korystuvach> _korystuvachiDaniService;
        private readonly IDaniService<Pytannia> _pytanniaDaniService;
        private readonly IDaniService<Rezultat> _rezultatyDaniService;

        private readonly IKorystuvachService _korystuvachService;
        private readonly IViktorynaService _viktorynaService;
        private readonly ITopResultsService _topResultsService;
        private readonly IMyResultsService _myResultsService;

        public JsonAppServicesFactory()
        {
            // Створюємо сховища ОДИН раз, щоб усі сервіси бачили ті самі дані.
            var korystuvachiStorage = new JsonStorage<Korystuvach>("korystuvachi.json");
            var pytanniaStorage = new JsonStorage<Pytannia>("pytannia.json");
            var rezultatyStorage = new JsonStorage<Rezultat>("rezultaty.json");

            _korystuvachiDaniService = new JsonDaniService<Korystuvach>(korystuvachiStorage);
            _pytanniaDaniService = new JsonDaniService<Pytannia>(pytanniaStorage);
            _rezultatyDaniService = new JsonDaniService<Rezultat>(rezultatyStorage);

            _korystuvachService = new KorystuvachService(_korystuvachiDaniService);
            _viktorynaService = new ViktorynaService(_pytanniaDaniService, _rezultatyDaniService);
            _topResultsService = new TopResultsService(_rezultatyDaniService);
            _myResultsService = new MyResultsService(_rezultatyDaniService);
        }

        public IKorystuvachService CreateKorystuvachService()
        {
            return _korystuvachService;
        }

        public IViktorynaService CreateViktorynaService()
        {
            return _viktorynaService;
        }

        public ITopResultsService CreateTopResultsService()
        {
            return _topResultsService;
        }

        public IMyResultsService CreateMyResultsService()
        {
            return _myResultsService;
        }

        public IDaniService<Korystuvach> CreateKorystuvachDaniService()
        {
            return _korystuvachiDaniService;
        }
    }
}
