namespace StromGedacht.NET.DTOs;

internal record ForecastValueDto
{
  public double Value { get; set; }
  public DateTimeOffset DateTime { get; set; }
}
