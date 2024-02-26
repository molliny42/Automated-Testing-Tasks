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

        public IWebElement WaitForElementToBeVisible(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public void WaitForCondition(Func<bool> condition)
        {
            _wait.Until(_ => condition());
        }

        public bool WaitForElementToBeSelected(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeSelected(locator));
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
