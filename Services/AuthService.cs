// Services/AuthService.cs
using ViktorynaApp.Models;
using System.Linq;
using ViktorynaApp.Validators;

namespace ViktorynaApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IKorystuvachService _korystuvachService;
        private readonly IValidator<Korystuvach> _validator;

        // Приймаємо залежності через конструктор
        public AuthService(IKorystuvachService korystuvachService, IValidator<Korystuvach> validator)
        {
            _korystuvachService = korystuvachService;
            _validator = validator;
        }

        public Korystuvach? Login(string login, string parol)
        {
            return _korystuvachService.Uviyty(login, parol);
        }

        public bool Register(Korystuvach korystuvach)
        {
            // Валідація
            if (_validator.Validate(korystuvach).Any())
                return false;

            return _korystuvachService.Zareiestruvaty(korystuvach);
        }
    }
}
