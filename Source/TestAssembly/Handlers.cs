using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAssembly
{
    public interface IHandler
    {
        void Execute(string command);
    }

    class MailCommand : IHandler
    {
        public void Execute(string command)
        {
            throw new NotImplementedException();
        }
    }

    class SaveCommand : IHandler
    {
        public void Execute(string command)
        {
            throw new NotImplementedException();
        }
    }
}
