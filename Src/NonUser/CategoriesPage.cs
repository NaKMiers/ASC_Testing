using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_Testing.Src.NonUser
{
    public class CategoriesPage
    {
        private ChromeDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Constants.BASE_URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        [Category("View_CategoriesPage_Normally")]
        public void View_CategoriesPage_Normally()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=spotify");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
        }

        [Test]
        [Category("View_CategoriesPage_With_QueryParams")]
        public void View_CategoriesPage_With_QueryParams()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=spotify&price=30000&stock=30");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
        }

        [Test]
        [Category("Search_Products_By_Categories_Vaild")]
        public void Search_Products_By_Categories_Vaild_NotFound()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=spotify");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("XXXXXXXXX");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_Categories_Vaild")]
        public void Search_Products_By_Categories_Vaild_Found()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=spotify");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("Spotify");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Search_Products_By_Categories_Invalid")]
        public void Search_Products_By_Categories_Invalid_Empty()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=grammarly");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var searchProduct = driver.FindElement(By.CssSelector("input[id='search']"));
            searchProduct.SendKeys("");
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Filter_Products_CategoriesPage")]
        public void Filter_Products_CategoriesPage_Selection()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=microsoft-office&ctg=canva");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_CategoriesPage")]
        public void Sort_Products_CategoriesPage_LatestAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=netflix");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Cập nhật cũ nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_CategoriesPage")]
        public void Sort_Products_CategoriesPage_LatestDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=netflix");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Cập nhật mới nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_CategoriesPage")]
        public void Sort_Products_CategoriesPage_CreatedAsc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=netflix");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Cũ nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Sort_Products_CategoriesPage")]
        public void Sort_Products_CategoriesPage_CreatedDesc()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=netflix");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            var sortDropdown = driver.FindElement(By.CssSelector("select[id='sort']"));
            sortDropdown.Click();
            Thread.Sleep(1000);
            var updateOldestOption = driver.FindElement(By.XPath("//option[contains(text(),'Mới nhất')]"));
            updateOldestOption.Click();
            driver.FindElement(By.XPath("//button[text()='Lọc']")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        [Category("Reset_Filter_CategoriesPage")]
        public void Reset_Filter_CategoriesPage()
        {
            driver.Navigate().GoToUrl(Constants.BASE_URL + "/category?ctg=netflix");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
            driver.FindElement(By.XPath("//button[text()='Đặt lại']")).Click();
            Thread.Sleep(2000);
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
