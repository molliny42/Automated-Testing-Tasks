using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace TopSellersSteamPageTests.PageObjects
{
	public class GamePageObject : BasePageObject
	{
		private readonly By _highlightsBlockGame = By.XPath("//div[@id='game_highlights']");
        private readonly By _gameNameElement = By.XPath("//div[contains(@class, 'HomeHeaderContent')]//div[contains(@class, 'AppName')]");
        private readonly By _gameReleaseDateElement = By.XPath("//div[contains(@class, 'release_date')]//div[contains(@class, 'date')]");
        private readonly By _gamePriceElement = By.XPath("(//div[contains(@class, 'game_purchase_price')] | //div[contains(@class, 'discount_final_price')])[1]");

		public GamePageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
		{			
		}

        public bool IsGamePageObjectDisplayed() => _elementWaiter.WaitForElementDisplayedAndEnabled(_highlightsBlockGame).Displayed;

        public string GetFirstGamePrice()
        {
            try
            {
                string price = _elementWaiter.WaitForElementDisplayedAndEnabled(_gamePriceElement).Text.Replace(",", ".");
                Match match = Regex.Match(price, @"\d+\.\d+");
                return match.Success ? match.Value : "0";
            }
            catch(NoSuchElementException)
            {
                throw new Exception("Test failed due to NoSuchElementException: Element '_gamePriceElement' not found or not displayed in 'GamePageObject' class.");
            }
        }

        public Game GetGame()
        {
            string name = _elementWaiter.WaitForElementDisplayedAndEnabled(_gameNameElement).Text; ;
            string releaseDate = _elementWaiter.WaitForElementDisplayedAndEnabled(_gameReleaseDateElement).Text; ;
            string price = GetFirstGamePrice();
            return new Game(name, releaseDate, price);
        }
    }
}

