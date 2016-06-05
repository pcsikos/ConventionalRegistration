using System;

namespace Flubar
{
    public interface IBehaviorConfiguration
    {
        ITypeFilter GetTypeFilter();
        Action<DiagnosticLevel, string> Log { get; }
    }
}
