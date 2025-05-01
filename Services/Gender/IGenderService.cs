using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Gender
{
    public interface IGenderService
    {
        List<string> GetBothGenders();
    }
}
