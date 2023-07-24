namespace TopSellersSteamPageTests;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TopSellersSteamPageTests.PageObjects;

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
    public void CheckFilteredGamesCountWithDisplayedGamesCount_CountsMatch()
    {
        var mainMenu = new MainMenuPageObject(_webDriver);
        var topSellers = new TopSellersPageObject(_webDriver);


        mainMenu.CloseCookiesPopup();

        Assert.IsTrue(mainMenu.IsMainMenuPageIsDisplayed(), "Main menu page is not opened!");

        mainMenu.NavigateToTopSellers();

        Assert.IsTrue(topSellers.IsTopSellersPageDisplayed(), "Top sellers page is not opened!");

       int initialGamesCount = topSellers.GetGamesCount();

        Assert.AreEqual(9262, initialGamesCount, "!!!!!!!!!!!!!!!!!!!!!");


    }

    [TearDown]
    public void TearDown()
    {
        //_webDriver.Quit();
    }
}
