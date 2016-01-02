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

    public class MailHandler : IHandler
    {
        public void Execute(string command)
        {
            throw new NotImplementedException();
        }
    }

    [ExcludeFromRegistration]
    public class SaveHandler : IHandler
    {
        public void Execute(string command)
        {
            throw new NotImplementedException();
        }
    }
}
