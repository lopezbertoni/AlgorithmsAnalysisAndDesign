using System;
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
            var d = ReadData("dijkstraData.txt");
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
            }

            return inputData;
        }
    }
}
