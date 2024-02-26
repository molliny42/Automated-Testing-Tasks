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
        _elementWaiter = new ElementWaiter(_webDriver, TimeSpan.FromSeconds(4));

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://buycycle.com");
    }

    [Test]
    public void Test1()
    {
        var homePage = new HomePageObject(_webDriver, _elementWaiter);
        homePage.CloseCookiesPopup();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened.");

        homePage.NavigateToAuthorizationPage();
        var authPage = new AuthorizationPageObject(_webDriver, _elementWaiter);
        Assert.That(authPage.IsAuthorizationPageDisplayed(), Is.EqualTo(true), "Authorization page is not opened.");
       
        authPage.InputLogin(JsonParser.GetString("credentials.json", JsonKeys.EMAIL));// consts
        authPage.WaitForLoginInput();
        authPage.InputPassword(JsonParser.GetString("credentials.json", JsonKeys.PASSWORD));
        authPage.WaitForPasswordInput();
        authPage.ConfirmLogin();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened.");

        homePage.NavigateToProfilePageObject();
        var profilePage = new ProfilePageObject(_webDriver, _elementWaiter);
        Assert.That(profilePage.IsProfilePageObjectIsDisplayed(), Is.EqualTo(true), "Profile page is not opened.");

        string actualUserName = profilePage.GetUserName();
        Assert.That(actualUserName, Is.EqualTo(JsonParser.GetString("credentials.json", JsonKeys.FULLNAME)), "User name is wrong.");

        profilePage.NavigateToEnduroCategory();
        var enduroCategoryPage = new EnduroCategoryPageObject(_webDriver, _elementWaiter);
        Assert.That(enduroCategoryPage.IsEnduroCategoryPageIsDisplayed, Is.EqualTo(true), "Enduro category page is not opened.");
        Assert.That(enduroCategoryPage.IsEnduroCheckboxChecked, Is.EqualTo(true), "Enduro category checkbox is not selected.");
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}