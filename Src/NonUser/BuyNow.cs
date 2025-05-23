﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/random-grammarly-may-man-luon-den-voi-nguoi-cham-chi");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var cartQuantityElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span[class='font-normal text-primary']")));
            int cartQuantity = int.Parse(cartQuantityElement.Text);
            Assert.That(cartQuantity, Is.GreaterThan(0), "Product was not added to the cart successfully.");
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
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/random-grammarly-may-man-luon-den-voi-nguoi-cham-chi");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var cartQuantityElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span[class='font-normal text-primary']")));
            int cartQuantity = int.Parse(cartQuantityElement.Text);
            Assert.That(cartQuantity, Is.GreaterThan(0), "Product was not added to the cart successfully.");
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
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/random-grammarly-may-man-luon-den-voi-nguoi-cham-chi");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var cartQuantityElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span[class='font-normal text-primary']")));
            int cartQuantity = int.Parse(cartQuantityElement.Text);
            Assert.That(cartQuantity, Is.GreaterThan(0), "Product was not added to the cart successfully.");
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
