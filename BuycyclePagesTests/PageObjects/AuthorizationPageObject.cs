using System;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class AuthorizationPageObject : BasePageObject
	{
		private readonly By _loginInputField = By.XPath("//form[@id='login--from']//input[@name='email']");
		private readonly By _passwordInputField = By.XPath("//form[@id='login--from']//input[@name='password']");
		private readonly By _enterLoginButton = By.XPath("//form[@id='login--from']//button[@type='submit']");

        public AuthorizationPageObject(IWebDriver webDriver) : base(webDriver)
		{
		}

		public MainMenuPageObject Login(string login, string password)
		{
			_elementWaiter.WaitForElementDisplayedAndEnabled(_loginInputField).SendKeys(login);
			_elementWaiter.WaitForElementDisplayedAndEnabled(_passwordInputField).SendKeys(password);
			_elementWaiter.WaitForElementDisplayedAndEnabled(_enterLoginButton).Click();
			return new MainMenuPageObject(_webDriver);
			//разбить метод на состовляющие 
		}
    }
}

