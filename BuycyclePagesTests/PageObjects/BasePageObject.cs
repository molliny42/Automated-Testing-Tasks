using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
    public class BasePageObject
    {
        protected IWebDriver _webDriver;
        protected ElementWaiter _elementWaiter;

        public BasePageObject(IWebDriver webDriver, ElementWaiter elementWaiter)
        {
            _webDriver = webDriver;
            _elementWaiter = elementWaiter;
        }
    }
}
