using System;
using System.Collections.Generic;

namespace ConventionalRegistration.Syntax
{
    /// <summary>
    /// Provides methods to execute a registration.
    /// </summary>
    public interface IRegisterSyntax
    {
        /// <summary>
        /// Register one registration at a time for each registrations. Registration is marked as registered automatically.
        /// </summary>
        /// <param name="registrationExecution">Action method to do a user defined registration.</param>
        void RegisterEach(Action<IRegistrationEntry> registrationExecution);

        /// <summary>
        /// Register all registration in one call. All registrations is marked as registered automatically.
        /// </summary>
        /// <param name="registrationExecution">Action method to do a user defined registrations.</param>
        void RegisterAll(Action<IEnumerable<IRegistrationEntry>> registrationExecution);
    }
}
