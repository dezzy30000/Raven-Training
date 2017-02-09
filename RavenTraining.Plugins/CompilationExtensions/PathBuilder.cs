using System.Collections.Generic;

namespace RavenTraining.Plugins.CompilationExtensions
{
    public static class PathBuilder
    {
        public static string Build(IEnumerable<string> slugs)
        {
            return string.Join("/", slugs).Replace("//", "/");
        }
    }
}
