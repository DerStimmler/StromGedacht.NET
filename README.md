# StromGedacht.NET

[![dotnet](https://img.shields.io/badge/platform-.NET-blue)](https://www.nuget.org/packages/StromGedacht.NET/)
[![nuget version](https://img.shields.io/nuget/v/StromGedacht.NET)](https://www.nuget.org/packages/StromGedacht.NET/)
[![nuget downloads](https://img.shields.io/nuget/dt/StromGedacht.NET)](https://www.nuget.org/packages/StromGedacht.NET/)
![build](https://github.com/DerStimmler/StromGedacht.NET/actions/workflows/build.yml/badge.svg)
[![codecov](https://codecov.io/gh/DerStimmler/StromGedacht.NET/branch/main/graph/badge.svg?token=8CCFM34SNC)](https://codecov.io/gh/DerStimmler/StromGedacht.NET)
[![GitHub license](https://img.shields.io/github/license/DerStimmler/StromGedacht.NET)](https://github.com/DerStimmler/StromGedacht.NET/blob/master/LICENSE.md)

C# client for the [StromGedacht](https://www.stromgedacht.de/) API

## Installation

Available on [NuGet](https://www.nuget.org/packages/StromGedacht.NET/).

```bash
dotnet add package StromGedacht.NET
```

or

```powershell
PM> Install-Package StromGedacht.NET
```

## Usage

The client can provide the region state at the current time or all states for a given time period.

The period may extend a maximum of 2 days into the future and 4 days into the past.

Each time you make a request, you will need to provide the zip code of the region for which you want to request the state.

### Initialization

First create an instance of `StromGedachtClient` by passing an instance of `HttpClient` to its constructor.

```csharp
var httpClient = new HttpClient();

var client = new StromGedachtClient(httpClient);
```

### Get current state

You can fetch the current state of a region by calling the `Now`
/ `NowAsync` methods and passing the zip code of the region.

```csharp
var state = client.Now("70173");
```

```csharp
var state = await client.NowAsync("70173");
```

### Get states for time period

You can fetch all states of a region for a specific time period by calling the `States`
/ `StatesAsync` methods and passing the zip code of the region, the start time and end time.

```csharp
var from = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.FromHours(2));
var to = new DateTimeOffset(2023, 1, 3, 23, 59, 59, TimeSpan.FromHours(2));

var states = client.States("70173", from, to);
```

```csharp
var from = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.FromHours(2));
var to = new DateTimeOffset(2023, 1, 3, 23, 59, 59, TimeSpan.FromHours(2));

var states = await client.StatesAsync("70173", from, to);
```

### Dependency Injection

You can register the `StromGedachtClient` in your Startup with a typed `HttpClient`.

```csharp
builder.Services.AddHttpClient<StromGedachtClient>();
```

Then inject the client wherever you like. E.g. in a controller:

```csharp
[Route("Home")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly StromGedachtClient _client;

    public HomeController(StromGedachtClient client)
    {
        _client = client;
    }
}
```

### API rate limits

The api is limited to about 6 requests per minute.

## Shoutout

The used API is provided by [StromGedacht](https://www.stromgedacht.de), TransnetBW GmbH.
