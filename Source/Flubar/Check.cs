using System;
using System.Collections.Generic;
using System.Linq;

namespace Flubar
{
    public static class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Strings.ArgumentIsNullOrWhitespace(parameterName), parameterName);
            }

            return value;
        }

        public static IEnumerable<T> NotEmpty<T>(IEnumerable<T> enumerable, string parameterName)
        {
            NotNull(enumerable, parameterName);
            if (!enumerable.Any())
            {
                throw new ArgumentException(Strings.ArgumentIsNullOrEmpty(parameterName), parameterName);
            }
            return enumerable;
        }
    }
}
