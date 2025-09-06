using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Domain.Exceptions
{
    public class NotFoundException:BaseException
    {
        public NotFoundException(int errorCode, string userMsg, string devMsg) : base(errorCode, userMsg, devMsg) { }
    }
}
