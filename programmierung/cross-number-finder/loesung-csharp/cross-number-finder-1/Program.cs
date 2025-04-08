
const int LIMIT = 100;

var fileName = GetFileName();
var linesIterator = File.ReadLines(fileName);
var lineCounter = 0;

foreach (var line in linesIterator)
{
  lineCounter++;
  var crossNumber = CalculateCrossNumber(line);
  if (crossNumber > LIMIT)
  {
    Console.WriteLine($"Zeile {lineCounter}: {crossNumber}");
  }
}


// Calculates the cross number of one line
int CalculateCrossNumber(String line)
{
  var crossNumber = 0;
  foreach (var number in line)
  {
    crossNumber += short.Parse(number.ToString());
  }
  return crossNumber;
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