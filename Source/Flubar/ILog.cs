using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar
{
    interface ILog
    {
        void Info(string message);
        void Info(string format, params object[] args);
        void Warning(string message);
    }
}
