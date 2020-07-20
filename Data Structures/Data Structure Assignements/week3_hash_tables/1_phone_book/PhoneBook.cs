using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
	public delegate void InputsForProblem();
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsForPhoneBookDiary;
            input.Invoke();
        }
		private static void InputsForPhoneBookDiary()
        {
            var queries = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();
            for (int i = 0; i < queries; i++)
            {
                var query = Console.ReadLine().Split(' ');
                if (query[0].Equals("add"))
                    ProcessPhoneBookQuery(query[0], phoneBook, query[1], query[2]);
                else
                    ProcessPhoneBookQuery(query[0], phoneBook, query[1]);
            }

        }

        private static void ProcessPhoneBookQuery(string query, Dictionary<string, string> phoneBook, string key, string value = "")
        {
            switch(query)
            {
                case "add":
                    {
                        if (phoneBook.ContainsKey(key))
                            phoneBook[key] = value;
                        else
                            phoneBook.Add(key, value);
                    }
                    break;
                case "del":
                    {
                        if (phoneBook.ContainsKey(key))
                            phoneBook.Remove(key);
                    }
                    break;
                default:
                    {
                        if (phoneBook.ContainsKey(key))
                            Console.WriteLine(phoneBook[key]);
                        else
                            Console.WriteLine("not found");
                    }
                    break;
            }
        }
    }
}