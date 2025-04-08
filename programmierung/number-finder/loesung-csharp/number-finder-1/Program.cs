
const string NUMER_TO_MATCH = "777";


var fileName = getFileName();
var linesIterator = File.ReadLines(fileName);
var count = 0;
var lineNumber = 0;
foreach (var line in linesIterator)
{
  lineNumber++;
  count += getCount(line);
}
Console.WriteLine($"In der Datei \"{fileName}\" kommt die Zahl {count} mal vor!");


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