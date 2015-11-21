using System;

namespace Flubar.Syntax
{
    public interface ITypeSourceSyntax
    {
        IStrategySyntax ExplicitlySpecifyTypes(params Type[] types);
    }
}
