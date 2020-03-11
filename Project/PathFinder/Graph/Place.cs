namespace PathFinder.Graph
{
    public class Place
    {
        public string Id { get; }
        public string Name { get; }

        public Place(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public bool Equals(Place place)
        {
            return Id.Equals(place.Id);
        }
    }
}