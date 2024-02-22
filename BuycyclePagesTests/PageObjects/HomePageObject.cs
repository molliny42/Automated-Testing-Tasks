using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class HomePageObject : BasePageObject
	{

        private readonly By _cookiesPopup = By.XPath("//div[@name='CybotCookiebotDialog']");
        private readonly By _acceptCookiesButton = By.XPath("//button[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']");
        private readonly By _homeSlider = By.XPath("//div[@class='home-head-slider slick-initialized slick-slider']");
        private readonly By _signButton = By.XPath("//button[@data-target='#login-modal']");
		private readonly By _userAvatar = By.XPath("//img[@alt='user avatar']");

		public HomePageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
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
                //Console.WriteLine("Cookies popup was not found."); // log4j
            }
        }

        public bool IsHomePageIsDisplayed() => _elementWaiter.WaitForElementDisplayedAndEnabled(_homeSlider).Displayed;

        public void NavigateToAuthorizationPage() => _elementWaiter.WaitForElementDisplayedAndEnabled(_signButton).Click();

        

        public void NavigateToProfilePageObject()
        {
            _elementWaiter.WaitForElementDisplayedAndEnabled(_userAvatar).Click();
        }
	}
}

