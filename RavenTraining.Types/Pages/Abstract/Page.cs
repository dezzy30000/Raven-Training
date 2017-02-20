using RavenTraining.Types.Pages.Interfaces;

namespace RavenTraining.Types.Pages.Abstract
{
    public abstract class Page : IPage
    {
        public string Id { get; set; }

        public string Slug { get; set; }

        public string Path { get; set; }
    }
}
