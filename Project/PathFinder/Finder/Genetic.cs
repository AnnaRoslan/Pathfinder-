using PathFinder.Graph;
using PathFinder.Path;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Finder
{
    internal class Genetic : IFinder
    {
        private readonly int _generationNumber;
        private readonly int _populationSize;
        private readonly Graph.Graph _graph;
        private readonly Random _random;
        private readonly AllPaths _allPaths;

        public Genetic(int generationNumber, int populationSize, Graph.Graph graph)
        {
            _graph = graph;
            _random = new Random();
            _allPaths = new AllPaths(graph);
            _generationNumber = generationNumber;
            _populationSize = populationSize;
        }

        public Path.Path FindPath(Place startPlace)
        {
            List<List<Place>> population = PreparePopulation(startPlace);
            List<Conspecifics> conspecificsList = new List<Conspecifics>();
            List<Path.Path> outPath = new List<Path.Path>();

            foreach (var place in population)
            {
                Path.Path? path = CreatePath(place);

                if (!path.Equals(null))
                {
                    outPath.Add(path);
                }
            }

            foreach (var path in outPath)
            {
                List<Place> placeList = path.Steps.Select(item => item.StartPlace).ToList();
                placeList.Add(path.EndPlace);
                conspecificsList.Add(new Conspecifics(placeList, path.TotalLength, path.Cost));
            }

            for (var i = 0; i < _generationNumber; i++)
            {
                Cross(conspecificsList);
                Mute(conspecificsList);
                conspecificsList = conspecificsList.OrderBy(item => item.TotalDistance).ToList();
                conspecificsList.RemoveRange(_populationSize, conspecificsList.Count - _populationSize);
            }

            var shortestPaths = conspecificsList.Where(item => item.TotalDistance.Equals(outPath.Min(item => item.TotalLength))).ToList();
            return CreatePath(shortestPaths
                .FirstOrDefault(item => item.Cost.Equals(shortestPaths.Min(item => item.Cost))).Population);
        }

        private void Cross(List<Conspecifics> conspecificsList)
        {
            Place startPlace = conspecificsList[0].Population[0];
            List<int> choosen = new List<int>();
            for (int i = 0; i < (conspecificsList.Count * 0.8); i++)
            {
                choosen.Add(_random.Next(conspecificsList.Count - 1));
            }

            for (int i = 0; i < choosen.Count - 1; i += 2)
            {
                List<Place> places1 = new List<Place>();
                places1.AddRange(conspecificsList[choosen[i]].Population);
                places1.RemoveAt(0);
                places1.RemoveAt(places1.Count - 1);
                List<Place> places2 = new List<Place>();
                places2.AddRange(conspecificsList[choosen[i + 1]].Population);
                places2.RemoveAt(0);
                places2.RemoveAt(places2.Count - 1);
                List<Place> places = new List<Place>();
                for (int j = 0; j < (places1.Count / 2); j++)
                {
                    places.Add(places1[j]);
                    places2.Remove(places1[j]);
                }
                places.AddRange(places2);
                places.Insert(0, startPlace);
                places.Add(startPlace);
                var path = CreatePath(places);
                if (!path.Count.Equals(0))
                {
                    Conspecifics conspecifics = new Conspecifics(places, path.TotalLength, path.Cost);
                    conspecificsList.Add(conspecifics);
                }
            }
        }

        private void Mute(List<Conspecifics> conspecificsList)
        {
            List<int> chosen = new List<int>();
            for (int i = 0; i < (_populationSize * 0.01); i++)
            {
                chosen.Add(_random.Next(conspecificsList.Count - 1));
            }

            foreach (var n in chosen)
            {
                List<Place> places = new List<Place>();
                places.AddRange(conspecificsList[n].Population);
                int i = _random.Next(1, places.Count - 2);
                int j = _random.Next(1, places.Count - 2);
                var tmp = places[i];
                places[i] = places[j];
                places[j] = tmp;

                var path = CreatePath(places);
                if (!path.Count.Equals(0))
                {
                    Conspecifics conspecifics = new Conspecifics(places, path.TotalLength, path.Cost);
                    conspecificsList.Add(conspecifics);
                }
            }
        }

        private List<List<Place>> PreparePopulation(Place startPlace)
        {
            List<Place> places = _graph.GetCopyOfChosenPlaces().ToList();
            places.Remove(startPlace);

            List<List<Place>> population = new List<List<Place>>();
            for (int i = 0; i < _populationSize; i++)
            {
                population.Add(places.OrderBy(x => _random.Next()).ToList());
                population[i].Insert(0, startPlace);
                population[i].Add(startPlace);
            }
            return population;
        }

        private class Conspecifics
        {
            public readonly List<Place> Population;
            public readonly TimeSpan TotalDistance;
            public readonly double Cost;

            public Conspecifics(List<Place> places, TimeSpan distance, double cost)
            {
                Population = places;
                TotalDistance = distance;
                Cost = cost;
            }
        }

        private Path.Path CreatePath(List<Place> places)
        {
            Path.Path? outPath = new Path.Path();

            List<Path.Path?> innerPath = new List<Path.Path>();
            for (int j = 0; j < places.Count - 1; j++)
            {
                string id = places[j].Id + "|" + places[j + 1].Id;
                innerPath.Add(_allPaths.AllPathsList[id]);
            }

            if (!innerPath.Contains(null))
            {
                foreach (var path in innerPath)
                {
                    outPath.Steps.AddRange(path.Steps);
                    outPath.ChosenSteps.Add(new Edge(path.StarPlace, path.EndPlace, path.TotalLength, path.Cost));
                }
            }
            return outPath;
        }
    }
}