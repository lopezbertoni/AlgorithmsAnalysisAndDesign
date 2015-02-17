using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingQuestion1
{
    class Program
    {
        static void Main()
        {
            //PseudoCode taken from http://stackoverflow.com/questions/337664/counting-inversions-in-an-array
            long numberInversions = 0;

            var testCase1 = new[] {1, 3, 5, 2, 4, 6};
            numberInversions = InvCount(testCase1);
            Console.WriteLine("There are {0} inversions.", numberInversions);


            var testCase2 = new[] {1, 5, 3, 2, 4};
            numberInversions = InvCount(testCase2);
            Console.WriteLine("There are {0} inversions.", numberInversions);


            var testCase3 = new[] { 1, 6, 3, 2, 4, 5 };
            numberInversions = InvCount(testCase3);
            Console.WriteLine("There are {0} inversions.", numberInversions);


            var testCase4 = new[] {9, 12, 3, 1, 6, 8, 2, 5, 14, 13, 11, 7, 10, 4, 0};
            numberInversions = InvCount(testCase4);
            Console.WriteLine("There are {0} inversions.", numberInversions);


            var testCase5 = new[] { 37, 7, 2, 14, 35, 47, 10, 24, 44, 17, 34, 11, 16, 48, 1, 39, 6, 33, 43, 26, 40, 4, 28, 5, 38, 41, 42, 12, 13, 21, 29, 18, 3, 19, 0, 32, 46, 27, 31, 25, 15, 36, 20, 8, 9, 49, 22, 23, 30, 45 };
            numberInversions = InvCount(testCase5);
            Console.WriteLine("There are {0} inversions.", numberInversions);

            var testCase6 = new[] { 4, 80, 70, 23, 9, 60, 68, 27, 66, 78, 12, 40, 52, 53, 44, 8, 49, 28, 18, 46, 21, 39, 51, 7, 87, 99, 69, 62, 84, 6, 79, 67, 14, 98, 83, 0, 96, 5, 82, 10, 26, 48, 3, 2, 15, 92, 11, 55, 63, 97, 43, 45, 81, 42, 95, 20, 25, 74, 24, 72, 91, 35, 86, 19, 75, 58, 71, 47, 76, 59, 64, 93, 17, 50, 56, 94, 90, 89, 32, 37, 34, 65, 1, 73, 41, 36, 57, 77, 30, 22, 13, 29, 38, 16, 88, 61, 31, 85, 33, 54 };
            numberInversions = InvCount(testCase6);
            Console.WriteLine("There are {0} inversions.", numberInversions);

            var txtData = File.ReadLines("IntegerArray.txt").ToArray();
            var intData = new int[txtData.Length];
            var intList = txtData.Select(i => Convert.ToInt32(i)).ToList();

            intData = intList.ToArray();

            //Array.Copy(txtData, intData, txtData.Length);
            numberInversions = InvCount(intData);
            Console.WriteLine("There are {0} inversions.", numberInversions);
        }

        private static long InvCount(int[] arr)
        {
            //Base case
            if (arr.Length < 2)
            {
                return 0;
            }

            //Split Routine
            var m = arr.Length/2;
            var left = new int[m];
            var right = new int[arr.Length - m];

            Array.Copy(arr, left, m);
            Array.Copy(arr, m, right, 0, right.Length);
            //Recursion
            return InvCount(left) + InvCount(right) + MergeCount(arr, left, right);
        }

        private static long MergeCount(int[] arr, int[] left, int[] right)
        {
            var i = 0;
            var j = 0;
            var count = 0;
            while (i < left.Length || j < right.Length)
            {
                if (i == left.Length)
                {
                    arr[i + j] = right[j];
                    j++;
                }
                else if (j == right.Length)
                {
                    arr[i + j] = left[i];
                    i++;
                }
                else if (left[i] <= right [j])
                {
                    arr[i + j] = left[i];
                    i++;
                }
                else
                {
                    arr[i + j] = right[j];
                    count += left.Length - i;
                    j++;
                }
            }
            return count;
        }

        private static int[] MergeSort(int[] array)
        {
            // If list size is 0 (empty) or 1, consider it sorted and return it
            // (using less than or equal prevents infinite recursion for a zero length array).
            if (array.Length <= 1)
            {
                return array;
            }

            // Else list size is > 1, so split the list into two sublists.
            var middleIndex = (array.Length) / 2;
            var left = new int[middleIndex];
            var right = new int[array.Length - middleIndex];

            Array.Copy(array, left, middleIndex);
            Array.Copy(array, middleIndex, right, 0, right.Length);

            // Recursively call MergeSort() to further split each sublist
            // until sublist size is 1.
            left = MergeSort(left);
            right = MergeSort(right);

            // Merge the sublists returned from prior calls to MergeSort()
            // and return the resulting merged sublist.
            return Merge(left, right);
        }

        private static int[] Merge(int[] left, int[] right)
        {
            // Convert the input arrays to lists, which gives more flexibility 
            // and the option to resize the arrays dynamically.
            var leftList = left.OfType<int>().ToList();
            var rightList = right.OfType<int>().ToList();
            var resultList = new List<int>();

            // While the sublist are not empty merge them repeatedly to produce new sublists 
            // until there is only 1 sublist remaining. This will be the sorted list.
            while (leftList.Count > 0 || rightList.Count > 0)
            {
                if (leftList.Count > 0 && rightList.Count > 0)
                {
                    // Compare the 2 lists, append the smaller element to the result list
                    // and remove it from the original list.
                    if (leftList[0] <= rightList[0])
                    {
                        resultList.Add(leftList[0]);
                        leftList.RemoveAt(0);
                    }
                    else
                    {
                        resultList.Add(rightList[0]);
                        rightList.RemoveAt(0);
                    }
                }
                else if (leftList.Count > 0)
                {
                    resultList.Add(leftList[0]);
                    leftList.RemoveAt(0);
                }
                else if (rightList.Count > 0)
                {
                    resultList.Add(rightList[0]);
                    rightList.RemoveAt(0);
                }
            }

            // Convert the resulting list back to a static array
            var result = resultList.ToArray();
            return result;
        }
    }
}
