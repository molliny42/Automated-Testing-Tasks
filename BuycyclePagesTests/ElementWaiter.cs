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
                return _wait.Until(ExpectedConditions.ElementToBeSelected(locator));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}

//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;

//namespace BuycyclePagesTests
//{
//    public class ElementWaiter
//    {
//        private IWebDriver _webDriver; 
//        private WebDriverWait _wait;

//        public ElementWaiter(IWebDriver webDriver, TimeSpan timeout)
//        {
//            _webDriver = webDriver;
//            _wait = new WebDriverWait(webDriver, timeout);
//        }

//        public IWebElement WaitForElementDisplayedAndEnabled(By locator)
//        {
//            try
//            {
//                return _wait.Until(driver =>
//                {

//                    IWebElement tempElement = driver.FindElement(locator);
//                    if (tempElement.Displayed && tempElement.Enabled)
//                    {
//                        return tempElement;
//                    }
//                    else
//                    {
//                        throw new NoSuchElementException("Element with locator '{locator}' is not displayed or enabled.");
//                    }
//                });
//            }
//            catch (NoSuchElementException)
//            {
//                throw; // Проброс исключения вверх
//            }
//        }

//        public void WaitForCondition(Func<bool> condition)
//        {
//            _wait.Until(driver => condition());
//        }

//    }
//}
