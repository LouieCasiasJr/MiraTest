using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MiraTest.PageObjects
{
    public class HomePage : BasePage
    {
        public static readonly Uri URL = new Uri(GetUrl("HomePage"));

        #region elements
        [FindsBy(How = How.Id, Using = "homepage-slider")]
		[CacheLookup]
		public IWebElement imageSlider;

		[FindsBy(How = How.CssSelector, Using = "a.homefeatured")]
		[CacheLookup]
		public IWebElement popularLink;

		[FindsBy(How = How.CssSelector, Using = "a.blockbestsellers")]
		[CacheLookup]
		public IWebElement bestSellersLink;

		[FindsBy(How = How.Id, Using = "homefeatured")]
		public IWebElement popularItemGridDisplay;

		[FindsBy(How = How.Id, Using = "blockbestsellers")]
		public IWebElement bestSellersGridDisplay;
        #endregion

        public HomePage(IWebDriver webDriver)
		{
			this.webDriver = webDriver;
			this.title = this.webDriver.Title;

			if (!this.webDriver.Url.Contains(URL.ToString()))
			{
				throw new InvalidElementStateException("This is not the Home page");
			}

			PageFactory.InitElements(this.webDriver, this);
		}

		

        public void ViewPopularItems()
        {
            popularLink.ButtonClick();
        }

		public void ViewBestSellerItems()
		{
			bestSellersLink.ButtonClick();
		}

		public SearchResultPage Search(string query)
		{
			base.searchField.SendKeys(query);
            base.searchButton.ButtonClick();

            return NewResultPage(query);
		}

        public SearchResultPage WomenSearchClick()
        {
            base.women_SearchButton.ButtonClick();

            return NewResultPage("women");
        }

		public SearchResultPage DressesSearchClick()
		{
			base.dresses_SearchButton.ButtonClick();

			return NewResultPage("dresses");
		}

		public SearchResultPage TshirtsSearchClick()
		{
            base.tshirts_SearchButton.ButtonClick();

			return NewResultPage("t-shirts");
		}

        public SearchResultPage NewResultPage(string query)
        {
			SearchResultPage resultPage = new SearchResultPage(webDriver, query);
			return resultPage;
        }
    }
}
