using OpenQA.Selenium;

namespace BuycyclePagesTests.PageObjects
{
	public class CategoryPageObject : BasePageObject
	{
        private readonly By _filteredBikeCount = By.XPath("//b[@class='mr-1']");
        private By GetCategoryFilterButtonXPath(string categoryFilter) => By.XPath($"//button[@data-target='#{categoryFilter}']");
        private By GetOpenedCategoryFilterXPath(string categoryFilter) => By.XPath($"//div[@id='{categoryFilter}' and @class='collapse show']");
        private By GetInputFieldCategoryFilterXPath(string categoryFilter) => By.XPath($"//div[@id='{categoryFilter}']//div[contains(@class, 'select-trigger')]");
        private By GetCheckboxFromMainListXPath(string checkboxName) => By.XPath($"//input[@value='{checkboxName}']");
        private readonly By _dropdownCheckboxes = By.XPath("//div[@aria-hidden='false']//ul[contains(@class, 'el-select-dropdown__list')]");
        private By GetCheckboxFromDropdownXPath(string checkboxName) => By.XPath($"//li[contains(@class, 'select-dropdown') and text()='{checkboxName}']");
        private By GetCheckboxFromInputFieldXPath(string checkboxName) => By.XPath($"//div[@id='frame-sizes']//span[@class='el-select__tags-text' and text()='{checkboxName}']");

        private By GetFilterTagXPath(string checkboxName) => By.XPath($"//div[@class='shop-top-filter']//span[text()='{checkboxName}']");
                          
        public CategoryPageObject(IWebDriver webDriver, ElementWaiter elementWaiter) : base(webDriver, elementWaiter) { }

        //TODO
            public bool IsCategoryPageDisplayed(string filterName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("window.scrollBy(0, 250);");

            return _elementWaiter.WaitForElementToBeVisible(GetFilterTagXPath(filterName))?.Displayed ?? false;
        }

        public void ToggleOnCategoryFilter(string categoryFilter) =>
            _elementWaiter.WaitForElementToBeClickable(GetCategoryFilterButtonXPath(categoryFilter))?.Click();
                
        public bool IsCategoryFilterToggleOn(string categoryFilter) =>
            _elementWaiter.WaitForElementToBeVisible(GetOpenedCategoryFilterXPath(categoryFilter))?.Displayed ?? false;
      
        public void ClickToInputFieldCategoryFilter(string categoryFilter) =>
            _elementWaiter.WaitForElementToBeClickable(GetInputFieldCategoryFilterXPath(categoryFilter))?.Click();

        public bool IsInputFocused(string categoryFilter)
        {
            var inputField = _elementWaiter.WaitForElementToBeClickable(GetInputFieldCategoryFilterXPath(categoryFilter));
            return inputField?.GetAttribute("aria-describedby") != null;
        }

        public bool IsDropdownCheckboxesOpen() => _elementWaiter.WaitForElementToBeVisible(_dropdownCheckboxes)?.Displayed ?? false;

        public void ClickToCheckboxFromMainList(string checkboxName) =>
            _elementWaiter.WaitForElementToBeClickable(GetCheckboxFromMainListXPath(checkboxName))?.Click();

        public void ClickToCheckboxFromDropdown(string checkboxName)
        {
            _elementWaiter.WaitForElementToBeClickable(GetCheckboxFromDropdownXPath(checkboxName))?.Click();
        }

        public bool IsCheckboxFromMainListChecked(string checkboxName) => _elementWaiter.WaitForElementToBeSelected(GetCheckboxFromMainListXPath(checkboxName));

        public bool IsCheckboxFromDropdownChecked(string checkboxName) => _elementWaiter.WaitForElementToBeVisible(GetCheckboxFromInputFieldXPath(checkboxName))?.Displayed ?? false;

        public bool IsFilterTagDisplayed(string checkboxName) => _elementWaiter.WaitForElementToBeVisible(GetFilterTagXPath(checkboxName))?.Displayed ?? false;

        public string GetBikeCount() => _elementWaiter.WaitForElementToBeVisible(_filteredBikeCount)?.Text ?? string.Empty;
    }
}