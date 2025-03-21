using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Testdoan
{
    public class Manage_Accounts
    {
        private IWebDriver? driver;
        private WebDriverWait? wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://asclone.vercel.app/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test_Delete_Account_Single()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnDeleteSingle"));
            ClickElement(By.Id("btnConfirm"));
            Assert.That(driver.PageSource.Contains("account has been deleted"), Is.True, "Xóa tài khoản đơn lẻ thất bại.");
        }

        [Test]
        public void Test_Delete_Account_Multiple()
        {
            LoginAsAdmin();
            SelectMultipleAccounts();
            ClickElement(By.Id("btnDeleteMultiple"));
            ClickElement(By.Id("btnConfirm"));
            Assert.That(driver.PageSource.Contains("accounts have been deleted"), Is.True, "Xóa nhiều tài khoản thất bại.");
        }

        [Test]
        public void Test_Deactivate_Account_Single()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnDeactivateSingle"));
            Assert.That(driver.PageSource.Contains("1 account has been deactivated"), Is.True, "Vô hiệu hóa tài khoản đơn lẻ thất bại.");
        }

        [Test]
        public void Test_Deactivate_Account_Multiple()
        {
            LoginAsAdmin();
            SelectMultipleAccounts();
            ClickElement(By.Id("btnDeactivateMultiple"));
            Assert.That(driver.PageSource.Contains("2 accounts have been deactivated"), Is.True, "Vô hiệu hóa nhiều tài khoản thất bại.");
        }

        [Test]
        public void Test_Activate_Account_Single()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnActivateSingle"));
            Assert.That(driver.PageSource.Contains("1 account has been activated"), Is.True, "Kích hoạt tài khoản đơn lẻ thất bại.");
        }

        [Test]
        public void Test_Activate_Account_Multiple()
        {
            LoginAsAdmin();
            SelectMultipleAccounts();
            ClickElement(By.Id("btnActivateMultiple"));
            Assert.That(driver.PageSource.Contains("2 accounts have been activated"), Is.True, "Kích hoạt nhiều tài khoản thất bại.");
        }
        [Test]
        public void Test_Add_Account_Invalid_Type_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            ClickElement(By.Id("btnSubmit"));
            Assert.That(driver.PageSource.Contains("error color on type input"), Is.True, "Không hiển thị lỗi khi bỏ trống loại tài khoản.");
        }

        [Test]
        public void Test_Add_Account_Invalid_Info_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            driver.FindElement(By.Id("txtType")).SendKeys("Grammarly Premium");
            ClickElement(By.Id("btnSubmit"));
            Assert.That(driver.PageSource.Contains("error color on info input"), Is.True, "Không hiển thị lỗi khi bỏ trống thông tin tài khoản.");
        }

        [Test]
        public void Test_Add_Account_Invalid_Info_TooLong()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            driver.FindElement(By.Id("txtType")).SendKeys("Grammarly Premium");
            driver.FindElement(By.Id("txtInfo")).SendKeys(new string('x', 2000));
            ClickElement(By.Id("btnSubmit"));
            Assert.That(driver.PageSource.Contains("error message on description"), Is.True, "Không hiển thị lỗi khi nhập thông tin quá dài.");
        }

        [Test]
        public void Test_Add_Account_Invalid_Time_Zero()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            driver.FindElement(By.Id("txtType")).SendKeys("Grammarly Premium");
            driver.FindElement(By.Id("txtInfo")).SendKeys("Valid Info");
            driver.FindElement(By.Id("txtRenew")).SendKeys("0");
            ClickElement(By.Id("btnSubmit"));
            Assert.That(driver.PageSource.Contains("error message"), Is.True, "Không hiển thị lỗi khi nhập thời gian bằng 0.");
        }

        [Test]
        public void Test_Add_Account_Invalid_Renew_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            driver.FindElement(By.Id("txtType")).SendKeys("Netflix 1 tuần");
            ClickElement(By.Id("btnSubmit"));
            Assert.That(driver.PageSource.Contains("error color on renew input"), Is.True, "Không hiển thị lỗi khi bỏ trống trường gia hạn.");
        }
        [Test]
        public void Test_Add_Account_Single()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            driver.FindElement(By.Id("txtType")).SendKeys("Netflix 1 tuần");
            driver.FindElement(By.Id("txtRenew")).SendKeys("7");
            driver.FindElement(By.Id("txtDays")).SendKeys("7");
            driver.FindElement(By.Id("txtHours")).SendKeys("0");
            driver.FindElement(By.Id("txtMinutes")).SendKeys("0");
            driver.FindElement(By.Id("txtSeconds")).SendKeys("0");
            ClickElement(By.Id("btnSubmit"));
            Assert.That(driver.PageSource.Contains("success message"), Is.True, "Thêm tài khoản đơn thất bại.");
        }

        [Test]
        public void Test_Add_Account_Consecutively()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnAddAccount"));
            driver.FindElement(By.Id("txtType")).SendKeys("Netflix 1 tuần");
            driver.FindElement(By.Id("txtRenew")).SendKeys("7");
            driver.FindElement(By.Id("txtDays")).SendKeys("7");
            driver.FindElement(By.Id("txtHours")).SendKeys("0");
            driver.FindElement(By.Id("txtMinutes")).SendKeys("0");
            driver.FindElement(By.Id("txtSeconds")).SendKeys("0");
            ClickElement(By.Id("btnDuplicate"));
            Assert.That(driver.PageSource.Contains("success messages"), Is.True, "Thêm tài khoản liên tục thất bại.");
        }
        [Test]
        public void Test_Edit_Account_Invalid_Type_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnEditAccount"));
            driver.FindElement(By.Id("txtType")).Clear();
            ClickElement(By.Id("btnSave"));
            Assert.That(driver.PageSource.Contains("error color on type input"), Is.True, "Kiểm thử lỗi khi loại tài khoản trống thất bại.");
        }

        [Test]
        public void Test_Edit_Account_Invalid_Info_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnEditAccount"));
            driver.FindElement(By.Id("txtInfo")).Clear();
            ClickElement(By.Id("btnSave"));
            Assert.That(driver.PageSource.Contains("error color on info input"), Is.True, "Kiểm thử lỗi khi thông tin trống thất bại.");
        }

        [Test]
        public void Test_Edit_Account_Invalid_Info_TooLong()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnEditAccount"));
            driver.FindElement(By.Id("txtInfo")).SendKeys(new string('x', 2001));
            ClickElement(By.Id("btnSave"));
            Assert.That(driver.PageSource.Contains("error message on info"), Is.True, "Kiểm thử lỗi khi thông tin quá dài thất bại.");
        }

        [Test]
        public void Test_Edit_Account_Invalid_Time_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnEditAccount"));
            driver.FindElement(By.Id("txtDays")).Clear();
            driver.FindElement(By.Id("txtHours")).Clear();
            driver.FindElement(By.Id("txtMinutes")).Clear();
            driver.FindElement(By.Id("txtSeconds")).Clear();
            ClickElement(By.Id("btnSave"));
            Assert.That(driver.PageSource.Contains("error message"), Is.True, "Kiểm thử lỗi khi thời gian trống thất bại.");
        }

        [Test]
        public void Test_Edit_Account_Invalid_Renew_Empty()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnEditAccount"));
            driver.FindElement(By.Id("txtRenew")).Clear();
            ClickElement(By.Id("btnSave"));
            Assert.That(driver.PageSource.Contains("error color on renew input"), Is.True, "Kiểm thử lỗi khi ngày gia hạn trống thất bại.");
        }

        [Test]
        public void Test_Edit_Account_Invalid_Message_TooLong()
        {
            LoginAsAdmin();
            ClickElement(By.Id("btnEditAccount"));
            driver.FindElement(By.Id("txtMessage")).SendKeys(new string('x', 2001));
            ClickElement(By.Id("btnSave"));
            Assert.That(driver.PageSource.Contains("error message on message input"), Is.True, "Kiểm thử lỗi khi tin nhắn quá dài thất bại.");
        }

        private void LoginAsAdmin()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("lehothanhtai@gmail.com");
            driver.FindElement(By.Id("txtPassword")).SendKeys("Thanhtai123");
            ClickElement(By.Id("btnLogin"));
        }

        private void SelectMultipleAccounts()
        {
            ClickElement(By.CssSelector("input[name='account'][value='1']"));
            ClickElement(By.CssSelector("input[name='account'][value='2']"));
        }

        private void ClickElement(By by)
        {
            try
            {
                wait?.Until(d => d.FindElement(by).Displayed);
                driver.FindElement(by).Click();
            }
            catch (NoSuchElementException)
            {
                Assert.Fail($"Element {by} not found.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}