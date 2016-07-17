namespace Flubar
{
    public static class Strings
    {
        internal static string ArgumentIsNullOrWhitespace(string parameterName)
        {
            return string.Format("The argument '{0}' cannot be null, empty or contain only white space.", parameterName);
        }

        internal static string ArgumentIsNullOrEmpty(string parameterName)
        {
            return string.Format("The argument '{0}' cannot be null or empty.", parameterName);
        }
    }
}
