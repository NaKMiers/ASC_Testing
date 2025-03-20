using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace ASC_testing.src.Auth
{
    public class Login_Github_Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/auth/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        // Phương thức đăng nhập bằng GitHub
        private void LoginWithGithub()
        {
            // Nhấn vào nút đăng nhập bằng GitHub
           // Tìm đúng nút GitHub dựa trên class
            IWebElement githubButton = driver.FindElement(By.XPath("//button[contains(@class, 'trans-200') and contains(@class, 'rounded-full') and contains(@class, 'border-slate-800')]"));
            githubButton.Click();

            // Chờ chuyển hướng đến trang GitHub Login
            wait.Until(ExpectedConditions.UrlContains("github.com/login"));

            // Nhập email hoặc username vào ô GitHub login
            IWebElement githubEmail = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login_field")));
            githubEmail.SendKeys("22dh112723@st.huflit.edu.vn");  // Thay thế bằng tài khoản GitHub của bạn

            // Nhập mật khẩu vào ô password
            IWebElement githubPassword = driver.FindElement(By.Id("password"));
            githubPassword.SendKeys("Chi230503");  // Thay thế bằng mật khẩu GitHub của bạn

            // Nhấn nút đăng nhập
            driver.FindElement(By.Name("commit")).Click();

            // Nếu GitHub yêu cầu xác nhận quyền ứng dụng, chờ và nhấn nút "Authorize"
            try
            {
                IWebElement authorizeButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Authorize')]")));
                authorizeButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Không có yêu cầu xác nhận quyền GitHub.");
            }

            // Chờ trang chuyển hướng về website chính sau khi đăng nhập thành công
            wait.Until(ExpectedConditions.UrlContains("home"));
        }

        [Test]
        public void Login_Github_FirstTime()
        {
            // Gọi phương thức đăng nhập qua GitHub lần đầu
            LoginWithGithub();

            // Kiểm tra xem trang có chuyển hướng về trang chủ không
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Không chuyển hướng về trang chủ sau khi đăng nhập GitHub lần đầu.");
        }

        [Test]
        public void Login_Github_SecondTime()
        {
            // Gọi phương thức đăng nhập qua GitHub lần thứ hai
            LoginWithGithub();

            // Kiểm tra xem trang có chuyển hướng về trang chủ không
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Không chuyển hướng về trang chủ sau khi đăng nhập GitHub lần thứ hai.");
        }
    }
}
