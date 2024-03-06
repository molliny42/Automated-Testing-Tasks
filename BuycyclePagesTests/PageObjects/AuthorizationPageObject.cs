using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class AuthorizationPageObject : BasePageObject
	{
		private readonly IDictionary<Elements, By> _elementMap = new Dictionary<Elements, By>
		{
			{ Elements._loginForm, By.XPath("//form[@id='login--from']") },
			{ Elements._loginInputField, By.XPath("//form[@id='login--from']//input[@name='email']") },
			{ Elements._passwordInputField, By.XPath("//form[@id='login--from']//input[@name='password']") },
			{ Elements._enterLoginButton, By.XPath("//form[@id='login--from']//button[@type='submit']") },
		};

		public AuthorizationPageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
		{
		}

        private IWebElement GetElement(Elements element)
        {
            var clickableElement = _elementWaiter.WaitForElementToBeClickable(_elementMap[element]);
            if (clickableElement == null)
            {
                throw new NoSuchElementException("Element not found or not clickable");
            }
            return clickableElement;
        }

        private void WaitForInputValue(Elements element) =>
			_elementWaiter.WaitForCondition(() =>
			GetElement(element).GetAttribute("value").Length > 0);

        public bool IsAuthorizationPageDisplayed() => GetElement(Elements._loginForm).Displayed;

		public void InputLogin(string login) => GetElement(Elements._loginInputField).SendKeys(login);

        public void WaitForLoginInput() => WaitForInputValue(Elements._loginInputField);

        public void InputPassword(string password) => GetElement(Elements._passwordInputField).SendKeys(password);

        public void WaitForPasswordInput() => WaitForInputValue(Elements._passwordInputField);

        public void ConfirmLogin() => GetElement(Elements._enterLoginButton).Click();

        public enum Elements
		{
            _loginForm,
            _loginInputField,
            _passwordInputField,
            _enterLoginButton
        }
	}
}