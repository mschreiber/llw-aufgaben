namespace TimeTracking;

/// <summary>
/// The ParsingService class is responsible for reading the time tracking data from the specified text files,
/// extracting the relevant information, and saving the results to the database using the PersistenceService.
/// </summary>
public class ParsingService
{
  private readonly PersistenceService PersistenceService;
  public ParsingService(PersistenceService persistenceService)
  {
    this.PersistenceService = persistenceService;
  }

  /// <summary>
  /// This method prompts the user to enter the path to the directory containing the *.txt files,
  /// processes each file, extracts the employee and project time information, 
  /// and saves the results to the database.
  /// </summary>
  public void ParseFiles()
  {

    Console.WriteLine("----------------------------");
    Console.Write("Geben Sie den Pfad zu den *.txt Dateien ein:");
    string path = Console.ReadLine();
    if (Directory.Exists(path))
    {
      string[] files = Directory.GetFiles(path, "*.txt");
      if (files.Length == 0)
      {
        Console.WriteLine("Keine *.txt Dateien gefunden.");
        return;
      }

      foreach (string file in files)
      {
        Console.WriteLine($"Verarbeite Datei: {file}");
        Employee employee = ParseFileAndGetResults(file);
        PrintResults(employee);
        PersistenceService.SaveResults(employee);
      }
    }
    else
    {
      Console.WriteLine("Der angegebene Pfad existiert nicht oder ist kein Verzeichnis.");
    }
  }

  /// <summary>
  /// This method reads a given file, extracts the employee information and project times, 
  /// and returns an Employee object with the results.
  /// <param name="filePath">Path to the file to parse</param>
  /// <returns>An Employee object with the extracted information and calculated project times</returns>
  /// </summary>
  public Employee ParseFileAndGetResults(string filePath)
  {
    Employee employee;
    using (StreamReader reader = new StreamReader(filePath))
    {
      string line = reader.ReadLine();
      employee = CreateEmployee(line);
      while ((line = reader.ReadLine()) != null)
      {
        ExtractProjectTime(line, employee);
      }
    }
    return employee;
  }

  private static Employee CreateEmployee(string line)
  {
    string[] parts = line.Split('|', ':');
    int id = int.Parse(parts[1].Trim());
    string name = parts[3].Trim();
    Employee employee = new Employee(id, name);
    return employee;
  }
  private void ExtractProjectTime(string line, Employee employee)
  {
    string[] parts = line.Split('|', ':');
    string projectName = parts[2].Trim();
    string startTime = parts[4].Trim() + ":" + parts[5].Trim();
    string endTime = parts[7].Trim() + ":" + parts[8].Trim();
    int timeSpent = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime)).Minutes;
    if (employee.ProjectTimes.ContainsKey(projectName))
    {
      employee.ProjectTimes[projectName] += timeSpent;
    }
    else
    {
      employee.ProjectTimes[projectName] = timeSpent;
    }
  }

  private void PrintResults(Employee employee)
  {
    Console.WriteLine("----------------------------");
    Console.WriteLine($"Mitarbeiter: {employee.Name} (ID: {employee.Id})");
    Console.WriteLine("Projektzeiten:");
    foreach (var project in employee.ProjectTimes)
    {
      Console.WriteLine($"- {project.Key}: {project.Value} Minuten");
    }
    int totalTime = employee.ProjectTimes.Values.Sum();
    Console.WriteLine($"Gesamtzeit: {totalTime.ToMinutesString()}");
    Console.WriteLine("----------------------------");

    Console.WriteLine("Top 3 Projekte:");
    foreach (var project in employee.ProjectTimes.OrderByDescending(p => p.Value).Take(3))
    {
      Console.WriteLine($"- {project.Key}: {project.Value.ToMinutesString()}");
    }
    Console.WriteLine("----------------------------");
  }
}

