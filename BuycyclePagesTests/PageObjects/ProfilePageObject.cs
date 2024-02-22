using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class ProfilePageObject : BasePageObject
	{
        private readonly By _profilePageContainer = By.XPath("//div[@class='profile-page-container']");
        private readonly By _userName = By.XPath("//a[contains(@class, 'profile-nav-link')]//strong");

        public ProfilePageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver,elementWaiter)
		{
		}

        public bool IsProfilePageObjectIsDisplayed() => _elementWaiter.WaitForElementDisplayedAndEnabled(_profilePageContainer).Displayed;

        public string GetUserName() => _elementWaiter.WaitForElementDisplayedAndEnabled(_userName).Text;
    }
}

