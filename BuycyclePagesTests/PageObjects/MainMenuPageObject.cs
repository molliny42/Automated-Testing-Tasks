using System;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class MainMenuPageObject : BasePageObject
	{

        private readonly By _signButton = By.XPath("//button[@data-target='#login-modal']");

		public MainMenuPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }

		public AuthorizationPageObject SignIn()
		{
            _elementWaiter.WaitForElementDisplayedAndEnabled(_signButton).Click();
			return new AuthorizationPageObject(_webDriver);
		}
	}
}

