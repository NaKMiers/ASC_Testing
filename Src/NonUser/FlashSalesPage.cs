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
    public class FlashSalesPage
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
        [Category("View_FlashSalesPage_Normally")]
        public void View_FlashSalesPage_Normally()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            Thread.Sleep(2000);
        }

        [Test]
        [Category("View_FlashSalesPage_Normally")]
        public void View_FlashsalesPage_With_QueryParams()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale?sort=createdAt|1");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_FlashSales_Valid")]
        public void Search_Products_By_FlashSales_Valid_NotFound()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("Netlixxxx");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_FlashSales_Valid")]
        public void Search_Products_By_FlashSales_Valid_Found()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("Canva");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_FlashSales_Invalid")]
        public void Search_Products_By_FlashSales_Invalid_Empty()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_FlashSalesPage")]
        public void Sort_Products_FlashSalesPage_LatestAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
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
        [Category("Sort_Products_FlashSalesPage")]
        public void Sort_Products_FlashSalesPage_LatestDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
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
        [Category("Sort_Products_FlashSalesPage")]
        public void Sort_Products_FlashSalesPage_CreatedAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
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
        [Category("Sort_Products_FlashSalesPage")]
        public void Sort_Products_FlashSalesPage_CreatedDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
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
        [Category("Reset_Filter_FlashSalesPage")]
        public void Reset_Filter_FlashSalesPage()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/flash-sale");
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
