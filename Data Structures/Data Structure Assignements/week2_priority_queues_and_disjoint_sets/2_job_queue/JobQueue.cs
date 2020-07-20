using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobQueue
{
	public delegate void InputsForProblem();
	public class MinHeap
    {
        public int HeapSize { get; set; }
        public Tuple<int,long>[] Heap { get; set; }
        public MinHeap(int heapSize, int jobsSize)
        {
            HeapSize = heapSize;
            Heap = new Tuple<int, long>[heapSize];
            for (int i = 0; i < heapSize; i++)
            {
                Heap[i] = new Tuple<int, long>(i, 0); 
            }
        }

        private void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
        private int GetParent(int index)
        {
            return (index -1) / 2;
        }

        private int GetLeftChild(int index)
        {
            return 2 * index + 1;
        }

        private int GetRightChild(int index)
        {
            return 2 * index + 2;
        }

       
        private bool Comparator(Tuple<int,long> thread1, Tuple<int, long> thread2)
        {
            if (thread1.Item2 != thread2.Item2)
                return thread1.Item2 < thread2.Item2;
            else
                return thread1.Item1 < thread2.Item1;
        }
        private void SiftUp(int index)
        {
          while(index > 0 && Comparator(Heap[index],Heap[GetParent(index)]))
            {
                Swap( ref Heap[index], ref Heap[GetParent(index)]);
                index = GetParent(index);
            }
        }

        private void SiftDown(int index)
        {
            int minIndex = index;
            int left = GetLeftChild(index);
            if (left < HeapSize && Comparator(Heap[left], Heap[minIndex]))
                minIndex = left;

            int right = GetRightChild(index);
            if (right < HeapSize && Comparator(Heap[right], Heap[minIndex]))
                minIndex = right;

            if(index != minIndex)
            {
                Swap(ref Heap[index], ref Heap[minIndex]);
                SiftDown(minIndex);
            }
        }

        public void ChangePriority(int index , long newPriority)
        {
            var oldPriority = Heap[index].Item2;
            Heap[index] = new Tuple<int, long>(Heap[index].Item1, newPriority);
            if (oldPriority > newPriority)
                SiftUp(index);
            SiftDown(index);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForPrallelProcessing;
            input.Invoke();
        }
        private static void InputsForPrallelProcessing()
        {
            var input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var threads = input[0];
            var jobsSize = input[1];
            var jobs = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
            //ProcessJobsBruteForce(jobs, threads);
            ProcessJobsOptimized(jobs, threads);
        }
		 private static void ProcessJobsOptimized(long[] jobs, int threadSize)
        {
            var jobsSize = jobs.Length;
            int[] threads = new int[jobsSize];
            long[] startTime = new long[jobsSize];
            var minHeap = new MinHeap(threadSize, jobsSize);
            for (int i = 0; i < jobsSize; i++)
            {
                threads[i] = minHeap.Heap[0].Item1;
                startTime[i] = minHeap.Heap[0].Item2;
                minHeap.ChangePriority(0, minHeap.Heap[0].Item2 + jobs[i]);
            }
            ShowOutput(threads, startTime);
        }
		
		private static void ShowOutput(int[] threads, long[] startTime)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                Console.WriteLine(threads[i] + " " + startTime[i]);
            }
        }
    }
}
