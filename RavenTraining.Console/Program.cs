using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;
using Raven.Json.Linq;
using RavenTraining.Console.Indexes;
using RavenTraining.Types;
using RavenTraining.Types.Pages;
using RavenTraining.Types.Pages.Interfaces;
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

                //CreateIndexes(store);

                using (var session = store.OpenSession())
                {
                    //Populate(session);

                    var result = session
                        .Query<IPage, Pages_BySlug_ForAll>()
                        .Where(page => page.Slug == "parkas")
                        .ProjectFromIndexFieldsInto<IPage>()
                        .OfType<RavenJObject>()
                        .Single();

                    var obj = result.ToObject<DetailsPage>();
                }
            }
        }

        #region Setup methods

        private static void CreateIndexes(DocumentStore store)
        {
            new Pages_BySlug_ForAll().Execute(store);
        }

        private static void Populate(IDocumentSession session)
        {
            var home = new HomePage { Slug = "/" };
            var men = new LandingPage { Slug = "men" };
            var women = new LandingPage { Slug = "women" };
            var dresses = new LandingPage { Slug = "dresses" };
            var coats = new LandingPage { Slug = "coats" };
            var trousers = new LandingPage { Slug = "trousers" };
            var blazers = new LandingPage { Slug = "blazers" };

            var coatPage = new DetailsPage { Slug = "parkas" };

            session.Store(home);
            session.Store(men);
            session.Store(women);
            session.Store(dresses);
            session.Store(coats);
            session.Store(trousers);
            session.Store(blazers);
            session.Store(coats);
            session.Store(coatPage);

            session.SaveChanges();

            session.Store(new PagesHierarchyTree
            {
                Root = new Node
                {
                    Id = home.Id,
                    Children = new List<Node>
                            {
                                new Node
                                {
                                    Id = men.Id,
                                    Children = new List<Node>
                                    {
                                        new Node { Id = trousers.Id },
                                        new Node { Id = blazers.Id }
                                    }
                                },
                                new Node
                                {
                                    Id = women.Id,
                                    Children = new List<Node>
                                    {
                                        new Node { Id = dresses.Id },
                                        new Node
                                        {
                                            Id = coats.Id,
                                            Children = new List<Node>
                                            {
                                                new Node { Id = coatPage.Id }
                                            }
                                        }
                                    }
                                }
                            }
                }
            });

            session.SaveChanges();
        }

        #endregion
    }
}
