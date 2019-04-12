using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day8Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            int codeChars = 0;
            int memoryChars = 0;
            var file = new StreamReader("advent.txt");
            var i = 0;
            while ((line = file.ReadLine()) != null)
            {
                i++;
                codeChars += line.Length;
                memoryChars += line.Length -2;
                var shit = Regex.Escape(line);
                var hexMatches = new Regex("AAAA").Matches(line);
                var otherMatches = new Regex("(\\\\\\\\)|(\\\\\")").Matches(line);
                memoryChars -= (hexMatches.Count * 3 + otherMatches.Count);
            }
            Console.WriteLine(codeChars - memoryChars);
            Console.ReadKey(false);
        }
    }
}
