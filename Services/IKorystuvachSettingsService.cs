using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public interface IKorystuvachSettingsService
    {
        bool ZminytyParol(string login, string staryiParol, string novyiParol);
        bool ZminytyDatuNarodzhennia(string login, string novaData);
        Korystuvach? OtrymatyKorystuvacha(string login);
    }
}