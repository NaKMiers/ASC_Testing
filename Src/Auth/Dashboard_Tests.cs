using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ASC_testing.src.Auth
{
    public class Dashboard_Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void View_Dashboard_Unauthorized()
        {
            // Truy cập Dashboard mà không đăng nhập
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/admin");

            // Kiểm tra xem có bị redirect về trang chủ không
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Người dùng chưa đăng nhập nhưng vẫn truy cập được Dashboard.");
        }

        [Test]
        public void View_Dashboard_Success()
        {
            // Đăng nhập với tài khoản Admin trước
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/auth/login");

            IWebElement emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("usernameOrEmail")));
            emailField.SendKeys("phong2305"); // Thay thế bằng email admin thực tế
            driver.FindElement(By.Id("password")).SendKeys("Chi2305"); // Thay thế bằng mật khẩu admin thực tế
            driver.FindElement(By.XPath("//button[contains(text(), 'Đăng nhập')]")).Click();

            // Chờ chuyển hướng về trang chủ sau khi đăng nhập
            wait.Until(ExpectedConditions.UrlContains("home"));

            // Truy cập vào Dashboard
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/admin");

            // Kiểm tra xem có vào đúng trang Dashboard không
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("dashboard"), Is.True, "Admin không truy cập được Dashboard.");
        }
    }
}
