using SchniderElecric.UITests.Config;

namespace SchniderElecric.UITests.Fixtures;

public abstract class UiTestBase : PageTest
{
    protected TestSettings Settings => ConfigurationLoader.Current;

    public override Task<BrowserTypeLaunchOptions?> LaunchOptionsAsync()
        => Task.FromResult<BrowserTypeLaunchOptions?>(new()
        {
            Headless = Settings.Headless,
            SlowMo = Settings.SlowMo,
            Args = ["--start-maximized"],
        });

    public override BrowserNewContextOptions ContextOptions()
        => new()
        {
            ViewportSize = ViewportSize.NoViewport,
            IgnoreHTTPSErrors = Settings.IgnoreHTTPSErrors,
            Locale = Settings.Locale,
            TimezoneId = Settings.TimezoneId,
        };

    [SetUp]
    public void ApplyPageTimeouts()
    {
        Page.SetDefaultTimeout(Settings.DefaultTimeoutMs);
        Page.SetDefaultNavigationTimeout(Settings.NavigationTimeoutMs);
        SetDefaultExpectTimeout(Settings.ExpectTimeoutMs);
    }
}
