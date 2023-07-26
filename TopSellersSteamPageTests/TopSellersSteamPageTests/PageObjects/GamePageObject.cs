using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TopSellersSteamPageTests.Helpers;

namespace TopSellersSteamPageTests.PageObjects
{
	public class GamePageObject : BasePageObject
	{

		private readonly By _highlightsBlockGame = By.XPath("//div[@id='game_highlights']");
		private readonly By _gamePriceElement = By.XPath("(//div[contains(@class, 'game_purchase_price')][@data-price-final] | //div[contains(@class, 'discount_final_price')])[1]");
        public GamePageObject(IWebDriver webDriver) : base(webDriver)
		{			
		}

		public bool IsGamePageObjectDisplayed()
		{
			return _elementWaiter.WaitForElementDisplayedAndEnabled(_highlightsBlockGame).Displayed;
		}

        public double GetGamePrice()
        {
            PriceHelper priceHelper = new PriceHelper(_webDriver,_elementWaiter);
            return priceHelper.GetGamePrice(_gamePriceElement);
        }        
    }
}

