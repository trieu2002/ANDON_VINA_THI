using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Domain.Exceptions
{
    public class UnauthoriedException:BaseException
    {
        public UnauthoriedException(int errorCode, string userMsg, string devMsg) : base(errorCode, userMsg, devMsg) { }
    }
}
