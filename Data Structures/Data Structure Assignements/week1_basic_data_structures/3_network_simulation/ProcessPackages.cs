using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPackages
{
	public delegate void InputsForProblem();
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForNetworkPacketSimuation;
            input.Invoke();
        }
        private static void InputsForNetworkPacketSimuation()
        {
            var input = Console.ReadLine().Split(' ');
            var stackSize = Convert.ToInt32(input[0]);
            var noOfPackets = Convert.ToInt32(input[1]);
            List<int> packageStartTime = new List<int>();
            List<int> packageFinishTime = new List<int>();
            List<int> deque = new List<int>();
            for (int i = 0; i < noOfPackets; i++)
            {
                var packet = Console.ReadLine().Split(' ');
                packageStartTime.Add(Convert.ToInt32(packet[0]));
                packageFinishTime.Add(Convert.ToInt32(packet[1]));
            }
            ProcessPacketBeginningTime(stackSize, packageStartTime, packageFinishTime, deque);
        }
		private static void ProcessPacketBeginningTime(int stackSize, List<int> packageStartTime,
            List<int> packageFinishTime, List<int> deque ) 
        {
            for (int i = 0; i < packageStartTime.Count; i++)
            {
                // before the arrival of every new packet
                // remove the pakets from the deque which has already been processed
                while(deque.Count > 0 && deque[0] <= packageStartTime[i])
                {
                    deque.RemoveAt(0);
                }
                // Keep apending the requests if the buffer size allows
                // otherwise request cannot be appended
                if(deque.Count < stackSize)
                {
                    // for the first request just output the arrival time of the request
                    if(deque.Count == 0)
                    {
                        deque.Add(packageStartTime[i] + packageFinishTime[i]);
                        Console.WriteLine(packageStartTime[i]);
                    }
                    else
                    {
                        int startTime = packageStartTime[i];
                        if (deque[deque.Count - 1] > startTime)
                            startTime = deque[deque.Count - 1];
                        else if (deque[deque.Count - 1] == startTime)
                            startTime = deque[deque.Count - 1] + 1;
                        deque.Add(startTime + packageFinishTime[i]);
                        Console.WriteLine(startTime);
                    }
                }
                else
                {
                    Console.WriteLine(-1);
                }
            }
        }
    }
}
