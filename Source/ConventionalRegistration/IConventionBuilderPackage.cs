namespace ConventionalRegistration
{
    /// <summary>
    /// Defines a method to support the registration from multiple packages.
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <typeparam name="TLifetime"></typeparam>
    public interface IConventionBuilderPackage<TContainer, TLifetime>
        where TLifetime : class
    {
        void RegisterByConvention(IConventionBuilderSyntax<TLifetime, TContainer> builder);
    }
}
