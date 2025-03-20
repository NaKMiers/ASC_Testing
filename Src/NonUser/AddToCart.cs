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
    public class AddToCart
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
        [Category("AddToCart_At_ProductCard_Success")]
        public void AddToCart_At_ProductCard_Success()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/microsoft-office-365-1-thang-trai-nghiem-dinh-cao-nang-tam-hieu-suat");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='hover:bg-primary-600 trans-200 group rounded-md bg-primary px-3 py-2 ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("AddToCart_At_CarouselCard_Fail")]
        public void AddToCart_At_CarouselCard_Fail_OutOfStock()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/netflix-premium-1-tuan-sieu-net-sieu-tien-loi");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='hover:bg-primary-600 trans-200 group rounded-md bg-primary px-3 py-2 ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("AddToCart_At_CarouselCard_Success")]
        public void AddToCart_At_CarouselCard_Success()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/netflix-premium-100-nam-xem-phim-4k-ben-vung-tron-doi");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='hover:bg-primary-600 trans-200 group rounded-md bg-primary px-3 py-2 ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("AddToCart_At_ProductPage_Fail")]
        public void AddToCart_At_ProductPage_Fail_OutOfStock()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/spotify-premium-2-thang-nghe-nhac-khong-gioi-han-gia-uu-dai");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='hover:bg-primary-600 trans-200 group rounded-md bg-primary px-3 py-2 ']")).Click();
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
