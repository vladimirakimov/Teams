using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using System;
using System.Linq;
using System.Reflection;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps
{
    public static class ClassMapsRegistrator
    {
        public static void RegisterMaps()
        {
            var assembly = Assembly.GetAssembly(typeof(ClassMapsRegistrator));

            var classMaps = assembly.GetTypes()
                  .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                    t.BaseType.GetGenericTypeDefinition() == typeof(DomainClassMap<>));

            foreach (var classMap in classMaps)
            {
                Activator.CreateInstance(classMap);
            }
        }
    }
}
