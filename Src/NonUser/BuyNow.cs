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
    public class BuyNow
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
        [Category("BuyNow_At_ProductCard_Fail")]
        public void BuyNow_At_ProductCard_Fail_OutOfStock()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/spotify-premium-2-thang-nghe-nhac-khong-gioi-han-gia-uu-dai");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='trans-200 rounded-md bg-secondary px-3 py-[5px] font-body text-xl font-semibold text-white hover:bg-primary ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("BuyNow_At_ProductCard_Success")]
        public void BuyNow_At_ProductCard_Success()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/youtube-premium-youtube-music-no-ads-1-nam-giai-tri-ca-nam-khong-lo-quang-cao");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='trans-200 rounded-md bg-secondary px-3 py-[5px] font-body text-xl font-semibold text-white hover:bg-primary ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("BuyNow_At_ProductPage_Fail")]
        public void BuyNow_At_CarouselCard_Fail_OutOfStock()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/random-netflix-van-may-se-thay-ban-tra-tien");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='trans-200 rounded-md bg-secondary px-3 py-[5px] font-body text-xl font-semibold text-white hover:bg-primary ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("BuyNow_At_CarouselCard_Success")]
        public void BuyNow_At_CarouselCard_Success()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/canva-pro-6-thang-sang-tao-chuyen-nghiep-thiet-ke-vo-song");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='trans-200 rounded-md bg-secondary px-3 py-[5px] font-body text-xl font-semibold text-white hover:bg-primary ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("BuyNow_At_ProductPage_Fail")]
        public void BuyNow_At_ProductPage_Fail_OutOfStock()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/netflix-premium-1-ngay-trai-nghiem-phim-sieu-net");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='trans-200 rounded-md bg-secondary px-3 py-[5px] font-body text-xl font-semibold text-white hover:bg-primary ']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("BuyNow_At_ProductPage_Success")]
        public void BuyNow_At_ProductPage_Success()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/microsoft-office-365-1-nam-khoi-day-cam-hung-hien-thuc-hoa-y-tuong");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.CssSelector("button[class='trans-200 rounded-md bg-secondary px-3 py-[5px] font-body text-xl font-semibold text-white hover:bg-primary ']")).Click();
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
