﻿using System;
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
            PrintGraph(testCase1);

            CountMinimumCuts(testCase1);
            //var vertices = testCase1.Count;
            //var randomVertexIndex = PickRandomIndex(vertices);
            //Console.WriteLine("Random Index at: {0}", randomVertexIndex);

            //var edges = testCase1.Values.ElementAt(randomVertexIndex); //Number of edges
            //var edgeCount = edges.Count;
            //Console.WriteLine(edgeCount);

            //var randomEdgeIndex = PickRandomIndex(edgeCount);

            //var edge = edges.ElementAt(randomEdgeIndex);

            //if (edgeCount > 1)
            //{
            //    MergeNodes(randomVertexIndex, randomEdgeIndex, testCase1);
            //}
            //PrintGraph(testCase1);
        }

        private static void CountMinimumCuts(Dictionary<int, List<int>> input)
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
                Console.WriteLine("Random Vertex Index at: {0} with value {1}, and edges {2}", randomVertexIndex, input.Keys.ElementAt(randomVertexIndex), String.Join(", ", edges));
                //Pick a random edge
                var randomEdgeIndex = PickRandomIndex(edges.Count);
                Console.WriteLine("Random edge picked from: {0} at index {1} with value {2}", String.Join(", ", edges), randomEdgeIndex, input.Values.ElementAt(randomVertexIndex)[randomEdgeIndex]);

                //If we have more than 1 edge, we need to merge them
                if (edges.Count > 1)
                {
                    MergeNodes(randomVertexIndex, randomEdgeIndex, input);
                }
                PrintGraph(input);
                vCount = input.Count;
            }
        }

        private static void MergeNodes(int vindex, int eindex, Dictionary<int, List<int>> input)
        {
            var vValue = input.ElementAt(vindex).Key;
            var eValue = input.ElementAt(vindex).Value[eindex];
            //eindex = input.Where(x => x.Key == eValue).Select(x => x.Key).First();
            var newNode = Convert.ToInt32(String.Format("{0}{1}", vValue, eValue));

            Console.WriteLine("Grabbing vertex at index {0} with value {1}. Edge at index {2} with value {3}.", vindex, vValue, eindex, eValue);

            var vList = input.Values.ElementAt(vindex);
            //var eList = input.Values.ElementAt(eindex);
            var eList = input.Where(x => x.Key == eValue).Select(x => x.Value).First();

            Console.WriteLine("Vertex index {0} contains edges {1}.", vindex, String.Join(", ", vList));
            Console.WriteLine("Vertex index {0} contains edges {1}.", eindex, String.Join(", ", eList));
            
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

            Console.WriteLine("New node {0} contains {1}", newNode, String.Join(", ", newlist));
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
