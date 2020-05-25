﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace BIT265_MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbersList = new List<int> { 82, 5, 173, 726, 55, 22, 91, 435, 57, 92, 6744, 734, 6928, 48, 2, 3910, 61, };
            Console.WriteLine("Original list of numbers");
            MergeSortAlgorithm ms = new MergeSortAlgorithm();
            ms.Print(numbersList);
            Console.WriteLine();
            Console.WriteLine("Now we'll sort it with MergeSort");
            List<int> newNumbersList = ms.MergeSort(numbersList);
            ms.Print(newNumbersList);
        }   

        class MergeSortAlgorithm 
        {
            public List<int> MergeSort(List<int> unsorted)
            {
                if (unsorted.Count <= 1)
                    return unsorted;

                List<int> left = new List<int>();

                List<int> right = new List<int>();

                int middle = unsorted.Count / 2;
                for (int i = 0; i < middle; i++)  //Dividing the unsorted list
                {
                    left.Add(unsorted[i]);
                }
                for (int i = middle; i < unsorted.Count; i++)
                {
                    right.Add(unsorted[i]);
                }

                left = MergeSort(left);
                right = MergeSort(right);
                return Merge(left, right);
            }

            public List<int> Merge(List<int> left, List<int> right)
            {
                List<int> result = new List<int>();

                while (left.Count > 0 || right.Count > 0)
                {
                    if (left.Count > 0 && right.Count > 0)
                    {
                        if (left.First() <= right.First())  //Comparing First two elements to see which is smaller
                        {
                            result.Add(left.First());
                            left.Remove(left.First());      //Rest of the list minus the first element
                        }
                        else
                        {
                            result.Add(right.First());
                            right.Remove(right.First());
                        }
                    }
                    else if (left.Count > 0)
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else if (right.Count > 0)
                    {
                        result.Add(right.First());

                        right.Remove(right.First());
                    }
                }
                return result;
            }

            public void Print(List<int> list)
            {
                foreach (int i in list)
                {
                    Console.Write(i + " ");
                }
            }
        }
    }
}
