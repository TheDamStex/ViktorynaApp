using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public interface IKorysnykService
    {
        bool Zareiestruvaty(Korystuvach korysnyk);
        Korystuvach Uviyty(string login, string parol);
        List<Korystuvach> OtrymatyVsih();
    }
}