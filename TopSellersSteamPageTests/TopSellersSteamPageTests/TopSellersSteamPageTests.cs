using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using TopSellersSteamPageTests.PageObjects;

namespace TopSellersSteamPageTests;

public class Tests
{
    private IWebDriver _webDriver;
    private ElementWaiter _elementWaiter;

    [SetUp]
    public void Setup()
    {
        ChromeOptions options = new ChromeOptions();
        options.BinaryLocation = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";

        _webDriver = new ChromeDriver(options);

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://store.steampowered.com");
    }

    [Test]
    public void CheckFilteredGamesCountWithDisplayedGamesCount_CountsMatch()
    {
        var mainMenu = new MainMenuPageObject(_webDriver, _elementWaiter);
        var topSellers = new TopSellersPageObject(_webDriver, _elementWaiter);
        var gamePage = new GamePageObject(_webDriver, _elementWaiter);

        mainMenu.CloseCookiesPopup();
        Assert.IsTrue(mainMenu.IsMainMenuPageIsDisplayed(), "Main menu page is not opened");

        mainMenu.NavigateToTopSellers();
        Assert.IsTrue(topSellers.IsTopSellersPageDisplayed(), "Top sellers page is not opened");

        int initialGamesCountFromText = topSellers.GetGamesCountFromText();

        topSellers.ApplyFilter(TopSellersPageObject.Elements._singleplayerTagCheckbox);
        topSellers.ApplyFilter(TopSellersPageObject.Elements._windowsTagCheckbox);
        topSellers.ApplyFilter(TopSellersPageObject.Elements._actionTagCheckbox);
        topSellers.ApplyFilter(TopSellersPageObject.Elements._simulationTagCheckbox);

        Assert.Multiple(() => {
            Assert.IsTrue(topSellers.IsCheckboxChecked(TopSellersPageObject.Elements._singleplayerTagCheckbox), "Singlplayer checkbox is not checked");
            Assert.IsTrue(topSellers.IsCheckboxChecked(TopSellersPageObject.Elements._windowsTagCheckbox), "Windows checkbox is not checked");
            Assert.IsTrue(topSellers.IsCheckboxChecked(TopSellersPageObject.Elements._actionTagCheckbox), "Action checkbox is not checked");
            Assert.IsTrue(topSellers.IsCheckboxChecked(TopSellersPageObject.Elements._simulationTagCheckbox), "Simulation checkbox is not checked");
        });

        topSellers.WaitForFiltersApplied();

        topSellers.ScrollToBottom();

        int filteredGamesCountFromText = topSellers.GetGamesCountFromText();
        Assert.Less(filteredGamesCountFromText, initialGamesCountFromText, "The number of games did not change after applying the filter");
                
        Assert.AreEqual(topSellers.GetDisplayedGamesCount(), filteredGamesCountFromText,
                        $"The number of games displayed is not as expected. Displayed game: {topSellers.GetDisplayedGamesCount()}, FilteredGamesCountFromText: {filteredGamesCountFromText}");


        Game firstGame = topSellers.GetFirstGame();

        topSellers.NavigateToGamePage();        
        Assert.IsTrue(gamePage.IsGamePageObjectDisplayed(), "Game page is not opened");

        Game game = gamePage.GetGame();
        
        Assert.Multiple(() =>
        {
            Assert.AreEqual(firstGame, game, "The game details do not match:");

            if (firstGame._name != game._name)
            {
                Assert.Fail($"Expected name: {firstGame._name}, Actual name: {game._name}");
            }

            if (firstGame._releaseDate != game._releaseDate)
            {
                Assert.Fail($"Expected release date: {firstGame._releaseDate}, Actual release date: {game._releaseDate}");
            }

            if (firstGame._price != game._price)
            {
                Assert.Fail($"Expected price: {firstGame._price}, Actual price: {game._price}");
            }
        });
    }

    [TearDown]
    public void TearDown()
    {
        //_webDriver.Quit();
    }
}

