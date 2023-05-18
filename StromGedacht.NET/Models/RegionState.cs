namespace StromGedacht.NET.Models;

/// <summary>
/// Describes different region states
/// </summary>
public enum RegionState
{
  /// <summary>
  /// Normal operation - nothing to do
  /// </summary>
  Green = 1,
  
  /// <summary>
  /// Bring consumption forward - use electricity now
  /// </summary>
  Yellow = 2,
  
  /// <summary>
  /// Reduce consumption to save costs and CO2
  /// </summary>
  Orange = 3,
  
  /// <summary>
  /// Reduce consumption to prevent power shortage
  /// </summary>
  Red = 4
}
