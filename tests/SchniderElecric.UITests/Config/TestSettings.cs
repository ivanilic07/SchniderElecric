namespace SchniderElecric.UITests.Config;

public sealed class TestSettings
{
    public const string SectionName = "TestSettings";

    public string BaseUrl { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Browser { get; set; } = "Chromium";

    public bool Headless { get; set; } = true;

    public int SlowMo { get; set; }

    public int DefaultTimeoutMs { get; set; } = 30_000;

    public int NavigationTimeoutMs { get; set; } = 30_000;

    public int ExpectTimeoutMs { get; set; } = 5_000;

    public int ViewportWidth { get; set; } = 1920;

    public int ViewportHeight { get; set; } = 1080;

    public bool IgnoreHTTPSErrors { get; set; } = true;

    public string Trace { get; set; } = "on-first-retry";

    public string Video { get; set; } = "retain-on-failure";

    public string Screenshot { get; set; } = "only-on-failure";

    public string Locale { get; set; } = "en-US";

    public string TimezoneId { get; set; } = "Europe/Belgrade";

    public int Workers { get; set; } = 1;
}
