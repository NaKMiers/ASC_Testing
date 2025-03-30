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
    public class View_RechargePage
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
        [Category("View_RechargePage_Fail")]
        public void View_RechargePage_Fail_NonUser()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/recharge");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
        }

        [Test]
        [Category("View_RechargePage_Success")]
        public void View_RechargePage_Success()
        {
            Login();
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/recharge");
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
