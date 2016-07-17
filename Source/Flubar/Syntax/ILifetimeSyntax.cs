namespace Flubar.Syntax
{
    /// <summary>
    /// Provides simplified lifetime selectors.
    /// </summary>
    /// <typeparam name="TLifetime"></typeparam>
    public interface ILifetimeSyntax<TLifetime>
        where TLifetime : class
    {
        TLifetime Singleton { get; }
        TLifetime Transient { get; }
    }
}
