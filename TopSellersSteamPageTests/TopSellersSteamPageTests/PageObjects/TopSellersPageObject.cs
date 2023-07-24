using System;
using System.Globalization;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace TopSellersSteamPageTests.PageObjects
{
    public class TopSellersPageObject : BasePageObject
    {
        private readonly By _ = By.XPath("");
        private readonly By _gamesCountElement = By.XPath("//div[@id='search_results_filtered_warning_persistent']/div[1]");



        public TopSellersPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }

        public bool IsTopSellersPageDisplayed()
        {
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_gamesCountElement).Displayed;
        }

        public int GetGamesCount()
        {
            string gamesCountTextElement = _elementWaiter.WaitForElementDisplayedAndEnabled(_gamesCountElement).Text;

            Match match = Regex.Match(gamesCountTextElement, @"\d{1,3}(?:,\d{3})*");

            if (match.Success)
            {
                string numberText = match.Value.Replace(",", "");
                int gamesCount = int.Parse(numberText);
                return gamesCount;
            }

            return 0;
        }

    }
}

