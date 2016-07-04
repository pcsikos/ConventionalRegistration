using System;
using Flubar.Diagnostics;
using Flubar.TypeFiltering;

namespace Flubar.Configuration
{
    public interface IBehaviorConfiguration
    {
        IServiceFilter GetServiceFilter();
        Action<DiagnosticLevel, string> Log { get; }
    }
}
