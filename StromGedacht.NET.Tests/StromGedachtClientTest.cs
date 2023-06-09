using System.Net;
using FluentAssertions;
using RichardSzalay.MockHttp;
using StromGedacht.NET.Models;

namespace StromGedacht.NET.Tests;

public class StromGedachtClientTest
{
  private static HttpClient GetMockedHttpClient()
  {
    var mockHttp = new MockHttpMessageHandler();

    mockHttp.When(
        "https://api.stromgedacht.de/v1/states?zip=server-error&from=2023-05-14T00%3A00%3A00.0000000%2B02%3A00&to=2023-05-20T23%3A59%3A59.0000000%2B02%3A00")
      .Respond(_ => new HttpResponseMessage(HttpStatusCode.InternalServerError));
    mockHttp.When("https://api.stromgedacht.de/v1/now?zip=server-error")
      .Respond(_ => new HttpResponseMessage(HttpStatusCode.InternalServerError));
    mockHttp.When("https://api.stromgedacht.de/v1/now?zip=70170").Respond(_ =>
      new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ResponseMocks.NoData) });
    mockHttp.When("https://api.stromgedacht.de/v1/now?zip=70173").Respond("application/json", ResponseMocks.Now);
    mockHttp.When(
        "https://api.stromgedacht.de/v1/states?zip=70173&from=2023-05-14T00%3A00%3A00.0000000%2B02%3A00&to=2023-05-20T23%3A59%3A59.0000000%2B02%3A00")
      .Respond("application/json", ResponseMocks.States);

    return new HttpClient(mockHttp);
  }

  [Fact]
  public void Now()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var state = client.Now("70173");

    NowAssertions(state);
  }

  [Fact]
  public async void NowAsync()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var state = await client.NowAsync("70173");

    NowAssertions(state);
  }

  private static void NowAssertions(RegionState? state)
  {
    state.Should().Be(RegionState.Green);
  }

  [Fact]
  public void States()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var states = client.States("70173", new DateTimeOffset(2023, 5, 14, 0, 0, 0, TimeSpan.FromHours(2)),
      new DateTimeOffset(2023, 5, 20, 23, 59, 59, TimeSpan.FromHours(2)));

    StatesAssertions(states);
  }

  [Fact]
  public async void StatesAsync()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var states = await client.StatesAsync("70173", new DateTimeOffset(2023, 5, 14, 0, 0, 0, TimeSpan.FromHours(2)),
      new DateTimeOffset(2023, 5, 20, 23, 59, 59, TimeSpan.FromHours(2)));

    StatesAssertions(states);
  }

  [Fact]
  public void NoData()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var state = client.Now("70170");

    state.Should().BeNull();
  }

  [Fact]
  public void ServerErrorNow()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var state = client.Now("server-error");

    state.Should().BeNull();
  }

  [Fact]
  public void ServerErrorStates()
  {
    var client = new StromGedachtClient(GetMockedHttpClient());

    var states = client.States("server-error", new DateTimeOffset(2023, 5, 14, 0, 0, 0, TimeSpan.FromHours(2)),
      new DateTimeOffset(2023, 5, 20, 23, 59, 59, TimeSpan.FromHours(2)));

    states.Should().BeEmpty();
  }

  private static void StatesAssertions(IReadOnlyList<RegionStatePeriod> states)
  {
    states.Should().HaveCount(5);

    states[0].State.Should().Be(RegionState.Green);
    states[0].From.Should().Be(new DateTimeOffset(2023, 5, 14, 0, 0, 0, TimeSpan.FromHours(2)));
    states[0].To.Should().Be(new DateTimeOffset(2023, 5, 14, 23, 59, 59, TimeSpan.FromHours(2)));

    states[1].State.Should().Be(RegionState.Yellow);
    states[1].From.Should().Be(new DateTimeOffset(2023, 5, 15, 0, 0, 0, TimeSpan.FromHours(2)));
    states[1].To.Should().Be(new DateTimeOffset(2023, 5, 15, 23, 59, 59, TimeSpan.FromHours(2)));

    states[2].State.Should().Be(RegionState.Orange);
    states[2].From.Should().Be(new DateTimeOffset(2023, 5, 16, 0, 0, 0, TimeSpan.FromHours(2)));
    states[2].To.Should().Be(new DateTimeOffset(2023, 5, 16, 23, 59, 59, TimeSpan.FromHours(2)));

    states[3].State.Should().Be(RegionState.Red);
    states[3].From.Should().Be(new DateTimeOffset(2023, 5, 17, 0, 0, 0, TimeSpan.FromHours(2)));
    states[3].To.Should().Be(new DateTimeOffset(2023, 5, 17, 23, 59, 59, TimeSpan.FromHours(2)));

    states[4].State.Should().Be(RegionState.Green);
    states[4].From.Should().Be(new DateTimeOffset(2023, 5, 18, 0, 0, 0, TimeSpan.FromHours(2)));
    states[4].To.Should().Be(new DateTimeOffset(2023, 5, 20, 23, 59, 59, TimeSpan.FromHours(2)));
  }
}
