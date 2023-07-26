using System;
using OpenQA.Selenium;

namespace TopSellersSteamPageTests.Helpers
{
    public class PriceHelper
    {
        private IWebDriver _webDriver;
        private ElementWaiter _elementWaiter;

        public PriceHelper(IWebDriver webDriver, ElementWaiter elementWaiter)
        {
            _webDriver = webDriver;
            _elementWaiter = new ElementWaiter(_webDriver, TimeSpan.FromSeconds(5));
        }

        public double GetGamePrice(By elementLocator)
        {
            string priceWithCurrency = _elementWaiter.WaitForElementDisplayedAndEnabled(elementLocator).Text;

            string numericPriceText = priceWithCurrency.Replace("€", "").Replace("£", "").Replace(",", ".");

            if (double.TryParse(numericPriceText, out double gamePrice))
            {
                return gamePrice;
            }

            return 0;
        }
    }
}

