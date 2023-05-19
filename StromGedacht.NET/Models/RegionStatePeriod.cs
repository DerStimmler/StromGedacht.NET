namespace StromGedacht.NET.Models;

/// <summary>
///   Contains region state for a specific period
/// </summary>
public record RegionStatePeriod
{
  /// <summary>
  ///   Region state in this period
  /// </summary>
  public RegionState State { get; set; }

  /// <summary>
  ///   Period starting time
  /// </summary>
  public DateTimeOffset From { get; set; }

  /// <summary>
  ///   Period end time
  /// </summary>
  public DateTimeOffset To { get; set; }
}
