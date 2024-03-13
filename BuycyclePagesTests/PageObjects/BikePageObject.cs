using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
    public class BikePageObject : BasePageObject
	{
        private readonly By _bikeDetailsSection = By.XPath("//div[contains(@class, 'shop-single-head-right')]");
        private readonly By _bikeSalePrice = By.XPath("//div[contains(@class, 'bike-price')]//p[contains(@class, 'sale')]");

        public BikePageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter) { }

        public bool IsBikePageObjectDisplayed() =>
            _elementWaiter.WaitForElementToBeVisible(_bikeDetailsSection)?.Displayed ?? false;

        public string GetBikePrice()
        {
            IReadOnlyList<IWebElement> bikesElements = _webDriver.FindElements(_bikeSalePrice);
            return bikesElements.Count > 0 ? bikesElements[bikesElements.Count - 1].Text : string.Empty;
        }
    }
}

