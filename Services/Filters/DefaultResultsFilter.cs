// Services/Filters/DefaultResultsFilter.cs
using System.Collections.Generic;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services.Filters
{
    public class DefaultResultsFilter : IResultsFilter
    {
        public List<Rezultat> Filter(List<Rezultat> rezultaty)
        {
            // Базовий фільтр не змінює дані
            return rezultaty;
        }
    }
}