using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using RavenTraining.Plugins.CompilationExtensions;
using RavenTraining.Types;
using RavenTraining.Types.Pages;
using RavenTraining.Types.Pages.Interfaces;
using System.Linq;

namespace RavenTraining.Console.Indexes
{
    public class Pages_BySlug_ForAll : AbstractMultiMapIndexCreationTask<IPage>
    {
        public const string PagesHierarchyTreeNodeId = "PagesHierarchyTrees/1";

        public Pages_BySlug_ForAll()
        {
            AddMap<HomePage>(homePages => from homepage in homePages
                                          select new
                                          {
                                              Id = homepage.Id,
                                              Slug = homepage.Slug,
                                              Path = PathBuilder.Build(PathCalculator.GetPathForNodeWithId(LoadDocument<PagesHierarchyTree>(PagesHierarchyTreeNodeId), homepage).Select(id => LoadDocument<HomePage>((string)id).Slug))
                                          });

            AddMap<LandingPage>(landingPages => from landingPage in landingPages
                                                select new
                                                {
                                                    Id = landingPage.Id,
                                                    Slug = landingPage.Slug,
                                                    Path = PathBuilder.Build(PathCalculator.GetPathForNodeWithId(LoadDocument<PagesHierarchyTree>(PagesHierarchyTreeNodeId), landingPage).Select(id => LoadDocument<LandingPage>((string)id).Slug))
                                                });

            AddMap<DetailsPage>(detailPages => from detailsPage in detailPages
                                               select new
                                               {
                                                   Id = detailsPage.Id,
                                                   Slug = detailsPage.Slug,
                                                   Path = PathBuilder.Build(PathCalculator.GetPathForNodeWithId(LoadDocument<PagesHierarchyTree>(PagesHierarchyTreeNodeId), detailsPage).Select(id => LoadDocument<DetailsPage>((string)id).Slug))
                                               });

            Stores.Add(page => page.Path, FieldStorage.Yes);
        }
    }
}
