using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	
	public class EnduroCategoryPageObject : BasePageObject
	{
		private readonly By _enduroFilterTagElement = By.XPath("//div[@class='shop-top-filter']//span[text()='Enduro']"); //for any filters //div[@class='shop-top-filter']//div[@class='filter-tag']

        private readonly By _enduroCategoryCheckboxElement = By.XPath("//input[@value='enduro']");
       
        public EnduroCategoryPageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter) { }

        public bool IsEnduroCategoryPageIsDisplayed()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("window.scrollBy(0, 250);");
            return _elementWaiter.WaitForElementToBeVisible(_enduroFilterTagElement).Displayed;
        }

        public bool IsEnduroCheckboxChecked() => _elementWaiter.WaitForElementToBeSelected(_enduroCategoryCheckboxElement);
    }
}

