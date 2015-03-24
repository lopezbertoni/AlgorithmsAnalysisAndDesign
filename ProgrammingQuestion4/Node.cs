using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion4
{
    public class NodeExpanded : Node
    {
        public List<int> Edges { get; set; }
        public bool Visited { get; set; }
        public int Index { get; set; }
    }

    public class Node
    {
        public int V { get; set; }
        public int E { get; set; }
    }
}
