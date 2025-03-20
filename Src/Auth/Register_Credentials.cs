using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ASC_testing.src.Auth
{
    public class Register_Credentials_Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/auth/register");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        // ✅ Phương thức Register() phải nằm bên trong class, nhưng ngoài phương thức khác
        private void Register(string username, string email, string password, string rePassword)
        {
            // Nếu username không rỗng thì nhập vào ô username
            if (!string.IsNullOrEmpty(username))
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("username"))).SendKeys(username);
            }

            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("rePassword")).SendKeys(rePassword);

            // Chờ nút đăng ký sẵn sàng và nhấn vào
            IWebElement registerButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng ký')]")));
            if (registerButton.Enabled)
            {
                registerButton.Click();
            }
            else
            {
                Console.WriteLine("Nút đăng ký bị vô hiệu hóa, kiểm tra lại form nhập.");
            }
        }

        [Test]
        public void Register_Credentials_Fail_Username_Empty()
        {
            // Gọi hàm đăng ký nhưng không nhập username
            Register("", "user123@gmail.com", "Asdasd@1", "Asdasd@1");

            // Chờ kiểm tra nếu ô username được focus sau khi nhấn nút đăng ký
            IWebElement usernameField = driver.FindElement(By.Id("username"));
            bool isFocused = usernameField.Equals(driver.SwitchTo().ActiveElement());

            // Kiểm tra xem con trỏ có tự động focus vào ô username không
            Assert.That(isFocused, Is.True, "Cursor không tự động focus vào ô username khi bị để trống.");
        }
        [Test]
        public void Register_Credentials_Fail_Username_LT_6letters()
        {
            Register("user1", "user123@gmail.com", "Asdasd@1", "Asdasd@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);
        }
        [Test]
        public void Register_Credentials_Fail_Username_GT_20letters()
        {
            Register("user5678901234567890123", "user123@gmail.com", "Asdasd@1", "Asdasd@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);
            Thread.Sleep(5000);

        }

        [Test]
        public void Register_Credentials_Fail_Username_Exists()
        {
            Register("user123", "user123@gmail.com", "Asdasd@1", "Asdasd@1");

            // Chờ thông báo lỗi xuất hiện
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("error-message"))); // Thay 'error-message' bằng class thực tế

            // Kiểm tra thông báo lỗi
            Assert.That(driver.PageSource.Contains("Tài khoản đã tồn tại"), Is.True);
        }
        [Test]
        public void Register_Credentials_Fail_Email_Empty()
        {
            // Gọi hàm đăng ký nhưng không nhập email
            Register("user123", "", "Asdasd@1", "Asdasd@1");

            // Chờ kiểm tra nếu ô email được focus sau khi nhấn nút đăng ký
            IWebElement emailField = driver.FindElement(By.Id("email"));
            bool isFocused = emailField.Equals(driver.SwitchTo().ActiveElement());

            // Kiểm tra xem con trỏ có tự động focus vào ô email không
            Assert.That(isFocused, Is.True, "Cursor không tự động focus vào ô email khi bị để trống.");
        }

        [Test]
        public void Register_Credentials_Fail_Email_Invalid()
        {
            Register("user123", "user123", "Asdasd@1", "Asdasd@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);


        }

        [Test]
        public void Register_Credentials_Fail_Email_Exists()
        {
            Register("user123", "user123@gmail.com", "Asdasd@1", "Asdasd@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);


        }

        [Test]
        public void Register_Credentials_Fail_Password_Empty()
        {
            Register("user123", "user123@gmail.com", "", "");
            Assert.IsTrue(driver.PageSource.Contains("Cursor auto focus on password input"));
        }

        [Test]
        public void Register_Credentials_Fail_Password_NoUpper()
        {
            Register("user123", "user123@gmail.com", "asdasd@1", "asdasd@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);


        }

        [Test]
        public void Register_Credentials_Fail_Password_NoLower()
        {
            Register("user123", "user123@gmail.com", "ASDASD@1", "ASDASD@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);


        }

        [Test]
        public void Register_Credentials_Fail_Password_NoNumber()
        {
            Register("user123", "user123@gmail.com", "Asdasd@", "Asdasd@");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);


        }

        [Test]
        public void Register_Credentials_Fail_Password_LT_6letters()
        {
            Register("user123", "user123@gmail.com", "Asd@1", "Asd@1");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);


        }

        [Test]
        public void Register_Credentials_Fail_Password_NotMatch_RePassword()
        {
            Register("user123", "user123@gmail.com", "Asdasd@1", "Asdasd@2");
            NUnit.Framework.Assert.That(driver.PageSource.Contains("Show error message"), Is.True);
        }

        [Test]
        public void Register_Credentials_Success()
        {
            // Gọi hàm đăng ký với thông tin hợp lệ
            Register("user12", "user12@gmail.com", "Asdasd@1", "Asdasd@1");

            // Chờ thông báo thành công xuất hiện
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'Đăng ký thành công')]")));  // Điều chỉnh XPath cho phù hợp với thông báo thành công của trang

            // Kiểm tra thông báo thành công
            bool isSuccessMessageDisplayed = driver.PageSource.Contains("Đăng ký thành công"); // Điều chỉnh nội dung thông báo thành công

            // Kiểm tra xem trang có tự động chuyển hướng về trang chủ hay không
            string currentUrl = driver.Url;
            bool isRedirectedToHomePage = currentUrl.Contains("home");  // Điều chỉnh URL nếu cần

            // Assert rằng thông báo thành công hiển thị và trang đã chuyển hướng về trang chủ
            Assert.That(isSuccessMessageDisplayed, Is.True, "Thông báo thành công không hiển thị.");
            Assert.That(isRedirectedToHomePage, Is.True, "Trang không tự động chuyển hướng về trang chủ.");
        }

    }
}
