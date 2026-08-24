namespace SchniderElecric.UITests.Pages;

public class ProfilePage
{
    private readonly IPage _page;

    public ProfilePage(IPage page)
    {
        _page = page;
    }

    public Task<string> GetWelcomeTextAsync()
        => _page.Locator(ProfileSelectors.Welcome).InnerTextAsync();

    public Task<string> GetEmailAsync()
        => _page.Locator(ProfileSelectors.Email).InnerTextAsync();

    public Task<string> GetPhoneAsync()
        => _page.Locator(ProfileSelectors.Phone).InnerTextAsync();

    public Task<string> GetVerifiedStatusAsync()
        => _page.Locator(ProfileSelectors.Verified).InnerTextAsync();

    public Task<string> GetFirstNameAsync()
        => _page.Locator(ProfileSelectors.FirstName).InputValueAsync();

    public Task<string> GetLastNameAsync()
        => _page.Locator(ProfileSelectors.LastName).InputValueAsync();

    public async Task UpdateProfileAsync(string firstName, string lastName)
    {
        await _page.Locator(ProfileSelectors.FirstName).FillAsync(firstName);
        await _page.Locator(ProfileSelectors.LastName).FillAsync(lastName);
        await _page.Locator(ProfileSelectors.SaveChangesButton).ClickAsync();
    }

    public Task<string> GetSuccessMessageAsync()
        => _page.Locator(ProfileSelectors.SuccessMessage).InnerTextAsync();

    public async Task UploadImageAsync(string filePath)
    {
        await _page.Locator(ProfileSelectors.ImageInput).SetInputFilesAsync(filePath);
        await _page.Locator(ProfileSelectors.UploadImageButton).ClickAsync();
    }

    public Task ClickUploadImageAsync()
        => _page.Locator(ProfileSelectors.UploadImageButton).ClickAsync();

    public Task<string> GetImageErrorAsync()
        => _page.Locator(ProfileSelectors.ImageError).InnerTextAsync();

    public Task ClickBackToFeedAsync()
        => _page.Locator(ProfileSelectors.BackToFeedLink).ClickAsync();

    public Task OpenMenuAsync()
        => _page.Locator(ProfileSelectors.MenuButton).ClickAsync();

    public Task LogoutAsync()
        => _page.Locator(ProfileSelectors.LogoutLink).ClickAsync();
}
