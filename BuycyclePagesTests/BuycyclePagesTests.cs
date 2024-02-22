using NUnit.Framework;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects;

public class Tests
{
    private IWebDriver _webDriver;
    private ElementWaiter _elementWaiter;

    [SetUp]
    public void Setup()
    {
        _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://buycycle.com");
       
    }

    [Test]
    public void Test1()
    {
        var homePage = new HomePageObject(_webDriver, _elementWaiter);
        var authPage = new AuthorizationPageObject(_webDriver, _elementWaiter);
        var profilePage = new ProfilePageObject(_webDriver, _elementWaiter);

        homePage.CloseCookiesPopup();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened.");

        homePage.NavigateToAuthorizationPage();
        Assert.That(authPage.IsAuthorizationPageDisplayed(), Is.EqualTo(true), "Authorization page is not opened.");

        authPage.InputLogin(UserNameForTests.StartLogin);
        authPage.WaitForLoginInput();
        authPage.InputPassword(UserNameForTests.StartPassword);
        authPage.WaitForPasswordInput();
        authPage.ConfirmLogin();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened.");

        homePage.NavigateToProfilePageObject();
        Assert.That(profilePage.IsProfilePageObjectIsDisplayed(), Is.EqualTo(true), "Profile page is not opened.");

        string actualUserName = profilePage.GetUserName();
        Assert.That(actualUserName, Is.EqualTo(UserNameForTests.ExpectedUserName), "User name is wrong.");
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}
