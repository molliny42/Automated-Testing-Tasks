using System;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class MainMenuPageObject : BasePageObject
	{

        private readonly By _cookiesPopup = By.XPath("//div[@name='CybotCookiebotDialog']");
        private readonly By _acceptCookiesButton = By.XPath("//button[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']");

        private readonly By _signButton = By.XPath("//button[@data-target='#login-modal']");
		private readonly By _userAvatar = By.XPath("//img[@alt='user avatar']");
		private readonly By _userName = By.XPath("//a[contains(@class, 'profile-nav-link')]//strong");

		public MainMenuPageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
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

        public AuthorizationPageObject NavigateToAuthorizationPage() //говорящие имена ClickOnSignInBtn
		{
            _elementWaiter.WaitForElementDisplayedAndEnabled(_signButton).Click();
			return new AuthorizationPageObject(_webDriver, _elementWaiter); 
		}

		public string getUserName()
		{
			_elementWaiter.WaitForElementDisplayedAndEnabled(_userAvatar).Click();
			string userName = _elementWaiter.WaitForElementDisplayedAndEnabled(_userName).Text;
			return userName;
			//
		}
	}
}

