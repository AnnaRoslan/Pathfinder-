using System;

namespace PathFinder.Graph
{
    public class Edge
    {
        public Place StartPlace { get; }
        public Place EndPlace { get; }
        private TimeSpan _distance;
        private double _cost;

        public double Cost
        {
            get => _cost;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("You cannot add negative cost");
                }

                _cost = value;
            }
        }

        public TimeSpan Distance
        {
            get => _distance;
            set
            {
                if (value.Hours < 0 || value.Minutes < 0)
                {
                    throw new ArgumentOutOfRangeException("You cannot add negative distance");
                }
                _distance = value;
            }
        }

        public Edge(Place startPlace, Place endPlace, TimeSpan distance, double cost)
        {
            StartPlace = startPlace;
            EndPlace = endPlace;
            Distance = distance;
            Cost = cost;
        }

        public Edge CloneAndReverse() => new Edge(EndPlace, StartPlace, Distance, Cost);
    }
}