namespace TimeTracking;

public static class IntToMinutesStringExtension
{
  public static string ToMinutesString(this int minutes)
  {
    int hours = minutes / 60;
    int remainingMinutes = minutes % 60;
    return $"{hours:D2}:{remainingMinutes:D2}";
  }
}