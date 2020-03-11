using PathFinder.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Path
{
    public class Dijkstra
    {
        private readonly Graph.Graph _graph;

        public Dijkstra(Graph.Graph graph) => _graph = graph;

        private readonly List<Place> _places = new List<Place>();
        private readonly Dictionary<Place, TimeSpan> _distances = new Dictionary<Place, TimeSpan>();
        private readonly Dictionary<Place, Edge?> _previous = new Dictionary<Place, Edge?>();

        public Path FindPath(Place startPlace, Place endPlace)
        {
            Init(startPlace);
            while (_places.Count > 0)
            {
                Place placeWithMinimalDistance = _places.OrderBy(x => _distances[x]).First();
                foreach (var neighborEdge in _graph.FindNeighborEdges(placeWithMinimalDistance))
                {
                    Place neighborPlace = neighborEdge.StartPlace.Equals(placeWithMinimalDistance) ? neighborEdge.EndPlace : neighborEdge.StartPlace;
                    if (_distances[neighborPlace] > _distances[placeWithMinimalDistance] + neighborEdge.Distance)
                    {
                        _distances[neighborPlace] = _distances[placeWithMinimalDistance] + neighborEdge.Distance;
                        _previous[neighborPlace] = neighborEdge;
                    }
                }
                _places.Remove(placeWithMinimalDistance);
            }

            Path foundPath = new Path();
            CreatePath(foundPath, endPlace);
            if (foundPath.Steps.Count.Equals(0))
            {
                return null;
            }

            if (foundPath.StarPlace.Equals(startPlace) && foundPath.EndPlace.Equals(endPlace))
            {
                return foundPath;
            }

            return null;
        }

        private void CreatePath(Path foundPath, Place currentPlace)
        {
            Place currentP = currentPlace;
            while (_previous[currentP] != null)
            {
                if (_previous[currentP].StartPlace.Equals(currentP))
                {
                    foundPath.InsertEdge(_previous[currentP].CloneAndReverse(), 0);
                }
                else
                {
                    foundPath.InsertEdge((_previous[currentP]), 0);
                }

                currentP = foundPath.StarPlace;
            }
        }

        private void Init(Place startPlace)
        {
            foreach (var copyOfPlace in _graph.GetCopyOfPlaces())
            {
                _distances[copyOfPlace] = (TimeSpan.MaxValue) / 2;
                _previous[copyOfPlace] = null;
                _places.Add(copyOfPlace);
            }
            _distances[startPlace] = new TimeSpan(0, 0, 0);
        }
    }
}