using System;

namespace ConventionalRegistration.RegistrationProducers
{
    public interface IRegistrationProducer
    {
        IRegistrationEntry CreateRegistrationEntry(Type type);
    }
}
