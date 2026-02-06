using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ViktorynaApp.Models
{
    public class Pytannia
    {
        public string Rozdil { get; set; } = "";
        public string Tekst { get; set; } = "";
        public List<string> Varianty { get; set; } = new List<string>();
        public List<int> PravylnyIndex { get; set; } = new List<int>();
    }
}

