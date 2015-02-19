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
    }
}
