using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_testing.src.Auth
{
    public class Register_Github_Tests
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

        // Phương thức đăng ký bằng GitHub
        private void RegisterWithGithub()
        {
            // Tìm và nhấn vào nút đăng ký GitHub
            IWebElement githubButton = driver.FindElement(By.XPath("//button[contains(@class, 'trans-200') and contains(@class, 'rounded-full')]"));
            githubButton.Click();

            // Chờ chuyển hướng đến trang GitHub Sign-In
            wait.Until(ExpectedConditions.UrlContains("github.com/login"));

            // Nhập username/email vào ô đăng nhập GitHub
            IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login_field")));
            usernameField.SendKeys("your-github-username"); // Thay thế với username thật

            // Nhập mật khẩu vào ô mật khẩu
            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys("your-github-password"); // Thay thế với mật khẩu thật
            passwordField.SendKeys(Keys.Enter);

            // Chờ GitHub xác thực và chuyển hướng về website chính
            wait.Until(ExpectedConditions.UrlContains("home"));
        }

        [Test]
        public void Register_Github_FirstTime()
        {
            RegisterWithGithub();
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Không chuyển hướng về trang chủ sau khi đăng ký GitHub lần đầu.");
        }

        [Test]
        public void Register_Github_SecondTime()
        {
            RegisterWithGithub();
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("home"), Is.True, "Không chuyển hướng về trang chủ sau khi đăng ký GitHub lần thứ hai.");
        }
    }
}
