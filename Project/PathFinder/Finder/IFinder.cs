using PathFinder.Graph;

namespace PathFinder.Finder
{
    internal interface IFinder
    {
        Path.Path FindPath(Place startPlace);
    }
}