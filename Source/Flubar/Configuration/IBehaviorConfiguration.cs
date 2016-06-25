using System;
using Flubar.Diagnostics;
using Flubar.TypeFiltering;

namespace Flubar.Configuration
{
    public interface IBehaviorConfiguration
    {
        ITypeFilter GetTypeFilter();
        Action<DiagnosticLevel, string> Log { get; }
    }
}
