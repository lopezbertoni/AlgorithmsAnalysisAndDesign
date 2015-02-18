using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 23, 31, 1, 21, 36, 72 };
            Console.WriteLine(String.Join(", ", input));
            var partitionTest = Partition(input, 0, input.Length);
            Console.WriteLine(String.Join(", ", input));
        }

        private static void QuickSort(int[] arr, int left, int right)
        {
            if (arr.Length < 2)
            {
                return;
            }

            //Choose Pivot 

            //Partition SubRoutine
            //Results in pivot in righteous position

            //Recursively Sort 1st part and 2nd part

        }

        private static int Partition(int[] arr, int left, int right)
        {
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
            }
            Swap(arr, left, i-1);
            return i;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            //Swap
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
