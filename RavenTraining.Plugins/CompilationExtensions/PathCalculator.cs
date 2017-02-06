using RavenTraining.Types;
using System.Collections.Generic;
using static RavenTraining.Types.PagesHierarchyTree;

namespace RavenTraining.Plugins.CompilationExtensions
{
    public static class PathCalculator
    {
        public static string[] GetPathForNodeWithId(PagesHierarchyTree pagesHierarchyTree, Page page)
        {
            var paths = new List<string>();

            if (LocateNode(paths, pagesHierarchyTree.Root, page.Id))
            {
                paths.Add(pagesHierarchyTree.Root.Id);
                paths.Reverse();
            }

            return paths.ToArray();
        }

        private static bool LocateNode(List<string> paths, Node currentNode, string id)
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
