// Models/Rezultat.cs
using System;

namespace ViktorynaApp.Models
{
    public class Rezultat
    {
        public string Login { get; set; } = "";
        public string Rozdil { get; set; } = "";
        public int KilkistPravylnyh { get; set; }
        public DateTime DataVykonannia { get; set; } = DateTime.Now;

        // Додаємо властивість для відсотка правильних відповідей
        public int ProcentPravylnyh => KilkistPravylnyh * 5; // 5% за кожну правильну відповідь з 20

        // Властивість для відображення у таблиці
        public string RezultatText => $"{KilkistPravylnyh}/20 ({ProcentPravylnyh}%)";
    }
}