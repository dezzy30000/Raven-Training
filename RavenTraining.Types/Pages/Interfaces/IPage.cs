namespace RavenTraining.Types.Pages.Interfaces
{
    public interface IPage
    {
        string Id { get; set; }
        string Slug { get; set; }
        string Path { get; set; }
    }
}
