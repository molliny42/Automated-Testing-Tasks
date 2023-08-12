using System;
using OpenQA.Selenium;

namespace DemoqaPageTests.PageObjects
{
	public class AlertsWindowsPageObject
	{
		private IWebDriver _webDriver;

        private readonly By _allertsWindowsPageHeader = By.XPath("//div[contains(@class, 'main-header') and text()='Alerts, Frame & Windows']");
        private readonly By _allertFromListButton = By.XPath("//span[text()='Alerts']");
        private readonly By _allertButton = By.XPath("//button[@id='alertButton']");

        

        public AlertsWindowsPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsAllertsWindowsPageDisplayed() => _webDriver.FindElement(_allertsWindowsPageHeader).Displayed;

        public void OpenAlertActions() => _webDriver.FindElement(_allertFromListButton).Click();

        public void ClickAlertButton() => _webDriver.FindElement(_allertButton).Click();

        public string GetAlertText()
        {
            IAlert alert = _webDriver.SwitchTo().Alert();
            return alert.Text;
        }

        public bool IsAlertTextEqual(string expectedText) =>  GetAlertText() == expectedText;

        public void AcceptAlert()
        {
            IAlert alert = _webDriver.SwitchTo().Alert();
            alert.Accept();
        }

    }
}

