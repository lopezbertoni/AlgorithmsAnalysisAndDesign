using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var q1TestCase1 = ReadDataQ1("Q1TestCase1.txt");
            //TwoSum(q1TestCase1);
            //var q1TestCase2 = ReadDataQ1("Q1TestCase2.txt");
            //TwoSum(q1TestCase2);
            //var q1TestCase3 = ReadDataQ1("Q1TestCase3.txt");
            //TwoSum(q1TestCase3);
            //var q1TestCase4 = ReadDataQ1("Q1TestCase4.txt");
            //TwoSum(q1TestCase4);
            //var data = ReadDataQ1("Question1.txt");
            //TwoSum(data);

            //MedianMaintenance("Q2TestCase1.txt");
            //MedianMaintenance("Q2TestCase2.txt");
            //MedianMaintenance("Q2TestCase3.txt");
            //MedianMaintenance("Q2TestCase4.txt");
            //MedianMaintenance("Q2TestCase5.txt");
            //MedianMaintenance("Q2TestCase6.txt");
            //MedianMaintenance("Q2TestCase7.txt");
            //MedianMaintenance("Q2TestCase8.txt");
            //MedianMaintenance("Q2TestCase9.txt");
            MedianMaintenance("Question2.txt");

            //var numbers = File.ReadAllLines("Q2TestCase9.txt");
            //CalculateMedians(numbers);

            //var numbers = File.ReadAllLines("Question2.txt");
            //CalculateMedians(numbers);
        }

        private static void MedianMaintenance(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData32 = new List<Int32>();
            var medians32 = new List<Int32>();
            var sums32 = new List<Int32>();
            var inputData64 = new List<Int64>();
            var medians64 = new List<Int64>();
            var sums64 = new List<Int64>();
            var sum = 0;
            var sum64 = 0.0;
            var i = 0;
            foreach (var s in txtData)
            {
                //Add to sorted list
                var intToAdd = Convert.ToInt32(s);

                inputData32.Add(intToAdd);
                inputData64.Add(Convert.ToInt64(s));

                //Compute sum
                var count = inputData32.Count;
                inputData32.Sort();
                inputData64.Sort();
                var index = 0;

                if (count%2 == 0)
                {
                    //Even number of elements
                    index = count/2 - 1;
                }
                else
                {
                    //Number is odd
                    index = ((count + 1)/2) - 1;
                }
                var val32 = Convert.ToInt32(inputData32[index]);
                var val64 = Convert.ToInt64(inputData64[index]);
                if (i > 3810)
                {
                    var t = sum;
                    var t1 = sum + val32;
                }
                medians32.Add(val32);
                medians64.Add(val64);
                //Debug.WriteLine("Median is {0}", val);
                sum += val32;
                sums32.Add(Convert.ToInt32(sum));
                sum64 += val64;
                sums64.Add(Convert.ToInt64(sum64));
                i++;
            }
            Console.WriteLine("Median Maintenance result is {0}", (sum).ToString("N"));
            Console.WriteLine("Median Maintenance result is {0}", (medians32.Sum()).ToString("N"));

            Console.WriteLine("Median Maintenance result is {0} - Int64", (sum64).ToString("N"));
            Console.WriteLine("Median Maintenance result is {0} - Int64", (medians64.Sum()).ToString("N"));
            //PrintToFile(String.Format("{0}_medians.txt", filename), medians.Select(x => x.ToString()).ToArray());
            //PrintToFile(String.Format("{0}_sums.txt", filename), sums.Select(x => x.ToString()).ToArray());
        }

        private static void PrintToFile(string filename, string[] data)
        {
            File.WriteAllLines(filename, data);
        }

        private static void TwoSum(Hashtable input)
        {
            const int minRange = -10000;
            const int maxRange = 10000;

            var results = new Hashtable();
            var count = 0;

            for (var t = minRange; t <= maxRange; t++)
            {
                foreach (var xvalues in input.Keys)
                {
                    var x = Convert.ToInt64(xvalues);
                    var y = t - x;

                    if (input.ContainsKey(y) && x != y && !results.ContainsKey(t))
                    {
                        //Console.WriteLine("value of x = {0}, y = {1}, t = {2}, {0} + {1} = {2}", xvalues , y, t);
                        count++;
                        results.Add(t, t);
                    }
                }
            }
            Console.WriteLine("TwoSum result is {0}", count);
        }

        private static Hashtable ReadDataQ1(string filename)
        {
            var txtData = File.ReadLines(filename).ToArray();
            var inputData = new Hashtable();

            foreach (var s in txtData)
            {
                var intToAdd = Convert.ToInt64(s);
                if (!inputData.ContainsKey(intToAdd))
                {
                    inputData.Add(intToAdd, intToAdd);
                }
            }
            return inputData;
        }
    }
}
