using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace ASC_testing.src.Auth
{
    public class Manage_Users_Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        // **Setup**
       [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/auth/login");
            driver.Manage().Window.Maximize(); // **Mở full màn hình để tránh lỗi hiển thị**
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); // **Tăng thời gian chờ**

            // **🔥 BƯỚC 1: ĐĂNG NHẬP ADMIN 🔥**
            IWebElement emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("usernameOrEmail")));
            emailField.SendKeys("phong2305");  

            IWebElement passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("Chi2305");  

            IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng nhập')]")));
            loginButton.Click();

            // **🔥 BƯỚC 2: CHỜ TRANG CHỦ LOAD HOÀN TẤT 🔥**
            // Sử dụng WebDriverWait để chờ đến khi URL của trang chủ xuất hiện
            wait.Until(ExpectedConditions.UrlContains("home")); // **Đảm bảo trang đã chuyển hướng**

            // **Chờ trang ổn định sau khi login**
            IWebElement avatar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//img[@alt='avatar']")));  // Chờ cho avatar xuất hiện

            // **Cuộn lên trên nếu cần (tránh lỗi không bấm được)**
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Thread.Sleep(1000); // Để chắc chắn

            // **🔥 BƯỚC 3: TÌM AVATAR & NHẤN VÀO 🔥**
            // Đảm bảo phần tử avatar có thể nhấp được
            wait.Until(ExpectedConditions.ElementToBeClickable(avatar));  // Kiểm tra phần tử avatar có thể click được

            Actions actions = new Actions(driver);
            actions.MoveToElement(avatar).Click().Perform();
            Thread.Sleep(2000); // **Chờ menu mở**

            // **🔥 BƯỚC 4: CHỌN QUẢN LÝ NGƯỜI DÙNG 🔥**
            IWebElement manageUsers = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/admin/user/all']")));
            manageUsers.Click();

            // **🔥 BƯỚC 5: CHỜ TRANG QUẢN LÝ NGƯỜI DÙNG TẢI XONG 🔥**
            wait.Until(ExpectedConditions.UrlContains("/admin/user/all"));
        }





        // **Private Method để kiểm tra phần tử có tồn tại**
        private bool IsElementPresent(By by)
        {
            return driver.FindElements(by).Count > 0;
        }

        // **Private Method để điều hướng tới trang quản lý người dùng**
        private void NavigateToUserManagementPage()
        {
           // **Nhấn vào avatar**
            IWebElement avatar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//img[contains(@class, 'wiggle-0') and contains(@class, 'aspect-square') and contains(@class, 'rounded-full')]")));
            avatar.Click();
            Thread.Sleep(2000);  


            // Chờ menu mở ra và vào trang quản lý người dùng
            IWebElement userManagementButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/admin/user/all']")));
            userManagementButton.Click();

            wait.Until(ExpectedConditions.UrlContains("https://anphashop-clone.vercel.app/admin/user/all"));
        }

        // **TearDown để đóng trình duyệt sau mỗi lần test**
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        // **Test Cases**

        // **Test Case 1: Kiểm tra truy cập trang quản lý người dùng**
        [Test]
        public void View_UserManagementPage()
        {
            NavigateToUserManagementPage();
            string currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("user"), Is.True, "Không thể truy cập trang quản lý người dùng.");
        }

        // **Test Case 2: Tìm kiếm người dùng hợp lệ**
        [Test]
        public void Search_Users_Valid()
        {
            NavigateToUserManagementPage();

            IWebElement searchIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'search')] | //svg[contains(@class, 'wiggle')]")));
            searchIcon.Click();

            Thread.Sleep(1000);

            IWebElement searchField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[contains(@placeholder, 'Tìm kiếm')]")));
            searchField.Click();

            searchField.SendKeys("gmail.com");
            searchField.SendKeys(Keys.Enter);

            Assert.That(driver.PageSource.Contains("gmail.com"), Is.True, "Không tìm thấy người dùng với email chứa 'gmail.com'.");
        }

        // **Test Case 3: Lọc người dùng theo số dư**
        [Test]
        public void Filter_Users_By_Balance()
        {
            NavigateToUserManagementPage();

            IWebElement balanceField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("balance")));
            balanceField.SendKeys("30000");
            Thread.Sleep(1000);

            IWebElement filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Lọc')]")));
            filterButton.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), '30,000')]")));

            Assert.That(driver.PageSource.Contains("30,000"), Is.True, "Lọc không hoạt động đúng với số dư 30,000.");
        }

        // **Test Case 4: Sắp xếp người dùng theo thời gian thêm mới**
        [Test]
        public void Sort_Users_LatestAdded()
        {
            NavigateToUserManagementPage();

            IWebElement sortDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sort")));
            sortDropdown.SendKeys("Cập nhật mới nhất");

            Assert.That(driver.PageSource.Contains("Mới nhất"), Is.True, "Sắp xếp người dùng không đúng.");
        }

        // **Test Case 5: Reset bộ lọc người dùng**
        [Test]
        public void Reset_Filter_Users()
        {
            NavigateToUserManagementPage();

            IWebElement resetButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("reset")));
            resetButton.Click();

            Assert.That(driver.PageSource.Contains("All Users"), Is.True, "Reset bộ lọc không hoạt động.");
        }

        // **Test Case 6: Xóa người dùng hợp lệ**
        [Test]
        public void Delete_User_Allowed()
        {
            NavigateToUserManagementPage();

            IWebElement deleteButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Delete')]")));
            deleteButton.Click();

            IWebElement confirmDelete = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Confirm')]")));
            confirmDelete.Click();

            Assert.That(driver.PageSource.Contains("User deleted successfully"), Is.True, "Xóa người dùng không thành công.");
        }

        // **Test Case 7: Thêm số dư cho người dùng**
        [Test]
        public void Add_User_Balance()
        {
            NavigateToUserManagementPage();

            IWebElement balanceField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("balance")));
            balanceField.SendKeys("10000");

            IWebElement addBalanceButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Add Balance')]")));
            addBalanceButton.Click();

            Assert.That(driver.PageSource.Contains("+10,000"), Is.True, "Thêm số dư thất bại.");
        }

        // **Test Case 8: Nâng cấp người dùng lên Admin**
        [Test]
        public void Upgrade_User_To_Admin()
        {
            NavigateToUserManagementPage();

            IWebElement upgradeButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Upgrade to Admin')]")));
            upgradeButton.Click();

            Assert.That(driver.PageSource.Contains("User upgraded to admin"), Is.True, "Không thể nâng cấp người dùng thành Admin.");
        }
    }
}
