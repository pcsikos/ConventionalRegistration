using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IBehaviorConfiguration
    {
        ITypeFilter GetServiceFilter();
        Action<DiagnosticLevel, string> Log { get; }
    }
}
