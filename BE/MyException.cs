using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class MyException : Exception
    {
        public MyException() : base() { }

        public MyException(string message) : base(message) { }

    }
}
