namespace StromGedacht.NET.Models;

/// <summary>
/// forecast data for specific zip
/// </summary>
public record Forecast
{
  /// <summary>
  /// Load
  /// </summary>
  public IEnumerable<ForecastValue> Load { get; set; } = Enumerable.Empty<ForecastValue>();

  /// <summary>
  /// Renewable Energy
  /// </summary>
  public IEnumerable<ForecastValue> RenewableEnergy { get; set; } = Enumerable.Empty<ForecastValue>();

  /// <summary>
  /// Residual Load
  /// </summary>
  public IEnumerable<ForecastValue> ResidualLoad { get; set; }= Enumerable.Empty<ForecastValue>();

  /// <summary>
  /// Super Green Threshold
  /// </summary>
  public IEnumerable<ForecastValue> SuperGreenThreshold { get; set; } = Enumerable.Empty<ForecastValue>();
}
