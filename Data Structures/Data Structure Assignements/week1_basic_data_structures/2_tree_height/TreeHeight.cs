using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputString = Console.ReadLine();
            int indexOfUnMatchedChar = 0;
            Dictionary<string, string> balanceParenthesis = new Dictionary<string, string>();
            balanceParenthesis.Add("[", "]");
            balanceParenthesis.Add("(", ")");
            balanceParenthesis.Add("{", "}");
            var isParenthesisBalanced = IsParenthesisBalanced(
                inputString, balanceParenthesis, out indexOfUnMatchedChar);
            if(isParenthesisBalanced)
                Console.WriteLine("Success");
            else
                Console.WriteLine(indexOfUnMatchedChar);
        }
        private static bool IsParenthesisBalanced(string str, 
            Dictionary<string, string> parenthesisLookUp, out int index)
        {
            index = 0;
            Stack<string> parenthesis = new Stack<string>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].ToString().Equals("[") || str[i].ToString().Equals("(") || str[i].ToString().Equals("{"))
                {
                    parenthesis.Push(str[i].ToString());
                }
                else if (str[i].ToString().Equals("]") || str[i].ToString().Equals(")") || str[i].ToString().Equals("}"))
                {
                    // check if stack is empty
                    if(parenthesis.Count > 0 && parenthesis.Any() 
                        && str[i].ToString().Equals(parenthesisLookUp[parenthesis.Peek()]))
                    {
                        parenthesis.Pop();
                    }
                    else
                    {
                        index = i + 1;
                        return false;
                    }
                }
                else
                    continue;
            }
            if (!parenthesis.Any())
                return true;
            else
                index = str.Length;
            return false;
        }
    }
}
