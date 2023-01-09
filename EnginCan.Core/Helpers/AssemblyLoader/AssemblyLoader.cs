using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace EnginCan.Core.Helpers.AssemblyLoader
{
    public static class AssemblyLoader
    {
        private static readonly List<string> _ASSEMBLIES = new List<string>()
        {
            "Interop.zkemkeeper.dll"
        };

        public static void Invoke()
        {
            string folderName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            foreach (var assembly in _ASSEMBLIES)
            {
                AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(folderName, assembly));
            }
        }
    }
}