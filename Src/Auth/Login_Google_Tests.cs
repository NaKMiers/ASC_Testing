using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_testing.src.Auth
{
    public class Login_Google_Tests
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

        // Phương thức đăng nhập bằng Google
        private void LoginWithGoogle()
        {
            // Tìm và nhấn vào nút đăng nhập Google
            IWebElement googleButton = driver.FindElement(By.XPath("//button[contains(@class, 'trans-200') and contains(@class, 'rounded-full')]"));
            googleButton.Click();

            // Chờ chuyển hướng đến trang Google Sign-In
            wait.Until(ExpectedConditions.UrlContains("accounts.google.com"));

            // Nhập email vào ô Email/Phone
            IWebElement emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='email']")));
            emailField.SendKeys("trambaochi2305@gmail.com"); // Thay thế email thật
            emailField.SendKeys(Keys.Enter);

            // Chờ trang tải trường nhập mật khẩu
            Thread.Sleep(3000); // Có thể thay bằng wait nếu cần

            // Nhập mật khẩu vào ô Password
            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='password']")));
            passwordField.SendKeys("trambaochi23052003"); // Thay thế mật khẩu thật
            passwordField.SendKeys(Keys.Enter);

            // Chờ trang xác thực và chuyển hướng về website chính
            wait.Until(ExpectedConditions.UrlContains("home"));
        }

        [Test]
        public void Login_Google_FirstTime()
        {
            LoginWithGoogle();
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Không chuyển hướng về trang chủ sau khi đăng nhập Google lần đầu.");
        }

        [Test]
        public void Login_Google_SecondTime()
        {
            LoginWithGoogle();
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Không chuyển hướng về trang chủ sau khi đăng nhập Google lần thứ hai.");
        }
    }
}
