using PathFinder.Graph;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Path
{
    public class AllPaths
    {
        public Dictionary<string, Path?> AllPathsList { get; } = new Dictionary<string, Path>();
        private readonly Dijkstra _dijkstra;
        private readonly Graph.Graph _graph;

        public AllPaths(Graph.Graph graph)
        {
            _graph = graph;
            _dijkstra = new Dijkstra(graph);
            CreateDictionary();
        }

        private void CreateDictionary()
        {
            List<Place> places = _graph.GetCopyOfPlaces().ToList();
            for (int i = 0; i < _graph.Places.Count; i++)
            {
                for (int j = 0; j < _graph.Places.Count; j++)
                {
                    Path? pat = _dijkstra.FindPath(places[i], places[j]);
                    string id = places[i].Id + "|" + places[j].Id;
                    AllPathsList.Add(id, pat);
                }
            }
        }
    }
}