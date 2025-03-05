using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ASC_Testing.Src.NonUser;

public class View_ProductPage
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
    [Category("View_ProductPage_Fail")]
    [TestCase("https://anphashop-clone.vercel.app/grammarly-ai-1-thang-tang-cao-hieu-suat-voi-ai")]
    public void View_ProductPage_Fail_DeactiveProduct(string slug)
    {
        driver.Navigate().GoToUrl(slug);
        Assert.That(driver.FindElement(By.XPath("//h1[text()='Không tìm thấy sản phẩm.']")).Displayed, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(1000);
        driver.Quit();
        driver.Dispose();
    }
}