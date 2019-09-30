using System;
using System.Linq;
using System.Reflection;

namespace ITG.Brix.Teams.Domain.Extensions
{
    public static class TypeExtensions
    {
        private static readonly char _separator = ',';

        public static string ToPrettyString(this Type type)
        {
            string result;
            try
            {
                result = GetRecursive(type, 0);
            }
            catch (Exception)
            {
                result = type.Name;
            }
            return result;
        }

        private static string GetRecursive(Type type, int depth)
        {
            if (depth > 3)
            {
                return type.Name;
            }

            var parts = type.Name.Split('`');
            if (parts.Length == 1)
            {
                return parts[0];
            }

            var genericArguments = type.GetTypeInfo().GetGenericArguments();
            return !type.IsConstructedGenericType
                ? string.Format("{0}<{1}>", parts[0], new string(_separator, genericArguments.Length - 1))
                : string.Format("{0}<{1}>", parts[0], string.Join(new string(_separator, 1), genericArguments.Select(t => GetRecursive(t, depth + 1))));
        }
    }
}
