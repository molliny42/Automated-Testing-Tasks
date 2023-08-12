﻿using OpenQA.Selenium;

namespace TopSellersSteamPageTests.PageObjects
{
    public class BasePageObject
    {
        protected IWebDriver _webDriver;
        protected ElementWaiter _elementWaiter;

        public BasePageObject(IWebDriver webDriver, ElementWaiter elementWaiter)
        {
            _webDriver = webDriver;
            _elementWaiter = new ElementWaiter(_webDriver, TimeSpan.FromSeconds(5));
        }
    }
}

