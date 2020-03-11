using PathFinder.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Path
{
    public class Path
    {
        public List<Edge> Steps = new List<Edge>();
        public List<Edge> ChosenSteps = new List<Edge>();
        public Place StarPlace => Steps[0].StartPlace;
        public Place EndPlace => Steps[Count - 1].EndPlace;

        public TimeSpan TotalLength
        {
            get
            {
                TimeSpan totalLength = new TimeSpan();
                foreach (var step in Steps)
                {
                    totalLength += step.Distance;
                }

                return totalLength;
            }
        }

        public double Cost => Steps.Select(item => item.Cost).Sum();
        public int Count => Steps.Count;

        public void InsertEdge(Edge edgeToAdd, int positionInPath)
        {
            Steps.Insert(positionInPath, edgeToAdd);
        }
    }
}