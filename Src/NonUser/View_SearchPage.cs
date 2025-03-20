using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASC_Testing.Src.NonUser
{
    public class View_SearchPage
    {
        private ChromeDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Constants.BASE_URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        [Category("Sort_Products_SearchPage")]
        public void Sort_Products_SearchPage_LatestAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/search");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Cập nhật cũ nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_SearchPage")]
        public void Sort_Products_SearchPage_LatestDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/search");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Cập nhật mới nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_SearchPage")]
        public void Sort_Products_SearchPage_CreatedAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/search");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Cũ nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_SearchPage")]
        public void Sort_Products_SearchPage_CreatedDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/search");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Mới nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Reset_Filter_SearchPage")]
        public void Reset_Filter_SearchPage()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/search");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(1000);
            driver.Quit();
            driver.Dispose();
        }
    }
}
