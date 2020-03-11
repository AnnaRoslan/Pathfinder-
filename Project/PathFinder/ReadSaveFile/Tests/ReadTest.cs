using NUnit.Framework;
using System.IO;

namespace PathFinder.ReadFile
{
    [TestFixture]
    internal class ReadTests
    {
        private static readonly Graph.Graph _graph = new Graph.Graph();

        [Test]
        public void ReadTest0()
        {
            string nonexist = "/nonexist";
            Assert.Throws<FileNotFoundException>(() => ReadData.Read(nonexist, _graph));
        }

        [Test]
        public void ReadTest1()

        {
            Assert.Throws<IOException>(() => ReadData.Read("../../../ReadSaveFile/Tests/data1.txt", _graph));
        }

        [Test]
        public void ReadTest2()
        {
            Assert.Throws<IOException>(() => ReadData.Read("../../../ReadSaveFile/Tests/data2.txt", _graph));
        }
    }
}