using System.Collections.Generic;

namespace ViktorynaApp.Validators
{
    public interface IValidator<T>
    {
        List<string> Validate(T model);
    }
}
