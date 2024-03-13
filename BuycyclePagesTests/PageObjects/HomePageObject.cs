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
                if (_elementWaiter.WaitForElementToBeVisible(_cookiesPopup)?.Displayed ?? false)
                {
                    _elementWaiter.WaitForElementToBeClickable(_acceptCookiesButton)?.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                // Log the absence of the popup using your preferred logging mechanism (e.g., log4j)
                Console.WriteLine("Cookies popup was not found.");
            }
        }

        public bool IsHomePageDisplayed() => _elementWaiter.WaitForElementToBeVisible(_homeSlider)?.Displayed ?? false;

        public void NavigateToAuthorizationPage() => _elementWaiter.WaitForElementToBeVisible(_signButton)?.Click();

        public void NavigateToProfilePageObject()
        {
            _elementWaiter.WaitForElementToBeClickable(_userAvatar)?.Click();
        }
    }
}