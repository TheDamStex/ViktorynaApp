// ViewModels/DisplayModels.cs
namespace ViktorynaApp.ViewModels
{
    // Для Топ-20
    public class TopResultDisplay
    {
        public int Position { get; set; }
        public string Login { get; set; } = "";
        public string Rozdil { get; set; } = "";
        public int KilkistPravylnyh { get; set; }
        public int TotalQuestions { get; set; }
        public int Procent { get; set; }
        public string DataVykonannia { get; set; } = "";
        public string RezultatText => $"{KilkistPravylnyh}/{TotalQuestions} ({Procent}%)";
    }

    // Для Моїх результатів
    public class MyResultDisplay
    {
        public string Rozdil { get; set; } = "";
        public int KilkistPravylnyh { get; set; }
        public int TotalQuestions { get; set; }
        public int Procent { get; set; }
        public string DataVykonannia { get; set; } = "";
        public string RezultatText => $"{KilkistPravylnyh}/{TotalQuestions} ({Procent}%)";
    }
}
