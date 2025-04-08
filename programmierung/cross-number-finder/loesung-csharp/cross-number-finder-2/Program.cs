
const int LIMIT = 50;

var fileName = GetFileName();
var linesIterator = File.ReadLines(fileName);
var lineCounter = 0;
var lineWithSmallesCount = Int32.MaxValue;
var smallestCount = Int32.MaxValue;

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
}
Console.WriteLine($"Zeile {lineWithSmallesCount} ist die Zeile, die am schnellsten die 50 erreicht hat");


// Calculates the cross number of one line
int CalculateCrossNumberLimit(String line)
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

  // if a file was specified as command line argument, use that
  if (args.Length == 1 && File.Exists(args[0]))
  {
    return args[0];
  }

  // No file was specified in the command line, ask the user for a path
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