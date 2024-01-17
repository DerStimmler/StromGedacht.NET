using StromGedacht.NET.Models;

namespace StromGedacht.NET.DTOs;

internal record RegionStatePeriodDto
{
  public RegionState State { get; set; }
  public DateTimeOffset From { get; set; }
  public DateTimeOffset To { get; set; }
}
