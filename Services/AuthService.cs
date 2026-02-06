// Services/AuthService.cs
using ViktorynaApp.Models;
using ViktorynaApp.Validators;

namespace ViktorynaApp.Services
{
    public class AuthService
    {
        private readonly IKorystuvachService _korystuvachService;
        private readonly KorystuvachValidator _validator;

        // Приймаємо залежності через конструктор
        public AuthService(IKorystuvachService korystuvachService, KorystuvachValidator validator)
        {
            _korystuvachService = korystuvachService;
            _validator = validator;
        }

        public Korystuvach Login(string login, string parol)
        {
            return _korystuvachService.Uviyty(login, parol);
        }

        public bool Register(Korystuvach korystuvach)
        {
            // Валідація
            if (!_validator.IsValid(korystuvach))
                return false;

            return _korystuvachService.Zareiestruvaty(korystuvach);
        }
    }
}