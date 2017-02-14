using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using RavenTraining.Console.ViewModels;
using RavenTraining.Plugins.CompilationExtensions;
using RavenTraining.Types;
using System.Collections.Generic;
using System.Linq;
using static RavenTraining.Types.PagesHierarchyTree;

namespace RavenTraining.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var store = new DocumentStore { Url = "http://localhost:8080/", DefaultDatabase = "dukaan" })
            {
                store.Initialize();

                using (var session = store.OpenSession())
                {
                    var result = session
                        .Query<Page>()
                        .Where(page => page.Slug == "dresses")
                        .ProjectFromIndexFieldsInto<Page>()
                        .Single();
                }

                //new PageViewModels_Query().Execute(store);

                //using (var session = store.OpenSession())
                //{
                //    var home = new Page { Slug = "/" };
                //    var men = new Page { Slug = "men" };
                //    var women = new Page { Slug = "women" };
                //    var dresses = new Page { Slug = "dresses" };
                //    var coats = new Page { Slug = "coats" };
                //    var trousers = new Page { Slug = "trousers" };
                //    var blazers = new Page { Slug = "blazers" };

                //    session.Store(home);
                //    session.Store(men);
                //    session.Store(women);
                //    session.Store(dresses);
                //    session.Store(coats);
                //    session.Store(trousers);
                //    session.Store(blazers);

                //    session.SaveChanges();

                //    session.Store(new PagesHierarchyTree
                //    {
                //        Root = new Node
                //        {
                //            Id = home.Id,
                //            Children = new List<Node>
                //            {
                //                new Node
                //                {
                //                    Id = men.Id,
                //                    Children = new List<Node>
                //                    {
                //                        new Node { Id = trousers.Id },
                //                        new Node { Id = blazers.Id }
                //                    }
                //                },
                //                new Node
                //                {
                //                    Id = women.Id,
                //                    Children = new List<Node>
                //                    {
                //                        new Node { Id = dresses.Id },
                //                        new Node { Id = coats.Id }
                //                    }
                //                }
                //            }
                //        }
                //    });

                //    session.SaveChanges();
                //}
            }
        }
    }

    public class PageViewModels_Query : AbstractIndexCreationTask<Page>
    {
        private const string PagesHierarchyTreeNodeId = "PagesHierarchyTrees/1";

        public PageViewModels_Query()
        {
            Map = pages => (from page in pages
                            select new PageViewModel
                            {
                                Id = page.Id,
                                Slug = page.Slug,
                                Path = PathBuilder.Build(PathCalculator.GetPathForNodeWithId(LoadDocument<PagesHierarchyTree>(PagesHierarchyTreeNodeId), page).Select(id => LoadDocument<Page>((string)id).Slug))
                            });

            StoreAllFields(FieldStorage.Yes);
        }
    }
}
