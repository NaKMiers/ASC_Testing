using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OfficeOpenXml;

namespace Testdoan
{
    public class Manage_Categories
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://asclone.vercel.app/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test_AddAccounts_Invalid()
        {
            // Read data from Excel
            string filePath = "path_to_your_excel_file.xlsx"; // Change to the correct path
            var testData = ReadTestData(filePath);

            foreach (var data in testData)
            {
                // Navigate to the account add page
                driver.Navigate().GoToUrl("https://asclone.vercel.app/admin/account/add");

                // Fill out the form with test data
                driver.FindElement(By.Id("type")).SendKeys(data.Type);
                driver.FindElement(By.Id("info")).SendKeys(data.Info);
                driver.FindElement(By.Id("renew")).SendKeys(data.Renew);
                driver.FindElement(By.Id("days")).SendKeys(data.Days.ToString());
                driver.FindElement(By.Id("hours")).SendKeys(data.Hours.ToString());
                driver.FindElement(By.Id("minutes")).SendKeys(data.Minutes.ToString());
                driver.FindElement(By.Id("seconds")).SendKeys(data.Seconds.ToString());

                // Click Add button (assuming you have a button with id "addButton")
                driver.FindElement(By.Id("addButton")).Click();

                // Verify error message or result (modify as per actual validation logic)
                Assert.That(driver.PageSource.Contains("error message"), Is.True, "Error message should be displayed.");
            }
        }

        private List<TestData> ReadTestData(string filePath)
        {
            var testDataList = new List<TestData>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
                {
                    var data = new TestData
                    {
                        Type = worksheet.Cells[row, 1].Text,
                        Info = worksheet.Cells[row, 2].Text,
                        Renew = worksheet.Cells[row, 3].Text,
                        Days = int.Parse(worksheet.Cells[row, 4].Text),
                        Hours = int.Parse(worksheet.Cells[row, 5].Text),
                        Minutes = int.Parse(worksheet.Cells[row, 6].Text),
                        Seconds = int.Parse(worksheet.Cells[row, 7].Text)
                    };
                    testDataList.Add(data);
                }
            }

            return testDataList;
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

    // Class to hold the test data
    public class TestData
    {
        public string Type { get; set; }
        public string Info { get; set; }
        public string Renew { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
}
