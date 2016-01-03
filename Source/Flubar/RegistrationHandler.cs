using System;
using System.Collections.Generic;
using Flubar.Syntax;

namespace Flubar
{
    class RegistrationHandler : IRegisterSyntax
    {
        private readonly IEnumerable<IRegistrationEntry> registrations;
        private readonly IServiceExclusion serviceExclusion;

        public RegistrationHandler(IEnumerable<IRegistrationEntry> registrations, IServiceExclusion serviceExclusion)
        {
            this.registrations = registrations;
            this.serviceExclusion = serviceExclusion;
        }

        #region IRegisterSyntax Members

        public void RegisterEach(Action<IRegistrationEntry> handleRegistration)
        {
            foreach (var registration in registrations)
            {
                handleRegistration(registration);
            }
        }

        public void RegisterEach(Action<IRegistrationEntry, IServiceExclusion> handleRegistration)
        {
            foreach (var registration in registrations)
            {
                handleRegistration(registration, serviceExclusion);
            }
        }

        public void RegisterAll(Action<IEnumerable<IRegistrationEntry>> handleRegistration)
        {
            handleRegistration(registrations);
        }

        public void RegisterAll(Action<IEnumerable<IRegistrationEntry>, IServiceExclusion> handleRegistration)
        {
            handleRegistration(registrations, serviceExclusion);
        }

        #endregion
    }
}
