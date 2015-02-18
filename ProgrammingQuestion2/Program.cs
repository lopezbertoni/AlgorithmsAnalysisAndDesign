using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion2
{
    class Program
    {
        public static int Count = 0;

        static void Main(string[] args)
        {
            //int[] input = { 23, 31, 1, 21, 36, 72 };
            //Console.WriteLine(String.Join(", ", input));
            //QuickSort(input, 0, input.Length, Pivots.First);
            //Console.WriteLine(String.Join(", ", input));
            //Console.WriteLine("There are {0} comparisons", Count);

            //First element as pivot
            //Test Case 1
            CalculateComparisons("10.txt", Pivots.First);

            //Test Case 2
            CalculateComparisons("100.txt", Pivots.First);

            //Test Case 3
            CalculateComparisons("1000.txt", Pivots.First);

            //Last element as pivot
            //Test Case 1
            CalculateComparisons("10.txt", Pivots.Last);

            //Test Case 2
            CalculateComparisons("100.txt", Pivots.Last);

            //Test Case 3
            CalculateComparisons("1000.txt", Pivots.Last);

            //Median element as pivot
            //Test Case 1
            //CalculateComparisons("10.txt", Pivots.Median);

            //Test Case 2
            //CalculateComparisons("100.txt", Pivots.Median);

            //Test Case 3
            //CalculateComparisons("1000.txt", Pivots.Median);
            
        }

        private static void CalculateComparisons(string filename, Pivots p)
        {
            Count = 0;
            var txtData = File.ReadLines(filename).ToArray();
            var intData = new int[txtData.Length];
            var intList = txtData.Select(i => Convert.ToInt32(i)).ToList();
            intData = intList.ToArray();

            //Console.WriteLine(String.Join(", ", intData));
            QuickSort(intData, 0, intData.Length, p);
            //Console.WriteLine(String.Join(", ", intData));
            Console.WriteLine("There are {0} comparisons", Count);
        }

        private static void QuickSort(int[] arr, int left, int right, Pivots pivot)
        {
            if (arr.Length < 2)
            {
                return;
            }

            //Choose Pivot 
            //Partition SubRoutine
            //Results in pivot in righteous position
            var index = Partition(arr, left, right, pivot);
            //Recursively Sort 1st part and 2nd part
            if (left < index - 1)
            {
                QuickSort(arr, left, index - 1, pivot);
            }
            if (index < right)
            {
                QuickSort(arr, index, right, pivot);
            }
        }

        private static int Partition(int[] arr, int left, int right, Pivots pivot)
        {
            if (pivot == Pivots.Last)
            {
                //Swap element
                Swap(arr, left, right-1);
            }
            //if (pivot == Pivots.Median)
            //{
            //    var size = right - left;
            //    var mid = (right - left) / 2;
            //    if (size > 1)
            //    {
            //        var candidates = new Dictionary<int, int>
            //        {
            //            {left, arr[left]},
            //            {mid, arr[mid]},
            //            {size, arr[size]}
            //        };

            //        var ind = candidates.OrderBy(x => x.Value).Select(x => x.Key).Skip(1).First();
            //        Swap(arr, left, ind);
            //    }
            //    else
            //    {
            //        Swap(arr, left, right);
            //    }
            //}
            var p = arr[left]; 

            var i = left + 1;
            var j = left + 1;

            while (j < right)
            {
                if (arr[j] < p)
                {
                    //Swap
                    Swap(arr, i, j);
                    i++;
                }
                j++;
                Count++;
            }
            //Console.WriteLine(String.Join(", ", arr));
            Swap(arr, left, i-1);
            //Console.WriteLine("Pivot = {0}", p);
            //Console.WriteLine(String.Join(", ", arr));
            return i;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            //Swap
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private enum Pivots
        {
            First,
            Median,
            Last
        }
    }
}
