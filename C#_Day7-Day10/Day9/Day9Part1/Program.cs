using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            var file = new StreamReader("advent.txt");
            Dictionary<string, bool> vertexes = new Dictionary<string, bool>();
            List<Dictionary<string, string>> distances = new List<Dictionary<string, string>>();
            int minDistance = int.MaxValue;

            while ((line = file.ReadLine()) != null)
            {
                var regResult = new Regex("([A-Za-z]+)\\sto\\s([A-Za-z]+)\\s=\\s([0-9]+)").Match(line);
                if (!vertexes.ContainsKey(regResult.Groups[1].Value))
                    vertexes.Add(regResult.Groups[1].Value, false);
                if (!vertexes.ContainsKey(regResult.Groups[2].Value))
                    vertexes.Add(regResult.Groups[2].Value, false);
                distances.Add(new Dictionary<string, string>
                {
                    {"vertex1", regResult.Groups[1].Value},
                    {"vertex2", regResult.Groups[2].Value},
                    {"distance", regResult.Groups[3].Value}
                });
            }

            foreach (var vert in vertexes) {
                var distance = GetShortestDistance(vertexes, distances, vert.Key);
                if (distance < minDistance)
                    minDistance = distance;
            }

            Console.WriteLine(minDistance);
            Console.ReadKey(false);
        }

        static int GetShortestDistance(Dictionary<string, bool> _vertexes, List<Dictionary<string, string>> distances, string startingVertex)
        {
            int vertexesVisited = 1;
            int distance = 0;
            string currentVertex = startingVertex;
            var vertexes = new Dictionary<string, bool>();
            foreach (var _vert in _vertexes)
                vertexes.Add(_vert.Key, false);
            vertexes[startingVertex] = true;
            while (vertexesVisited < vertexes.Count)
            {
                int minimalDistance = int.MaxValue;
                string nextVertex = String.Empty;
                var targets = distances.Where(v => v["vertex1"] == currentVertex || v["vertex2"] == currentVertex).ToList();
                foreach (var vertex in vertexes)
                    if (vertex.Key != currentVertex && vertex.Value)
                        targets.RemoveAll(v => v["vertex1"] == vertex.Key || v["vertex2"] == vertex.Key);

                foreach (var target in targets)
                {
                    if (int.Parse(target["distance"]) < minimalDistance)
                    {
                        minimalDistance = int.Parse(target["distance"]);
                        nextVertex = target["vertex1"] == currentVertex ? target["vertex2"] : target["vertex1"];
                    }
                }
                distance += minimalDistance;
                vertexes[nextVertex] = true;
                currentVertex = nextVertex;
                vertexesVisited++;
            }
            return distance;
        }

    }
}
