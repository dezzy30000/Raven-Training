using Raven.Database.Plugins;
using RavenTraining.Types;

namespace RavenTraining.Plugins.CompilationExtensions
{
    public class PathCalculatorDynamicCompilationExtension : AbstractDynamicCompilationExtension
    {
        public override string[] GetNamespacesToImport()
        {
            return new[]
            {
                typeof (PathCalculator).Namespace,
                typeof (Page).Namespace
            };
        }

        public override string[] GetAssembliesToReference()
        {
            return new[]
            {
                typeof (PathCalculator).Assembly.Location,
                typeof (Page).Assembly.Location
            };
        }
    }
}
