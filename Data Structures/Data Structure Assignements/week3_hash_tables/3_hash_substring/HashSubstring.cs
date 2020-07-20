using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSubstring
{
	public delegate void InputsForProblem();
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForFindPatternInText;
            input.Invoke();
        }
		private static void InputsForFindPatternInText()
        {
            var pattern = Console.ReadLine();
            var text = Console.ReadLine();
            RabinKarpPattrenMatching(pattern, text);
        }
        private static long PolyHash(string input, long constant = 1000000007, int factor = 263)
        {
            long hash = 0;
            for (int i = input.Length - 1; i >= 0; --i)
                hash = ((hash * factor + (int)input[i]) % constant + constant) % constant;
            return hash;
        }

        private static long[] PrecomputeHashValues(string pattern, string text, long constant = 1000000007, int factor = 263)
        {
            int patternLength = pattern.Length;
            int textLength = text.Length;
            long power = 1;
            for (int i = 0; i < patternLength; i++)
            {
                power = (power * factor) % constant;
            }

            long[] hash = new long[text.Length - patternLength + 1];
            string hashForLastSet = text.Substring(textLength - patternLength, patternLength);
            hash[textLength - patternLength] = PolyHash(hashForLastSet);
            for (int i = textLength - patternLength - 1; i >= 0 ; i--)
            {
                hash[i] = ((factor * hash[i + 1] + (int)text[i] - power * (int)text[i + patternLength]) % constant + constant) % constant;
            }
            return hash;
        }

        private static void RabinKarpPattrenMatching(string pattern, string text)
        {
            int patternLength = pattern.Length;
            int textLength = text.Length;
            var hash = PrecomputeHashValues(pattern, text);
            var patternHash = PolyHash(pattern);
            for (int i = 0; i <= textLength - patternLength; i++)
            {
                if(hash[i] == patternHash)
                {
                    if(pattern.Equals(text.Substring(i,patternLength)))
                        Console.Write(i + " ");
                        
                }
            }
        }
    }
}