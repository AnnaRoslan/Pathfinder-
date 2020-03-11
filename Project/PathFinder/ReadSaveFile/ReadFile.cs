using PathFinder.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PathFinder.ReadFile
{
    public class ReadData
    {
        private static readonly List<string> _data1 = new List<string>();
        private static readonly List<string> _data2 = new List<string>();
        private static readonly List<string> _data3 = new List<string>();
        private static readonly Regex Segment1 = new Regex(@"\s*###\s*Miejsca podróży");
        private static readonly Regex Segment2 = new Regex(@"\s*###\s*Czas przejścia");
        private static readonly Regex Segment3 = new Regex(@"\s*###\s*Wybrane miejsca podróży");
        private static readonly Regex Pattern1 = new Regex(@"\s*\d.\s*[|]((\s*[^|]+\s*)+[|]\s*){3}");
        private static readonly Regex Pattern2 = new Regex(@"\s*\d.\s*[|]((\s*[^|]+\s*)+[|]\s*){2}(\s*\d+[:][0-5]\d\s*[|]){2}\s*(([0-9]+)|([-]{2}))\s*[|]");
        private static readonly Regex Pattern3 = new Regex(@"\s*\d.\s*[|]((\s*[^|]+\s*)+[|]\s*)");

        public static void Read(string filePath, Graph.Graph graph)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"There is no file in  \n {filePath}");
            }

            using StreamReader file = new StreamReader(filePath, Encoding.UTF8);

            string ln;

            while ((ln = file.ReadLine()) != null && !Segment1.IsMatch(ln)) { }

            while ((ln = file.ReadLine()) != null && !Segment2.IsMatch(ln))
            {
                if (ln == "")
                {
                    continue;
                }

                if (!Pattern1.IsMatch(ln))
                {
                    throw new IOException($"Wrong data structure {ln}");
                }

                _data1.Add(ln);
            }

            while ((ln = file.ReadLine()) != null && !Segment3.IsMatch(ln))
            {
                if (ln == "")
                {
                    continue;
                }

                if (!Pattern2.IsMatch(ln))
                {
                    throw new IOException($"Wrong data structure in line: {ln}");
                }

                ln = ln.Replace("--", "0");
                _data2.Add(ln);
            }
            while ((ln = file.ReadLine()) != null && !Segment3.IsMatch(ln))
            {
                if (ln == "")
                {
                    continue;
                }

                if (!Pattern3.IsMatch(ln))
                {
                    throw new IOException($"Wrong data structure in line: {ln}");
                }

                _data3.Add(ln);
            }

            if (_data1.Count == 0)
            {
                throw new IOException("Wrong file structure! Missing \"Miejsca Podróży\" ");
            }

            if (_data2.Count == 0)
            {
                throw new IOException("Wrong file structure! Missing \"Czas przejscia\" ");
            }
            AddPlace(graph);
            AddEdge(graph);
            

            if (_data1.Count == 1)
            {
                throw new IOException($"You cannot find path from one place");
            }
            if (_data3.Count == 1)
            {
                throw new IOException($"You cannot find path from one place");
            }
            if (_data3.Count != 0)
            {
                AddChosenPlaces(graph, true);
            }
            else
            {
                AddChosenPlaces(graph);
            }

            file.Close();
        }

        private static void AddPlace(Graph.Graph graph)
        {
            foreach (var ln in _data1)
            {
                List<String> data = ln.Split("|").ToList();
                graph.AddPlace(new Place((data[1].Replace(" ", "")), data[2]));
            }
        }

        private static void AddEdge(Graph.Graph graph)
        {
            foreach (var ln in _data2)
            {
                List<String> data = ln.Split("|").ToList();
                graph.AddEdge(data[1].Replace(" ", ""), data[2].Replace(" ", ""), data[3].Split(":"), data[5]);

                if (!data[4].Equals("0:00"))
                    graph.AddEdge(data[2].Replace(" ", ""), data[1].Replace(" ", ""), data[4].Split(":"), data[5]);
            }
        }

        private static void AddChosenPlaces(Graph.Graph graph, bool isGiven = false)
        {
            if (isGiven)
            {
                foreach (var ln in _data3)
                {
                    List<String> id = ln.Split("|").ToList();

                    Place place = graph.Places.Find(item => item.Id.Equals(id[1].Replace(" ", "")));
                    if (graph.ChosenPlaces.Exists(item => item.Equals(place)))
                    {
                        throw new IOException($"Cannot choose place twice {ln}");
                    }
                    if (place == null)
                    {
                        throw new IOException($"Cannot choose place that not exist {ln}");
                    }

                    graph.AddChosenPlace(place);
                }
            }
            else
            {
                graph.ChosenPlaces.AddRange(graph.Places);
            }
        }
    }
}