using Raven.Abstractions.Linq;
using System.Collections.Generic;

namespace RavenTraining.Plugins.CompilationExtensions
{
    public static class PathCalculator
    {
        public static DynamicList GetPathForNodeWithId(dynamic pagesHierarchyTree, dynamic page)
        {
            var paths = new List<string>();

            if (LocateNode(paths, pagesHierarchyTree.Root, page.Id))
            {
                paths.Add(pagesHierarchyTree.Root.Id);
                paths.Reverse();
            }

            return new DynamicList(paths.ToArray());
        }

        private static bool LocateNode(dynamic paths, dynamic currentNode, string id)
        {
            if (currentNode.Id == id)
            {
                return true;
            }

            if (currentNode.Children == null)
            {
                return false;
            }

            foreach (var node in currentNode.Children)
            {
                if (LocateNode(paths, node, id))
                {
                    paths.Add(node.Id);
                    return true;
                }
            }

            return false;
        }
    }
}
