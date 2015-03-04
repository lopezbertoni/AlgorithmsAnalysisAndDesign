using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion3
{
    class Program
    {
        static void Main(string[] args)
        {
            //var kargerValues = ReadData("kargerMinCut.txt");
            //PrintGraph(testCase1);

            var minCuts = new List<int>();
            //Do this several times
            for (var i = 0; i < 200; i++)
            {
                var kargerValues = ReadData("kargerMinCut.txt");
                minCuts.Add(CountMinimumCuts(kargerValues));
            }
            Console.WriteLine("{0}", String.Join(", ", minCuts));
            Console.WriteLine("The number of minimum cuts is: {0}", minCuts.Min());
        }

        private static int CountMinimumCuts(Dictionary<string, List<string>> input)
        {
            //Get initial vertices count
            var vCount = input.Count;

            //Do this until we are left with 2 vertices
            while (vCount > 2)
            {
                //Get random vertex, gets random value between 0 and vCount
                var randomVertexIndex = PickRandomIndex(vCount);
                //Print the edges for that vertex
                var edges = input.Values.ElementAt(randomVertexIndex);
                //Console.WriteLine("Random Vertex Index at: {0} with value {1}, and edges {2}", randomVertexIndex, input.Keys.ElementAt(randomVertexIndex), String.Join(", ", edges));
                //Pick a random edge
                var randomEdgeIndex = PickRandomIndex(edges.Count);
                //Console.WriteLine("Random edge picked from: {0} at index {1} with value {2}", String.Join(", ", edges), randomEdgeIndex, input.Values.ElementAt(randomVertexIndex)[randomEdgeIndex]);

                //If we have more than 1 edge, we need to merge them
                if (edges.Count > 1)
                {
                    MergeNodes(randomVertexIndex, randomEdgeIndex, input);
                }
                //PrintGraph(input);
                vCount = input.Count;
            }
            //Console.WriteLine("The number of minimum cuts is: {0}", input.Values.First().Count);
            return input.Values.First().Count;
        }

        private static void MergeNodes(int vindex, int eindex, Dictionary<string, List<string>> input)
        {
            var vValue = input.ElementAt(vindex).Key;
            var eValue = input.ElementAt(vindex).Value[eindex];
            var newNode = String.Format("{0}{1}", vValue.PadLeft(3, '0'), eValue.PadLeft(3, '0'));

            //Console.WriteLine("Grabbing vertex at index {0} with value {1}. Edge at index {2} with value {3}.", vindex, vValue, eindex, eValue);

            var vList = input.Values.ElementAt(vindex);
            //var eList = input.Values.ElementAt(eindex);
            var eList = input.Where(x => x.Key == eValue).Select(x => x.Value).First();

            //Console.WriteLine("Vertex index {0} contains edges {1}.", vindex, String.Join(", ", vList));
            //Console.WriteLine("Vertex index {0} contains edges {1}.", eindex, String.Join(", ", eList));
            
            //New node with edges
            var newlist = vList.Where(i => i != vValue && i != eValue).ToList();
            newlist.AddRange(eList.Where(i => i != vValue && i != eValue));

            var distinctVertices = newlist.Distinct().ToList();
            foreach (var vertex in distinctVertices)
            {
                //Get all values
                var edgesInVertex = input.FirstOrDefault(x => x.Key == vertex).Value;
                for (var i = 0; i < edgesInVertex.Count; i++)
                {
                    if (edgesInVertex[i] == vValue || edgesInVertex[i] == eValue)
                    {
                        SwapValue(input, vertex, i, newNode);
                    }
                }
            }

            //Delete previous nodes
            input.Remove(vValue);
            input.Remove(eValue);
            input.Add(newNode, newlist);

            //Console.WriteLine("New node {0} contains {1}", newNode, String.Join(", ", newlist));
        }

        private static void SwapValue(Dictionary<string, List<string>> input, string key, int valIndex, string newValue)
        {
            var list = input.First(x => x.Key == key).Value;
            list[valIndex] = newValue;
            input[key] = list;
        }

        private static int PickRandomIndex(int number)
        {
            var rnd = new Random();
            return rnd.Next(0, number);
        }

        private static Dictionary<string, List<string>> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new Dictionary<string, List<string>>();

            foreach (var s in txtData)
            {
                var x = s.Split('\t');
                var v = String.Empty;
                var e = new List<string>();
                for (var i = 0; i < x.Length; i++)
                {
                    if (i == 0)
                    {
                        v = x[i];
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(x[i]))
                        {
                            e.Add(x[i]);
                        } 
                    }
                }
                inputData.Add(v, e);
            }
            return inputData;
        }

        private static void PrintGraph(Dictionary<string, List<string>> input)
        {
            foreach (var x in input)
            {
                if (String.IsNullOrEmpty(x.Key))
                {
                    Console.WriteLine("Null Value");
                }
                else
                {
                    if (x.Value.Contains(String.Empty))
                    {
                        Console.WriteLine("Null Value");
                    }
                    Console.WriteLine("{0} {1}", x.Key, String.Join(", ", x.Value));
                }
            }
        }
    }
}
