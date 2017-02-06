using Raven.Database.Plugins;

namespace RavenTraining.Plugins.CompilationExtensions
{
    public class PathBuilderDynamicCompilationExtension : AbstractDynamicCompilationExtension
    {
        public override string[] GetNamespacesToImport()
        {
            return new[]
            {
                typeof (PathBuilder).Namespace
            };
        }

        public override string[] GetAssembliesToReference()
        {
            return new[]
            {
                typeof (PathBuilder).Assembly.Location
            };
        }
    }
}
