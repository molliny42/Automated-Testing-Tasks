using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TopSellersSteamPageTests.PageObjects
{
    public class TopSellersPageObject : BasePageObject
    {
        private readonly By _gamesCountFromTextElement = By.XPath("//div[@id='search_results_filtered_warning_persistent']/div[1]");
        private readonly By _singleplayerTagCheckbox = By.XPath("//div[@data-loc='Singleplayer']");
        private readonly By _actionTagCheckbox = By.XPath("//div[@data-loc='Action']");
        private readonly By _windowsTagCheckbox = By.XPath("//div[@data-loc='Windows']");
        private readonly By _simulationTagCheckbox = By.XPath("//div[@data-loc='Simulation']");
        private readonly By _gameElement = By.XPath("//a[contains(@class, 'search_result_row')]");

        private readonly By _finalPriceFirstGameElement = By.XPath("//a[contains(@class, 'search_result_row')][1]/div[@class='discount_final_price']");


        public TopSellersPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }

        public bool IsTopSellersPageDisplayed()
        {
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_gamesCountFromTextElement).Displayed;
        }

        public int GetGamesCountFromText()
        {
            string gamesCountTextElement = _elementWaiter.WaitForElementDisplayedAndEnabled(_gamesCountFromTextElement).Text;

            Match match = Regex.Match(gamesCountTextElement, @"\d{1,3}(?:,\d{3})*");

            if (match.Success)
            {
                string numberText = match.Value.Replace(",", "");
                int gamesCount = int.Parse(numberText);
                return gamesCount;
            }

            return 0;
        }

        /// <summary>
        /// Scrolls to the bottom of the page until no more content is loaded.
        /// </summary>
        public void ScrollToBottom()
        {
            long initialHeight = (long)((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.body.scrollHeight");

            while (true)
            {
                ((IJavaScriptExecutor)_webDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                Thread.Sleep(5000);

                long newHeight = (long)((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.body.scrollHeight");

                if (newHeight == initialHeight)
                {
                    break;
                }

                initialHeight = newHeight;
            }
        }

        /// <summary>
        /// Gets the number of displayed games on the page.
        /// </summary>
        /// <returns>The count of displayed games.</returns>
        public int GetDisplayedGamesCount()
        {
            IReadOnlyList<IWebElement> gamesElements = _webDriver.FindElements(_gameElement);

            return gamesElements.Count;
        }


        public void ApplyFilters()
        {
            _elementWaiter.WaitForElementDisplayedAndEnabled(_singleplayerTagCheckbox).Click();
            _elementWaiter.WaitForElementDisplayedAndEnabled(_windowsTagCheckbox).Click();
            _elementWaiter.WaitForElementDisplayedAndEnabled(_actionTagCheckbox).Click();
            _elementWaiter.WaitForElementDisplayedAndEnabled(_simulationTagCheckbox).Click();
        }

        //TODO
        public void WaitForFiltersApplied()
        {
            //WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            //wait.Until(ExpectedConditions.StalenessOf(_webDriver.FindElement(_gamesCountFromTextElement)));

            Thread.Sleep(1000);
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

        public bool IsSimulationCheckboxChecked()
        {
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_simulationTagCheckbox).GetAttribute("class").Contains("checked");
        }

    }
}