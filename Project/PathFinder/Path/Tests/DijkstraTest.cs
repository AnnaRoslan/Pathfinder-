using NUnit.Framework;
using PathFinder.Graph;
using System.Collections.Generic;

namespace PathFinder.Path.Tests
{
    [TestFixture]
    internal class DijkstraTest
    {
        private readonly List<Place> _places = new List<Place>()
        {
            new Place("A", "nameA"),
            new Place("B", "nameB"),
            new Place("C", "nameC"),
            new Place("D", "nameD"),
        };

        [Test]
        public void FindPathTest()
        {
            Graph.Graph graph = CreateGraph();
            Dijkstra dijkstra = new Dijkstra(graph);
            Path path = dijkstra.FindPath(_places[0], _places[3]);
            Assert.AreEqual(3, path.TotalLength.Hours);
            Assert.AreEqual("A", path.Steps[0].StartPlace.Id);
            Assert.AreEqual("B", path.Steps[1].StartPlace.Id);
            Assert.AreEqual("C", path.Steps[2].StartPlace.Id);
            Assert.AreEqual("D", path.Steps[2].EndPlace.Id);
        }

        [Test]
        public void FindNoPathTest()
        {
            Graph.Graph graph = CreateGraph();
            Dijkstra dijkstra = new Dijkstra(graph);
            Path path = dijkstra.FindPath(_places[0], _places[0]);
            Assert.Null(path);
        }

        private Graph.Graph CreateGraph()
        {
            Graph.Graph graph = new Graph.Graph();
            graph.Places.AddRange(_places);
            graph.ChosenPlaces.AddRange(_places);
            graph.AddEdge("A", "B", new[] { "1", "00" }, "0");
            graph.AddEdge("A", "C", new[] { "4", "00" }, "0");
            graph.AddEdge("B", "C", new[] { "1", "00" }, "0");
            graph.AddEdge("C", "D", new[] { "1", "00" }, "0");
            return graph;
        }
    }
}