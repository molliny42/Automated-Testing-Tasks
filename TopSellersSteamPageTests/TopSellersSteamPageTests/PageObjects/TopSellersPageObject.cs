using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace TopSellersSteamPageTests.PageObjects
{
    public class TopSellersPageObject : BasePageObject
    {
        private readonly IDictionary<Elements, By> _elementMap = new Dictionary<Elements, By>
        {
            { Elements._gamesCountFromTextElement, By.XPath("//div[@id='search_results_filtered_warning_persistent']/div[not(contains(@class, 'settings_tab'))]") },
            { Elements._singleplayerTagCheckbox, By.XPath("//div[@data-loc='Singleplayer']") },
            { Elements._actionTagCheckbox, By.XPath("//div[@data-loc='Action']") },
            { Elements._windowsTagCheckbox, By.XPath("//div[@data-loc='Windows']") },
            { Elements._simulationTagCheckbox, By.XPath("//div[@data-loc='Simulation']") },
            //{ Elements._gameElement, By.XPath("//a[contains(@class, 'search_result_row')]") },
            { Elements._gameElement, By.XPath("//a[contains(@class, 'search_result_row') and not(contains(@style, 'display: none'))]") },

            { Elements._firstGameElement, By.XPath("//a[contains(@class, 'search_result_row')][1]") },
            { Elements._firstGameNameElement, By.XPath("//a[contains(@class, 'search_result_row')][1]//span[@class='title']") },
            { Elements._firstGameReleaseDateElement, By.XPath("//a[contains(@class, 'search_result_row')][1]//div[contains(@class, 'search_released')]") },
            { Elements._priceFirstGameElement, By.XPath("(//a[contains(@class, 'search_result_row')][1]//div[@class='discount_final_price']) | (//a[contains(@class, 'search_result_row')][1]//div[@data-price-final='0'])") },
        };

        public TopSellersPageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
        {
        }

        private IWebElement GetElement(Elements element) => _elementWaiter.WaitForElementDisplayedAndEnabled(_elementMap[element]);

        public bool IsTopSellersPageDisplayed() => GetElement(Elements._gamesCountFromTextElement).Displayed;


        public int GetGamesCountFromText()
        {
            try
            {
                string gamesCountTextElement = GetElement(Elements._gamesCountFromTextElement).Text;
                Match match = Regex.Match(gamesCountTextElement, @"\d{1,3}(?:,\d{3})*");

                if (match.Success)
                {
                    string gamesCountString = match.Value.Replace(",", "");
                    int gamesCount = int.Parse(gamesCountString);
                    return gamesCount;
                }
                throw new Exception("Failed to extract games count from the text.");                             
            }

            catch (NoSuchElementException)
            {
                throw new Exception("Test failed due to NoSuchElementException: Element '_gamesCountFromTextElement' not found or not displayed in 'TopSellersPageObject' class.");
            }
        }

        public void ApplyFilter(Elements element) => GetElement(element).Click();
                
        public bool IsCheckboxChecked(Elements element) => GetElement(element).GetAttribute("class").Contains("checked");

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

        public int GetDisplayedGamesCount()
        {
            IReadOnlyList<IWebElement> gamesElements = _webDriver.FindElements(_elementMap[Elements._gameElement]);
            return gamesElements.Count;
        }

        public string GetFirstGamePrice()
        {
            try
            {
                string price = GetElement(Elements._priceFirstGameElement).Text.Replace(",", ".");
                Match match = Regex.Match(price, @"\d+\.\d+");
                return match.Success ? match.Value : "0";
            }

            catch (NoSuchElementException)
            {
                throw new Exception("Test failed due to NoSuchElementException: Element '_priceFirstGameElement' not found or not displayed in 'TopSellersPageObject' class.");
            }

        }

        public Game GetFirstGame()
        {
            string name = GetElement(Elements._firstGameNameElement).Text;
            string releaseDate = GetElement(Elements._firstGameReleaseDateElement).Text;
            string price = GetFirstGamePrice();
            return new Game(name, releaseDate, price);
        }

        public void NavigateToGamePage() => GetElement(Elements._firstGameElement).Click();

        public enum Elements
        {
            _gamesCountFromTextElement,
            _singleplayerTagCheckbox,
            _actionTagCheckbox,
            _windowsTagCheckbox,
            _simulationTagCheckbox,
            _gameElement,
            _firstGameElement,
            _firstGameNameElement,
            _firstGameReleaseDateElement,
            _priceFirstGameElement
        }
    }
}