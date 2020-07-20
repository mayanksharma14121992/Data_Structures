using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBstAdvanced
{
	public delegate void InputsForProblem();
	 public class BinarySerachTree
    {
        public int Nodes { get; set; }
        public TreeNode[] Tree { get; set; }

        public void ReadInput()
        {
            Nodes = Convert.ToInt32(Console.ReadLine());
            Tree = new TreeNode[Nodes];
            for (int i = 0; i < Nodes; i++)
            {
                var treeNode = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                Tree[i] = new TreeNode(treeNode[0],treeNode[1],treeNode[2]);
            }
        }

        public bool IsBinarySerachTree()
        {
            List<int> inOrder = new List<int>();
            InOrderTraversal(inOrder, Tree[0]);
            for (int i = 0; i < inOrder.Count - 1; i++)
            {
                if (inOrder[i] > inOrder[i + 1])
                    return false;
            }
            return true;
        }

        private void InOrderTraversal(List<int> inOrder, TreeNode treeNode)
        {
            if (treeNode.Left == -1 && treeNode.Right == -1)
            {
                inOrder.Add(treeNode.Key);
                return;
            }
            if (treeNode.Left != -1)
                InOrderTraversal(inOrder, Tree[treeNode.Left]);
            inOrder.Add(treeNode.Key);
            if (treeNode.Right != -1)
                InOrderTraversal(inOrder, Tree[treeNode.Right]);
        }

        public bool IsBinarySerachTreeAdvanced()
        {
            List<int[]> inOrder = new List<int[]>();
            InOrderTraversalAdvanced(inOrder, Tree[0]);
            for (int i = 0; i < inOrder.Count - 1; i++)
            {
                if (inOrder[i][0] > inOrder[i+1][0])
                    return false;
                if (inOrder[i][0] == inOrder[i + 1][0] && inOrder[i + 1][1] != -1 && Tree[inOrder[i + 1][1]].Key == inOrder[i][0])
                    return false;
            }
            return true;
        }

        private void InOrderTraversalAdvanced(List<int[]> inOrder, TreeNode treeNode)
        {
            if (treeNode.Left == -1 && treeNode.Right == -1)
            {
                inOrder.Add(new int[2] { treeNode.Key, treeNode.Left });
                return;
            }
            if (treeNode.Left != -1)
                InOrderTraversalAdvanced(inOrder, Tree[treeNode.Left]);
            inOrder.Add(new int[2] { treeNode.Key, treeNode.Left });
            if (treeNode.Right != -1)
                InOrderTraversalAdvanced(inOrder, Tree[treeNode.Right]);
        }
    }

    public class TreeNode
    {
        public int Key { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        
        public TreeNode(int key, int left, int right)
        {
            Key = key;
            Left = left;
            Right = right;

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForBinarySerachTreeAdvanced;
            input.Invoke();
        }
		
		private static void InputsForBinarySerachTreeAdvanced()
        {
            BinarySerachTree binarySerachTree = new BinarySerachTree();
            binarySerachTree.ReadInput();
            if (binarySerachTree.Nodes <= 1 || binarySerachTree.IsBinarySerachTreeAdvanced())
                Console.WriteLine("CORRECT");
            else
                Console.WriteLine("INCORRECT");
        }
    }
}