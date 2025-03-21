using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TestProject1
{
    public class Manage_FlashSales
    {
        private ChromeDriver driver;
        private string base_url;
        private readonly string BASE_URL = "https://asclone.vercel.app";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(BASE_URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            login("phanthang", "phanthang01");

            driver.Navigate().GoToUrl(base_url + "/admin/flash-sale");



            Login("phanthang", "Phanthang01");

            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");
        }

        private void login(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        private void Login(string usernameOrEmail, string password)
        {
            driver.Navigate().GoToUrl(BASE_URL + "/auth/login");
            driver.FindElement(By.Id("usernameOrEmail")).SendKeys(usernameOrEmail);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        public void Add_FlashSale_Valid_FixedReduce_Value()
        {
            try
            {

                driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


                var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
                typeDropdown.SelectByText("Fixed Reduce");


                var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
                valueInput.Clear();
                valueInput.SendKeys("10000");


                var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
                beginInput.Clear();
                beginInput.SendKeys("22/02/2025 12:00");

                var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
                timeTypeDropdown.SelectByText("loop");


                var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
                durationInput.Clear();
                durationInput.SendKeys("120");


                var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
                addButton.Click();


                Thread.Sleep(2000); // Wait for success message or redirection
                var successMessage = wait.Until(driver => driver.FindElement(By.XPath("//div[@role='status']")));
                Assert.That(successMessage.Text, Is.EqualTo("Flash sale added successfully"));
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Element not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred: {ex.Message}");
            }
        }

        [Test]
        public void Add_FlashSale_Invalid_FixedReduce_Value_Not_A_Number()
        {
            try
            {

                driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


                var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
                typeDropdown.SelectByText("Fixed Reduce");


                var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
                valueInput.Clear();
                valueInput.SendKeys("xxxxxx");


                var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
                beginInput.Clear();
                beginInput.SendKeys("22/02/2025 12:00");


                var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
                timeTypeDropdown.SelectByText("loop");


                var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
                durationInput.Clear();
                durationInput.SendKeys("120");


                var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
                addButton.Click();


                var errorMessage = wait.Until(driver => driver.FindElement(By.Id("value-error")));
                Assert.That(errorMessage.Text, Is.EqualTo("Value must be a number"));
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Element not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred: {ex.Message}");
            }
        }

        [Test]
        public void Add_FlashSale_Invalid_FixedReduce_Value_Not_A_Minus_Number()
        {
            try
            {

                driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


                var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
                typeDropdown.SelectByText("Fixed Reduce");


                var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
                valueInput.Clear();
                valueInput.SendKeys("-10000");


                var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
                beginInput.Clear();
                beginInput.SendKeys("22/02/2025 12:00");


                var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
                timeTypeDropdown.SelectByText("loop");


                var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
                durationInput.Clear();
                durationInput.SendKeys("120");


                var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
                addButton.Click();


                var errorMessage = wait.Until(driver => driver.FindElement(By.Id("value-error")));
                Assert.That(errorMessage.Text, Is.EqualTo("Value must be a positive number"));
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Element not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred: {ex.Message}");
            }
        }

        [Test]
        public void Add_FlashSale_Invalid_Fixed_Value_Not_A_Number()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("fixed");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("xxxxxx");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("loop");


            var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
            durationInput.Clear();
            durationInput.SendKeys("120");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();

            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("value-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Value must be a number"));
        }


        [Test]
        public void Add_FlashSale_Invalid_Fixed_Value_Minus()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("fixed");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("-10000");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("loop");


            var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
            durationInput.Clear();
            durationInput.SendKeys("120");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();


            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("value-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Value must be a positive number"));
        }



        /// //////////////////////



        [Test]
        public void Add_FlashSale_Invalid_Begin_Empty()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("percentage");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("10%");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("loop");


            var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
            durationInput.Clear();
            durationInput.SendKeys("120");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();


            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("begin-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Begin date is required"));
        }



        /// ///////////////////////



        [Test]
        public void Add_FlashSale_Invalid_Loop_Duration_Empty()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("fixed");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("-10000");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("loop");

            // 6. Click add button
            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();

            // Verify error message at duration input
            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("duration-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Duration is required"));
        }





        /// ////////////////////////


        [Test]
        public void Add_FlashSale_Invalid_Loop_Duration_0()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("fixed");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("-10000");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("loop");


            var durationInput = wait.Until(driver => driver.FindElement(By.Id("duration")));
            durationInput.Clear();
            durationInput.SendKeys("0");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();


            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("duration-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Duration must be greater than 0"));
        }


        /// ////////////////////////////////
        [Test]
        public void Add_FlashSale_Invalid_Once_Expire_Empty()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("fixed");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("-10000");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("once");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();

            // Verify error message at expire input
            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("expire-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Expire date is required"));
        }



        /// ///////////////////////////


        [Test]
        public void Add_FlashSale_Invalid_Once_Expire_LTE_Begin()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("percentage");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("10%");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("once");


            var expireInput = wait.Until(driver => driver.FindElement(By.Id("expire")));
            expireInput.Clear();
            expireInput.SendKeys("10/02/2024 12:00");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();

            // Verify error message at expire input
            var errorMessage = wait.Until(driver => driver.FindElement(By.Id("expire-error")));
            Assert.That(errorMessage.Text, Is.EqualTo("Expire date must be greater than begin date"));
        }



        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>

        [Test]
        public void Add_FlashSale_Valid()
        {

            driver.Navigate().GoToUrl(BASE_URL + "/admin/flash-sale/add");


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            var typeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("type"))));
            typeDropdown.SelectByText("percentage");


            var valueInput = wait.Until(driver => driver.FindElement(By.Id("value")));
            valueInput.Clear();
            valueInput.SendKeys("10%");


            var beginInput = wait.Until(driver => driver.FindElement(By.Id("begin")));
            beginInput.Clear();
            beginInput.SendKeys("22/02/2025 12:00");


            var timeTypeDropdown = new SelectElement(wait.Until(driver => driver.FindElement(By.Id("timeType"))));
            timeTypeDropdown.SelectByText("once");


            var expireInput = wait.Until(driver => driver.FindElement(By.Id("expire")));
            expireInput.Clear();
            expireInput.SendKeys("28/02/2025 12:00");


            var addButton = wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Add']")));
            addButton.Click();


            var successMessage = wait.Until(driver => driver.FindElement(By.XPath("//div[@role='status']")));
            Assert.That(successMessage.Text, Is.EqualTo("Flash sale added successfully"));
        }


        [Test]
        public void Add_Product_Invalid_Title_Empty()
        {

            var addProductButton = driver.FindElement(By.Id("addProductButton")); // Giả sử nút "Add Product" có ID là "addProductButton"
            addProductButton.Click();


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


            var priceInput = driver.FindElement(By.Id("price"));
            priceInput.Clear();
            priceInput.SendKeys("100000");


            var oldPriceInput = driver.FindElement(By.Id("oldPrice"));
            oldPriceInput.Clear();
            oldPriceInput.SendKeys("500000");


            var tagsInput = driver.FindElement(By.Id("tags"));
            tagsInput.SendKeys("Xem phim, Giải trí");


            var categoriesDropdown = new SelectElement(driver.FindElement(By.Id("categories")));
            categoriesDropdown.SelectByText("Netflix");


            var imageInput = driver.FindElement(By.Id("image"));
            imageInput.SendKeys("C:\\path\\to\\file.png"); // Thay thế bằng đường dẫn thực tế đến file ảnh


            var addButton = driver.FindElement(By.Id("addButton")); // Giả sử nút "Add" có ID là "addButton"
            addButton.Click();

            // Verify that the Title input is auto-focused
            var titleInput = driver.FindElement(By.Id("title"));
            bool isTitleFocused = titleInput.Equals(driver.SwitchTo().ActiveElement());
            Assert.That(isTitleFocused, Is.True, "Title input should be auto-focused");
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