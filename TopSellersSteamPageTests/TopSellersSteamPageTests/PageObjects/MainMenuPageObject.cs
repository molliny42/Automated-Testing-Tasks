using OpenQA.Selenium;

namespace TopSellersSteamPageTests.PageObjects
{
    public class MainMenuPageObject : BasePageObject
    {
        private readonly By _cookiesPopup = By.XPath("//div[@class='cookiepreferences_popup_content']");
        private readonly By _acceptCookiesButton = By.XPath("//div[@id='acceptAllButton']");
        private readonly By _homePageGutter = By.XPath("//div[@class='home_page_gutter']");
        private readonly By _topSellersButton = By.XPath("(//a[contains(@href, 'topsellers')])[1]\n");

        public MainMenuPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }
      
        public void CloseCookiesPopup()
        {
            try
            {
                if (_elementWaiter.WaitForElementDisplayedAndEnabled(_cookiesPopup).Displayed)
                {
                    _elementWaiter.WaitForElementDisplayedAndEnabled(_acceptCookiesButton).Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                 //Console.WriteLine("Cookies popup was not found.");
            }
        }

        public bool IsMainMenuPageIsDisplayed()
        {            
            return _elementWaiter.WaitForElementDisplayedAndEnabled(_homePageGutter).Displayed;
        }

        public TopSellersPageObject NavigateToTopSellers()
        {
            _elementWaiter.WaitForElementDisplayedAndEnabled(_topSellersButton).Click();
            return new TopSellersPageObject(_webDriver);
        } 
    }
}