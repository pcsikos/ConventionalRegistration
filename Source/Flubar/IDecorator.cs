namespace Flubar
{
    /// <summary>
    /// Provides members to expose the decorated object.
    /// </summary>
    public interface IDecorator
    {
        object Decoratee { get; }
    }
}
