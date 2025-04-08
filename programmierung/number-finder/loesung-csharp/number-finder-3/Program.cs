
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

const string NUMER_TO_MATCH = "777";


var fileName = getFileName();

using (var connection = new SQLiteConnection("Data Source=NumberFinder.db"))
{
  connection.Open();
  checkDatabase(connection);
  var fileId = addFileToDb(connection, fileName);
  var linesIterator = File.ReadLines(fileName);
  var count = 0;
  var lineNumber = 0;
  foreach (var line in linesIterator)
  {
    lineNumber++;
    var countInLine = getCount(line);
    if (countInLine > 0)
    {
      Console.WriteLine($"Zeile {lineNumber}: {countInLine}x");
      addLineToDb(connection, fileId, lineNumber, countInLine);
    }
    count += countInLine;
  }
  Console.WriteLine($"Summe: {count}");
}

void addLineToDb(SQLiteConnection connection, long fileId, int lineNumber, int countInLine)
{
  var command = connection.CreateCommand();
  command.CommandText = "insert into lines(file_id, line_number, line_count) values(:fId, :lNr, :lCnt)";
  command.Parameters.AddWithValue(":fId", fileId);
  command.Parameters.AddWithValue(":lNr", lineNumber);
  command.Parameters.AddWithValue(":lCnt", countInLine);
  command.ExecuteNonQuery();
}

long addFileToDb(SQLiteConnection connection, string fileName)
{
  var command = connection.CreateCommand();
  command.CommandText = "insert into files(file_path) values(:filePath); ";
  command.Parameters.AddWithValue(":filePath", fileName);
  command.ExecuteNonQuery();
  return connection.LastInsertRowId;
}

// Searches in one line fo NUMBER_TO_MATCH and return the count
int getCount(string line)
{
  int count = 0;
  string[] chunks = line.Split(" ");
  foreach (string chunk in chunks)
  {
    if (NUMER_TO_MATCH == chunk)
    {
      count++;
    }
  }
  return count;
}


// Get the file name either from the command line or 
// by asking the user to enter the path 
string getFileName()
{

  // if a file was specified as command line argument, use that
  if (args.Length == 1 && File.Exists(args[0]))
  {
    return args[0];
  }

  // No file was specified in the command line, ask the user for a path
  Console.WriteLine();
  Console.WriteLine("Geben sie den Pfad zur Datei an, die geladen werden soll:");
  string? input = Console.ReadLine();
  while (input == null || input.Length == 0 || !File.Exists(input))
  {
    Console.WriteLine("Ungültiger Pfad! Bitte geben sie einen gültigen Pfad ein z.B. C:/data/file1.txt");
    input = Console.ReadLine();
  }
  return input;
}

void checkDatabase(SQLiteConnection connection)
{
  checkFilesTable(connection);
  checkLinesTable(connection);
}

void checkLinesTable(SQLiteConnection connection)
{
  var command = connection.CreateCommand();
  command.CommandText = "create table if not exists lines(id integer primary key autoincrement, file_id integer, line_number integer, line_count integer, foreign key(file_id) references files(id))";
  command.ExecuteNonQuery();
}

void checkFilesTable(SQLiteConnection connection)
{
  var command = connection.CreateCommand();
  command.CommandText = "create table if not exists files(id integer primary key autoincrement, file_path varchar(255))";
  command.ExecuteNonQuery();
}

