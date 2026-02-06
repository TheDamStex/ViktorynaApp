using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public interface IAuthService
    {
        Korystuvach Login(string login, string parol);
        bool Register(Korystuvach korystuvach);
    }
}
