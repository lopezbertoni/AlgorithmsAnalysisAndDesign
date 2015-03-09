using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion4
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = ReadData("TestCase1.txt");
            var t2 = ReadData("TestCase2.txt");
            DfsLoop(t1);
            DfsLoop(t2);
            //var t = ReadData("SCC.txt"); 
        }

        private static void DfsLoop(List<Node> graph)
        {
            var n = graph.Count;
            var index = n - 1;

            for (var j = 0; j < n; j++)
            {
                Console.WriteLine("Checking index {0} with value {1}", index, graph[index].V);
                if (!graph[index].Visited)
                {
                    Dfs(graph, index);
                }
                index--;
            }
        }

        private static void Dfs(List<Node> graph, int nodeIndex)
        {
            //Mark nodes as visited
            graph[nodeIndex].Visited = true;

            foreach (var edge in graph[nodeIndex].Edges)
            {
                var node = edge;
                var eindex = graph.FirstOrDefault(x => x.V == node);
                if (eindex != null)
                {
                    if (!graph[eindex.Index].Visited)
                    {
                        Console.WriteLine("Traversing node {0}", edge);
                        Dfs(graph, edge - 1);
                    }
                }
            }
        }

        private static List<Node> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new List<Node>();

            var v = 0;
            
            foreach (var s in txtData)
            {
                var x = s.Split(' ');
                
                //Check if new node
                var currentNode = Convert.ToInt32(x[0]);
                var edge = Convert.ToInt32(x[1]);
                if (currentNode == v)
                {
                    //Add to current node
                    var index = inputData.Count;
                    inputData[index - 1].Edges.Add(edge);
                }
                else
                {
                    //New node so add vertex and edges
                    var e = new List<int>();
                    e.Add(edge);
                    inputData.Add(new Node { V = currentNode, Edges = e, Visited = false, Index = inputData.Count });
                }
                v = currentNode;
            }
            return inputData;
        }
    }
}
