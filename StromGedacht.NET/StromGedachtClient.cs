using System.Net;
using System.Text.Json;
using StromGedacht.NET.DTOs;
using StromGedacht.NET.Models;
using StromGedacht.NET.Utils;

namespace StromGedacht.NET;

/// <summary>
///   Client for fetching StromGedacht API.
/// </summary>
public class StromGedachtClient
{
  private readonly HttpClient _httpClient;

  /// <summary>
  ///   Instantiate client for fetching StromGedacht API.
  /// </summary>
  /// <param name="httpClient"></param>
  public StromGedachtClient(HttpClient httpClient)
  {
    httpClient.BaseAddress = ApiAddresses.BaseAddress;

    _httpClient = httpClient;
  }

  /// <summary>
  /// Get current region state
  /// </summary>
  /// <param name="zip"></param>
  /// <returns>Current region state</returns>
  /// <exception cref="HttpRequestException"></exception>
  /// <exception cref="InvalidDataException"></exception>
  public RegionState? Now(string zip) => NowAsync(zip).Result;

  /// <summary>
  /// Get current region state
  /// </summary>
  /// <param name="zip"></param>
  /// <returns>Current region state</returns>
  /// <exception cref="HttpRequestException"></exception>
  /// <exception cref="InvalidDataException"></exception>
  public async Task<RegionState?> NowAsync(string zip)
  {
    var uri = ApiAddresses.Now(zip);

    var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    if (response.StatusCode == HttpStatusCode.BadRequest)
      return null;

    if (!response.IsSuccessStatusCode)
      throw new HttpRequestException(content);

    var dto = JsonSerializer.Deserialize<NowDto>(content,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (dto is null)
      throw new InvalidDataException("Couldn't deserialize JSON response.");

    return dto.State;
  } 

  /// <summary>
  /// Get all region states in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Region states in time requested time period</returns>
  public IReadOnlyList<RegionStatePeriod> States(string zip, DateTimeOffset from, DateTimeOffset to) =>
    StatesAsync(zip, from, to).Result;

  /// <summary>
  /// Get all region states in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Region states in time requested time period</returns>
  public async Task<IReadOnlyList<RegionStatePeriod>> StatesAsync(string zip, DateTimeOffset from, DateTimeOffset to)
  {
    var uri = ApiAddresses.States(zip, from, to);

    var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    if (response.StatusCode == HttpStatusCode.BadRequest)
      return Array.Empty<RegionStatePeriod>();

    if (!response.IsSuccessStatusCode)
      throw new HttpRequestException(content);
    
    var dto = JsonSerializer.Deserialize<StatesDto>(content,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (dto is null)
      throw new InvalidDataException("Couldn't deserialize JSON response.");

    return dto.States
      .ToList()
      .AsReadOnly();
  }
}
