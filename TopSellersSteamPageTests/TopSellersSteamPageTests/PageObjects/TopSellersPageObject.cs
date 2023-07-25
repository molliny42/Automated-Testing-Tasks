using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TopSellersSteamPageTests.PageObjects
{
    public class TopSellersPageObject : BasePageObject
    {
        private readonly By _gamesCountElement = By.XPath("//div[@id='search_results_filtered_warning_persistent']/div[1]");
        private readonly By _singleplayerTagCheckbox = By.XPath("//div[@data-loc='Singleplayer']");
        private readonly By _actionTagCheckbox = By.XPath("//div[@data-loc='Action']");
        private readonly By _windowsTagCheckbox = By.XPath("//div[@data-loc='Windows']");

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

        public void ApplyFilters()
        {
            _elementWaiter.WaitForElementDisplayedAndEnabled(_singleplayerTagCheckbox).Click();
            _elementWaiter.WaitForElementDisplayedAndEnabled(_actionTagCheckbox).Click();
            _elementWaiter.WaitForElementDisplayedAndEnabled(_windowsTagCheckbox).Click();
        }

        public void WaitForFiltersApplied()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.StalenessOf(_webDriver.FindElement(_gamesCountElement)));
        }

        public bool IsSingleplayerCheckboxChecked()
        {
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_singleplayerTagCheckbox).GetAttribute("class").Contains("checked");
        }

        public bool IsActionCheckboxChecked()
        {
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_actionTagCheckbox).GetAttribute("class").Contains("checked");
        }

        public bool IsWindowsCheckboxChecked()
        {
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_windowsTagCheckbox).GetAttribute("class").Contains("checked");
        }

    }
}