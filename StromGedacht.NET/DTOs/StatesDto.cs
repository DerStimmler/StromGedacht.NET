namespace StromGedacht.NET.DTOs;

internal record StatesDto
{
  public IEnumerable<RegionStatePeriodDto> States { get; set; } = Enumerable.Empty<RegionStatePeriodDto>();
}
