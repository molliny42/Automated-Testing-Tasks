using OpenQA.Selenium;

namespace BuycyclePagesTests;

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
        Assert.Pass();
    }
    //
    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}
