using System;
using System.Collections.Generic;

namespace TestAssembly
{
    public class CustomCommand : ICommand
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

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        string Handle(TCommand command);
    }

    public class CustomCommandHandler : ICommandHandler<CustomCommand>
    {
        public string Handle(CustomCommand command)
        {
            return "a";
        }
    }

    public class TransactionCommandHandler<TCommand> : ICommandHandler<TCommand>, ITransaction where TCommand : ICommand
    {
        readonly ICommandHandler<TCommand> commandHandler;

        public TransactionCommandHandler(ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public string Handle(TCommand command)
        {
            return "b" + commandHandler.Handle(command) + "b";
        }
    }

    public class LoggerCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        readonly ICommandHandler<TCommand> commandHandler;

        public LoggerCommandHandler(ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public string Handle(TCommand command)
        {
            return "c" + commandHandler.Handle(command) + "c";
        }
    }

    public interface ITransaction
    {

    }

    public interface ICommandValidator<TCommand> where TCommand : ICommand
    {
        bool Validate(TCommand command);
    }

    public abstract class AbstractCommandValidator<TCommand> : ICommandValidator<TCommand> where TCommand : ICommand
    {
        protected abstract bool ValidateCommand(TCommand command);

        #region ICommandValidator<TCommand> Members

        public bool Validate(TCommand command)
        {
            return ValidateCommand(command);
        }

        #endregion
    }

    public abstract class CustomerCommand : ICommand
    {
        public Guid CustomerId { get; set; }

        public string GetString()
        {
            throw new NotImplementedException();
        }
    }

    public class PlaceOrderCommand : CustomerCommand
    {
        public IEnumerable<Product> Products { get; set; }
    }

    public class CancelOrderCommand : CustomerCommand
    {
        public Guid OrderId { get; set; }
    }

    public abstract class CustomerCommandValidator<TCustomerCommand> : AbstractCommandValidator<TCustomerCommand> where TCustomerCommand : CustomerCommand
    {
        protected override bool ValidateCommand(TCustomerCommand command)
        {
            //do something
            return true;
        }
    }

    public class PlaceOrderCommandValidator : CustomerCommandValidator<PlaceOrderCommand>
    {
        protected override bool ValidateCommand(PlaceOrderCommand command)
        {
            return base.ValidateCommand(command);
        }
    }

    public class CancelOrderCommandValidator : CustomerCommandValidator<CancelOrderCommand>
    {
        protected override bool ValidateCommand(CancelOrderCommand command)
        {
            return base.ValidateCommand(command);
        }
    }


    public class ChangeDeliveryAddressCommand : ICommand
    {
        public string GetString()
        {
            throw new NotImplementedException();
        }
    }

    public class ChangeInvoiceAddressCommand : ICommand
    {
        public string GetString()
        {
            throw new NotImplementedException();
        }
    }

    public class AddressCommandValidator : ICommandValidator<ChangeDeliveryAddressCommand>, ICommandValidator<ChangeInvoiceAddressCommand>
    {
        public bool Validate(ChangeInvoiceAddressCommand command)
        {
            throw new NotImplementedException();
        }

        public bool Validate(ChangeDeliveryAddressCommand command)
        {
            throw new NotImplementedException();
        }
    }


}
