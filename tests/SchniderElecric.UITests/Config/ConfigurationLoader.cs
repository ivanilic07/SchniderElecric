using Microsoft.Extensions.Configuration;

namespace SchniderElecric.UITests.Config;

public static class ConfigurationLoader
{
    private static readonly Lazy<TestSettings> Settings = new(Load);

    public static TestSettings Current => Settings.Value;

    private static TestSettings Load()
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var settings = new TestSettings();
        configuration.GetSection(TestSettings.SectionName).Bind(settings);
        return settings;
    }
}
