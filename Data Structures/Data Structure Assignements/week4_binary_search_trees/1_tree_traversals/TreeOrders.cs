using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeOrders
{
	public delegate void InputsForProblem();
	public class TreeTraversals
    {
        public int Vertices { get; set; }
        public long[] Key { get; set; }
        public int[] LeftIndexArray { get; set; }
        public int[] RightIndexArray { get; set; }

        public void ReadInputForTree()
        {
            Vertices = Convert.ToInt32(Console.ReadLine());
            Key = new long[Vertices];
            LeftIndexArray = new int[Vertices]; 
            RightIndexArray = new int[Vertices];
            for (int i = 0; i < Vertices; i++)
            {
                var treeVertex = Console.ReadLine().Split(' ');
                Key[i] = Convert.ToInt64(treeVertex[0]);
                LeftIndexArray[i] = Convert.ToInt32(treeVertex[1]);
                RightIndexArray[i] = Convert.ToInt32(treeVertex[2]);
            }
        }

        public List<long> InOrder()
        {
            var inOrder = new List<long>();
            InOrderTraversal(0, inOrder);
            return inOrder;
        }

        public void InOrderTraversal(int index, List<long> inOrder )
        {
            if (LeftIndexArray[index] != -1)
                InOrderTraversal(LeftIndexArray[index],inOrder);
            inOrder.Add(Key[index]);
            if (RightIndexArray[index] != -1)
                InOrderTraversal(RightIndexArray[index], inOrder);

        }

        public List<long> PreOrder()
        {
            var preOrder = new List<long>();
            PreOrderTraversal(0, preOrder);
            return preOrder;
        }
        public void PreOrderTraversal(int index, List<long> preOrder)
        {
            preOrder.Add(Key[index]);
            if (LeftIndexArray[index] != -1)
                PreOrderTraversal(LeftIndexArray[index], preOrder);
            if (RightIndexArray[index] != -1)
                PreOrderTraversal(RightIndexArray[index], preOrder);
        }

        public List<long> PostOrder()
        {
            var postOrder = new List<long>();
            PostOrderTraversal(0, postOrder);
            return postOrder;
        }
        public void PostOrderTraversal(int index, List<long> postOrder)
        {
            if (LeftIndexArray[index] != -1)
                PostOrderTraversal(LeftIndexArray[index], postOrder);
            if (RightIndexArray[index] != -1)
                PostOrderTraversal(RightIndexArray[index], postOrder);
            postOrder.Add(Key[index]);
        }

        public void PrintTree(List<long> treeOrder)
        {
            foreach (var item in treeOrder)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForBinaryTreeTranversal;
            input.Invoke();
        }
		private static void InputsForBinaryTreeTranversal()
        {
            TreeTraversals treeTraversals = new TreeTraversals();
            treeTraversals.ReadInputForTree();
            treeTraversals.PrintTree(treeTraversals.InOrder());
            treeTraversals.PrintTree(treeTraversals.PreOrder());
            treeTraversals.PrintTree(treeTraversals.PostOrder());
        }
    }
}