using System;
using System.Collections.Generic;
using Flubar.Syntax;

namespace Flubar
{
    class RegistrationHandler : IRegisterSyntax
    {
        private readonly IEnumerable<IRegistrationEntry> registrations;
        private readonly ITypeExclusion notify;

        public RegistrationHandler(IEnumerable<IRegistrationEntry> registrations, ITypeExclusion notify)
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

        public void RegisterEach(Action<IRegistrationEntry, ITypeExclusion> handleRegistration)
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

        public void RegisterAll(Action<IEnumerable<IRegistrationEntry>, ITypeExclusion> handleRegistration)
        {
            handleRegistration(registrations, notify);
        }

        #endregion
    }
}
