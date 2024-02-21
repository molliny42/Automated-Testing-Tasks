using System;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
    public class BasePageObject
    {
        protected IWebDriver _webDriver;
        protected ElementWaiter _elementWaiter;

        public BasePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _elementWaiter = new ElementWaiter(_webDriver, TimeSpan.FromSeconds(3));
        }
    }
}

