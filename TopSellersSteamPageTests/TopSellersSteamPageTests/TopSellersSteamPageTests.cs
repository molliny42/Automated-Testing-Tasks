using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using TopSellersSteamPageTests.PageObjects;

namespace TopSellersSteamPageTests;

public class Tests
{
    private IWebDriver _webDriver;

    [SetUp]
    public void Setup()
    {
        ChromeOptions options = new ChromeOptions();
        options.BinaryLocation = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"; // Укажите полный путь к исполняемому файлу Chrome

        _webDriver = new ChromeDriver(options);

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://store.steampowered.com");
    }

    [Test]
    public void CheckFilteredGamesCountWithDisplayedGamesCount_CountsMatch()
    {
        var mainMenu = new MainMenuPageObject(_webDriver);
        var topSellers = new TopSellersPageObject(_webDriver);
        var gamePage = new GamePageObject(_webDriver);

        mainMenu.CloseCookiesPopup();
        Assert.IsTrue(mainMenu.IsMainMenuPageIsDisplayed(), "Main menu page is not opened");

        mainMenu.NavigateToTopSellers();
        Assert.IsTrue(topSellers.IsTopSellersPageDisplayed(), "Top sellers page is not opened");

        int initialGamesCountFromText = topSellers.GetGamesCountFromText();

        topSellers.ApplyFilters();
        Assert.IsTrue(topSellers.IsSingleplayerCheckboxChecked(), "Singlplayer checkbox is not checked");
        Assert.IsTrue(topSellers.IsWindowsCheckboxChecked(), "Windows checkbox is not checked");
        Assert.IsTrue(topSellers.IsActionCheckboxChecked(), "Action checkbox is not checked");
        Assert.IsTrue(topSellers.IsSimulationCheckboxChecked(), "Simulation checkbox is not checked");

        topSellers.WaitForFiltersApplied();

        int filteredGamesCountFromText = topSellers.GetGamesCountFromText();

        Console.WriteLine("initialGamesCount = " + initialGamesCountFromText + "\n");
        Console.WriteLine("filteredGamesCountFromText = " + filteredGamesCountFromText + "\n");
        Assert.Greater(initialGamesCountFromText, filteredGamesCountFromText, "The number of games did not change after applying the filter");

        topSellers.ScrollToBottom();

        Console.WriteLine("Found games = " + topSellers.GetDisplayedGamesCount() + "\n");

        Assert.AreEqual(topSellers.GetDisplayedGamesCount(), filteredGamesCountFromText, "The number of games displayed is not as expected");

        double priceFirstGame = topSellers.GetFirstGamePrice();

        Console.WriteLine("First game price = " + topSellers.GetFirstGamePrice().ToString());

        topSellers.NavigateToGamePage();
        Assert.IsTrue(gamePage.IsGamePageObjectDisplayed(), "Game page is not opened");

        Console.WriteLine("expected price" + priceFirstGame + "actual price" + gamePage.GetGamePrice());
        Assert.AreEqual(priceFirstGame, gamePage.GetGamePrice(), "The price of game displayed is not as expected");
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }
}

