using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_testing.src.Auth
{
    public class Logout_Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        // Phương thức chung để thực hiện logout
        private void PerformLogout()
        {
            // Click vào avatar để hiển thị menu logout
            IWebElement avatar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//img[contains(@class, 'avatar-class')]")));
            avatar.Click();

            // Click vào nút Logout
            IWebElement logoutButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Logout')]")));
            logoutButton.Click();

            // Chờ trang render lại và avatar biến mất (được thay thế bằng nút login)
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Login')]")));
        }

        [Test]
        public void Logout_Credentials()
        {
            PerformLogout();

            // Kiểm tra xem avatar có biến mất và nút login xuất hiện không
            bool isLoginVisible = driver.FindElement(By.XPath("//button[contains(text(), 'Login')]")).Displayed;
            Assert.That(isLoginVisible, Is.True, "Logout thất bại, avatar vẫn còn.");
        }

        [Test]
        public void Logout_Google()
        {
            PerformLogout();

            // Kiểm tra xem avatar có biến mất và nút login xuất hiện không
            bool isLoginVisible = driver.FindElement(By.XPath("//button[contains(text(), 'Login')]")).Displayed;
            Assert.That(isLoginVisible, Is.True, "Logout thất bại, avatar vẫn còn.");
        }

        [Test]
        public void Logout_Github()
        {
            PerformLogout();

            // Kiểm tra xem avatar có biến mất và nút login xuất hiện không
            bool isLoginVisible = driver.FindElement(By.XPath("//button[contains(text(), 'Login')]")).Displayed;
            Assert.That(isLoginVisible, Is.True, "Logout thất bại, avatar vẫn còn.");
        }
    }
}
