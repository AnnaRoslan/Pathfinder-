using PathFinder.Graph;
using PathFinder.Path;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Finder
{
    public class Permutation : IFinder
    {
        private readonly Graph.Graph _graph;
        private readonly AllPaths allPaths;

        public Permutation(Graph.Graph graph)
        {
            _graph = graph;
            allPaths = new AllPaths(_graph);
        }

        public Path.Path FindPath(Place startPlace)
        {
            List<List<Place>> list = FindPermutation(startPlace);
            List<Path.Path> outPath = new List<Path.Path>();
            foreach (var t in list)
            {
                List<Path.Path?> innerPath = new List<Path.Path>();
                for (int j = 0; j < t.Count - 1; j++)
                {
                    string id = t[j].Id + "|" + t[j + 1].Id;
                    innerPath.Add(allPaths.AllPathsList[id]);
                }

                if (!innerPath.Contains(null))
                {
                    outPath.Add(new Path.Path());
                    foreach (var path in innerPath)
                    {
                        outPath.Last().Steps.AddRange(path.Steps);
                    }
                }
            }

            var shortestPaths = outPath.Where(item => item.TotalLength.Equals(outPath.Min(item => item.TotalLength))).ToList();

            return shortestPaths.FirstOrDefault(item => item.Cost.Equals(shortestPaths.Min(item => item.Cost)));
        }

        private IEnumerable<IEnumerable<TPlace>> GetPermutations<TPlace>(IEnumerable<TPlace> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        private List<List<Place>> FindPermutation(Place startPlace)
        {
            List<Place> places = _graph.GetCopyOfChosenPlaces().ToList();
            places.Remove(startPlace);
            List<IEnumerable<Place>> tmp = GetPermutations(places, places.Count).ToList();
            List<List<Place>> list = tmp.Select(item => (item.Concat(new[] { startPlace })).ToList()).ToList();
            list.ForEach(item => item.Insert(0, startPlace));
            return list;
        }
    }
}