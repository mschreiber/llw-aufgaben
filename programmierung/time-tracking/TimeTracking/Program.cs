namespace TimeTracking;

class Program
{
  static void Main(string[] args)
  {
    //TODO: Dependency Injection
    PersistenceService persistenceService = new("timetracking.db");
    StatisticService statisticService = new(persistenceService);
    ParsingService parsingService = new(persistenceService);

    Console.WriteLine("Zeittracking-Analyse-Tool");
    Console.WriteLine("----------------------------");
    Console.WriteLine("1... Parse und Persistieren der Daten");
    Console.WriteLine("2... Statistik über alle gelesenen Daten anzeigen");
    Console.WriteLine("Bitte wählen Sie eine Option (1 oder 2):");
    string option = Console.ReadLine();
    switch (option)
    {
      case "1":
        parsingService.ParseFiles();
        break;
      case "2":
        statisticService.ShowStatistics();
        break;
      default:
        Console.WriteLine("Ungültige Option. Bitte wählen Sie 1 oder 2.");
        break;
    }
  }
}