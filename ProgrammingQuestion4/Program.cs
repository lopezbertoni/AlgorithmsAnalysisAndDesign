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
            //var testCase1 = ReadData("TestCase1.txt");
            //DfsLoop(testCase1);
            t = 0;
            var testCase2 = ReadData("TestCase2.txt");
            DfsLoop(testCase2);
            //t = 0;
            //var testCase3 = ReadData("TestCase3.txt");
            //DfsLoop(testCase3);
            t = 0;
            var testCase4 = ReadData("TestCase4.txt");
            DfsLoop(testCase4);
            //var testCase5 = ReadData("TestCase5.txt");
            //DfsLoop(testCase5);
            //var testCase6 = ReadData("TestCase6.txt");
            //DfsLoop(testCase6);
            //var t = ReadData("SCC.txt");
            //DfsLoop(t);
            //var testCase1 = ReadDataStack("TestCase1.txt");
            //DfsLoop(testCase1);
            //var testCase2 = ReadDataStack("TestCase2.txt");
            //ComputeScc(testCase2);
            //var testCase3 = ReadDataStack("TestCase3.txt");
            //DfsStack(testCase3);
            //var testCase4 = ReadDataStack("TestCase4.txt");
            //DfsStack(testCase4);
            //var testCase5 = ReadDataStack("TestCase5.txt");
            //DfsStack(testCase5);
            //var testCase6 = ReadDataStack("TestCase6.txt");
            //DfsLoop(testCase6);
            //var t = ReadDataStack("SCC.txt");
            //DfsLoop(t);
        }

        private static List<String> ComputeScc(List<Node> graph)
        {
            var results = new List<String>();

            //Compute DFS on reverse graph.
            var grev = graph.Select(n => new Node { V = n.E, E = n.V }).ToList();
            DfsLoop(graph);
            return results;
        }

        private static void DfsLoop(List<Node> graph)
        {
            var n = graph.Count;
            var index = n - 1;

            for (var j = 0; j < n; j++)
            {
                if (!graph[index].Visited)
                {
                    Console.WriteLine("Checking index {0} with value {1}", index, graph[index].V);
                    DfsRecursive(graph, index);
                }
                index--;
            }
        }

        private static void DfsStack(List<Node> graph, int nodeIndex)
        {
            var s = new Stack<int>();
            s.Push(graph[nodeIndex].V);
            
            var f = 0;
            while (s.Count > 0)
            {
                var v = s.Pop();
                var vertex = graph.FirstOrDefault(x => x.V == v);
                if (vertex != null)
                {
                    if (!vertex.Visited)
                    {
                        Console.WriteLine("Processing V = {0}", v);
                        //Mark all V's as visited
                        var nodes = graph.Where(x => x.V == vertex.V);
                        //Console.WriteLine("Traversing node {0}", vertex.V);
                        foreach (var node in nodes)
                        {
                            node.Visited = true;
                            s.Push(node.E);
                        }
                    }
                    else
                    {
                        //Node is finished
                        Console.WriteLine("{0} is done", v);
                    }
                }
                else
                {
                    //Node is finished
                    Console.WriteLine("{0} is done", v);
                }
                f++;
            }
            Console.WriteLine("Finishing Time = {0}", f);
        }

        private static int t = 0;
        private static void DfsRecursive(List<Node> graph, int nodeIndex)
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
                        //Console.WriteLine("Traversing node {0}", edge);
                        DfsRecursive(graph, eindex.Index);
                    }
                }
            }
            t++;
            Console.WriteLine("Finishing Time of v {1}= {0}", t, graph[nodeIndex].V);
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

        private static List<Node> ReadDataStack(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            return txtData.Select(s => s.Split(' ')).Select(x => new Node {V = Convert.ToInt32(x[0]), E = Convert.ToInt32(x[1]), Visited = false}).ToList();
        }
    }
}
