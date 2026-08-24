namespace SchniderElecric.UITests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public Task EnterEmailAsync(string email)
        => _page.Locator(LoginSelectors.EmailInput).FillAsync(email);

    public Task EnterPasswordAsync(string password)
        => _page.Locator(LoginSelectors.PasswordInput).FillAsync(password);

    public Task ClickLoginAsync()
        => _page.Locator(LoginSelectors.LoginButton).ClickAsync();

    public async Task LoginAsync(string email, string password)
    {
        await EnterEmailAsync(email);
        await EnterPasswordAsync(password);
        await ClickLoginAsync();
    }

    public Task ClickForgotPasswordAsync()
        => _page.Locator(LoginSelectors.ForgotPasswordLink).ClickAsync();

    public Task ClickRegisterAsync()
        => _page.Locator(LoginSelectors.RegisterLink).ClickAsync();
}
