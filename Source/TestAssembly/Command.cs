namespace TestAssembly
{
    public class Command : ICommand
    {
        public string GetString()
        {
            return "a";
        }
    }

    public interface ICommand
    {
        string GetString();
    }

    public class TransactionCommand : ICommand, ITransaction
    {
        readonly ICommand command;

        public TransactionCommand(ICommand command)
        {
            this.command = command;
        }

        public string GetString()
        {
            return "b" + command.GetString() + "b";
        }
    }

    public class LoggerCommand : ICommand
    {
        readonly ICommand command;

        public LoggerCommand(ICommand command)
        {
            this.command = command;
        }

        public string GetString()
        {
            return "c" + command.GetString() + "c";
        }
    }

    public interface ITransaction
    {

    }
}
