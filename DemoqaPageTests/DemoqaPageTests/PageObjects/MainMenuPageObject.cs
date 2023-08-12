using System;
using OpenQA.Selenium;


namespace DemoqaPageTests.PageObjects
{
    class MainMenuPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _homeContentElement = By.XPath("//div[contains(@class,'home-content')]");
        private readonly By _alertsWindowsButton = By.XPath("//h5[text()='Alerts, Frame & Windows']");

        public MainMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsMainMenuPageDisplayed() => _webDriver.FindElement(_homeContentElement).Displayed;
        
        public void NavigateToAlertsWindows()
        {
            var element = _webDriver.FindElement(_alertsWindowsButton);
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Click();
        }
    }
}