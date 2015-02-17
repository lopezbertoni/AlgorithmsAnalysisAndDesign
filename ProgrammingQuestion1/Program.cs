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
            testCase1 = MergeSort(testCase1);

            foreach (var x in testCase1)
            {
                Console.WriteLine(x.ToString());
            }

            var testCase2 = new[] {1, 5, 3, 2, 4};
            testCase2 = MergeSort(testCase2);

            foreach (var x in testCase2)
            {
                Console.WriteLine(x.ToString());
            }

            var testCase3 = new[] { 1, 6, 3, 2, 4, 5 };
            testCase3 = MergeSort(testCase3);

            foreach (var x in testCase3)
            {
                Console.WriteLine(x.ToString());
            }

            var testCase4 = new[] {9, 12, 3, 1, 6, 8, 2, 5, 14, 13, 11, 7, 10, 4, 0};
            testCase4 = MergeSort(testCase4);

            foreach (var x in testCase4)
            {
                Console.WriteLine(x.ToString());
            }
        }

        public static int[] MergeSort(int[] array)
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
            return MergeCount(left, right);
        }

        public static int[] Merge(int[] left, int[] right)
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

        public static int[] MergeCount(int[] left, int[] right)
        {
            // Convert the input arrays to lists, which gives more flexibility 
            // and the option to resize the arrays dynamically.
            var leftList = left.OfType<int>().ToList();
            var rightList = right.OfType<int>().ToList();
            var resultList = new List<int>();
            var count = 0;
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
                        count += leftList.Count;
                        Console.WriteLine("There are {0} inversions", count);
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
