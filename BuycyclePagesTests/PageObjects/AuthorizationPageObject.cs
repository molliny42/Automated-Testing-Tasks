using System;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class AuthorizationPageObject : BasePageObject
	{
		private readonly By _loginInputField = By.XPath("//form[@id='login--from']//input[@name='email']");

		public AuthorizationPageObject(IWebDriver webDriver) : base(webDriver)
		{
		}

		public MainMenuPageObject Login(string login, string password)
		{
			_elementWaiter.WaitForElementDisplayedAndEnabled(_loginInputField).SendKeys(login);

			return new MainMenuPageObject(_webDriver);
		}
	}
}

