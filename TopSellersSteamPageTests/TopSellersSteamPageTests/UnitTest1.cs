namespace TopSellersSteamPageTests;

using OpenQA.Selenium;


public class Tests
{
    private IWebDriver _webDriver;

    [SetUp]
    public void Setup()
    {
        _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://store.steampowered.com");
    }

    [Test]
    public void Test1()
    {
    }

    [TearDown]
    public void TearDown()
    {

    }
}
