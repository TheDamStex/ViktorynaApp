
using System.Collections.Generic;

namespace ViktorynaApp.Models
{
    public class DisplayQuestion
    {
        public string Text { get; set; } = "";
        public List<DisplayAnswer> Answers { get; set; } = new List<DisplayAnswer>();
    }

    public class DisplayAnswer
    {
        public string Text { get; set; } = "";
        public bool IsSelected { get; set; }
        public bool IsCorrect { get; set; }
    }
}