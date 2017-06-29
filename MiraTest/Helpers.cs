using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using MiraTest.PageObjects;
using System.Text.RegularExpressions;
using System.Linq;

namespace MiraTest
{
    public class TestHelper
    {
        public enum browserType { chrome, firefox, internetexplorer };

        public IWebDriver driver;

        public TestHelper(browserType browser)
        {
            switch (browser)
            {
                case browserType.internetexplorer:
                    driver = InternetExplorerTestDriver();
                    break;
                case browserType.firefox:
                    break;
                case browserType.chrome:
                    driver = ChromeTestDriver();
                    break;
                default:
                    break;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public IWebDriver ChromeTestDriver()
        {
            ChromeOptions opts = new ChromeOptions();
            opts.AddArguments("--incognito");
            return new ChromeDriver("/usr/local/bin", opts);
        }

        public IWebDriver InternetExplorerTestDriver()
        {
            InternetExplorerOptions opts = new InternetExplorerOptions();
			opts.ForceCreateProcessApi = true;
			opts.BrowserCommandLineArguments = "-private";
            return new InternetExplorerDriver(opts);
        }

        public IWebDriver FirefoxTestDriver()
		{
            return new FirefoxDriver();
		}

        public void LaunchStartPage()
        {
            driver.Navigate().GoToUrl(BasePage.GetUrl("StartPage"));
        }

        public void QuitBrowser()
        {
            driver.Quit();
        }
    }

    public static class ControlHelpers
    {
        

        public static void MakeCleanEntry(this IWebElement field, string text)
        {
            field.MakeEmpty();
            if (!(text == null || text == ""))
                field.SendKeys(text);
        }

        private static void MakeEmpty(this IWebElement field)
        {
            field.SendKeys("");
            for (int i = 0; i < 70; i++)
                field.SendKeys(Keys.Backspace);
        }

        public static void MakeSelectionValue(this IWebElement field, string text)
        {
            try
            {
                var DropSelect = new SelectElement(field);
                DropSelect.SelectByValue(text);
            }
            catch (Exception e)
            {
                Assert.Fail("Unable to select value '" + text + "' from drop menu : " + e);
            }
        }

        public static void ButtonClick(this IWebElement button)
        {
            const int timeoutinteger = 8;//2 seconds
            bool cont = true;

            for (int second = 0; cont; second++)
            {
                try
                {
                    button.Click();
                    cont = false;
                }
                catch (Exception e)
                {
                    if (second >= timeoutinteger)
                        Assert.Fail("Unable to click the element " + button.Text + " : " + e.Message);
                    Thread.Sleep(250);
                }
            }
        }

        public static IWebElement FindParent(this IWebElement elem, int up = 1)
        {
            IWebElement result = elem.FindElement(By.XPath(".."));

            for (int i = 1; i < up; i++)
                result = result.FindElement(By.XPath(".."));

            return result;
        }

        public static bool isStale(this IWebElement element)
        {
            try
            {
                return !element.Enabled;
            }
            catch
            {
                return true;
            }
        }

        public static bool isElementDisplayed(this IWebElement elem)
        {
            try { return elem.Displayed; }
            catch (Exception e)
            {
                string check = e.Message;
                return false;
            }
        }

        public static bool isElementHidden(this IWebDriver driver, IWebElement elem)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try { return !elem.Displayed; }
            catch { return true; }
            finally { driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2); }
        }

		public static bool innerTextContains(this IWebElement element, string text)
		{
            return element.Text.ContainsCaseInsensitive(text);
		}

		

    }

    public static class Helpers
    {
		public static bool ContainsCaseInsensitive(this string source, string toCheck)
		{
            return source != null && toCheck != null && source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public static bool innerNumericGTZero(this string text)
		{
            return (text.innerNumeric() > 0);
		}

		public static int innerNumeric(this string text)
		{
            MatchCollection nums = Regex.Matches(text, @"\d+");
            string innerNumber = string.Join(";", from Match match in nums select match.Value);
			int number = Int32.Parse(innerNumber);

			return number;
		}
    }
}
