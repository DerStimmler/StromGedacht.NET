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
  ///   Get current region state
  /// </summary>
  /// <param name="zip"></param>
  /// <returns>Current region state</returns>
  public RegionState? Now(string zip) => NowAsync(zip).Result;

  /// <summary>
  ///   Get current region state
  /// </summary>
  /// <param name="zip"></param>
  /// <returns>Current region state</returns>
  public async Task<RegionState?> NowAsync(string zip)
  {
    var uri = ApiAddresses.Now(zip);

    var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    if (!response.IsSuccessStatusCode)
      return null;

    var dto = JsonSerializer.Deserialize<NowDto>(content,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return dto!.State;
  }

  /// <summary>
  ///   Get region states in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Region states in requested time period</returns>
  public IReadOnlyList<RegionStatePeriod> States(string zip, DateTimeOffset from, DateTimeOffset to) =>
    StatesAsync(zip, from, to).Result;

  /// <summary>
  ///   Get region states in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Region states in requested time period</returns>
  public async Task<IReadOnlyList<RegionStatePeriod>> StatesAsync(string zip, DateTimeOffset from, DateTimeOffset to)
  {
    var uri = ApiAddresses.States(zip, from, to);

    var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    if (!response.IsSuccessStatusCode)
      return Array.Empty<RegionStatePeriod>();

    var dto = JsonSerializer.Deserialize<StatesDto>(content,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return dto!.States
      .ToList()
      .AsReadOnly();
  }

  /// <summary>
  ///   Get region states in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="hoursInPast"></param>
  /// <param name="hoursInFuture"></param>
  /// <returns>Region states in requested time period</returns>
  public IReadOnlyList<RegionStatePeriod> States(string zip, int hoursInPast, int hoursInFuture) =>
    StatesAsync(zip, hoursInPast, hoursInFuture).Result;

  /// <summary>
  ///   Get region states in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="hoursInPast"></param>
  /// <param name="hoursInFuture"></param>
  /// <returns>Region states in requested time period</returns>
  public async Task<IReadOnlyList<RegionStatePeriod>> StatesAsync(string zip, int hoursInPast, int hoursInFuture)
  {
    var uri = ApiAddresses.StatesRelative(zip, hoursInPast, hoursInFuture);

    var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    if (!response.IsSuccessStatusCode)
      return Array.Empty<RegionStatePeriod>();

    var dto = JsonSerializer.Deserialize<StatesDto>(content,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return dto!.States
      .ToList()
      .AsReadOnly();
  }

  /// <summary>
  ///   Get forecast in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Region states in requested time period</returns>
  public Forecast? Forecast(string zip, DateTimeOffset from, DateTimeOffset to) =>
    ForecastAsync(zip, from, to).Result;

  /// <summary>
  ///   Get forecast in a specific time period
  /// </summary>
  /// <param name="zip"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Region states in requested time period</returns>
  public async Task<Forecast?> ForecastAsync(string zip, DateTimeOffset from, DateTimeOffset to)
  {
    var uri = ApiAddresses.Forecast(zip, from, to);

    var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

    if (!response.IsSuccessStatusCode)
      return null;

    var dto = JsonSerializer.Deserialize<ForecastDto>(content,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (dto is null)
      return null;

    return new Forecast
    {
      Load = dto.Load
        .Select(fv => new ForecastValue { DateTime = fv.DateTime, Value = fv.Value }),
      RenewableEnergy = dto.RenewableEnergy
        .Select(fv => new ForecastValue { DateTime = fv.DateTime, Value = fv.Value }),
      ResidualLoad = dto.ResidualLoad
        .Select(fv => new ForecastValue { DateTime = fv.DateTime, Value = fv.Value }),
      SuperGreenThreshold = dto.SuperGreenThreshold
        .Select(fv => new ForecastValue { DateTime = fv.DateTime, Value = fv.Value })
    };
  }
}
