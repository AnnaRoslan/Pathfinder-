using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Graph
{
    public class Graph
    {
        public List<Place> Places { get; } = new List<Place>();
        public List<Edge> Edges { get; } = new List<Edge>();
        public List<Place> ChosenPlaces { get; } = new List<Place>();

        public void AddPlace(Place placeToAdd)
        {
            if (Places.Any(x => x.Id.Equals(placeToAdd.Id)))
            {
                throw new ArgumentException($"Id: {placeToAdd.Id} cannot be the same for two places");
            }

            if (Places.Contains(placeToAdd))
            {
                throw new ArgumentException($"You already add {placeToAdd}");
            }

            Places.Add(placeToAdd);
        }

        public void AddChosenPlace(Place placeToAdd)
        {
            ChosenPlaces.Add(placeToAdd);
        }

        public void AddEdge(string startID, string endID, string[] distance, string cost)
        {
            Place startPlace = Find(startID);
            Place endPlace = Find(endID);
            TimeSpan t = new TimeSpan(int.Parse(distance[0]), int.Parse(distance[1]), 0);

            Edges.Add(new Edge(startPlace, endPlace, t, double.Parse(cost)));
        }

        public IEnumerable<Edge> FindNeighborEdges(Place place)
             => Edges.FindAll(x => x.StartPlace.Equals(place));

        public IEnumerable<Place> GetCopyOfPlaces() => Places.GetRange(0, Places.Count);

        public IEnumerable<Place> GetCopyOfChosenPlaces() => ChosenPlaces.GetRange(0, ChosenPlaces.Count);

        private Place Find(string id)
        {
            Place? place = null;
            foreach (var v in Places)
            {
                if (v.Id.Equals(id))
                {
                    place = v;
                }
            }
            if (place == null)
            {
                throw new ArgumentException($"You cannot create edge with place with id:{id} that not exist");
            }

            return place;
        }

        public void print()
        {
            foreach (var a in Places)
            {
                Console.WriteLine($"{a.Id} \t {a.Name}");
            }

            Console.WriteLine("********choosen********");
            foreach (var a in ChosenPlaces)
            {
                Console.WriteLine($"{a.Id} \t {a.Name}");
            }

            Console.WriteLine("*******edges*********");
            foreach (var a in Edges)
            {
                Console.WriteLine($"{a.StartPlace.Id}\t{a.EndPlace.Id}\t{a.Distance.ToString()}\t{a.Cost}");
            }
        }
    }
}