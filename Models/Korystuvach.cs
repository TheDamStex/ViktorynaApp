// Models/Korystuvach.cs
using System;

namespace ViktorynaApp.Models
{
    public class Korystuvach
    {
        public string Login { get; set; } = "";
        public string Parol { get; set; } = "";
        public string DataNarodzhennia { get; set; } = "";

        // Метод для перевірки пароля
        public bool ParolSpivpadaye(string parol)
        {
            return Parol == parol;
        }

        // Метод для зміни пароля
        public void ZminytyParol(string novyiParol)
        {
            // Тут можна додати шифрування пароля
            Parol = novyiParol;
        }

        // Метод для зміни дати народження
        public void ZminytyDatuNarodzhennia(string novaData)
        {
            DataNarodzhennia = novaData;
        }

        // Перевірка валідності дати
        public bool ChyDataKorektna(string data)
        {
            return DateTime.TryParse(data, out _);
        }
    }
}