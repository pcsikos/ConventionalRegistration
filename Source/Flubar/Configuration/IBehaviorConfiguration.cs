using System;
using ConventionalRegistration.Diagnostics;
using ConventionalRegistration.TypeFiltering;

namespace ConventionalRegistration.Configuration
{
    public interface IBehaviorConfiguration
    {
        IServiceFilter GetServiceFilter();
        Action<DiagnosticLevel, string> Log { get; }
    }
}
