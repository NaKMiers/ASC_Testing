using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASC_Testing.Src.NonUser
{
    public class TagsPage
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
        [Category("View_TagssPage_With_QueryParams")]
        public void View_TagssPage_With_QueryParams()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=xem-phim&tag=vinh-vien&price=305187&stock=30");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
        }

        [Test]
        [Category("Search_Products_By_Tags_Vaild")]
        public void Search_Products_By_Tags_Vaild_NotFound()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=hoc-tap");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("XXXXXXXXX");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_Tags_Vaild")]
        public void Search_Products_By_Tags_Vaild_Found()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=xem-phim");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("Netflix");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_Tags_Invalid")]
        public void Search_Products_By_Tags_Invalid_Empty()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=nghe-nhac");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Filter_Products_TagsPage")]
        public void Filter_Products_TagsPage_Price()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=dung-chung");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var priceProduct = driver.FindElement(By.CssSelector("input[id='price']"));
            Actions actions = new Actions(driver);
            actions.ClickAndHold(priceProduct).MoveByOffset(50, 0).Release().Perform();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Filter_Products_TagsPage")]
        public void Filter_Products_TagsPage_Selection()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=giai-tri&tag=hoc-tap");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);

        }

        [Test]
        [Category("Filter_Products_TagsPage")]
        public void Filter_Products_TagsPage_PriceSelection()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=giai-tri&tag=hoc-tap&price=30000");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Product_TagsPage")]
        public void Sort_Product_TagsPage_LatestAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=tieng-anh");
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
        [Category("Sort_Product_TagsPage")]
        public void Sort_Product_TagsPage_LatestDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=tieng-anh");
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
        [Category("Sort_Product_TagsPage")]
        public void Sort_Product_TagsPage_CreatedAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=tieng-anh");
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
        [Category("Sort_Product_TagsPage")]
        public void Sort_Product_TagsPage_CreatedDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=tieng-anh");
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
        [Category("Reset_Filter_TagsPage")]
        public void Reset_Filter_TagsPage()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/tag?tag=xem-phim");
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
