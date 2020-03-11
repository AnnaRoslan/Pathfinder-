using System.IO;

namespace PathFinder.ReadFile
{
    internal static class SaveFile
    {
        public static void Save(Graph.Graph graph, Path.Path path)
        {
            using StreamWriter outputFile = new StreamWriter("../../../../../Result/result.txt");
            foreach (var pathPart in path.Steps)
            {
                if (graph.ChosenPlaces.Exists(item => item.Id.Equals(pathPart.StartPlace.Id)))
                {
                    outputFile.Write($"{pathPart.StartPlace.Name}\n->");
                }
            }
            outputFile.WriteLine(path.EndPlace.Name);
            outputFile.Write("Czas:");
            if (!path.TotalLength.Days.Equals(0))
            {
                outputFile.Write($"{path.TotalLength.Days} dni ");
            }
            outputFile.WriteLine($" {path.TotalLength.Hours} godzin {path.TotalLength.Minutes} minut");
            outputFile.WriteLine($"Koszt: {path.Cost} zł");
        }
    }
}