using NUnit.Framework;
using PathFinder.Graph;
using System.Collections.Generic;

namespace PathFinder.Path.Tests
{
    [TestFixture]
    internal class AllPathTest
    {
        private readonly List<Place> _places = new List<Place>()
        {
            new Place("A", "nameA"),
            new Place("B", "nameB"),
            new Place("C", "nameC"),
        };

        [Test]
        public void AllpathsTest()
        {
            AllPaths allpath = new AllPaths(CreateGraph());
            Assert.AreEqual(9, allpath.AllPathsList.Count);
            Assert.True(allpath.AllPathsList.ContainsKey("A|A"));
            Assert.True(allpath.AllPathsList.ContainsKey("A|B"));
            Assert.True(allpath.AllPathsList.ContainsKey("A|C"));
            Assert.True(allpath.AllPathsList.ContainsKey("B|B"));
            Assert.True(allpath.AllPathsList.ContainsKey("B|C"));
            Assert.True(allpath.AllPathsList.ContainsKey("B|A"));
            Assert.True(allpath.AllPathsList.ContainsKey("C|C"));
            Assert.True(allpath.AllPathsList.ContainsKey("C|A"));
            Assert.True(allpath.AllPathsList.ContainsKey("C|B"));
        }

        private Graph.Graph CreateGraph()
        {
            Graph.Graph graph = new Graph.Graph();
            graph.Places.AddRange(_places);
            graph.ChosenPlaces.AddRange(_places);
            return graph;
        }
    }
}