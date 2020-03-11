using NUnit.Framework;
using PathFinder.Graph;
using System.Collections.Generic;

namespace PathFinder.Finder.Test
{
    [TestFixture]
    internal class PermutationTest
    {
        private readonly List<Place> _places = new List<Place>()
        {
            new Place("A", "nameA"),
            new Place("B", "nameB"),
            new Place("C", "nameC"),
            new Place("D", "nameD"),
        };

        [Test]
        public void findTest()
        {
            IFinder finder = new Permutation(CreateGraph());
            Path.Path path = finder.FindPath(_places[0]);
            Assert.AreEqual("A", path.Steps[0].StartPlace.Id);
            Assert.AreEqual("B", path.Steps[1].StartPlace.Id);
            Assert.AreEqual("C", path.Steps[2].StartPlace.Id);
            Assert.AreEqual("D", path.Steps[3].StartPlace.Id);
            Assert.AreEqual("A", path.Steps[3].EndPlace.Id);
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
            graph.AddEdge("D", "A", new[] { "1", "00" }, "0");
            return graph;
        }
    }
}