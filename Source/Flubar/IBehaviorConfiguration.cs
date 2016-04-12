using System;

namespace Flubar
{
    public interface IBehaviorConfiguration
    {
        IServiceFilter GetServiceFilter();
        Action<DiagnosticLevel, string> Log { get; }
    }
}
