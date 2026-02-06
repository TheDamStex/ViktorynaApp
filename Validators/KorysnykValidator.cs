// Validators/KorystuvachValidator.cs
using ViktorynaApp.Models;

namespace ViktorynaApp.Validators
{
    public class KorystuvachValidator
    {
        public bool IsValid(Korystuvach korystuvach)
        {
            if (korystuvach == null)
                return false;
                
            if (string.IsNullOrWhiteSpace(korystuvach.Login))
                return false;
                
            if (string.IsNullOrWhiteSpace(korystuvach.Parol))
                return false;
                
            if (string.IsNullOrWhiteSpace(korystuvach.DataNarodzhennia))
                return false;
                
            // Можна додати додаткові перевірки
            if (korystuvach.Parol.Length < 4)
                return false;
                
            return true;
        }
    }
}