// Validators/KorystuvachValidator.cs
using System.Collections.Generic;
using ViktorynaApp.Models;

namespace ViktorynaApp.Validators
{
    public class KorystuvachValidator : IValidator<Korystuvach>
    {
        private readonly IDataValidator _dataValidator;

        public KorystuvachValidator(IDataValidator dataValidator)
        {
            _dataValidator = dataValidator;
        }

        public List<string> Validate(Korystuvach korystuvach)
        {
            var errors = new List<string>();

            if (korystuvach == null)
            {
                errors.Add("Користувач відсутній.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(korystuvach.Login))
                errors.Add("Логін не може бути порожнім.");

            if (string.IsNullOrWhiteSpace(korystuvach.Parol))
                errors.Add("Пароль не може бути порожнім.");
            else if (!_dataValidator.ChyParolValidnyi(korystuvach.Parol))
                errors.Add("Пароль має бути не коротший 4 символів і без пробілів.");

            if (string.IsNullOrWhiteSpace(korystuvach.DataNarodzhennia))
                errors.Add("Дата народження є обов'язковою.");
            else if (!_dataValidator.ChyDataValidna(korystuvach.DataNarodzhennia))
                errors.Add("Дата народження має бути коректною.");

            return errors;
        }
    }
}
