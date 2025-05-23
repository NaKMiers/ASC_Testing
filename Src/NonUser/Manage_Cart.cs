﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_Testing.Src.NonUser
{
    public class Manage_Cart
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
        [Category("Change_ItemQuantity_By_Button")]
        public void Change_ItemQuantity_By_Button_Increase_Once()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/grammarly-premium-7-ngay-danh-bay-loi-ngu-phap-trai-nghiem-ngay");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[class='trans-200 group flex items-center justify-center rounded-br-md rounded-tr-md border px-3 py-[10px] hover:bg-secondary border-secondary bg-white']")).Click();
        }

        [Test]
        [Category("Change_ItemQuantity_By_Button")]
        public void Change_ItemQuantity_By_Button_Decrease_Once()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/grammarly-premium-7-ngay-danh-bay-loi-ngu-phap-trai-nghiem-ngay");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[class='trans-200 group flex items-center justify-center rounded-br-md rounded-tr-md border px-3 py-[10px] hover:bg-secondary border-secondary bg-white']")).Click();
            driver.FindElement(By.CssSelector("button[class='trans-200 group flex items-center justify-center rounded-bl-md rounded-tl-md border px-3 py-[10px] hover:bg-secondary border border-secondary bg-white']")).Click();
        }

        [Test]
        [Category("Change_ItemQuantity_By_Input_Valid")]
        public void Change_ItemQuantity_By_Input_Valid()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/grammarly-premium-7-ngay-danh-bay-loi-ngu-phap-trai-nghiem-ngay");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var numberInput = driver.FindElement(By.CssSelector("input[class='number-input max-w-14 border-y border-slate-100 px-2 text-center font-body text-lg font-semibold text-dark outline-none']"));
            numberInput.Click();
            numberInput.SendKeys("xxx");
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Change_ItemQuantity_By_Input_Invalid")]
        public void Change_ItemQuantity_By_Input_Valid_NotEnough()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/grammarly-premium-7-ngay-danh-bay-loi-ngu-phap-trai-nghiem-ngay");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var numberInput = driver.FindElement(By.CssSelector("input[class='number-input max-w-14 border-y border-slate-100 px-2 text-center font-body text-lg font-semibold text-dark outline-none']"));
            numberInput.Click();
            numberInput.SendKeys("1000000");
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Change_ItemQuantity_By_Input_Invalid")]
        public void Change_ItemQuantity_By_Input_Valid_Lte0()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/grammarly-premium-7-ngay-danh-bay-loi-ngu-phap-trai-nghiem-ngay");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var numberInput = driver.FindElement(By.CssSelector("input[class='number-input max-w-14 border-y border-slate-100 px-2 text-center font-body text-lg font-semibold text-dark outline-none']"));
            numberInput.Click();
            numberInput.SendKeys("-10");
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Delete_CartItem_Local")]
        public void Delete_CartItem_Local_Non_User()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/grammarly-premium-7-ngay-danh-bay-loi-ngu-phap-trai-nghiem-ngay");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var buyNowButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'MUA NGAY')]")));
            buyNowButton.Click();
            Thread.Sleep(2000);
            var deleteButton = driver.FindElement(By.CssSelector("input[class='trans-200 wiggle cursor-pointer text-secondary hover:scale-110']"));
            deleteButton.Click();
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
