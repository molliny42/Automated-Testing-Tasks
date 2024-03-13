using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BuycyclePagesTests
{
    public class ElementWaiter
    {
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        public ElementWaiter(IWebDriver webDriver, TimeSpan timeout)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(webDriver, timeout);
        }

        public IWebElement? WaitForElementToBeVisible(By locator)
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }
        public IWebElement? WaitForElementToBeClickable(By locator)
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public bool WaitForCondition(Func<bool> condition)
        {
            try
            {
                _wait.Until(_ => condition());
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool WaitForElementToBeSelected(By locator)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementToBeSelected(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

    }
}