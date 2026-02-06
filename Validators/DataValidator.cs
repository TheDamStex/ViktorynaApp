using System;

namespace ViktorynaApp.Validators
{
    public class DataValidator : IDataValidator
    {
        public bool ChyParolValidnyi(string parol)
        {
            if (string.IsNullOrWhiteSpace(parol))
                return false;

            if (parol.Length < 4)
                return false;

            if (parol.Contains(" "))
                return false;

            return true;
        }

        public bool ChyDataValidna(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return false;

            if (DateTime.TryParse(data, out DateTime result))
            {
                if (result > DateTime.Now)
                    return false;

                var minDate = DateTime.Now.AddYears(-120);
                var maxDate = DateTime.Now.AddYears(-5);

                return result >= minDate && result <= maxDate;
            }

            return false;
        }
    }
}