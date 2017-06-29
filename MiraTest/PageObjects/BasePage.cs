using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;
            
namespace MiraTest.PageObjects
{
    public class BasePage
    {
        protected IWebDriver webDriver;

        [FindsBy(How = How.Id, Using = "header_logo")]
        [CacheLookup]
        public IWebElement yourLogo;

        [FindsBy(How = How.Id, Using = "search_query_top")]
        [CacheLookup]
        public IWebElement searchField;

        [FindsBy(How = How.Name, Using = "submit_search")]
        [CacheLookup]
		public IWebElement searchButton;

        [FindsBy(How = How.PartialLinkText, Using = "controller=order")]
        [CacheLookup]
		public IWebElement shoppingCart;

        [FindsBy(How = How.CssSelector, Using = "[title=Women]")]
		[CacheLookup]
		public IWebElement women_SearchButton;

		[FindsBy(How = How.CssSelector, Using = "[title=Dresses]")]
		[CacheLookup]
		public IWebElement dresses_SearchButton;

		[FindsBy(How = How.CssSelector, Using = "[title=T-shirts]")]
		[CacheLookup]
		public IWebElement tshirts_SearchButton;





		public string title { get; protected set; }

		public static string GetUrl(string key)
		{
            return ConfigurationManager.AppSettings[key];
		}

    }
}
