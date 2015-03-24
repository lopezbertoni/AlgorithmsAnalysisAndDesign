using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion5
{
    class Program
    {
        private static readonly HashSet<int> Visited = new HashSet<int>();
        private static readonly Dictionary<int, int> Distances = new Dictionary<int, int>(); 
        private static readonly Stack<int> VisitOrder = new Stack<int>(); 

        static void Main(string[] args)
        {
            //var d = ReadData("dijkstraData.txt");

            var testCase1 = ReadData("TestCase1.txt");
            GetDijkstraMinPath(testCase1);
        }

        private static void GetDijkstraMinPath(Dictionary<int, Tuple<int, int>[]> graph)
        {
            //Initialize variables. 
            var v = Distances.First().Key; //Get initial node
            Distances[v] = 0; //Set initial distance to zero, all other are int.MaxValue
            VisitOrder.Push(v);
            
            //Start doing work
            while (VisitOrder.Count > 0)
            {
                //Get node
                var vertex = VisitOrder.Pop();
                Visited.Add(vertex);
                var currentNodeEdges = graph[vertex];
                var currentDistance = Distances[vertex];

                var edgeDistances = new SortedList<int, int>();

                //Loop through node edges
                foreach (var currentNodeEdge in currentNodeEdges)
                {
                    //Check if node has been visited
                    if (!Visited.Contains(currentNodeEdge.Item1))
                    {
                        //Node has not been visited. 
                        //Update distance if less than previously computed distance
                        var newDistance = currentDistance + currentNodeEdge.Item2;
                        if (newDistance < Distances[currentNodeEdge.Item1])
                        {
                            Distances[currentNodeEdge.Item1] = newDistance;
                        }
                        if (!edgeDistances.ContainsKey(newDistance))
                        {
                            edgeDistances.Add(newDistance, currentNodeEdge.Item1);
                        }
                    }
                }
                if (edgeDistances.Any())
                {
                    VisitOrder.Push(edgeDistances.First().Value);
                }
            }
        }

        private static Dictionary<int, Tuple<int,int>[]> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new Dictionary<int, Tuple<int, int>[]>();

            foreach (var s in txtData)
            {
                var x = s.Split(new[] {' ','\t',','}, StringSplitOptions.RemoveEmptyEntries);
                var n = Convert.ToInt32(x[0]);
                var t = new List<Tuple<int, int>>();
                for (var i = 1; i < x.Length; i+=2)
                {
                    t.Add(new Tuple<int, int>(Convert.ToInt32(x[i]), Convert.ToInt32(x[i+1])));
                }
                inputData.Add(n, t.ToArray());
                Distances.Add(n, int.MaxValue);
            }

            return inputData;
        }
    }
}
