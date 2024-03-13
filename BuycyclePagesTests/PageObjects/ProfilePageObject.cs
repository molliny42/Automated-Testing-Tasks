using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
    public class ProfilePageObject : BasePageObject
    {
        private readonly By _profilePageContainer = By.XPath("//div[@class='profile-page-container']");
        private readonly By _userName = By.XPath("//a[contains(@class, 'profile-nav-link')]//strong");
        private readonly By _enduroCategoryHeaderLink = By.XPath("//a[@data-text='Enduro']");

        public ProfilePageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter)
        {
        }

        public bool IsProfilePageObjectDisplayed() => _elementWaiter.WaitForElementToBeClickable(_profilePageContainer)?.Displayed ?? false;

        public string GetUserName() =>
            _elementWaiter.WaitForElementToBeClickable(_userName)?.Text ?? string.Empty;


        public void NavigateToEnduroCategory()
        {
            _elementWaiter.WaitForElementToBeClickable(_enduroCategoryHeaderLink)?.Click();
        }
    }
}