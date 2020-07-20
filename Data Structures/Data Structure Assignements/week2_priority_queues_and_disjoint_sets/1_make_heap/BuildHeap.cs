using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBrackets
{
	public delegate void InputsForProblem();
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForMaxHeap;
            input.Invoke(); 
        }
		 private static void InputsForMaxHeap()
        {
            var noOfElements = Convert.ToInt32(Console.ReadLine());
            var inputArray = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
            ConstructHeapFromArray(inputArray, noOfElements);
        }
		
		private static void ConstructHeapFromArray(long[] array, int size)
        {
            List<Tuple<int, int>> swappedIndexesTupple = new List<Tuple<int, int>>();
            for (int index = size/2 - 1; index >= 0; index--)
            {
                ShiftDown(array, index, size, swappedIndexesTupple);
            }
            if(swappedIndexesTupple.Any())
            {
                Console.WriteLine(swappedIndexesTupple.Count);
                foreach (var item in swappedIndexesTupple)
                {
                    Console.WriteLine(item.Item1 + " " + item.Item2);
                }
            }
            else
                Console.WriteLine(0);
        }
		
		private static void ShiftDown(long[] array, int index, int size, List<Tuple<int,int>> swappedIndexesTupple)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int minIndex = index;
            if (left < size && array[left] < array[minIndex])
                minIndex = left;
            if (right < size && array[right] < array[minIndex])
                minIndex = right;

            if(index != minIndex)
            {
                // swap the indexes
                var temp = array[index];
                array[index] = array[minIndex];
                array[minIndex] = temp;
                var tuple = new Tuple<int, int>(index, minIndex);
                swappedIndexesTupple.Add(tuple);
                //for (int i = 0; i < size; i++)
                //{
                //    Console.Write(array[i] + " ");
                //}
                //Console.WriteLine();
                ShiftDown(array, minIndex, size, swappedIndexesTupple);
            }
        }
    }
}
