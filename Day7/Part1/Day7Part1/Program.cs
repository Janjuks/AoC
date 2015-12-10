using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7Part1
{
    class Program
    {
        public static List<Dictionary<string, string>> commands = new List<Dictionary<string, string>>();
        public static Dictionary<string, ushort?> wires = new Dictionary<string, ushort?>();
        static void Main(string[] args)
        {
            string line;
            var file = new StreamReader("advent.txt");

            while ((line = file.ReadLine()) != null)
            {
                PopulateCommands(line);
            }
            foreach (var comm in commands)
            {
                if (!wires.ContainsKey(comm["source"]))
                    wires.Add(comm["source"], TryParse2(comm["source"]));
                if (!wires.ContainsKey(comm["destination"]))
                    wires.Add(comm["destination"], TryParse2(comm["destination"]));
                if (comm.ContainsKey("source2") && !wires.ContainsKey(comm["source2"]))
                    wires.Add(comm["source2"], TryParse2(comm["source2"]));
            }

            while (wires["a"] == null)
            {
                for (var i = 0; i < commands.Count; i++)
                {
                    var sourceKey = commands[i]["source"];
                    var source2Key = "";
                    var destinationKey = commands[i]["destination"];
                    ushort? source = wires[sourceKey];
                    ushort? source2 = null;
                    if (source != null)
                    {
                        if (commands[i].ContainsKey("source2"))
                        {
                            source2Key = commands[i]["source2"];
                            source2 = wires[source2Key];
                        }
                        switch (commands[i]["type"])
                        {
                            case "RSHIFT":
                                if (wires[destinationKey] == null)
                                {
                                    wires[destinationKey] = (UInt16)(source >> source2);
                                    break;
                                }
                                else break;
                            case "LSHIFT":
                                if (wires[destinationKey] == null)
                                {
                                    wires[destinationKey] = (UInt16)(source << source2);
                                    break;
                                }
                                else break;
                            case "OR":
                                if (source2 != null && wires[destinationKey] == null)
                                {
                                    wires[destinationKey] = (UInt16)(source | source2);
                                    break;
                                }
                                else break;
                            case "AND":
                                if (source2 != null && wires[destinationKey] == null)
                                {
                                    wires[destinationKey] = (UInt16)(source & source2);
                                    break;
                                }
                                else break;
                            case "NOT":
                                if (wires[destinationKey] == null)
                                {
                                    wires[destinationKey] = (UInt16)(~source);
                                    break;
                                }
                                else break;
                            case "ASSIGNMENT":
                                if (wires[destinationKey] == null)
                                {
                                    wires[destinationKey] = source;
                                    break;
                                }
                                else break;

                        }
                    }
                    else continue;
                }
            }

            Console.WriteLine(wires["a"]);
            Console.ReadKey(false);
        }



        static void PopulateCommands(string line)
        {
            if (new Regex("RSHIFT").IsMatch(line))
                commands.Add(new Dictionary<string, string> { 
                { "type", "RSHIFT" }, 
                { "source", new Regex("^([a-z]{1,2})").Match(line).Groups[1].Value },
                { "source2", new Regex("([0-9]{1,2})").Match(line).Groups[1].Value },
                { "destination", new Regex("([a-z]{1,2})$").Match(line).Groups[1].Value}
                });
            else if (new Regex("LSHIFT").IsMatch(line))
                commands.Add(new Dictionary<string, string> {
                { "type", "LSHIFT" }, 
                { "source", new Regex("^([a-z]{1,2})").Match(line).Groups[1].Value },
                { "source2", new Regex("([0-9]{1,2})").Match(line).Groups[1].Value },
                { "destination", new Regex("([a-z]{1,2})$").Match(line).Groups[1].Value}
                });
            else if (new Regex("OR").IsMatch(line))
                commands.Add(new Dictionary<string, string> {
                { "type", "OR" }, 
                { "source", new Regex("^([0-9a-z]{1,2})").Match(line).Groups[1].Value },
                { "source2", new Regex("OR\\s([0-9a-z]{1,2})").Match(line).Groups[1].Value },
                { "destination", new Regex("([a-z]{1,2})$").Match(line).Groups[1].Value}
                });
            else if (new Regex("AND").IsMatch(line))
                commands.Add(new Dictionary<string, string> {
                { "type", "AND" }, 
                { "source", new Regex("^([0-9a-z]{1,2})").Match(line).Groups[1].Value },
                { "source2", new Regex("AND\\s([0-9a-z]{1,2})").Match(line).Groups[1].Value },
                { "destination", new Regex("([a-z]{1,2})$").Match(line).Groups[1].Value}
                });
            else if (new Regex("NOT").IsMatch(line))
                commands.Add(new Dictionary<string, string> { 
                { "type", "NOT" }, 
                { "source", new Regex("NOT\\s([0-9a-z]{1,2})").Match(line).Groups[1].Value },
                { "destination", new Regex("([a-z]{1,2})$").Match(line).Groups[1].Value}
                });
            else
                commands.Add(new Dictionary<string, string> { 
                { "type", "ASSIGNMENT" }, 
                { "source", new Regex("^([0-9a-z]{1,4})").Match(line).Groups[1].Value },
                { "destination", new Regex("([a-z]{1,2})$").Match(line).Groups[1].Value}
                });
        }

        public static ushort? TryParse2(string s)
        {
            ushort i;
            if (!UInt16.TryParse(s, out i))
            {
                return null;
            }
            else
            {
                return i;
            }
        }

    }
}
