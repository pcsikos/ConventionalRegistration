using System;
using System.Collections.Generic;

namespace Flubar
{
    public interface IBehaviorConfiguration
    {
        IServiceFilter GetServiceFilter();
    }
}
