using OpenQA.Selenium;
using System.Threading;

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
        var homePage = new MainMenuPageObject(_webDriver, _elementWaiter);

        homePage.CloseCookiesPopup();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened");

        homePage.NavigateToAuthorizationPage()
            .Login(UserNameForTests.StartLogin, UserNameForTests.StartPassword);

        string actualUserName = homePage.getUserName();
        Assert.That(actualUserName, Is.EqualTo(UserNameForTests.ExpectedUserName), "User name is wrong or Login wasn't completed.");
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}
