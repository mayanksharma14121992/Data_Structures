using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSlidingWindow
{
	public delegate void InputsForProblem();
    public delegate int[] ConvertStringArrayToIntArray(string[] inputStringArray);
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForMaxSumInSlidingWindowProblem;
            input.Invoke();
        }
		private static void InputsForMaxSumInSlidingWindowProblem()
        {
            var noOfElements = Convert.ToInt32(Console.ReadLine());
            var elementsString = Console.ReadLine().Split(' ');
            var slideWindowSize = Convert.ToInt32(Console.ReadLine());
            if(noOfElements == 1)
            {
                Console.WriteLine(elementsString[0]);
            }
            else
            {
                ConvertStringArrayToIntArray del = new ConvertStringArrayToIntArray(GetIntergeerArray);
                var elements = del.Invoke(elementsString);
                List<int> elementsQueue = new List<int>();
                List<int> maxInWindow = new List<int>();
                MaxInSlidingWindow(elementsQueue, slideWindowSize, elements);
            }
        }
		private static void MaxInSlidingWindow(List<int> deQueue, int slideWindowLength , int[] array)
        {
            for (int i = 0; i < slideWindowLength; i++)
            {
                while (deQueue.Count > 0 && array[deQueue[deQueue.Count - 1]] <= array[i])
                    deQueue.RemoveAt(deQueue.Count - 1);

                deQueue.Insert(deQueue.Count, i);
            }
            for (int j = slideWindowLength; j < array.Length; j++)
            {
                Console.Write(array[deQueue[0]] + " ");
                while (deQueue.Count > 0 && deQueue[0] <= j - slideWindowLength)
                    deQueue.RemoveAt(0);

                while (deQueue.Count > 0 && array[j] >= array[deQueue[deQueue.Count - 1]])
                    deQueue.RemoveAt(deQueue.Count - 1);

                deQueue.Insert(deQueue.Count, j);
            }
            Console.Write(array[deQueue[0]]);
        }
		
        private static int[] GetIntergeerArray(string[] strArray)
        {
            int size = strArray.Length;
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = Convert.ToInt32(strArray[i]);
            }
            return array;
        }
    }
}
