using System.Text.RegularExpressions;

using SchniderElecric.UITests.Fixtures;
using SchniderElecric.UITests.Pages;
using SchniderElecric.UITests.TestData;

namespace SchniderElecric.UITests.Tests;

public class LoginTests : UiTestBase
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public async Task OpenLoginPage()
    {
        await Page.GotoAsync(Settings.BaseUrl);
        _loginPage = new LoginPage(Page);
    }

    [Test]
    public async Task SuccessfulLogin_WithValidCredentials_RedirectsToFeed()
    {
        await _loginPage.LoginAsync(Settings.Email, Settings.Password);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/feed\\.html"));
        await Expect(Page.GetByText(LoginTestData.LoggedInUserText)).ToBeVisibleAsync();
    }

    [Test]
    public async Task Login_WithInvalidPassword_ShowsError()
    {
        await _loginPage.LoginAsync(Settings.Email, LoginTestData.InvalidPassword);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveTextAsync(LoginTestData.IncorrectPasswordMessage);
    }

    [Test]
    public async Task Login_WithInvalidEmail_ShowsError()
    {
        await _loginPage.LoginAsync(LoginTestData.InvalidEmail, Settings.Password);

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveTextAsync(LoginTestData.UserDoesNotExistMessage);
    }

    [Test]
    public async Task Login_WithEmptyEmail_StaysOnLoginPage()
    {
        await _loginPage.EnterPasswordAsync(Settings.Password);
        await _loginPage.ClickLoginAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" })).ToBeEmptyAsync();
    }

    [Test]
    public async Task Login_WithEmptyPassword_StaysOnLoginPage()
    {
        await _loginPage.EnterEmailAsync(Settings.Email);
        await _loginPage.ClickLoginAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" })).ToBeEmptyAsync();
    }

    [Test]
    public async Task ForgotPasswordLink_NavigatesToForgotPasswordPage()
    {
        await _loginPage.ClickForgotPasswordAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/forgot-password\\.html"));
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = LoginTestData.ForgotPasswordHeading })).ToBeVisibleAsync();
    }

    [Test]
    public async Task RegisterLink_NavigatesToRegisterPage()
    {
        await _loginPage.ClickRegisterAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/register\\.html"));
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = LoginTestData.RegisterHeading })).ToBeVisibleAsync();
    }
}
