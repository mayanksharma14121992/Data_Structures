using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashChains
{
	public delegate void InputsForProblem();
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = HashingWithChains;
            input.Invoke();
        }
		 private static void HashingWithChains()
        {
            int mod = Convert.ToInt32(Console.ReadLine());
            int queries = Convert.ToInt32(Console.ReadLine());
            Dictionary<int, List<string>> chainRecords = new Dictionary<int, List<string>>();
            for (int i = 0; i < queries; i++)
            {
                var input = Console.ReadLine().Split(' ');
                ProcessChainQueries(chainRecords, input[0], input[1], mod);
            }
        }
		 private static void ProcessChainQueries(Dictionary<int,List<string>> chainRecords ,string action, string value, int mod)
        {
            switch(action)
            {
                case "add":
                    {
                        //int key = (int)GetHashValue(value) % mod;
                        int key = (int)GetHashValue(value,mod);
                        if (chainRecords.ContainsKey(key))
                        {
                            var listOfRecords = chainRecords[key];
                            var isNewRecord = listOfRecords.FirstOrDefault(x => x.Equals(value));
                            if(isNewRecord == null)
                            {
                                listOfRecords.Insert(0,value);
                                chainRecords[key] = listOfRecords;
                            }
                        }
                        else
                        {
                            List<string> newRecord = new List<string>();
                            newRecord.Add(value);
                            chainRecords.Add(key, newRecord);
                        }
                    }
                    break;
                case "del":
                    {
                        //int key = (int)GetHashValue(value) % mod;
                        int key = (int)GetHashValue(value, mod);
                        if (chainRecords.ContainsKey(key))
                        {
                            var listOfRecords = chainRecords[key];
                            var isNewRecord = listOfRecords.FirstOrDefault(x => x.Equals(value));
                            if (isNewRecord != null)
                            {
                                listOfRecords.Remove(value);
                                chainRecords[key] = listOfRecords;
                            }
                        }
                    }
                    break;
                case "find":
                    {
                        //int key = (int)GetHashValue(value) % mod;
                        int key = (int)GetHashValue(value, mod);
                        if (chainRecords.ContainsKey(key))
                        {
                            var listOfRecords = chainRecords[key];
                            var isNewRecord = listOfRecords.FirstOrDefault(x => x.Equals(value));
                            if (isNewRecord != null)
                                Console.WriteLine("yes");
                            else
                                Console.WriteLine("no");
                        }
                        else
                            Console.WriteLine("no");
                    }
                    break;
                default: // default case is for check
                    {
                        int key = int.Parse(value);
                        if (chainRecords.ContainsKey(key))
                        {
                            var listOfRecords = chainRecords[key];
                            foreach (var item in listOfRecords)
                            {
                                Console.Write(item + " ");
                            }
                            Console.WriteLine();
                        }
                        else
                            Console.WriteLine();
                    }
                    break;
            }
        }

        private static long GetHashValue(string input)
        {
            long constant = 1000000007;
            int factor = 263;
            long getHash = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char chr = input[i];
                long getHashOfChar = (long)(Math.Pow(factor, i) % constant);
                getHashOfChar = (getHashOfChar * (int)chr) % constant;
                getHash = (getHash + getHashOfChar) % constant;
            }
            return getHash;
        }
		
		private static long GetHashValue(string s, int m)
        {
            long hash = 0;
            for (int i = s.Length - 1; i >= 0; --i)
                hash = (hash * 263 + (int)s[i]) % 1000000007;
            return (hash % m);
        }
    }
}