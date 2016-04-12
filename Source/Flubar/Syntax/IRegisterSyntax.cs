using System;
using System.Collections.Generic;

namespace Flubar.Syntax
{
    public interface IRegisterSyntax
    {
        /// <summary>
        /// Register one registration at a time for each registrations. Registration is marked as registered automatically.
        /// </summary>
        /// <param name="handleRegistration">Action method to do a user defined registration.</param>
        void RegisterEach(Action<IRegistrationEntry> handleRegistration);

        ///// <summary>
        ///// Register one registration at a time for each registrations. Need to call IServiceExclusion methods to mark services as registered.
        ///// </summary>
        ///// <param name="handleRegistration">Action method to do a user defined registration. Second parameter INotify van be used to mark the current registration as registered.</param>
        //void RegisterEach(Action<IRegistrationEntry, IServiceExclusion> handleRegistration);

        /// <summary>
        /// Register all registration in one call. All registrations is marked as registered automatically.
        /// </summary>
        /// <param name="handleRegistration">Action method to do a user defined registrations.</param>
        void RegisterAll(Action<IEnumerable<IRegistrationEntry>> handleRegistration);

        ///// <summary>
        ///// Register all registration in one call. Need to call IServiceExclusion methods to mark services as registered.
        ///// </summary>
        ///// <param name="handleRegistration">Action method to do a user defined registrations. Second parameter INotify van be used to mark the current registration as registered.</param>
        //void RegisterAll(Action<IEnumerable<IRegistrationEntry>, IServiceExclusion> handleRegistration);
    }
}
