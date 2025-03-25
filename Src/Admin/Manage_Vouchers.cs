using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
namespace TestProject2
{
    public class Manage_Vouchers_Tests
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



        [Test]
        public void View_VoucherManagementPage_Unauthorized()
        {
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            // Kiểm tra xem có bị redirect về trang chủ không
            Assert.That(driver.Url, Is.EqualTo(BASE_URL + "/"));
        }


        [Test]
        public void View_VoucherManagementPage_Normally()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            // Kiểm tra xem có truy cập được trang quản lý voucher không
            Assert.That(driver.Url, Is.EqualTo(BASE_URL + "/admin/voucher/all"));
        }


        [Test]
        public void View_VoucherManagementPage_With_QueryParams()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all?sort=createdAt|1");
            var sortedCorrectly = driver.PageSource.Contains("Ordered by Oldest");
            Assert.That(sortedCorrectly, Is.True);
        }

        [Test]
        public void Search_Vouchers_ProductManagementPage_Valid()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            var searchBox = driver.FindElement(By.Id("search-box"));
            searchBox.SendKeys("ABCD");

            var filterButton = driver.FindElement(By.Id("filter-button"));
            filterButton.Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results.Contains("ABCD"), Is.True);
        }


        [Test]
        public void Search_Vouchers_ProductManagementPage_Invalid()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            var filterButton = driver.FindElement(By.Id("filter-button"));
            filterButton.Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Is.EqualTo(""));
        }


        [Test]
        public void Filter_Vouchers_ProductManagementPage_MinTotal()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("min-total")).SendKeys("50000");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Min total >= 50000"));
        }

        [Test]
        public void Filter_Vouchers_ProductManagementPage_MaxReduce()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("max-reduce")).SendKeys("50000");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Max reduce >= 50000"));
        }



        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Filter_Vouchers_ProductManagementPage_BeginFrom(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            var dateInput = driver.FindElement(By.CssSelector("[placeholder='dd/mm/yyyy']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(dateInput).Click().SendKeys("14/09/2024").Perform();

            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Begin from 14/09/2024"));
        }





        [Test]
        public void Filter_Vouchers_ProductManagementPage_BeginTo()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("begin-to")).SendKeys("14/09/2025 12:00");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Begin to 14/09/2025 12:00"));
        }




        [Test]
        public void Filter_Vouchers_ProductManagementPage_BeginFromTo()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("begin-from")).SendKeys("14/09/2024 12:00");
            driver.FindElement(By.Id("begin-to")).SendKeys("14/09/2025 12:00");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Begin from 14/09/2024 12:00 to 14/09/2025 12:00"));
        }








        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Filter_Vouchers_ProductManagementPage_ExpireFrom(string username, string password)
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");
            


            driver.FindElement(By.Id("expire-from")).SendKeys("14/09/2024 12:00");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Expire from 14/09/2024 12:00"));
        }

        [Test]
        public void Filter_Vouchers_ProductManagementPage_ExpireTo()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("expire-to")).SendKeys("14/09/2025 12:00");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Expire to 14/09/2025 12:00"));
        }


        [Test]
        public void Filter_Vouchers_ProductManagementPage_ExpireFromTo()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("expire-from")).SendKeys("14/09/2024 12:00");
            driver.FindElement(By.Id("expire-to")).SendKeys("14/09/2025 12:00");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Expire from 14/09/2024 12:00 to 14/09/2025 12:00"));
        }


        [Test]
        public void Filter_Vouchers_ProductManagementPage_TimesLeft()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("times-left")).SendKeys("Run out");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Times left: Run out"));
        }

        [Test]
        public void Filter_Vouchers_ProductManagementPage_Type()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("voucher-type")).SendKeys("Fixed");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Type: Fixed"));
        }

        [Test]
        public void Filter_Vouchers_ProductManagementPage_Active()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("voucher-active")).SendKeys("Off");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Active: Off"));
        }


        [Test]
        public void Sort_Vouchers_ProductManagementPage_LatestAsc()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("sort-option")).SendKeys("Cập nhật cũ nhất");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Cập nhật cũ nhất"));
        }

        [Test]
        public void Sort_Vouchers_ProductManagementPage_LatestDesc()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("sort-option")).SendKeys("Cập nhật mới nhất");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Cập nhật mới nhất"));
        }

        [Test]
        public void Sort_Vouchers_ProductManagementPage_CreatedAsc()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("sort-option")).SendKeys("Cũ nhất");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Cũ nhất"));
        }

        [Test]
        public void Sort_Vouchers_ProductManagementPage_CreatedDesc()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("sort-option")).SendKeys("Mới nhất");
            driver.FindElement(By.Id("filter-button")).Click();

            var results = driver.FindElement(By.Id("voucher-results")).Text;
            Assert.That(results, Does.Contain("Mới nhất"));
        }

        [Test]
        public void Reset_Filter_ProductManagementPage()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("reset-button")).Click();

            Assert.That(driver.FindElement(By.Id("min-total")).GetAttribute("value"), Is.EqualTo("max"));
            Assert.That(driver.FindElement(By.Id("max-reduce")).GetAttribute("value"), Is.EqualTo("max"));
            Assert.That(driver.FindElement(By.Id("begin-from")).GetAttribute("value"), Is.EqualTo("dd/mm/yyyy"));
            Assert.That(driver.FindElement(By.Id("begin-to")).GetAttribute("value"), Is.EqualTo("dd/mm/yyyy"));
            Assert.That(driver.FindElement(By.Id("expire-from")).GetAttribute("value"), Is.EqualTo("dd/mm/yyyy"));
            Assert.That(driver.FindElement(By.Id("expire-to")).GetAttribute("value"), Is.EqualTo("dd/mm/yyyy"));
            Assert.That(driver.FindElement(By.Id("sort-option")).GetAttribute("value"), Is.EqualTo("latest"));
            Assert.That(driver.FindElement(By.Id("times-left")).GetAttribute("value"), Is.EqualTo("all"));
            Assert.That(driver.FindElement(By.Id("type")).GetAttribute("value"), Is.EqualTo("all"));
            Assert.That(driver.FindElement(By.Id("active")).GetAttribute("value"), Is.EqualTo("all"));
        }


        [Test]
        public void Delete_Vouchers_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("delete-button")).Click();
            driver.FindElement(By.Id("confirm-delete")).Click();

            var message = driver.FindElement(By.Id("message-box")).Text;
            Assert.That(message, Does.Contain("voucher has been deleted"));
        }

        [Test]
        public void Delete_Vouchers_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/all");

            driver.FindElement(By.Id("select-multiple")).Click();
            driver.FindElement(By.Id("delete-button")).Click();
            driver.FindElement(By.Id("confirm-delete")).Click();

            var message = driver.FindElement(By.Id("message-box")).Text;
            Assert.That(message, Does.Contain("vouchers have been deleted"));
        }


        [Test]
        public void Add_Voucher_Invalid_Code_Empty()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("owner-select")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("begin-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("min-total-input")).SendKeys("0");
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("10000");
            driver.FindElement(By.Id("type-select")).SendKeys("Percentage");
            driver.FindElement(By.Id("value-input")).SendKeys("10%");
            driver.FindElement(By.Id("times-left-input")).SendKeys("1");
            driver.FindElement(By.Id("add-button")).Click();

            var focusedElement = driver.SwitchTo().ActiveElement();
            Assert.That(focusedElement.GetAttribute("id"), Is.EqualTo("code-input"));
        }


        [Test]
        public void Add_Voucher_Invalid_Code_LT_5()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("code-input")).SendKeys("XXX");
            driver.FindElement(By.Id("owner-select")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("begin-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("min-total-input")).SendKeys("0");
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("10000");
            driver.FindElement(By.Id("type-select")).SendKeys("Percentage");
            driver.FindElement(By.Id("value-input")).SendKeys("10%");
            driver.FindElement(By.Id("times-left-input")).SendKeys("1");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("code-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Add_Voucher_Invalid_Code_GT_10()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("code-input")).SendKeys("XXXXYYYYZZZZ");
            driver.FindElement(By.Id("owner-select")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("begin-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("min-total-input")).SendKeys("0");
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("10000");
            driver.FindElement(By.Id("type-select")).SendKeys("Percentage");
            driver.FindElement(By.Id("value-input")).SendKeys("10%");
            driver.FindElement(By.Id("times-left-input")).SendKeys("1");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("code-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Add_Voucher_Invalid_Begin_Empty()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("code-input")).SendKeys("XXXXYYYYZZZZ");
            driver.FindElement(By.Id("owner-select")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("expire-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("min-total-input")).SendKeys("0");
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("10000");
            driver.FindElement(By.Id("type-select")).SendKeys("Percentage");
            driver.FindElement(By.Id("value-input")).SendKeys("10%");
            driver.FindElement(By.Id("times-left-input")).SendKeys("1");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("begin-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Add_Voucher_Invalid_Begin_GTE_Expire()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("code-input")).SendKeys("XXX");
            driver.FindElement(By.Id("owner-select")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("begin-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("begin-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_MinTotal_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("min-total-input")).SendKeys("-1000");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("min-total-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_MaxReduce_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("max-reduce-input")).SendKeys("-10000");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("max-reduce-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_Value_Empty()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("value-input")).SendKeys("");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("value-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_Percentage_Value_Not_Include_Percent()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("value-input")).SendKeys("10");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("value-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }




        [Test]
        public void Add_Voucher_Invalid_Fixed_Value_Not_A_Number()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("value-input")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("value-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_Fixed_Value_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("value-input")).SendKeys("-10000");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("value-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_FixedReduce_Value_Not_A_Number()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("value-input")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("value-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Add_Voucher_Invalid_FixedReduce_Value_Not_A_Minus_Number()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("value-input")).SendKeys("10000");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("value-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Add_Voucher_Invalid_TimesLeft_LTE_0()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("times-left-input")).SendKeys("0");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("times-left-error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Add_Voucher_Valid()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher/add");

            driver.FindElement(By.Id("code-input")).SendKeys("XXX");
            driver.FindElement(By.Id("owner-input")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("begin-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("min-total-input")).SendKeys("0");
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("-10000");
            driver.FindElement(By.Id("type-input")).SendKeys("Percentage");
            driver.FindElement(By.Id("value-input")).SendKeys("10%");
            driver.FindElement(By.Id("times-left-input")).SendKeys("1");
            driver.FindElement(By.Id("add-button")).Click();

            var successMessage = driver.FindElement(By.Id("success-message")).Text;
            Assert.That(successMessage, Is.Not.Empty);
        }


        [Test]
        public void Edit_Voucher_Invalid_Code_Empty()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("code-input")).Clear();
            driver.FindElement(By.Id("save-button")).Click();

            Assert.That(driver.SwitchTo().ActiveElement(), Is.EqualTo(driver.FindElement(By.Id("code-input"))));
        }

        [Test]
        public void Edit_Voucher_Invalid_Code_LT_5()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("code-input")).SendKeys("XXX");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Code_GT_10()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("code-input")).SendKeys("XXXXYYYYZZZZ");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Begin_Empty()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("begin-input")).Clear();
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Begin_GTE_Expire()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("begin-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_MinTotal_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("min-total-input")).SendKeys("-50000");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Edit_Voucher_Invalid_MaxReduce_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("-10000");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Value_Empty()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("value-input")).Clear();
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Percentage_Value_Not_Include_Percent()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("value-input")).SendKeys("10");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Fixed_Value_Not_A_Number()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("value-input")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_Fixed_Value_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("value-input")).SendKeys("-10000");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_FixedReduce_Value_Not_A_Number()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("value-input")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Edit_Voucher_Invalid_FixedReduce_Value_Not_A_Minus_Number()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("value-input")).Clear();
            driver.FindElement(By.Id("value-input")).SendKeys("10000");
            driver.FindElement(By.Id("save-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }


        [Test]
        public void Edit_Voucher_Invalid_TimesLeft_LTE_0()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("times-left-input")).SendKeys("0");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }

        [Test]
        public void Edit_Voucher_Invalid_TimesLeft_Minus()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("times-left-input")).SendKeys("-1");
            driver.FindElement(By.Id("add-button")).Click();

            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.That(errorMessage, Is.Not.Empty);
        }



        [Test]
        public void Edit_Voucher_Valid()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("edit-button")).Click();
            driver.FindElement(By.Id("code-input")).SendKeys("XXX");
            driver.FindElement(By.Id("owner-input")).SendKeys("Pi Pi");
            driver.FindElement(By.Id("description-input")).SendKeys("xxxxx");
            driver.FindElement(By.Id("begin-input")).SendKeys("22/02/2025 12:00");
            driver.FindElement(By.Id("expire-input")).SendKeys("28/02/2025 12:00");
            driver.FindElement(By.Id("min-total-input")).SendKeys("0");
            driver.FindElement(By.Id("max-reduce-input")).SendKeys("-10000");
            driver.FindElement(By.Id("type-input")).SendKeys("Percentage");
            driver.FindElement(By.Id("value-input")).SendKeys("10%");
            driver.FindElement(By.Id("times-left-input")).SendKeys("1");
            driver.FindElement(By.Id("save-button")).Click();

            var successMessage = driver.FindElement(By.Id("success-message")).Text;
            Assert.That(successMessage, Is.Not.Empty);
        }


        [Test]
        public void Activate_Voucher_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("activate-button")).Click();
            driver.FindElement(By.Id("confirm-activate")).Click();

            var successMessage = driver.FindElement(By.Id("success-message")).Text;
            Assert.That(successMessage, Does.Contain("voucher has been activated"));
        }

        [Test]
        public void Activate_Voucher_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("select-multiple-vouchers")).Click();
            driver.FindElement(By.Id("activate-button")).Click();
            driver.FindElement(By.Id("confirm-activate")).Click();

            var successMessage = driver.FindElement(By.Id("success-message")).Text;
            Assert.That(successMessage, Does.Contain("vouchers have been activated"));
        }

        [Test]
        public void Deactivate_Voucher_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("deactivate-button")).Click();
            driver.FindElement(By.Id("confirm-deactivate")).Click();

            var successMessage = driver.FindElement(By.Id("success-message")).Text;
            Assert.That(successMessage, Does.Contain("voucher has been deactivated"));
        }

        [Test]
        public void Deactivate_Voucher_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/voucher");

            driver.FindElement(By.Id("select-multiple-vouchers")).Click();
            driver.FindElement(By.Id("deactivate-button")).Click();
            driver.FindElement(By.Id("confirm-deactivate")).Click();

            var successMessage = driver.FindElement(By.Id("success-message")).Text;
            Assert.That(successMessage, Does.Contain("vouchers have been deactivated"));
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