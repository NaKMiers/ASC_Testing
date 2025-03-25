using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
namespace TestProject1
{
    public class Manage_Products_Tests
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


        //không tìm thấy trang
        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_Title_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("price")).SendKeys("100000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("500000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("file.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var titleInput = driver.FindElement(By.Id("title"));
            Assert.That(titleInput, Is.Not.Null);
            Assert.That(titleInput.Equals(driver.SwitchTo().ActiveElement()), "Title input should be auto-focused");
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_Title_TooLong(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys(new string('A', 201));
            driver.FindElement(By.Id("price")).SendKeys("40000000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'Title is too long')]")).Text;
            Assert.That(errorMessage, Is.EqualTo("Title is too long"), "Should show 'Title is too long' error message");
        }



        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_Description_TooLong(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("description")).SendKeys(new string('A', 2001));
            driver.FindElement(By.Id("price")).SendKeys("40000000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'Description is too long')]")).Text;
            Assert.That(errorMessage, Is.EqualTo("Description is too long"), "Should show 'Description is too long' error message");
        }



        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_Price_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("oldPrice")).SendKeys("99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var priceInput = driver.FindElement(By.Id("price"));
            Assert.That(priceInput, Is.Not.Null);
            Assert.That(priceInput.Equals(driver.SwitchTo().ActiveElement()), "Price input should be auto-focused");
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_Price_Minus(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("-1000000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'Price is minus')]")).Text;
            Assert.That(errorMessage, Is.EqualTo("Price is minus"), "Should show 'Price is minus' error message");
        }


        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_OldPrice_Empty(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("40000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var focusedElement = driver.SwitchTo().ActiveElement();
            Assert.That(focusedElement.GetAttribute("id"), Is.EqualTo("oldPrice"), "Old Price input should be auto-focused");
        }

        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_OldPrice_Minus(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("40000000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("-99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'Old Price is minus')]")).Text;
            Assert.That(errorMessage, Is.EqualTo("Old Price is minus"), "Should show 'Old Price is minus' error message");
        }



        [Test]
        [TestCase("phanthang", "Phanthang01")]
        public void Add_Product_Invalid_Price_GT_OldPrice(string username, string password)
        {
            Login(username, password);
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.XPath("//button[text()='Add Product']")).Click();
            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("1000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("2000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("categories")).SendKeys("Netflix");
            driver.FindElement(By.Id("images")).SendKeys("image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'Price cannot be greater than Old Price')]")).Text;
            Assert.That(errorMessage, Is.EqualTo("Price cannot be greater than Old Price"), "Should show 'Price cannot be greater than Old Price' error message");
        }




        [Test]
        public void Add_Product_Invalid_No_TagSelected()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/add");

            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("1000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("2000");
            driver.FindElement(By.Id("category")).SendKeys("Netflix");
            driver.FindElement(By.Id("imageUpload")).SendKeys("C:\\path\\to\\image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("No tags selected"));
        }



        [Test]
        public void Add_Product_Invalid_No_CategorySelected()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/add");

            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("1000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("2000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("imageUpload")).SendKeys("C:\\path\\to\\image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("No categories selected"));
        }


        [Test]
        public void Add_Product_Invalid_No_Image()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/add");

            driver.FindElement(By.Id("title")).SendKeys("xxxxxx");
            driver.FindElement(By.Id("price")).SendKeys("1000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("2000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("category")).SendKeys("Netflix");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("No image selected"));
        }

        [Test]
        public void Add_Product_Valid()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/add");

            driver.FindElement(By.Id("title")).SendKeys("xxxx");
            driver.FindElement(By.Id("price")).SendKeys("40000000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("category")).SendKeys("Netflix");
            driver.FindElement(By.Id("imageUpload")).SendKeys("C:\\path\\to\\image.png");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been added"));
        }

        [Test]
        public void Edit_Product_Invalid_No_TagSelected()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/edit");

            driver.FindElement(By.Id("tags")).Clear();
            driver.FindElement(By.XPath("//button[text()='Save']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("No tags selected"));
        }

        [Test]
        public void Edit_Product_Invalid_No_CategorySelected()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/edit");

            driver.FindElement(By.Id("category")).Clear();
            driver.FindElement(By.XPath("//button[text()='Save']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("No categories selected"));
        }

        [Test]
        public void Edit_Product_Invalid_No_Image()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/edit");

            driver.FindElement(By.Id("imageUpload")).Clear();
            driver.FindElement(By.XPath("//button[text()='Save']")).Click();

            var errorMessage = driver.FindElement(By.ClassName("error-message")).Text;
            Assert.That(errorMessage, Is.EqualTo("No image selected"));
        }


        [Test]
        public void Edit_Product_Valid()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products/edit");

            driver.FindElement(By.Id("title")).SendKeys("xxxx");
            driver.FindElement(By.Id("price")).SendKeys("40000000");
            driver.FindElement(By.Id("oldPrice")).SendKeys("99000000");
            driver.FindElement(By.Id("tags")).SendKeys("Xem phim, Giải trí");
            driver.FindElement(By.Id("category")).SendKeys("Netflix");
            driver.FindElement(By.Id("imageUpload")).SendKeys("C:\\path\\to\\image.png");
            driver.FindElement(By.XPath("//button[text()='Save']")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been added"));
        }


        [Test]
        public void Edit_ProductProperty_Sold()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            var soldElement = driver.FindElement(By.Id("sold"));
            soldElement.Click();
            soldElement.Clear();
            soldElement.SendKeys("2000");
            driver.FindElement(By.TagName("body")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Success"));
        }

        [Test]
        public void Edit_ProductProperty_Stock()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            var stockElement = driver.FindElement(By.Id("stock"));
            stockElement.Click();
            stockElement.Clear();
            stockElement.SendKeys("20");
            driver.FindElement(By.TagName("body")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Success"));
        }


        [Test]
        public void Activate_Products_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("activate-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been activated"));
        }

        [Test]
        public void Activate_Products_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("product-checkbox-1")).Click();
            driver.FindElement(By.Id("product-checkbox-2")).Click();
            driver.FindElement(By.Id("activate-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Products have been activated"));
        }

        [Test]
        public void Deactivate_Products_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("deactivate-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been deactivated"));
        }

        [Test]
        public void Deactivate_Products_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("product-checkbox-1")).Click();
            driver.FindElement(By.Id("product-checkbox-2")).Click();
            driver.FindElement(By.Id("deactivate-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Products have been deactivated"));
        }


        [Test]
        public void Boot_Products_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("boot-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been booted"));
        }

        [Test]
        public void Boot_Products_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("product-checkbox-1")).Click();
            driver.FindElement(By.Id("product-checkbox-2")).Click();
            driver.FindElement(By.Id("boot-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Products have been booted"));
        }


        [Test]
        public void Deboot_Products_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("deboot-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been debooted"));
        }

        [Test]
        public void Deboot_Products_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("product-checkbox-1")).Click();
            driver.FindElement(By.Id("product-checkbox-2")).Click();
            driver.FindElement(By.Id("deboot-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Products have been debooted"));
        }


        [Test]
        public void Remove_ProductsFlashsale_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("remove-flashsale-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been removed flash sale"));
        }

        [Test]
        public void Remove_ProductsFlashsale_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("product-checkbox-1")).Click();
            driver.FindElement(By.Id("product-checkbox-2")).Click();
            driver.FindElement(By.Id("remove-flashsale-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Products have been removed flash sale"));
        }

        [Test]
        public void Sync_Products_Single()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("sync-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Product has been sync"));
        }

        [Test]
        public void Sync_Products_Multiple()
        {
            Login("phanthang", "Phanthang01");
            driver.Navigate().GoToUrl(BASE_URL + "/admin/products");

            driver.FindElement(By.Id("product-checkbox-1")).Click();
            driver.FindElement(By.Id("product-checkbox-2")).Click();
            driver.FindElement(By.Id("sync-button")).Click();

            var successMessage = driver.FindElement(By.ClassName("success-message")).Text;
            Assert.That(successMessage, Is.EqualTo("Products have been sync"));
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