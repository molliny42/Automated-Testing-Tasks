using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TopSellersSteamPageTests
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

        public IWebElement WaitForElementDisplayedAndEnabled(By locator)
        {
            return _wait.Until(driver =>
            {
                IWebElement tempElement = driver.FindElement(locator);
                return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
            });
        }
    }
}