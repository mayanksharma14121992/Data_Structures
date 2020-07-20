using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWithMax
{
    class Program
    {
        static void Main(string[] args)
        {
            InputsForStackExtension();
        }
        private static void InputsForStackExtension()
        {
            var queries = Convert.ToInt32(Console.ReadLine());
            Stack<int> stack = new Stack<int>();
            List<int> maxNumberList = new List<int>();
            for (int i = 0; i < queries; i++)
            {
                var input = Console.ReadLine().Split(' ');
                switch(input[0])
                {
                    case "push":
                        {
                            ProcessPushPopOperation(
                                stack, maxNumberList, Convert.ToInt32(input[1]), true);
                        }
                        break;
                    case "pop":
                        {
                            ProcessPushPopOperation(stack, maxNumberList);
                        }
                        break;
                    default:
                        {
                            Console.WriteLine(maxNumberList[0]);
                        }
                        break;
                }
            }
        }
private static void ProcessPushPopOperation(Stack<int> stack, 
            List<int> maxNumberList, int newEntry = 0, bool isPush = false)
        {
            if(isPush)
            {
                stack.Push(newEntry);
                if(!maxNumberList.Any())
                {
                    maxNumberList.Insert(0, newEntry);
                }
                else if(newEntry >= maxNumberList[0])
                {
                    maxNumberList.Insert(0, newEntry);
                }
                else if(newEntry < maxNumberList[maxNumberList.Count - 1])
                {
                    maxNumberList.Insert(maxNumberList.Count, newEntry);
                }
                else
                {
                    //int index = 0;
                    //foreach (var item in maxNumberList)
                    //{
                    //    if (item > newEntry)
                    //        index++;
                    //}
                    maxNumberList.Add(newEntry);
                }
            }
            else
            {
                var elementToBeRemoved = stack.Peek();
                stack.Pop();
                if (elementToBeRemoved == maxNumberList[0])
                    maxNumberList.RemoveAt(0);
                else
                    maxNumberList.RemoveAt(maxNumberList.Count - 1);
                //int index = 0;
                //foreach (var item in maxNumberList)
                //{
                //    if (item == elementToBeRemoved)
                //        break;
                //    index++;
                //}
                //maxNumberList.RemoveAt(index);
            }
        } 
    }
}
