﻿using System.Text.Encodings.Web;

namespace StromGedacht.NET.Utils;

internal static class ApiAddresses
{
  internal static readonly Uri BaseAddress = new("https://api.stromgedacht.de/v1/");

  internal static string Now(string zip) => new($"now?zip={zip}");

  internal static string States(string zip, DateTimeOffset from, DateTimeOffset to)
  {
    var encodedFrom = UrlEncoder.Default.Encode(from.ToString("O"));
    var encodedTo = UrlEncoder.Default.Encode(to.ToString("O"));

    return $"states?zip={zip}&from={encodedFrom}&to={encodedTo}";
  }

  internal static string StatesRelative(string zip, int hoursInPast, int hoursInFuture) =>
    $"statesRelative?zip={zip}&hoursInPast={hoursInPast}&hoursInFuture={hoursInFuture}";

  internal static string Forecast(string zip, DateTimeOffset from, DateTimeOffset to)
  {
    var encodedFrom = UrlEncoder.Default.Encode(from.ToString("O"));
    var encodedTo = UrlEncoder.Default.Encode(to.ToString("O"));

    return $"forecast?zip={zip}&from={encodedFrom}&to={encodedTo}";
  }
}
