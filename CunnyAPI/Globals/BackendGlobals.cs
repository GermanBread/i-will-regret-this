namespace CunnyApi.Globals;

public static class BackendGlobals
{
    static BackendGlobals()
    {
        HttpClient = new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(1) });

        ApiVersion = new Version(2, 0, 0);
        LatestVersionRoute = "v2";

        HttpClient.DefaultRequestHeaders.Add("User-Agent", "CunnyApi");
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-Ver", ApiVersion.ToString());
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-URL", "https://github.com/ProjectCuteAndFunny/CunnyApi");
    }

    public static List<string> UuidList { get; } = new();

    public static string LatestVersionRoute { get; }
    public static HttpClient HttpClient { get; }
    public static Version ApiVersion { get; }
}