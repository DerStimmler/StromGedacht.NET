using StromGedacht.NET.Models;

namespace StromGedacht.NET.DTOs;

internal record StatesDto
{
  public IEnumerable<RegionStatePeriod> States { get; set; } = null!;
}
