using System.Collections.Generic;

namespace RavenTraining.Types
{
    public class PagesHierarchyTree
    {
        public class Node
        {
            public string Id { get; set; }
            public List<Node> Children { get; set; }
        }

        public Node Root { get; set; }
    }
}
