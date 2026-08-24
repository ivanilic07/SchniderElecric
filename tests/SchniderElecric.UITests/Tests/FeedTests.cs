using System.Text.RegularExpressions;

using SchniderElecric.UITests.Fixtures;
using SchniderElecric.UITests.Pages;
using SchniderElecric.UITests.TestData;

namespace SchniderElecric.UITests.Tests;

public class FeedTests : AuthenticatedTestBase
{
    private FeedPage _feedPage = null!;

    [SetUp]
    public void CreateFeedPage()
    {
        _feedPage = new FeedPage(Page);
    }

    [Test]
    public async Task FeedLoadsAfterSuccessfulLogin()
    {
        await Expect(Page).ToHaveURLAsync(new Regex(".*/feed\\.html"));
        Assert.That(await _feedPage.GetWhoAmIAsync(), Does.Contain(LoginTestData.LoggedInUserText));
        Assert.That(await _feedPage.GetPostCountAsync(), Is.GreaterThan(0));
        Assert.That(await _feedPage.GetFirstPostAuthorAsync(), Is.Not.Empty);
        Assert.That(await _feedPage.GetFirstPostContentAsync(), Is.Not.Empty);
    }

    [Test]
    public async Task UserCanLogout()
    {
        await _feedPage.OpenMenuAsync();
        await _feedPage.LogoutAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/login\\.html"));
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" })).ToBeVisibleAsync();
    }
}
