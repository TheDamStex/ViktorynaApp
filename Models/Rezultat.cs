// Models/Rezultat.cs
using System;

namespace ViktorynaApp.Models
{
    public class Rezultat
    {
        public string Login { get; set; } = "";
        public string Rozdil { get; set; } = "";
        public int KilkistPravylnyh { get; set; }
        public int TotalQuestions { get; set; } = 20;
        public DateTime DataVykonannia { get; set; } = DateTime.Now;

        public int EffectiveTotalQuestions => TotalQuestions > 0 ? TotalQuestions : 20;

        // Додаємо властивість для відсотка правильних відповідей
        public int ProcentPravylnyh => EffectiveTotalQuestions == 0
            ? 0
            : (int)Math.Round((double)KilkistPravylnyh / EffectiveTotalQuestions * 100);

        // Властивість для відображення у таблиці
        public string RezultatText => $"{KilkistPravylnyh}/{EffectiveTotalQuestions} ({ProcentPravylnyh}%)";
    }
}
