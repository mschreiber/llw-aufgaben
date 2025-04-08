
using System.Data.SQLite;

const int LIMIT = 50;
const string CONNECTION_STRING = "Data Source=crossNumber.db";


// Prints a menu on the console
Console.WriteLine("Quersummen Statistik Programm\n");
Console.WriteLine("Funktionen:");
Console.WriteLine("\t1...Datei parsen");
Console.WriteLine("\t2...Statistik anzeigen\n");
Console.WriteLine("Wählen sie 1,2 jede andere Taste beendet das Programm");
var consoleKey = Console.ReadKey(true);

// Calls operation based on the users input
switch (consoleKey.KeyChar)
{
  case '1':
    PrepareDatabase();
    ParseFile();
    break;
  case '2':
    PrepareDatabase();
    ShowStatistic();
    break;
}

// Displays the statistic of all parsed files
void ShowStatistic()
{
  using (var connection = new SQLiteConnection(CONNECTION_STRING))
  {
    connection.Open();
    Console.WriteLine("Statistik:\n");
    PrintFastetLimit(connection);
    PrintAverages(connection);
    PrintFirstLineMin(connection);
  }
}

// Prints in which file the limit was reached
// with the minimum amount of numbers in line 1
// connection = the sql connection that should be used
// Caller is responsible for closing the connection
void PrintFirstLineMin(SQLiteConnection connection)
{
  Console.WriteLine("Erreichen des Limits in Zeile 1:");
  var command = connection.CreateCommand();
  command.CommandText = "select file_path, min(limit_reached_at) as max_limit_reached from files join lines on files.id = lines.file_id where lines.line_number=1 group by files.id order by max_limit_reached limit 1";
  using (var reader = command.ExecuteReader())
  {
    if (reader.Read())
    {
      var filePath = reader.GetString(0);
      var limitReachedAt = reader.GetInt32(1);
      Console.WriteLine($"\tDatei: {filePath}; Limit erreicht bei {limitReachedAt} Zeichen/Zahlen");
    }
  }
}

void PrintFastetLimit(SQLiteConnection connection)
{
  Console.WriteLine("Schnellstes Erreichen des Limits:");
  var command = connection.CreateCommand();
  command.CommandText = "select file_path, min(limit_reached_at) as min_limit_reached from files join lines on files.id = lines.file_id group by files.id order by min_limit_reached limit 1";
  using (var reader = command.ExecuteReader())
  {
    if (reader.Read())
    {
      var filePath = reader.GetString(0);
      var limitReachedAt = reader.GetInt32(1);
      Console.WriteLine($"\tDatei: {filePath}; Limit erreicht bei {limitReachedAt} Zeichen/Zahlen");
    }
  }
}

void PrintAverages(SQLiteConnection connection)
{
  Console.WriteLine("Durchschnitt pro Datei:");
  var command = connection.CreateCommand();
  command.CommandText = "select file_path, avg(limit_reached_at) as avg_limit_reached from files join lines on files.id = lines.file_id group by files.id order by avg_limit_reached";
  using (var reader = command.ExecuteReader())
  {
    while (reader.Read())
    {
      var filePath = reader.GetString(0);
      var limitReachedAt = reader.GetDouble(1).ToString("##.00");
      Console.WriteLine($"\tDatei: {filePath}; Durchschnitt: {limitReachedAt}");
    }
  }
}


void ParseFile()
{

  var fileName = GetFileName();
  var linesIterator = File.ReadLines(fileName);
  var lineCounter = 0;
  var lineWithSmallesCount = int.MaxValue;
  var smallestCount = int.MaxValue;

  using (var connection = new SQLiteConnection(CONNECTION_STRING))
  {
    connection.Open();
    var fileId = InsertFile(connection, fileName);
    foreach (var line in linesIterator)
    {
      lineCounter++;
      var crossNumberLimit = CalculateCrossNumberLimit(line);
      if (crossNumberLimit > 0 && crossNumberLimit < smallestCount)
      {
        lineWithSmallesCount = lineCounter;
        smallestCount = crossNumberLimit;
      }
      Console.WriteLine($"Zeile {lineCounter}: {crossNumberLimit}");
      InsertLine(connection, fileId, lineCounter, crossNumberLimit);
    }
    Console.WriteLine($"Zeile {lineWithSmallesCount} ist die Zeile, die am schnellsten die 50 erreicht hat");
  }
}

void InsertLine(SQLiteConnection connection, long fileId, int lineCounter, int crossNumberLimit)
{
  var command = connection.CreateCommand();
  command.CommandText = "insert into lines(file_id, line_number, limit_reached_at) values($fileId,$lineNumber,$reachedAt)";
  command.Parameters.AddWithValue("$fileId", fileId);
  command.Parameters.AddWithValue("$lineNumber", lineCounter);
  command.Parameters.AddWithValue("$reachedAt", crossNumberLimit);
  command.ExecuteNonQuery();
}

long InsertFile(SQLiteConnection connection, string fileName)
{
  var command = connection.CreateCommand();
  command.CommandText = "insert into files(file_path) values($filepath)";
  command.Parameters.AddWithValue("$filepath", fileName);
  return connection.LastInsertRowId;
}

// Calculates the cross number of one line
int CalculateCrossNumberLimit(string line)
{
  var crossNumber = 0;
  var count = 0;
  foreach (var number in line)
  {
    count++;
    crossNumber += short.Parse(number.ToString());
    if (crossNumber >= LIMIT)
    {
      return count;
    }
  }
  return -1;
}



// Get the file name either from the command line or 
// by asking the user to enter the path 
string GetFileName()
{
  Console.WriteLine();
  Console.WriteLine("Please specify the path to the file that should be read:");
  string? input = Console.ReadLine();
  while (input == null || input.Length == 0 || !File.Exists(input))
  {
    Console.WriteLine("Please specify a valid path!");
    input = Console.ReadLine();
  }
  return input;
}


// Check if the database contains the tables, if not create them
void PrepareDatabase()
{
  using (var connection = new SQLiteConnection(CONNECTION_STRING))
  {
    connection.Open();
    var createFilesTableCmd = connection.CreateCommand();
    createFilesTableCmd.CommandText = "create table if not exists files(id integer primary key autoincrement, file_path varchar(255))";
    createFilesTableCmd.ExecuteNonQuery();
    var createLinesTableCmd = connection.CreateCommand();
    createLinesTableCmd.CommandText = "create table if not exists lines(id integer primary key autoincrement, file_id integer, line_number, limit_reached_at integer, foreign key (file_id) references files(id))";
    createLinesTableCmd.ExecuteNonQuery();
  }
}
