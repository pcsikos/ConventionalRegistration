using System;
using System.Collections.Generic;
using Flubar.Syntax;

namespace Flubar
{
    class RegistrationHandler : IRegisterSyntax
    {
        private readonly IEnumerable<IRegistrationEntry> registrations;
        private readonly IConfigurationServiceExclusion notify;

        public RegistrationHandler(IEnumerable<IRegistrationEntry> registrations, IConfigurationServiceExclusion notify)
        {
            this.registrations = registrations;
            this.notify = notify;
        }

        #region IRegisterSyntax Members

        public void RegisterEach(Action<IRegistrationEntry> handleRegistration)
        {
            foreach (var registration in registrations)
            {
                handleRegistration(registration);
            }
        }

        public void RegisterEach(Action<IRegistrationEntry, IConfigurationServiceExclusion> handleRegistration)
        {
            foreach (var registration in registrations)
            {
                handleRegistration(registration, notify);
            }
        }

        public void RegisterAll(Action<IEnumerable<IRegistrationEntry>> handleRegistration)
        {
            handleRegistration(registrations);
        }

        public void RegisterAll(Action<IEnumerable<IRegistrationEntry>, IConfigurationServiceExclusion> handleRegistration)
        {
            handleRegistration(registrations, notify);
        }

        #endregion
    }
}
