using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MiraTest.PageObjects
{
    public class SearchResultPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "p.alert.alert-warning")]
		public IWebElement searchWarningMessage;

		[FindsBy(How = How.CssSelector, Using = "span.heading-counter")]
		public IWebElement searchCountHeading;

		[FindsBy(How = How.CssSelector, Using = "div.breadcrumb a.home")]
		public IWebElement homePageBreadcrumb;

        [FindsBy(How = How.CssSelector, Using = "span.navigation_page", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "div.breadcrumb.clearfix", Priority = 1)]
		public IWebElement navigationPageTitle;

		[FindsBy(How = How.CssSelector, Using = "span.cat-name")]
		public IWebElement resultCategoryLabel;

		[FindsBy(How = How.CssSelector, Using = "ul.product_list.row.grid", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "ul.product_list.row.list", Priority = 1)]
		public IWebElement returnResultPresenter;

        public int ReturnItemDisplayCount()
        {
            return returnResultPresenter.FindElements(By.XPath("/*")).Count;
        }

        public void ValidateReturnsNoResults()
        {
            Assert.IsTrue(this.searchWarningMessage.innerTextContains("no results"), "No results error message not shown as expected");
			Assert.IsTrue(this.searchCountHeading.innerTextContains("0"), "Search count is not shown as '0'");
        }

		public void ValidateReturnedResultCounts()
		{
            int returnsCounted = this.searchCountHeading.Text.innerNumeric();
            int returnsShown = ReturnItemDisplayCount();

            Assert.IsTrue(webDriver.isElementHidden(this.searchWarningMessage), "Results error message is shown");
            Assert.IsTrue(returnsCounted > 0, "Search count is not greater than '0'");
            Assert.IsTrue(returnsShown == returnsCounted, string.Format("expected {0} items in return, {1} displayed", returnsCounted, returnsShown));
		}

		public SearchResultPage(IWebDriver driver, string query)
        {
			this.webDriver = driver;
            PageFactory.InitElements(this.webDriver, this);

            if (!(this.navigationPageTitle.Text.ContainsCaseInsensitive("search") || this.navigationPageTitle.Text.ContainsCaseInsensitive(query)))
			{
				throw new InvalidElementStateException("This is not the proper search result page!");
			}
        }
    }
}
