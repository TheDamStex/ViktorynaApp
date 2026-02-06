using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViktorynaApp.Validators
{
    public interface IValidator<T>
    {
        List<string> Validate(T model);
    }

}
