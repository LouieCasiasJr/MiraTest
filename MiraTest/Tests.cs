using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using MiraTest.PageObjects;


namespace MiraTest
{
    [TestFixture(TestHelper.browserType.chrome)]
    [TestFixture(TestHelper.browserType.firefox)]
    [TestFixture(TestHelper.browserType.internetexplorer)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SearchTests
    {
        private TestHelper Test;
		private IWebDriver driver;
        private TestHelper.browserType browser;

        public SearchTests(TestHelper.browserType webbrowser)
        {
            browser = webbrowser;
        }

        [SetUp]
        public void BeforeEachTest()
        {
			Test = new TestHelper(browser);
			driver = Test.driver;
            Test.LaunchStartPage();
        }

        [TearDown]
        public void AfterEachTest()
        {
            Test.QuitBrowser();
        }

        [Test, Property("Description", "Searching for 'nothing'.")]
        public void SearchforNothing()
        {
			string searchTerm = "nothing";
            HomePage homePage = new HomePage(driver);
			SearchResultPage resultPage = homePage.Search(searchTerm);

            resultPage.ValidateReturnsNoResults();
        }

		[Test, Property("Description", "Use 'Women' preset search.")]
		public void WomenPresetSearch()
		{
			HomePage homePage = new HomePage(driver);
            SearchResultPage resultPage = homePage.WomenSearchClick();

            resultPage.ValidateReturnedResultCounts();
		}





    }
}
