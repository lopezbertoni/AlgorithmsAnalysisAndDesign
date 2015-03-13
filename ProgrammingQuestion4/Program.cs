using System;
using System.Collections;
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
            //ComputeScc("TestCase1.txt");
            //ComputeScc("TestCase2.txt");
            //ComputeScc("TestCase3.txt"); 
            //ComputeScc("TestCase4.txt");
            //ComputeScc("TestCase5.txt");
            //ComputeScc("TestCase6.txt");
            //ComputeScc("TestCase7.txt");
            //ComputeScc("TestCase8.txt");
            //ComputeScc("mediumDG.txt");

            ComputeScc("SCC.txt");
        }

        private static int t = 0;
        private static readonly Stack<int> NodesFinishingTimes = new Stack<int>(); 
        private static readonly Dictionary<int, int> Results = new Dictionary<int, int>();
        private static int SccCount = 1;
        private static readonly HashSet<int> Visited = new HashSet<int>();

        private static void ComputeScc(string filename)
        {
            t = 0;
            NodesFinishingTimes.Clear();
            Visited.Clear();
            Results.Clear();
            var graph = ReadDataStack(filename);
            var grev = new HashSet<Node>(graph.Select(x => new Node { V = x.E, E = x.V }).ToList());

            //Compute DFS on reverse graph.
            DfsLoop(grev);

            Visited.Clear();
            SccCount = 1;
            //Compute DFS on graph
            DfsLoopStack(graph);

            Console.WriteLine("Results for {0} are {1}", filename, String.Join(",", Results.Values.OrderByDescending(x => x).Take(5)));
        }

        private static void DfsLoop(HashSet<Node> graph)
        {
            var n = graph.Count;
            var index = n - 1;

            foreach (var node in graph)
            {
                if (!Visited.Contains(node.V))
                {
                    Console.WriteLine("Checking index {0} with value {1}", index, node.V);
                    DfsRecursive(graph, node.V);
                }
                index--;
            }
            //foreach (var x in NodesFinishingTimes)
            //{
            //    Console.WriteLine("Node index {0} with value {1}", x, graph[x].V);
            //}
        }

        private static void DfsRecursive(HashSet<Node> graph, int v)
        {
            //Mark nodes as visited
            Visited.Add(v);
            //Get node edges
            var edges = graph.Where(x => x.V == v).Select(x => x.E).ToList();
            foreach (var edge in edges)
            {
                if (!Visited.Contains(edge))
                {
                    SccCount++;
                    DfsRecursive(graph, edge);
                }
            }
            t++;
            NodesFinishingTimes.Push(v);
            //Console.WriteLine("Finishing Time of node V {0} = {1}", v, t);
        }
        
        private static void DfsLoopStack(HashSet<Node> graph)
        {
            while (NodesFinishingTimes.Count > 0)
            {
                var v = NodesFinishingTimes.Pop();
                if (!Visited.Contains(v))
                {
                    //Console.WriteLine("Checking V = {0}", v);
                    DfsRecursive(graph, v);
                    Results.Add(v, SccCount);
                    SccCount = 1;
                }
            }
        }

        private static HashSet<Node> ReadDataStack(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var list = txtData.Select(s => s.Split(' ')).Select(x => new Node{V = Convert.ToInt32(x[0]), E = Convert.ToInt32(x[1])}).ToList();
            return new HashSet<Node>(list);
        }
    }
}
