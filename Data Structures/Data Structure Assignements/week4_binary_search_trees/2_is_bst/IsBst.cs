using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBst
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
            InputsForProblem input = InputsForBinarySerachTree;
            input.Invoke();
        }
		private static void InputsForBinarySerachTree()
        {
             BinarySerachTree binarySerachTree = new BinarySerachTree();
            binarySerachTree.ReadInput();
            if(binarySerachTree.Nodes <= 1 ||  binarySerachTree.IsBinarySerachTree())
                Console.WriteLine("CORRECT");
            else
                Console.WriteLine("INCORRECT");
        }
    }
}