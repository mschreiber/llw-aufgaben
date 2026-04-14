using Microsoft.Data.Sqlite;

namespace Logbook;

public class Programm
{

  private const decimal KM_PRICE_CAR = 0.42M;
  private const decimal KM_PRICE_MOTORCYCLE = 0.24M;

  public static void Main(string[] args)
  {

    Console.WriteLine("------------------------------");
    Console.WriteLine("Fahrtenbuch - App");
    Console.WriteLine("------------------------------");
    Console.WriteLine("1 ... Fahrtendatei einlesen");
    Console.WriteLine("2 ... Auswertung über alles anzeigen");
    Console.WriteLine("Geben Sie Ihre Auswahl ein:");
    string? eingabe = Console.ReadLine();
    switch (eingabe)
    {
      case "1":
        ParseFiles();
        break;
      case "2":
        ShowStatistics();
        break;
      default:
        Console.WriteLine("Ungültige Auswahl!");
        break;
    }
  }
  // Asks the user for a file path, reads the file, processes the data and persists it.
  private static void ParseFiles()
  {
    try
    {
      Console.WriteLine("Fahrtendatei einlesen");
      Console.WriteLine("Geben Sie den Pfad zur Fahrtendatei ein:");
      string? pfad = Console.ReadLine();
      if (string.IsNullOrEmpty(pfad))
      {
        Console.WriteLine("Ungültiger Pfad!");
        return;
      }
      // Two dictionaries to store the data for cars and motorcycles (Date as key, Price as value)
      Dictionary<string, decimal> carData = new();
      Dictionary<string, decimal> motorcycleData = new();
      using StreamReader reader = new StreamReader(pfad);
      string? line = reader.ReadLine(); // Skip headline
      while ((line = reader.ReadLine()) != null)
      {
        ParseLine(line, carData, motorcycleData);
      }
      Persist(carData, motorcycleData);
      Console.WriteLine($"PKW-Kilometer-Geld:\t\t{carData.Values.Sum(),10}");
      Console.WriteLine($"Motorrad-Kilometer-Geld:\t{motorcycleData.Values.Sum(),10}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Fehler beim Einlesen und Speichern der Fahrtendatei: {ex.Message}");
    }
  }

  // Parsing one line and store the values in the dictionaries.
  private static void ParseLine(string line, Dictionary<string, decimal> carData, Dictionary<string, decimal> motorcycleData)
  {
    string[] parts = line.Split(";");
    string date = parts[0];
    string startKm = parts[1];
    string endKm = parts[2];
    string type = parts[3];
    bool isPrivate = parts[4].ToLower().Contains("privat");
    // private? > do not store it
    if (isPrivate)
    {
      return;
    }
    int distance = int.Parse(endKm) - int.Parse(startKm);
    if (type == "PKW")
    {
      carData.TryGetValue(date, out decimal value);
      value = value + distance * KM_PRICE_CAR;
      carData[date] = value;
    }
    if (type == "Motorrad")
    {
      motorcycleData.TryGetValue(date, out decimal value);
      value = value + distance * KM_PRICE_MOTORCYCLE;
      motorcycleData[date] = value;
    }
  }

  // Persist the data in the database.
  private static void Persist(Dictionary<string, decimal> carData, Dictionary<string, decimal> motorcycleData)
  {
    using SqliteConnection connection = new SqliteConnection("Data Source=milageAllowance.db");
    connection.Open();

    // Create table if
    using SqliteCommand createCommand = connection.CreateCommand();
    createCommand.CommandText = "CREATE TABLE IF NOT EXISTS data(id integer primary key autoincrement, kmDate date, kmPrice decimal, type text)";
    createCommand.ExecuteNonQuery();

    // Delete the old data for the given date and type
    SqliteCommand deleteCommand = connection.CreateCommand();
    deleteCommand.CommandText = "DELETE FROM data where type = @type and kmDate = @date";
    SqliteParameter deleteTypeParameter = deleteCommand.Parameters.Add("@type", SqliteType.Text);
    SqliteParameter deleteDateParameter = deleteCommand.Parameters.Add("@date", SqliteType.Text);
    SqliteCommand insertCommand = connection.CreateCommand();

    // Insert the new data
    insertCommand.CommandText = $"INSERT INTO data(kmDate, kmPrice, type) VALUES(@date, @price, @type)";
    SqliteParameter insertDateParameter = insertCommand.Parameters.Add("@date", SqliteType.Text);
    SqliteParameter insertPriceParameter = insertCommand.Parameters.Add("@price", SqliteType.Real);
    SqliteParameter insertTypeParameter = insertCommand.Parameters.Add("@type", SqliteType.Text);

    foreach (KeyValuePair<string, decimal> item in carData)
    {
      deleteTypeParameter.Value = "PKW";
      deleteDateParameter.Value = item.Key;
      deleteCommand.ExecuteNonQuery();
      insertDateParameter.Value = item.Key;
      insertPriceParameter.Value = item.Value;
      insertTypeParameter.Value = "PKW";
      insertCommand.ExecuteNonQuery();
    }
    foreach (KeyValuePair<string, decimal> item in motorcycleData)
    {
      deleteTypeParameter.Value = "Motorrad";
      deleteDateParameter.Value = item.Key;
      deleteCommand.ExecuteNonQuery();
      insertDateParameter.Value = item.Key;
      insertPriceParameter.Value = item.Value;
      insertTypeParameter.Value = "Motorrad";
      insertCommand.ExecuteNonQuery();
    }
  }

  // Prints the statistics for a given data range.
  private static void ShowStatistics()
  {
    try
    {
      // Ask the user for a data range
      Console.WriteLine("Startdatum eingeben (yyyy-MM-dd):");
      string? startDate = Console.ReadLine();
      Console.WriteLine("Enddatum eingeben (yyyy-MM-dd):");
      string? endDate = Console.ReadLine();

      // Query the database
      using SqliteConnection connection = new SqliteConnection("Data Source=milageAllowance.db");
      connection.Open();
      using SqliteCommand command = connection.CreateCommand();
      command.CommandText = "SELECT SUM(kmPrice) AS kmPrice FROM data WHERE kmDate BETWEEN @startDate AND @endDate and type = @type";
      command.Parameters.Add("@startDate", SqliteType.Text).Value = startDate;
      command.Parameters.Add("@endDate", SqliteType.Text).Value = endDate;

      // Car data query
      command.Parameters.Add("@type", SqliteType.Text).Value = "PKW";
      using SqliteDataReader reader = command.ExecuteReader();
      bool hasCarData = reader.Read();
      if (hasCarData)
      {
        decimal carPrice = reader.GetDecimal(0);
        Console.WriteLine($"PKW-Kilometer-Geld:\t\t{carPrice,10}");
      }
      else
      {
        Console.WriteLine("Keine PKW-Daten gefunden!");
      }
      reader.Close();

      // Motorcycle data query
      command.Parameters["@type"].Value = "Motorrad";
      using SqliteDataReader motorcycleReader = command.ExecuteReader();
      bool hasMotorcycleData = motorcycleReader.Read();
      if (hasMotorcycleData)
      {
        decimal motorcyclePrice = motorcycleReader.GetDecimal(0);
        Console.WriteLine($"Motorrad-Kilometer-Geld:\t{motorcyclePrice,10}");
      }
      else
      {
        Console.WriteLine("Keine Motorrad-Daten gefunden!");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Fehler bei der Abfrage der Statistik: {ex.Message}");
    }
  }

}