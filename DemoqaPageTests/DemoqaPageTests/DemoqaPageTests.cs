using DemoqaPageTests.PageObjects;
using OpenQA.Selenium;

namespace DemoqaPageTests;

public class Tests
{
    private IWebDriver _webDriver;

    [SetUp]
    public void Setup()
    {
        _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://demoqa.com/");
       
    }

    [Test]
    public void Test1()
    {
        var mainMenuPage = new MainMenuPageObject(_webDriver);
        var alertsWindowsPage = new AlertsWindowsPageObject(_webDriver);

        Assert.IsTrue(mainMenuPage.IsMainMenuPageDisplayed(), "'Main menu' page is not opened");

        mainMenuPage.NavigateToAlertsWindows();
        Assert.IsTrue(alertsWindowsPage.IsAllertsWindowsPageDisplayed(), "'Alerts, Frame & Windows' page is not opened");

        alertsWindowsPage.OpenAlertActions();
        alertsWindowsPage.ClickAlertButton();
        Assert.IsTrue(alertsWindowsPage.IsAlertTextEqual(DataForTests.ExpectedAlertButtonText), "The text in the Alert window does not match");
        alertsWindowsPage.AcceptAlert();
    }

    [TearDown]
    public void TearDown()
    {
        //_webDriver.Quit();
    }
}
