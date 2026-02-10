using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.Factories
{
    public interface IAppServicesFactory
    {
        IKorystuvachService CreateKorystuvachService();
        IViktorynaService CreateViktorynaService();
        ITopResultsService CreateTopResultsService();
        IMyResultsService CreateMyResultsService();

        // Потрібно для налаштувань користувача (спільне сховище).
        IDaniService<Korystuvach> CreateKorystuvachDaniService();
    }
}
