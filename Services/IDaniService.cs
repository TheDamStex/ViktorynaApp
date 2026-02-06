using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface IDaniService<T>
    {
        List<T> Zavantazhyty();
        void Zberehty(List<T> dani);
    }
}
