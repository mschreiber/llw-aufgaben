namespace TimeTracking;

/// <summary>
/// The StatisticService class is responsible for calculating and displaying statistics 
/// based on the data loaded from the database.
/// </summary>
public class StatisticService
{
  private readonly PersistenceService persistenceService;

  public StatisticService(PersistenceService persistenceService)
  {
    this.persistenceService = persistenceService;
  }

  public void ShowStatistics()
  {
    Console.WriteLine("Statistik über alle gelesenen Daten:");
    List<Employee> employees = persistenceService.LoadEmployees();
    ShowTimesPerEmployee(employees);
    ShowTimePerProject(employees);
    ShowTotalTimes(employees);
  }

  private void ShowTotalTimes(List<Employee> employees)
  {
    Console.WriteLine("----------------------------");
    int totalTime = employees.Sum(e => e.ProjectTimes.Values.Sum());
    Console.WriteLine($"Gesamtzeit aller Mitarbeiter: {totalTime.ToMinutesString()}");
    Console.WriteLine($"Durchschnittliche Zeit pro Mitarbeiter: {(employees.Count > 0 ? totalTime / employees.Count : 0).ToMinutesString()} ");
  }
  private void ShowTimesPerEmployee(List<Employee> employees)
  {
    Console.WriteLine("----------------------------");
    Console.WriteLine("Zeit pro Mitarbeiter:");
    foreach (var employee in employees)
    {
      int totalTime = employee.ProjectTimes.Values.Sum();
      Console.WriteLine($"- {employee.Name} (ID: {employee.Id}): {totalTime.ToMinutesString()}");
    }
  }
  private void ShowTimePerProject(List<Employee> employees)
  {
    Console.WriteLine("----------------------------");
    Dictionary<string, int> projectTimes = new();
    foreach (var employee in employees)
    {
      foreach (var project in employee.ProjectTimes)
      {
        if (projectTimes.ContainsKey(project.Key))
        {
          projectTimes[project.Key] += project.Value;
        }
        else
        {
          projectTimes[project.Key] = project.Value;
        }
      }
    }
    Console.WriteLine("Zeit pro Projekt:");
    foreach (var project in projectTimes)
    {
      Console.WriteLine($"- {project.Key}: {project.Value.ToMinutesString()}");
    }
  }
}