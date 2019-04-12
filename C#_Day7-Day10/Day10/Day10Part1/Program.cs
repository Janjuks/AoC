using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day10Part1 {
    class Program {
        static void Main(string[] args) {
            string input = "1113122113";
            for (var i = 0; i < 40; i++)
                input = LookAndSay(input);
            Console.WriteLine(input.Length);
            Console.ReadKey(false);
        }

        static string LookAndSay (string input) {
            var groups = new Regex("([0-9])\\1*").Matches(input);
            var result = String.Empty;

            foreach (var group in groups)
                result += String.Concat(group.ToString().Length, group.ToString()[0]);
            return result;
        }
    }
}
