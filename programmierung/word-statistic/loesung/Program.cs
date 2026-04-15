using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace WordStatistic
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Bitte geben Sie den Pfad zur UTF-8 Textdatei ein:");
      string filePath = Console.ReadLine();
      Console.WriteLine("Präfix? [Enter = kein Präfix]:");
      string prefix = Console.ReadLine().Trim(); //TODO: Regex for alphanumeric only
      Console.WriteLine("Postfix? [Enter = kein Postfix]:");
      string postfix = Console.ReadLine().Trim(); //TODO: Regex for alphanumeric only


      try
      {
        if (File.Exists(filePath))
        {
          Statistic statistic = HandleFile(filePath, prefix, postfix);
          PrintResults(statistic);
          SaveStatistic(statistic);
        }
        else
        {
          Console.WriteLine("Fehler: Datei wurde nicht gefunden.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
      }
    }

    static Statistic HandleFile(string path, string prefix, string postfix)
    {
      int wordCount = 0;
      Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
      IEnumerable<string> lines = File.ReadLines(path, Encoding.UTF8);
      foreach (var line in lines)
      {
        MatchCollection matches = Regex.Matches(line, $@"{prefix}([a-zA-Z0-9]+){postfix}");
        AddWordsToDictionary(wordFrequency, matches);
        wordCount += matches.Count;
      }
      List<KeyValuePair<string, int>> sortedKeyValues = wordFrequency.OrderByDescending(kvp => kvp.Value).ToList();
      List<KeyValuePair<string, int>> top10Words = sortedKeyValues.Take(10).ToList();
      return new Statistic
      {
        Path = path,
        Count = wordCount,
        Top10Words = top10Words
      };
    }

    private static void PrintResults(Statistic statistic)
    {
      Console.WriteLine("-----------------------------------------");
      Console.WriteLine($"Analyse der Datei: {Path.GetFileName(statistic.Path)}");
      Console.WriteLine($"Gesamtanzahl der Wörter: {statistic.Count}");
      Console.WriteLine("-----------------------------------------");
      Console.WriteLine("Häufigkeit der Wörter:");
      foreach (var kvp in statistic.Top10Words)
      {
        Console.WriteLine($"  {statistic.Top10Words.IndexOf(kvp) + 1}. {kvp.Key}: {kvp.Value}");
      }
      Console.WriteLine("-----------------------------------------");
    }

    private static void AddWordsToDictionary(Dictionary<string, int> wordFrequency, MatchCollection matches)
    {
      foreach (Match match in matches)
      {
        string word = match.Value.ToLower();
        if (wordFrequency.ContainsKey(word))
        {
          wordFrequency[word]++;
        }
        else
        {
          wordFrequency[word] = 1;
        }
      }
    }

    private static void SaveStatistic(Statistic statistic)
    {

      SQLiteConnection sqlite = new SQLiteConnection("Data Source=/path/to/file.db");
      sqlite.Open();
      string createTableQuery = @"CREATE TABLE IF NOT EXISTS WordStatistics (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    FilePath TEXT NOT NULL,
                                    Word TEXT NOT NULL,
                                    Count INTEGER NOT NULL
                                  );";
      using (SQLiteCommand command = new SQLiteCommand(createTableQuery, sqlite))
      {
        command.ExecuteNonQuery();
      }
      using (SQLiteTransaction transaction = sqlite.BeginTransaction())
      {
        string insertQuery = "INSERT INTO WordStatistics (FilePath, Word, Count) VALUES (@FilePath, @Word, @Count);";
        using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqlite))
        {
          foreach (var kvp in statistic.Top10Words)
          {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@FilePath", statistic.Path);
            command.Parameters.AddWithValue("@Word", kvp.Key);
            command.Parameters.AddWithValue("@Count", kvp.Value);
            command.ExecuteNonQuery();
          }
        }
        transaction.Commit();
      }
    }

  }