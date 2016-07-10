using System;
using System.Collections.Generic;
using Flubar.Syntax;

namespace Flubar
{
    /// <summary>
    /// Executes the act of registration itself by calling a passed handler.
    /// </summary>
    class RegistrationPerformer : IRegisterSyntax
    {
        private readonly IEnumerable<IRegistrationEntry> registrations;

        public RegistrationPerformer(IEnumerable<IRegistrationEntry> registrations)
        {
            this.registrations = registrations;
        }

        #region IRegisterSyntax Members

        public void RegisterEach(Action<IRegistrationEntry> registrationExecution)
        {
            foreach (var registration in registrations)
            {
                registrationExecution(registration);
            }
        }

        public void RegisterAll(Action<IEnumerable<IRegistrationEntry>> registrationExecution)
        {
            registrationExecution(registrations);
        }

        #endregion
    }
}
