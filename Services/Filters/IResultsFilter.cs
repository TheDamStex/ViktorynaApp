using System.Collections.Generic;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services.Filters
{
    public interface IResultsFilter
    {
        List<Rezultat> Filter(List<Rezultat> rezultaty);
    }
}