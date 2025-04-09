using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASC_Testing.Src.User
{
    public class Manage_OrderHistory
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

        public void Login()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/login");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var usernameOrEmail = driver.FindElement(By.CssSelector("input[id='usernameOrEmail']"));
            var password = driver.FindElement(By.CssSelector("input[id='password']"));
            usernameOrEmail.SendKeys("lehothanhtai@gmail.com");
            password.SendKeys("Thanhtai123");
            driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("View_OrderHistoryPage_Unauthorized")]
        public void View_OrderHistoryPage_Unauthorized()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/order-history");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
        }

        [Test]
        [Category("View_OrderHistoryPage_Normally")]
        public void View_OrderHistoryPage_Normally()
        {
            Login();
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/order-history");
            Thread.Sleep(2000);
        }

        [Test]
        [Category("View_OrderHistoryPage_With_QueryParams")]
        public void View_OrderHistoryPage_With_QueryParams()
        {
            Login();
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/order-history?sort=createdAt|1");
            Thread.Sleep(5000);
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
