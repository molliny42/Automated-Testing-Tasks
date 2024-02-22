using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class AuthorizationPageObject : BasePageObject
	{
		private readonly By _loginForm = By.XPath("//form[@id='login--from']");
		private readonly By _loginInputField = By.XPath("//form[@id='login--from']//input[@name='email']");
		private readonly By _passwordInputField = By.XPath("//form[@id='login--from']//input[@name='password']");
		private readonly By _enterLoginButton = By.XPath("//form[@id='login--from']//button[@type='submit']");

        public AuthorizationPageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
        {
		}

		public bool IsAuthorizationPageDisplayed() => _elementWaiter.WaitForElementDisplayedAndEnabled(_loginForm).Displayed;

        public HomePageObject Login(string login, string password)
		{
			_elementWaiter.WaitForElementDisplayedAndEnabled(_loginInputField).SendKeys(login);
			_elementWaiter.WaitForElementDisplayedAndEnabled(_passwordInputField).SendKeys(password);
			_elementWaiter.WaitForElementDisplayedAndEnabled(_enterLoginButton).Click();
			return new HomePageObject(_webDriver, _elementWaiter);
			//разбить метод на составляющие 
		}
    }
}

