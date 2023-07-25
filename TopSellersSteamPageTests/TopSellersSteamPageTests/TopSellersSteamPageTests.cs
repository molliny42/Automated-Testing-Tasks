using OpenQA.Selenium;
using TopSellersSteamPageTests.PageObjects;

namespace TopSellersSteamPageTests;

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

        topSellers.ApplyFilters();
        topSellers.WaitForFiltersApplied();


        int filteredGamesCount = topSellers.GetGamesCount();

        Assert.IsTrue(topSellers.IsSingleplayerCheckboxChecked(), "Singlplayer checkbox is not checked");
        Assert.IsTrue(topSellers.IsActionCheckboxChecked(), "Action checkbox is not checked");
        Assert.IsTrue(topSellers.IsWindowsCheckboxChecked(), "Windows checkbox is not checked");
        Assert.Greater(initialGamesCount, filteredGamesCount, "The number of games did not change after applying the filter");


    }

    [TearDown]
    public void TearDown()
    {
        //_webDriver.Quit();
    }
}
