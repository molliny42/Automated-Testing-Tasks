using OpenQA.Selenium;
using System.Threading;

namespace BuycyclePagesTests.PageObjects;

public class Tests
{
    private IWebDriver _webDriver;

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
        var mainMenu = new MainMenuPageObject(_webDriver);
        mainMenu
            .SignIn()
            .Login("molliny42@gmail.com", "hpmor107");

        Assert.Pass();
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(TimeSpan.FromSeconds(10));
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}
