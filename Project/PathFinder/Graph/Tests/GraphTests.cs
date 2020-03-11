using NUnit.Framework;
using System;

namespace PathFinder.Graph.Tests
{
    [TestFixture]
    internal class GraphTests
    {
        private readonly Graph graph = new Graph();

        [Test]
        public void AddPlaceTest()
        {
            Place place = new Place("A", "aa");
            graph.AddPlace(place);
            Assert.Throws<ArgumentException>(() => graph.AddPlace(place));
            Place place2 = new Place("A", "ab");
            Assert.Throws<ArgumentException>(() => graph.AddPlace(place2));
        }

        [Test]
        public void AddEdgeTest()
        {
            Assert.Throws<ArgumentException>(() => graph.AddEdge("A", "B", new string[] { "2", "0" }, "1"));
        }
    }
}