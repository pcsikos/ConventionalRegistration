namespace Flubar.Syntax
{
    public interface ILifetimeSyntax<TLifetime>
        where TLifetime : class
    {
        TLifetime Singleton { get; }
        TLifetime Transient { get; }
    }
}
