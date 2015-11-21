using System;

namespace Flubar.RegistrationProducers
{
    public interface IRegistrationProducer
    {
        IRegistrationEntry CreateRegistrationEntry(Type type);
    }
}
