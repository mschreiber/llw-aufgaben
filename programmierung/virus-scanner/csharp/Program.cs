using Microsoft.Data.Sqlite;

namespace at.technikland.llw;


public class Programm
{
    static Dictionary<int, int> tops = new Dictionary<int, int>();

    public static void Main(string[] args)
    {
        // Input of different stuff
        string filename = AskFileName();
        int byte1 = AskInt("Geben sie das erstes Byte der Virenkennung ein, Enter bedeutet: 0xAC.", 0xac);
        int byte2 = AskInt("Geben sie das zweite Byte der Virenkennung ein, Enter bedeutet: 0xAE.", 0xae);
        int threashold = AskInt("Geben sie die Anzahl erlaubter Kennungen pro Datei ein. Enter bedeutet: 2", 2);

        // Search for viruses
        int count = PareseFileAndGetCount(filename, byte1, byte2);

        // Result
        PrintResult(filename, count, threashold, byte1, byte2);
        PrintTop5InFile();

        // Save to db
        SaveTop5ToDb(filename);

        // Print overall top values
        PrintTopOfDb();
        PrintFilesOfDb();
    }

    // Print the given message and ask the user for a number. 
    // returns the entered number.
    // Method retunrs only if the user entered a correct number (int32)
    private static int AskInt(string message, int defaultValue)
    {
        Console.WriteLine(message);
        Console.Write("Wert: ");
        string valueAsString = Console.In.ReadLine();
        int value = 0;
        if (String.IsNullOrEmpty(valueAsString))
        {
            return defaultValue;
        }
        while (!Int32.TryParse(valueAsString, out value))
        {
            Console.WriteLine("Ungültiger Wert!");
            Console.Write("Wert: ");
            valueAsString = Console.In.ReadLine();
        }
        return value;
    }

    // Checks the given file by reading a byte pair and checking if the 
    // pair contains 0xac and 0xae
    // Prints out kind of an indicator; "." for regular read, and "x" if 
    // the byte combination was found
    private static int PareseFileAndGetCount(string filename, int byte1, int byte2)
    {
        int currentByte = 0;
        int byteBefore = 0;
        int count = 0;
        Console.Write("Suche in Datei");
        using (FileStream fs = File.OpenRead(filename))
        {
            // Read one byte, store it in currentByte and loop till end of file
            while ((currentByte = fs.ReadByte()) != -1)
            {
                // compare last byte and current byte with the virus signature bytes
                // if it matches, increment the count
                if (byteBefore == byte1 && currentByte == byte2)
                {
                    Console.Write("x");
                    count++;
                }
                else
                {
                    Console.Write(".");
                }
                if (tops.ContainsKey(currentByte))
                {
                    tops[currentByte] = tops[currentByte] + 1;
                }
                else
                {
                    tops[currentByte] = 1;
                }
                // store the current byte in the byteBefore
                byteBefore = currentByte;
            }
        }
        Console.WriteLine("");
        return count;
    }

    // Prints the result if the file is potentially a virus or not including the count
    private static void PrintResult(string filename, int count, int threashold, int byte1, int byte2)
    {
        Console.WriteLine("===================");
        Console.WriteLine($"Analyse von Datei '{Path.GetFileName(filename)}' hat ergeben:");
        if (count > threashold)
        {
            Console.WriteLine($"\t-> Enthält sehr wahrscheinlich einen Virus! ({byte1};{byte2} Paar {count} mal gefunden)");
        }
        else
        {
            Console.WriteLine($"\t-> Datei ist ok! ({byte1};{byte2} Paar {count} mal gefunden)");
        }
        Console.WriteLine("===================");
    }

    private static void PrintTop5InFile()
    {
        Console.WriteLine("===================");
        Console.WriteLine("Top 5:");
        foreach (var item in tops.OrderByDescending((it) => it.Value).Take(5))
        {
            Console.WriteLine("Byte: " + item.Key.ToString("0X") + ": " + item.Value);
        }
        Console.WriteLine("===================");
    }

    private static void PrintTopOfDb()
    {
        Console.WriteLine("===============");
        Console.WriteLine("Top 5 von allen bisher untersuchten Dateien:");
        using (SqliteConnection connection = GetConnection())
        using (var command = new SqliteCommand("SELECT bytevalue, sum(countvalue) from topvalues group by bytevalue", connection))
        using (var reader = command.ExecuteReader()) {
            while (reader.Read())  // Iteriere über alle Zeilen
            {
                // Lese die Werte aus den entsprechenden Spalten
                string bytevalue = reader.GetString(0);  // 'bytevalue' als String
                int countvalueSum = reader.GetInt32(1);  // Summe von 'countvalue'

                // Ausgabe der Werte
                Console.WriteLine($"{bytevalue} : {countvalueSum}");
            }
        }
    }

    private static void PrintFilesOfDb()
    {
        Console.WriteLine("===============");
        Console.WriteLine("Bisher untersuchten Dateien:");
        using (SqliteConnection connection = GetConnection())
        using (var command = new SqliteCommand("SELECT distinct filename from topvalues", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string filename = reader.GetString(0);
                Console.WriteLine(filename);
            }
        }
    }


    // Ask the user for a path to a file, that should be scanned. 
    // This method will only return if the user specifies a valid path
    private static string AskFileName()
    {
        Console.WriteLine("");
        Console.WriteLine("======== Viren Scanner ===========");
        Console.Write("Geben sie einen Dateipfad an:");
        string filename = Console.In.ReadLine();
        while (!File.Exists(filename))
        {
            Console.WriteLine("Angegebener Pfad ist ungültig, Datei nicht gefunden.");
            Console.WriteLine("Bitte geben sie einen Dateipfad an:");
            filename = Console.In.ReadLine();
        }
        return filename;
    }


    private static SqliteConnection GetConnection()
    {
        // Erstelle eine Verbindung zur SQLite-Datenbank
        SqliteConnection connection = new SqliteConnection("Data Source=TopValues.db;");
        connection.Open();
        string createTableQuery = @"
                    CREATE TABLE if not exists topvalues (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        byteValue INTEGER NOT NULL,
                        countValue INTEGER NOT NULL,
                        fileName TEXT
                    );";
        using (var createCmd = new SqliteCommand(createTableQuery, connection))
        {
            createCmd.ExecuteNonQuery();
        }



        // Gib die offene Verbindung zurück
        return connection;
    }

    private static void SaveTop5ToDb(string filename)
    {
        using (SqliteConnection connection = GetConnection())
        {
            foreach (var item in tops.OrderByDescending((it) => it.Value).Take(5))
            {
                string query = "INSERT INTO topvalues (byteValue, countValue, filename) VALUES (@bytevalue, @countvalue, @filename)";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bytevalue", item.Key);
                    command.Parameters.AddWithValue("@countvalue", item.Value);
                    command.Parameters.AddWithValue("@filename", filename);

                    command.ExecuteNonQuery();
                }
            }
        }
        }
   

}

