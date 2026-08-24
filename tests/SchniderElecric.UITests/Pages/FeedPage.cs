namespace SchniderElecric.UITests.Pages;

public class FeedPage
{
    private readonly IPage _page;

    public FeedPage(IPage page)
    {
        _page = page;
    }

    public Task<string> GetWhoAmIAsync()
        => _page.Locator(FeedSelectors.WhoAmI).InnerTextAsync();

    public Task<int> GetPostCountAsync()
        => _page.Locator(FeedSelectors.Posts).CountAsync();

    public Task<string> GetFirstPostAuthorAsync()
        => _page.Locator(FeedSelectors.FirstPostAuthor).First.InnerTextAsync();

    public Task<string> GetFirstPostContentAsync()
        => _page.Locator(FeedSelectors.FirstPostContent).First.InnerTextAsync();

    public Task OpenMenuAsync()
        => _page.Locator(FeedSelectors.MenuButton).ClickAsync();

    public Task ClickProfileAsync()
        => _page.Locator(FeedSelectors.ProfileLink).ClickAsync();

    public Task LogoutAsync()
        => _page.Locator(FeedSelectors.LogoutLink).ClickAsync();
}
