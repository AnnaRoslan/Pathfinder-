using PathFinder.Finder;
using PathFinder.Graph;
using PathFinder.ReadFile;
using System;

namespace PathFinder
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You need to give file path to data!");
                return;
            }

            String filePath = "";
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            Graph.Graph graph = new Graph.Graph();
            ReadData.Read(filePath, graph);
            Place startPlace;
            if (args.Length == 2)
            {
                startPlace = graph.ChosenPlaces.Find(item => item.Id.Equals(args[1]));
                if (startPlace == null)
                {
                    Console.WriteLine("You cannot choose start place that not exist");
                    return;
                }
            }
            else
            {
                startPlace = graph.ChosenPlaces[0];
            }
            Finder.IFinder finder;
            if (graph.ChosenPlaces.Count > 10)
            {
                finder = new Genetic(2000, 1000, graph);
            }
            else
            {
                finder = new Permutation(graph);
            }

            Path.Path? path = finder.FindPath(startPlace);
            if (path != null)
            {
                SaveFile.Save(graph, path);
                Console.WriteLine("Check the result");
            }
            else
            {
                Console.WriteLine("Path do not exist");
            }
        }
    }
}