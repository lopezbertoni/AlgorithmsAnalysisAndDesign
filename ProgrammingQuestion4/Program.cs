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
            var test = new Stack<Int32>();
            var t1 = ReadData("TestCase1.txt");
            var t2 = ReadData("TestCase2.txt");
            var t = ReadData("SCC.txt"); 
        }

        private static List<Node> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new List<Node>();

            var v = 0;
            var i = 0;
            
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
                    inputData.Add(new Node { V = currentNode, Edges = e, Visited = false });
                }
                v = currentNode;
            }
            return inputData;
        }

    }
}
