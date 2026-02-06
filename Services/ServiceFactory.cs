// Services/ServiceFactory.cs
using ViktorynaApp.Models;
using ViktorynaApp.Validators;

namespace ViktorynaApp.Services
{
    public static class ServiceFactory
    {
        public static IKorystuvachService CreateKorystuvachService()
        {
            var daniService = new JsonDaniService<Models.Korystuvach>("korystuvachi.json");
            return new KorystuvachService(daniService);
        }

        public static IViktorynaService CreateViktorynaService()
        {
            var pytanniaService = new JsonDaniService<Models.Pytannia>("pytannia.json");
            var rezultatyService = new JsonDaniService<Models.Rezultat>("rezultaty.json");
            return new ViktorynaService(pytanniaService, rezultatyService);
        }

        public static IQuizManager CreateQuizManager()
        {
            var viktorynaService = CreateViktorynaService();
            return new QuizManagerService(viktorynaService);
        }

        public static IKorystuvachSettingsService CreateKorystuvachSettingsService()
        {
            var daniService = new JsonDaniService<Korystuvach>("korystuvachi.json");
            var korystuvachService = new KorystuvachService(daniService);
            var validator = new DataValidator();
            return new KorystuvachSettingsService(korystuvachService, validator, daniService);
        }

        public static ITopResultsService CreateTopResultsService()
        {
            var rezultatyService = new JsonDaniService<Rezultat>("rezultaty.json");
            return new TopResultsService(rezultatyService);
        }

        public static IMyResultsService CreateMyResultsService()
        {
            var rezultatyService = new JsonDaniService<Rezultat>("rezultaty.json");
            return new MyResultsService(rezultatyService);
        }
    }
}
