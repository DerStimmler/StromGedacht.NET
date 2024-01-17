namespace StromGedacht.NET.DTOs;

internal record ForecastDto
{
  public IEnumerable<ForecastValueDto> Load { get; set; } = Enumerable.Empty<ForecastValueDto>();
  public IEnumerable<ForecastValueDto> RenewableEnergy { get; set; } = Enumerable.Empty<ForecastValueDto>();
  public IEnumerable<ForecastValueDto> ResidualLoad { get; set; } = Enumerable.Empty<ForecastValueDto>();
  public IEnumerable<ForecastValueDto> SuperGreenThreshold { get; set; } = Enumerable.Empty<ForecastValueDto>();
}
