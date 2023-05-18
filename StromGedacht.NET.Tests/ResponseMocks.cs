namespace StromGedacht.NET.Tests;

public static class ResponseMocks
{
  public const string Now = """
    {
      "state": 1
    }
    """;

  public const string States = """
    {
      "states": [
        {
          "from": "2023-05-14T00:00:00+02:00",
          "to": "2023-05-14T23:59:59+02:00",
          "state": 1
        },
        {
          "from": "2023-05-15T00:00:00+02:00",
          "to": "2023-05-15T23:59:59+02:00",
          "state": 2
        },
        {
          "from": "2023-05-16T00:00:00+02:00",
          "to": "2023-05-16T23:59:59+02:00",
          "state": 3
        },
        {
          "from": "2023-05-17T00:00:00+02:00",
          "to": "2023-05-17T23:59:59+02:00",
          "state": 4
        },
        {
          "from": "2023-05-18T00:00:00+02:00",
          "to": "2023-05-20T23:59:59+02:00",
          "state": 1
        }
      ]
    }
    """;

  public const string NoData = """
    "No data is available for the specified zip code (76130)."
    """;
}
