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
            var testCase1 = ReadData("TestCase1.txt");
            var vertices = testCase1.Count;
            var rnd = PickRandomIndex(vertices);
            Console.WriteLine(rnd);

            var edges = testCase1.Values.ElementAt(2); //Number of edges
            var edgeCount = edges.Count;
            Console.WriteLine(edgeCount);

            var randomEdgeIndex = PickRandomIndex(edgeCount);

            var edge = edges.ElementAt(2);

            if (edgeCount > 1)
            {
                MergeNodes(2, 3, testCase1);
            }
            PrintGraph(testCase1);
        }

        private static void MergeNodes(int vindex, int eindex, Dictionary<int, List<int>> input)
        {
            var vValue = input.ElementAt(vindex).Key;
            var eValue = input.ElementAt(eindex).Key;
            var newNode = Convert.ToInt32(String.Format("{0}{1}", vValue, eValue));

            Console.WriteLine("Grabbing vertex at index {0} with value {1}. Edge at index {2} and value {3}.", vindex, vValue, eindex, eValue);

            var vList = input.Values.ElementAt(vindex);
            var eList = input.Values.ElementAt(eindex);

            Console.WriteLine("Vertex index {0} contains edges {1}.", vindex, String.Join(", ", vList));
            Console.WriteLine("Vertex index {0} contains edges {1}.", eindex, String.Join(", ", eList));
            
            //New node with edges
            var newlist = vList.Where(i => i != vValue && i != eValue).ToList();
            newlist.AddRange(eList.Where(i => i != vValue && i != eValue));

            var distinctVertices = newlist.Distinct().ToList();
            foreach (var vertex in distinctVertices)
            {
                //Get all values
                var edgesInVertex = input.First(x => x.Key == vertex).Value;
                for (var i = 0; i < edgesInVertex.Count; i++)
                {
                    if (edgesInVertex[i] == vValue || edgesInVertex[i] == eValue)
                    {
                        SwapValue(input, vertex, i, newNode);
                    }
                }
            }
            //Update Vertices/Edges individually. 
            //foreach (var x in newlist)
            //{
            //    var edgeList = input.Where(y => y.Key == x).Select(y => y.Value).First();
            //    var k = 0;
            //    foreach (var e in edgeList)
            //    {
            //        if (edgeList[k] == vValue || edgeList[k] == eValue)
            //        {
            //            edgeList[k] = newNode;
            //        }
            //        k++;
            //    }
            //}

            //Delete previous nodes
            input.Remove(vValue);
            input.Remove(eValue);
            input.Add(newNode, newlist);

            Console.WriteLine("New node {0} contains {1}",newNode, String.Join(", ", newlist));
        }

        private static void SwapValue(Dictionary<int, List<int>> input, int key, int valIndex, int newValue)
        {
            var list = new List<int>();
            list = input.First(x => x.Key == key).Value;
            list[valIndex] = newValue;
            input[key] = list;
        }

        private static int PickRandomIndex(int number)
        {
            var rnd = new Random();
            return rnd.Next(0, number);
        }

        private static Dictionary<int, List<int>> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new Dictionary<int, List<int>>();

            foreach (var s in txtData)
            {
                var x = s.Split('\t');
                var v = 0;
                var e = new List<int>();
                for (var i = 0; i < x.Length; i++)
                {
                    if (i == 0)
                    {
                        v = Convert.ToInt32(x[i]);
                    }
                    else
                    {
                        e.Add(Convert.ToInt32(x[i]));
                    }
                }
                inputData.Add(v, e);
            }
            return inputData;
        }

        private static void PrintGraph(Dictionary<int, List<int>> input)
        {
            foreach (var x in input)
            {
                Console.WriteLine("{0} {1}", x.Key, String.Join(", ", x.Value));
            }
        }
    }
}
