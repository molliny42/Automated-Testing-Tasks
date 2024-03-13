using NUnit.Framework;
using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects;

public class Tests
{
    private IWebDriver _webDriver;
    private ElementWaiter _elementWaiter;


    [SetUp]
    public void Setup()
    {
        _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
        _elementWaiter = new ElementWaiter(_webDriver, TimeSpan.FromSeconds(4));

        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl("https://buycycle.com");
    }

    [Test]
    public void Test1()
    {
        var homePage = new HomePageObject(_webDriver, _elementWaiter);
        homePage.CloseCookiesPopup();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened.");

        homePage.NavigateToAuthorizationPage();
        var authPage = new AuthorizationPageObject(_webDriver, _elementWaiter);
        Assert.That(authPage.IsAuthorizationPageDisplayed(), Is.EqualTo(true), "Authorization page is not opened.");
       
        authPage.InputLogin(JsonParser.GetString("credentials.json", JsonKeys.EMAIL));// consts
        authPage.WaitForLoginInput();
        authPage.InputPassword(JsonParser.GetString("credentials.json", JsonKeys.PASSWORD));
        authPage.WaitForPasswordInput();
        authPage.ConfirmLogin();
        Assert.That(homePage.IsHomePageIsDisplayed(), Is.EqualTo(true), "Home page is not opened.");

        homePage.NavigateToProfilePageObject();
        var profilePage = new ProfilePageObject(_webDriver, _elementWaiter);
        Assert.That(profilePage.IsProfilePageObjectIsDisplayed(), Is.EqualTo(true), "Profile page is not opened.");

        string actualUserName = profilePage.GetUserName();
        Assert.That(actualUserName, Is.EqualTo(JsonParser.GetString("credentials.json", JsonKeys.FULLNAME)), "User name is wrong.");

        profilePage.NavigateToEnduroCategory();
        var categoryPage = new CategoryPageObject(_webDriver, _elementWaiter);
        Assert.That(categoryPage.IsCategoryPageDisplayed("Enduro"), Is.EqualTo(true), "'Enduro' category page is not opened.");//consts
        Assert.That(categoryPage.IsCategoryFilterToggleOn("categories"), Is.EqualTo(true), "'Categories' category filter is not opened.");
        Assert.That(categoryPage.IsCheckboxFromMainListChecked("enduro"), Is.EqualTo(true), "'Enduro' checkbox is not selected.");
        Assert.That(categoryPage.IsFilterTagDisplayed("Enduro"), Is.EqualTo(true), "'Enduro' filter tag is not displayed.");

        string initialBycyclesCount = categoryPage.GetBikeCount();

        categoryPage.ToggleOnCategoryFilter("frame-sizes");
        Assert.That(categoryPage.IsCategoryFilterToggleOn("frame-sizes"), Is.EqualTo(true), "'Frame-sizes' category filter is not opened.");
        categoryPage.ClickToInputFieldCategoryFilter("frame-sizes");
        Assert.That(categoryPage.IsInputFocused("frame-sizes"), Is.EqualTo(true), "Input field for 'Frame-sizes' category filter is not focused before opening the dropdown list.");
        Assert.That(categoryPage.IsDropdownCheckboxesOpen(), Is.EqualTo(true), "Dropdown list is not visible after clicking on input field.");
        categoryPage.ClickToCheckboxFromDropdown("M (53-55)");
        Assert.That(categoryPage.IsCheckboxFromDropdownChecked("M (53-55)"), Is.EqualTo(true), "'M (53-55)' checkbox is not selected.");
        Assert.That(categoryPage.IsFilterTagDisplayed("M (53-55)"), Is.EqualTo(true), "'M (53-55)' filter tag is not displayed.");

        categoryPage.ToggleOnCategoryFilter("brands");
        Assert.That(categoryPage.IsCategoryFilterToggleOn("brands"), Is.EqualTo(true), "'Brands' category filter is not opened.");
        categoryPage.ClickToInputFieldCategoryFilter("brands");
        Assert.That(categoryPage.IsInputFocused("brands"), Is.EqualTo(true), "Input field for 'Brands' category filter is not focused before opening the dropdown list.");
        Assert.That(categoryPage.IsDropdownCheckboxesOpen(), Is.EqualTo(true), "Dropdown list is not visible after clicking on input field.");
        categoryPage.ClickToCheckboxFromDropdown("CUBE");
        Assert.That(categoryPage.IsCheckboxFromDropdownChecked("CUBE"), Is.EqualTo(true), "'CUBE' checkbox is not selected.");
        Assert.That(categoryPage.IsFilterTagDisplayed("CUBE"), Is.EqualTo(true), "'CUBE' filter tag is not displayed.");

        categoryPage.ToggleOnCategoryFilter("frame-material");
        Assert.That(categoryPage.IsCategoryFilterToggleOn("frame-material"), Is.EqualTo(true), "'Frame-material' category filter is not opened.");
        categoryPage.ClickToCheckboxFromMainList("carbon");
        Assert.That(categoryPage.IsCheckboxFromMainListChecked("carbon"), Is.EqualTo(true), "'Carbon' checkbox is not selected.");
        Assert.That(categoryPage.IsFilterTagDisplayed("Carbon"), Is.EqualTo(true), "'Carbon' filter tag is not displayed.");


        //categoryPage.ToggleOnCategoryFilter("colors");
        //Assert.That(categoryPage.IsCategoryFilterToggleOn("colors"), Is.EqualTo(true), "'Colors' category filter is not opened.");
               
        bool valueChanged = categoryPage.WaitForValueChange(() => categoryPage.GetBikeCount(), initialBycyclesCount, TimeSpan.FromSeconds(4));
        string filteredBycyclesCount = categoryPage.GetBikeCount();
        Assert.That(valueChanged, Is.EqualTo(true), "Count of bicycles has not changed after filtering.");

        string initialLastBikePrice = categoryPage.GetLastBikePrice();
        categoryPage.ClickToLastBike();
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(6000);
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}