using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViktorynaApp.Services
{
    public interface IDaniService<T>
    {
        List<T> Zavantazhyty();
        void Zberehty(List<T> dani);
    }
}
