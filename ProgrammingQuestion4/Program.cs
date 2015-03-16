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
            ComputeScc("TestCase6.txt");
            //ComputeScc("TestCase7.txt");
            //ComputeScc("TestCase8.txt");
            ComputeScc("mediumDG.txt");
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
            var grev = ReadDataRev(filename);

            //Compute DFS on reverse graph.
            DfsLoopStack(grev);

            Visited.Clear();
            Results.Clear();
            SccCount = 1;
            //Compute DFS on graph
            var graph = ReadData(filename);
            DfsLoopStackSecondPass(graph);

            Console.WriteLine("Results for {0} are {1}", filename, String.Join(",", Results.Values.OrderByDescending(x => x).Take(5)));
            //t = 0;
            //NodesFinishingTimes.Clear();
            //Visited.Clear();
            //Results.Clear();
            //var graph = ReadDataStack(filename);
            //var grev = new HashSet<Node>(graph.Select(x => new Node { V = x.E, E = x.V }).ToList());

            ////Compute DFS on reverse graph.
            //DfsLoop(grev);

            //Visited.Clear();
            //SccCount = 1;
            ////Compute DFS on graph
            //DfsLoopStack(graph);

            //Console.WriteLine("Results for {0} are {1}", filename, String.Join(",", Results.Values.OrderByDescending(x => x).Take(5)));
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

        private static void DfsLoop(Dictionary<int, List<int>> graph)
        {
            var n = graph.Count;
            var index = n - 1;

            foreach (var node in graph)
            {
                if (!Visited.Contains(node.Key))
                {
                    //Console.WriteLine("Checking node {0}", node.Key);
                    DfsRecursive(graph, node.Key);
                }
                index--;
            }
            //foreach (var x in NodesFinishingTimes)
            //{
            //    Console.WriteLine("Node index {0} with value {1}", x, graph[x].V);
            //}
        }

        private static void DfsRecursive(Dictionary<int, List<int>> graph, int v)
        {
            //Mark nodes as visited
            Visited.Add(v);
            //Get node edges
            
            var edges = new List<int>();
            graph.TryGetValue(v, out edges);
            if (edges != null)
            {
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
                Console.WriteLine("Finishing Time of node V {0} = {1}", v, t);
            }
        }

        private static void DfsLoopStack(Dictionary<int, List<int>> graph)
        {
            var n = graph.Count;
            var index = n - 1;

            foreach (var node in graph)
            {
                if (!Visited.Contains(node.Key))
                {
                    //Console.WriteLine("Checking node {0}", node.Key);
                    DfsStack(graph, node.Key);
                }
                index--;
            }
            //foreach (var x in NodesFinishingTimes)
            //{
            //    Console.WriteLine("Node index {0} with value {1}", x, graph[x].V);
            //}
        }

        private static void DfsLoopStackSecondPass(Dictionary<int, List<int>> graph)
        {
            while (NodesFinishingTimes.Count > 0)
            {
                var v = NodesFinishingTimes.Pop();
                if (!Visited.Contains(v))
                {
                    //Console.WriteLine("Checking node {0}", v);
                    DfsStack(graph, v);
                }
            }
        }

        private static void DfsStack(Dictionary<int, List<int>> graph, int v)
        {
            var s = new Stack<int>(graph.Count);
            s.Push(v);
            var ft = new List<int>();
            SccCount = 0;
            while (s.Count > 0)
            {
                var currentV = s.Pop();
                if (!Visited.Contains(currentV))
                {
                    Visited.Add(currentV);
                    SccCount++;
                    ft.Add(currentV);
                    var edges = new List<int>();
                    graph.TryGetValue(currentV, out edges);
                    if (edges != null)
                    {
                        foreach (var edge in edges)
                        {
                            if (!Visited.Contains(edge))
                            {
                                s.Push(edge);
                                
                                //Console.WriteLine("Edge = {0}", edge);
                            }
                        }
                    }
                }
            }
            Results.Add(v, SccCount);
            var ftindex = ft.Count - 1;
            for (var i = ftindex; i >= 0; i--)
            {
                NodesFinishingTimes.Push(ft[i]);
            }
        }

        private static Dictionary<int, List<int>> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new Dictionary<int, List<int>>();

            foreach (var s in txtData)
            {
                var x = s.Split(' ');

                //Check if new node
                var currentNode = Convert.ToInt32(x[0]);
                var currentEdge = Convert.ToInt32(x[1]);

                var e = new List<int>();
                if (inputData.ContainsKey(currentNode))
                {
                    //Add it to its edges
                    inputData.TryGetValue(currentNode, out e);
                    e.Add(currentEdge);
                    inputData[currentNode] = e;
                }
                else
                {
                    e.Add(currentEdge);
                    inputData[currentNode] = e;
                }

            }
            return inputData;
        }

        private static Dictionary<int, List<int>> ReadDataRev(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new Dictionary<int, List<int>>();

            foreach (var s in txtData)
            {
                var x = s.Split(' ');

                //Check if new node
                var currentNode = Convert.ToInt32(x[1]);
                var currentEdge = Convert.ToInt32(x[0]);

                var e = new List<int>();
                if (inputData.ContainsKey(currentNode))
                {
                    //Add it to its edges
                    inputData.TryGetValue(currentNode, out e);
                    e.Add(currentEdge);
                    inputData[currentNode] = e;
                }
                else
                {
                    e.Add(currentEdge);
                    inputData[currentNode] = e;
                }

            }
            return inputData;
        }
    }
}
