namespace TimeTracking;

public class Employee
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public Dictionary<string, int> ProjectTimes { get; } = new Dictionary<string, int>();

  public Employee(int id, string name)
  {
    Id = id;
    Name = name;
  }
}