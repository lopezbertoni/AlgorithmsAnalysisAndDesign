using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion1
{
    class Program
    {
        static void Main(string[] args)
        {
            var testCase1 = new[] {1, 3, 5, 2, 4, 6};

            MergeSort(testCase1, 0, 5);

            foreach (var x in testCase1)
            {
                Console.WriteLine(x.ToString());
            }
        }

        private static void MergeSort(int[] a, int i, int j)
        {
            var mid = 0;

            if (j > i)
            {
                mid = (i + j)/2;
                MergeSort(a, i, mid);
                MergeSort(a, mid + 1, j);
                Merge(a);
                //MergeCountInversion(a, i, j, mid);
            }
        }

        private static void Merge(int[] a)
        {
            var n = a.Length;
            var output = new int[n];
            var left = 0;
            var right = a.Length/2;
            var mid = a.Length/2;

            for (var k = 0; k < n; k++)
            {
                if (a[left] < a[right] && left < mid)
                {
                    output[k] = a[left];
                    left++;
                }
                else if (right < n)
                {
                    output[k] = a[right];
                    right++;
                }
            }

            for (var x = 0; x < n; x++)
            {
                a[x] = output[x];
            }
        }

        private static Results MergeCountInversion(int[] a, int i, int j, int mid)
        {
            var inversions = 0;
            var results = new Results {SortedList = new List<int>()};

            for (var k = 0; k < j; k++)
            {
                if (a[i] < a[j])
                {
                    results.SortedList.Add(a[i]);
                    i++;
                }
                else
                {
                    results.SortedList.Add(a[j]);
                    j++;
                }
            }

            return results;
        }

        private class Results
        {
            public List<int> SortedList { get; set; }
            public int InversionCount { get; set; }
        }
    }
}
