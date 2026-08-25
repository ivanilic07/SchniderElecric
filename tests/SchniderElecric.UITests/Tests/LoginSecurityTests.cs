using System.Text.RegularExpressions;

using SchniderElecric.UITests.Fixtures;
using SchniderElecric.UITests.Pages;
using SchniderElecric.UITests.TestData;

namespace SchniderElecric.UITests.Tests;

public class LoginSecurityTests : UiTestBase
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public async Task OpenLoginPage()
    {
        await Page.GotoAsync(Settings.BaseUrl);
        _loginPage = new LoginPage(Page);
    }

    [Test]
    public async Task SqlInjectionInEmailDoesNotAuthenticate()
    {
        await _loginPage.LoginAsync(LoginTestData.SqlInjectionPayload, LoginTestData.InvalidPassword);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/feed\\.html"));
        await Expect(Page.GetByText(LoginTestData.LoggedInUserText)).ToHaveCountAsync(0);
    }

    [Test]
    public async Task SqlInjectionInPasswordDoesNotAuthenticate()
    {
        await _loginPage.LoginAsync(LoginTestData.InvalidEmail, LoginTestData.SqlInjectionPayload);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/feed\\.html"));
        await Expect(Page.GetByText(LoginTestData.LoggedInUserText)).ToHaveCountAsync(0);
    }

    [Test]
    public async Task XssPayloadIsNotExecuted()
    {
        var dialogAppeared = false;
        Page.Dialog += async (_, dialog) =>
        {
            dialogAppeared = true;
            await dialog.DismissAsync();
        };

        await _loginPage.LoginAsync(LoginTestData.InvalidEmail, LoginTestData.XssProbe);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page.GetByRole(AriaRole.Alert)).ToBeVisibleAsync();
        Assert.That(dialogAppeared, Is.False);
    }

    [Test]
    public async Task PasswordIsNotExposedInUrl()
    {
        await _loginPage.LoginAsync(Settings.Email, Settings.Password);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/feed\\.html"));
        Assert.That(Page.Url, Does.Not.Contain(Settings.Password));
    }

    [Test]
    public async Task LoginResponsesDoNotAccidentallyAuthenticateInvalidCredentials()
    {
        await _loginPage.LoginAsync(LoginTestData.InvalidEmail, LoginTestData.InvalidPassword);

        await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/feed\\.html"));
        await Expect(Page.GetByText(LoginTestData.LoggedInUserText)).ToHaveCountAsync(0);

        await Page.GotoAsync(new Uri(new Uri(Settings.BaseUrl), "/feed.html").ToString());

        await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/feed\\.html"));
        await Expect(Page.GetByText(LoginTestData.LoggedInUserText)).ToHaveCountAsync(0);
    }
}
