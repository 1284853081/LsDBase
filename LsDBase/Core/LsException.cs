using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Core
{
    internal class LsException:ApplicationException
    {
        public LsException() { }
        public LsException(string message) : base(message) { }
        public LsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
