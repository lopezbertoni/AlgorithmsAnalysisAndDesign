using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion1
{
    class Program
    {
        static void Main()
        {
            var testCase1 = new[] {1, 3, 5, 2, 4, 6};
            MergeSortCount(testCase1, 0, 5);

            foreach (var x in testCase1)
            {
                Console.WriteLine(x.ToString());
            }

            var testCase2 = new[] {1, 6, 3, 2, 4, 5};
            MergeSortCount(testCase2, 0, testCase2.Length - 1);
        }

        private static void MergeSortCount(int[] a, int i, int j)
        {
            var mid = 0;
            if (j > i)
            {
                mid = (i + j)/2;
                MergeSortCount(a, i, mid);
                MergeSortCount(a, mid + 1, j);
                MergeCount(a);
            }
        }

        private static void MergeCount(int[] a)
        {
            var n = a.Length;
            var output = new int[n];
            var left = 0;
            var right = a.Length/2;
            var mid = a.Length/2;
            var count = 0;
           
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

                    //Count inversions
                    count += mid - left;
                    if (count > 0)
                    {
                        Console.WriteLine("Current inversions are equal to {0}", count);
                    }
                }
            }

            for (var x = 0; x < n; x++)
            {
                a[x] = output[x];
            }
        }
    }
}
