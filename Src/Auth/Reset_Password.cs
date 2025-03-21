using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Threading;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace ASC_Testing.Src.Auth
{
    class Reset_Password
    {
        private ChromeDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Constants.BASE_URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_AccessPage_NoToken()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            wait.Until(ExpectedConditions.UrlContains("/auth/login"));
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_Empty_NewPassword()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("");
            reNewPasswordInput.SendKeys("Asdasd@2");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_Empty_ReNewPassword()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("Asdasd@2");
            reNewPasswordInput.SendKeys("");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_SameOldPW()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("Asdasd@1");
            reNewPasswordInput.SendKeys("Asdasd@1");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("error-message")));
            Assert.That(errorMessage.Displayed, "Không hiển thị thông báo lỗi khi mật khẩu mới giống với mật khẩu cũ.");
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_NoUpper()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("asdasd@2");
            reNewPasswordInput.SendKeys("asdasd@2");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".error-message")));
            Assert.That(errorMessage.Displayed, "Không hiển thị thông báo lỗi khi mật khẩu mới không có chữ hoa.");
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_NoLower()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("ASDASD@2");
            reNewPasswordInput.SendKeys("ASDASD@2");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".error-message")));
            Assert.That(errorMessage.Displayed, "Không hiển thị thông báo lỗi khi mật khẩu mới không có chữ thường.");
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_NoNumber()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("Asdasd@");
            reNewPasswordInput.SendKeys("Asdasd@");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".error-message")));
            Assert.That(errorMessage.Displayed, "Không hiển thị thông báo lỗi khi mật khẩu mới không có số.");
        }

        [Test]
        [Category("Reset_Password_Fail")]
        public void Reset_Password_Fail_NotMatchReEnterPW()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("Asdasd@2");
            reNewPasswordInput.SendKeys("Asdasd@xxx");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Reset_Password_Success")]
        public void Reset_Password_Success()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/reset-password?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlaG90aGFuaHRhaUBnbWFpbC5jb20iLCJpYXQiOjE3NDE5NzI0ODQsImV4cCI6MTc0MTk3OTY4NH0.udQO08udtRpOEFnWGvBeb2xed8-J0oO38W3VfrOI0H8");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var newPasswordInput = driver.FindElement(By.CssSelector("input[id='newPassword']"));
            var reNewPasswordInput = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
            newPasswordInput.SendKeys("Asdasd@2");
            reNewPasswordInput.SendKeys("Asdasd@2");
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            wait.Until(ExpectedConditions.UrlContains("/auth/login"));
            Assert.That(driver.Url.Contains("/auth/login"), "Người dùng không được điều hướng về trang đăng nhập sau khi đặt lại mật khẩu thành công.");
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
