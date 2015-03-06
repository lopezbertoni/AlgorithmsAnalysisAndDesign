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
            var t2 = ReadData("TestCase1.txt");
            var t = ReadData("SCC.txt"); 
        }

        private static List<Node> ReadData(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new List<Node>();

            var v = 0;
            var e = new List<int>();
            foreach (var s in txtData)
            {
                var x = s.Split(' ');
                
                if (Convert.ToInt32(x[0]) != v)
                {
                    for (var i = 0; i < x.Length; i++)
                    {
                        if (i == 0)
                        {
                            v = Convert.ToInt32(x[i]);
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(x[i]))
                            {
                                e.Add(Convert.ToInt32(x[i]));
                            }
                        }
                    }
                }
                inputData.Add(new Node {V = v, Edges = e, Visited = false});
            }
            return inputData;
        }

    }
}
