using Microsoft.Data.Sqlite;

namespace TimeTracking;

//TODO: Das hier ist keine schöne 3te Normalform, aber für die Zwecke der Aufgabe reicht es aus. 
//      In einer echten Anwendung würde ich wahrscheinlich eine komplexere Datenbankstruktur mit 
//      mehreren Tabellen und Beziehungen verwenden.  
// This service is responsible for saving the results of the parsing process to a SQLite database and
// for loading the data back from the database when needed.
public class PersistenceService
{
  private readonly string dbPath;

  public PersistenceService(string dbPath)
  {
    this.dbPath = dbPath;
  }


  /// <summary>
  /// Saves the employee data to the SQLite database. 
  /// If the database or the required table does not exist, it will be created automatically.
  /// </summary>
  /// <param name="employee"></param>
  public void SaveResults(Employee employee)
  {
    using SqliteConnection connection = new($"Data Source={dbPath}");
    connection.Open();
    CreateTableIfNotExists(connection);
    InsertIntoDatabase(connection, employee);
  }

  /// <summary>
  /// Loads the employee data from the SQLite database.
  /// </summary>
  /// <returns>A list of Employee objects with the loaded data.</returns>

  public List<Employee> LoadEmployees()
  {
    List<Employee> employees = new();
    using SqliteConnection connection = new($"Data Source={dbPath}");
    connection.Open();
    string selectQuery = "SELECT EmployeeId, EmployeeName, ProjectName, TimeSpent FROM EmployeeProjectTimes";
    using SqliteCommand command = new(selectQuery, connection);
    using SqliteDataReader reader = command.ExecuteReader();
    while (reader.Read())
    {
      int id = reader.GetInt32(0);
      string name = reader.GetString(1);
      string projectName = reader.GetString(2);
      int timeSpent = reader.GetInt32(3);

      Employee employee = employees.FirstOrDefault(e => e.Id == id);
      if (employee == null)
      {
        employee = new Employee(id, name);
        employees.Add(employee);
      }
      if (employee.ProjectTimes.ContainsKey(projectName))
      {
        employee.ProjectTimes[projectName] += timeSpent;
      }
      else
      {
        employee.ProjectTimes[projectName] = timeSpent;
      }
    }
    return employees;
  }


  private void InsertIntoDatabase(SqliteConnection connection, Employee employee)
  {
    foreach (var project in employee.ProjectTimes)
    {
      string insertQuery = @"
          INSERT INTO EmployeeProjectTimes (EmployeeId, EmployeeName, ProjectName, TimeSpent)
          VALUES (@EmployeeId, @EmployeeName, @ProjectName, @TimeSpent)";
      using SqliteCommand command = new(insertQuery, connection);
      command.Parameters.AddWithValue("@EmployeeId", employee.Id);
      command.Parameters.AddWithValue("@EmployeeName", employee.Name);
      command.Parameters.AddWithValue("@ProjectName", project.Key);
      command.Parameters.AddWithValue("@TimeSpent", project.Value);
      command.ExecuteNonQuery();
    }
  }

  private static void CreateTableIfNotExists(SqliteConnection connection)
  {
    string createTableQuery = @"
        CREATE TABLE IF NOT EXISTS EmployeeProjectTimes (
          Id INTEGER PRIMARY KEY AUTOINCREMENT,
          EmployeeId INTEGER,
          EmployeeName TEXT,
          ProjectName TEXT,
          TimeSpent INTEGER
        )";
    using SqliteCommand command = new(createTableQuery, connection);
    command.ExecuteNonQuery();
  }
}