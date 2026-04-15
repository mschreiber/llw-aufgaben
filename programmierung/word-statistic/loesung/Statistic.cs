namespace WordStatistic
{
  public class Statistic
  {
    public string Path { get; set; } = string.Empty;
    public List<KeyValuePair<string, int>> Top10Words { get; set; } = new List<KeyValuePair<string, int>>();
    public int Count { get; set; }
  }
}