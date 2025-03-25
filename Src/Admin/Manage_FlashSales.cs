using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TestProject1
{



    public class Manage_FlashSales_Tests
    {
        private IWebDriver? driver;
        private readonly string BASE_URL = "https://anphashop-clone.vercel.app";

        private void Login(string usernameOrEmail, string password)
        {
            if (driver == null) throw new NullReferenceException("Driver is not initialized.");

            driver.Navigate().GoToUrl(BASE_URL + "/auth/login");
            driver.FindElement(By.Id("usernameOrEmail")).SendKeys(usernameOrEmail);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();

            new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                .Until(drv => drv.FindElement(By.XPath("//div[@role='status']")) != null);
        }

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-notifications");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Percentage_Value_Not_Include_Percent(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(drv => drv.FindElement(By.Id("type"))).SendKeys("percentage");
            wait.Until(drv => drv.FindElement(By.Id("value"))).SendKeys("10");
            wait.Until(drv => drv.FindElement(By.Id("begin"))).SendKeys("22/02/2025 12:00");
            wait.Until(drv => drv.FindElement(By.Id("timeType"))).SendKeys("loop");
            wait.Until(drv => drv.FindElement(By.Id("duration"))).SendKeys("120");

            wait.Until(drv => drv.FindElement(By.XPath("//button[text()='Add']"))).Click();

            var errorMessage = wait.Until(drv => drv.FindElement(By.ClassName("error-message"))).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at value input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void AddFlashSaleInvalidFixedReduceValueNotANumber(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed-reduce");
            driver.FindElement(By.Id("value")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).SendKeys("120");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at value input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_FixedReduce_Value_Not_A_Minus_Number(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed-reduce");
            driver.FindElement(By.Id("value")).SendKeys("10000");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).SendKeys("120");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at value input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_FixedReduce_Value_Not_A_Number(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed");
            driver.FindElement(By.Id("value")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).SendKeys("120");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at value input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Fixed_Value_Minus(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed");
            driver.FindElement(By.Id("value")).SendKeys("-10000");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).SendKeys("120");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at value input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Begin_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("percentage");
            driver.FindElement(By.Id("value")).SendKeys("10%");
            driver.FindElement(By.Id("begin")).Clear();
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).SendKeys("120");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at begin input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Loop_Duration_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed");
            driver.FindElement(By.Id("value")).SendKeys("-10000");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).Clear();
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at duration input"));
        }


        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Loop_Duration_0(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed");
            driver.FindElement(By.Id("value")).SendKeys("-10000");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("loop");
            driver.FindElement(By.Id("duration")).SendKeys("0");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at duration input"));
        }



        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Once_Expire_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("fixed");
            driver.FindElement(By.Id("value")).SendKeys("-10000");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("once");
            driver.FindElement(By.Id("expire")).Clear();
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at expire input"));
        }


        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Invalid_Once_Expire_LTE_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("percentage");
            driver.FindElement(By.Id("value")).SendKeys("10%");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("once");
            driver.FindElement(By.Id("expire")).Clear();
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("Show error message at expire input"));
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_FlashSale_Valid(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");

            driver.FindElement(By.Id("type")).SendKeys("percentage");
            driver.FindElement(By.Id("value")).SendKeys("10%");
            driver.FindElement(By.Id("begin")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("timeType")).SendKeys("once");
            driver.FindElement(By.Id("expire")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Show success message"));
        }





        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(8000);
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
