using NUnit.Framework;
using NUnit.Framework.Internal;
using PathFinder.Graph;
using System;
using System.Collections.Generic;
using System.IO;

namespace PathFinder.ReadFile.Tests
{
    [TestFixture]
    internal class SaveFile
    {
        private static readonly List<Place> places = new List<Place>()
        {
            new Place("A","aa"),
            new Place("B","bb"),
            new Place("C","cc"),
            new Place("D","dd")
        };

        private Graph.Graph _graph = new Graph.Graph();

        private readonly Path.Path path = new Path.Path()
        {
            Steps =
            {
                new Edge(places[0], places[1], new TimeSpan(2, 0, 0), 2),
                new Edge(places[1], places[2], new TimeSpan(2, 0, 0), 2),
                new Edge(places[2], places[3], new TimeSpan(2, 0, 0), 2),
                new Edge(places[3], places[0], new TimeSpan(2, 0, 0), 2),
            }
        };

        [Test]
        public void TestSave()
        {
            _graph.ChosenPlaces.AddRange(places.GetRange(0, 2));
            ReadFile.SaveFile.Save(_graph, path);
            string readProper = "aa\n->bb\n->aa\r\nCzas: 8 godzin 0 minut\r\nKoszt: 8 zł\r\n";
            string readSaveFile = File.ReadAllText("../../../../../Result/result.txt");
            Assert.AreEqual(readProper, readSaveFile);
        }
    }
}