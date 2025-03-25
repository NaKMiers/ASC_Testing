using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TestProject3
{
    public class Quick_Move_To_Edit_Product_Tests
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


        private void LoginAsAdmin()
        {
            if (driver == null) throw new NullReferenceException("Driver is not initialized.");

            driver.Navigate().GoToUrl(BASE_URL + "/auth/login");

            // Nhập thông tin đăng nhập (Cần thay thế bằng tài khoản admin thực tế)
            driver.FindElement(By.Id("usernameOrEmail")).SendKeys("phanthang");
            driver.FindElement(By.Id("password")).SendKeys("Phanthang01");

            // Nhấn nút đăng nhập
            driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();

            // Chờ cho trang admin dashboard tải xong
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(drv => drv.Url.Contains("/dashboard") || drv.FindElement(By.XPath("//div[@role='status']")) != null);

            // Kiểm tra đăng nhập thành công bằng cách xác minh URL
            Assert.IsTrue(driver.Url.Contains("/dashboard"), "Đăng nhập thất bại.");
        }

        [Test]
        public void QuickMoveToEdit_ProductCard()
        {
            LoginAsAdmin(); // Đăng nhập trước khi test

            // Click vào nút Edit trên Product Card
            driver.FindElement(By.CssSelector(".product-card .edit-button")).Click();
            Thread.Sleep(1000);

            // Kiểm tra xem có chuyển sang trang chỉnh sửa sản phẩm không
            Assert.IsTrue(driver.Url.Contains("admin/product/edit"), "Không chuyển đúng trang quản lý sản phẩm.");
        }

        [Test]
        public void QuickMoveToEdit_ProductCarouselCard()
        {
            LoginAsAdmin(); // Đăng nhập trước khi test

            // Click vào nút Edit trên Carousel Card
            driver.FindElement(By.CssSelector(".carousel-card .edit-button")).Click();
            Thread.Sleep(1000);

            // Kiểm tra xem có chuyển sang trang chỉnh sửa sản phẩm không
            Assert.IsTrue(driver.Url.Contains("admin/product/edit"), "Không chuyển đúng trang quản lý sản phẩm.");
        }

        [Test]
        public void QuickMoveToEdit_ProductPage()
        {
            LoginAsAdmin(); // Đăng nhập trước khi test

            // Click vào một sản phẩm để vào trang chi tiết
            driver.FindElement(By.CssSelector(".product-item")).Click();
            Thread.Sleep(1000);

            // Click vào nút Edit trên Product Page
            driver.FindElement(By.CssSelector(".product-page .edit-button")).Click();
            Thread.Sleep(1000);

            // Kiểm tra xem có chuyển sang trang chỉnh sửa sản phẩm không
            Assert.IsTrue(driver.Url.Contains("admin/product/edit"), "Không chuyển đúng trang quản lý sản phẩm.");
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