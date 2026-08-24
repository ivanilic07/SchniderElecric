using System.Text.RegularExpressions;

using SchniderElecric.UITests.Pages;
using SchniderElecric.UITests.TestData;

namespace SchniderElecric.UITests.Fixtures;

public abstract class AuthenticatedTestBase : UiTestBase
{
    [SetUp]
    public async Task LoginBeforeTest()
    {
        await Page.GotoAsync(Settings.BaseUrl);
        var loginPage = new LoginPage(Page);
        await loginPage.LoginAsync(Settings.Email, Settings.Password);
        await Expect(Page).ToHaveURLAsync(new Regex(".*/feed\\.html"));
        await Expect(Page.GetByText(LoginTestData.LoggedInUserText)).ToBeVisibleAsync();
    }
}
