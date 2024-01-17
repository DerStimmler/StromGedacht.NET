namespace StromGedacht.NET.Models;

/// <summary>
///   Forecast value for specific time
/// </summary>
public record ForecastValue
{
  /// <summary>
  ///   Value of forecast
  /// </summary>
  public double Value { get; set; }

  /// <summary>
  ///   Time of forecast
  /// </summary>
  public DateTimeOffset DateTime { get; set; }
}
