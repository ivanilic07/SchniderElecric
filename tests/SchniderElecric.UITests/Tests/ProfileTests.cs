using System.Text.RegularExpressions;

using SchniderElecric.UITests.Fixtures;
using SchniderElecric.UITests.Pages;
using SchniderElecric.UITests.TestData;

namespace SchniderElecric.UITests.Tests;

public class ProfileTests : AuthenticatedTestBase
{
    private FeedPage _feedPage = null!;
    private ProfilePage _profilePage = null!;

    [SetUp]
    public void CreatePages()
    {
        _feedPage = new FeedPage(Page);
        _profilePage = new ProfilePage(Page);
    }

    [Test]
    public async Task ProfileDisplaysUserInformation()
    {
        await _feedPage.OpenMenuAsync();
        await _feedPage.ClickProfileAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/profile\\.html"));
        Assert.That(await _profilePage.GetWelcomeTextAsync(), Is.EqualTo(ProfileTestData.WelcomeText));
        Assert.That(await _profilePage.GetEmailAsync(), Is.EqualTo(Settings.Email));
        Assert.That(await _profilePage.GetPhoneAsync(), Is.EqualTo(ProfileTestData.Phone));
        Assert.That(await _profilePage.GetVerifiedStatusAsync(), Is.EqualTo(ProfileTestData.VerifiedStatus));
        Assert.That(await _profilePage.GetFirstNameAsync(), Is.EqualTo(ProfileTestData.FirstName));
        Assert.That(await _profilePage.GetLastNameAsync(), Is.EqualTo(ProfileTestData.LastName));
    }

    [Test]
    public async Task UserCanUpdateProfile()
    {
        await _feedPage.OpenMenuAsync();
        await _feedPage.ClickProfileAsync();

        var originalFirstName = await _profilePage.GetFirstNameAsync();
        var originalLastName = await _profilePage.GetLastNameAsync();

        try
        {
            await _profilePage.UpdateProfileAsync(ProfileTestData.UpdatedFirstName, ProfileTestData.UpdatedLastName);
            Assert.That(await _profilePage.GetSuccessMessageAsync(), Is.EqualTo(ProfileTestData.ProfileUpdatedMessage));
            Assert.That(await _profilePage.GetFirstNameAsync(), Is.EqualTo(ProfileTestData.UpdatedFirstName));
            Assert.That(await _profilePage.GetLastNameAsync(), Is.EqualTo(ProfileTestData.UpdatedLastName));

            await Page.ReloadAsync();
            Assert.That(await _profilePage.GetFirstNameAsync(), Is.EqualTo(ProfileTestData.UpdatedFirstName));
            Assert.That(await _profilePage.GetLastNameAsync(), Is.EqualTo(ProfileTestData.UpdatedLastName));
        }
        finally
        {
            await _profilePage.UpdateProfileAsync(originalFirstName, originalLastName);
            Assert.That(await _profilePage.GetFirstNameAsync(), Is.EqualTo(originalFirstName));
            Assert.That(await _profilePage.GetLastNameAsync(), Is.EqualTo(originalLastName));
            Assert.That(await _profilePage.GetSuccessMessageAsync(), Is.EqualTo(ProfileTestData.ProfileUpdatedMessage));
        }
    }

    [Test]
    public async Task UserCanNavigateBackToFeed()
    {
        await _feedPage.OpenMenuAsync();
        await _feedPage.ClickProfileAsync();
        await _profilePage.ClickBackToFeedAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/feed\\.html"));
        Assert.That(await _feedPage.GetWhoAmIAsync(), Does.Contain(LoginTestData.LoggedInUserText));
    }

    [Test]
    public async Task UploadWithoutFileShowsValidationMessage()
    {
        await _feedPage.OpenMenuAsync();
        await _feedPage.ClickProfileAsync();
        await _profilePage.ClickUploadImageAsync();

        Assert.That(await _profilePage.GetImageErrorAsync(), Is.EqualTo(ProfileTestData.UploadWithoutFileMessage));
    }
}
