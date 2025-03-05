using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Globalization;

namespace ASC_Testing.Src.Admin;

public class Manage_Orders
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


  private void AdminLogin(string usernameOrEmail = "diwas118151@gmail.com", string password = "Asdasd@1")
  {
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/login");
    driver.FindElement(By.Id("usernameOrEmail")).SendKeys(usernameOrEmail);
    driver.FindElement(By.Id("password")).SendKeys(password);
    driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
    Thread.Sleep(2000);
  }

  [Test]
  [Category("View_OrderManagementPage_Unauthorized")] // T31.2.1
  public void View_OrderManagementPage_Normally()
  {
    AdminLogin();

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");
    Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all"));
  }

  [Test]
  [Category("View_OrderManagementPage_Normally")]
  [TestCase("?sort=createdAt|1")] // T31.3.1
  public void View_OrderManagementPage_With_QueryParams(string query)
  {
    AdminLogin();

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all" + query);

    Assert.Multiple(
      () =>
      {
        Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all" + query));
        Thread.Sleep(1000);
        Assert.That(driver.FindElement(By.Id("sort")).GetAttribute("value"), Is.EqualTo("createdAt|1"));
      }
    );
  }


  [Test]
  [Category("Search_Orders_OrderManagementPage_Valid")]
  [TestCase("CA938")] // T31.4.1
  public void Search_Orders_OrderManagementPage_Valid(string search)
  {
    AdminLogin();

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    driver.FindElement(By.Id("search")).SendKeys(search);
    driver.FindElement(By.XPath("//button[text()='Filter']")).Click();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("search="));
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all?search=" + search));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Search failed");
    }
  }

  [Test] // T31.5.1
  [Category("Search_Orders_OrderManagementPage_Invalid")]
  [TestCase("")]
  public void Search_Orders_OrderManagementPage_Invalid_Empty(string search)
  {
    AdminLogin();

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    driver.FindElement(By.Id("search")).SendKeys(search);
    driver.FindElement(By.XPath("//button[text()='Filter']")).Click();

    Thread.Sleep(2000);
    Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all"));
  }

  [Test] // T31.6.1
  [Category("Filter_Orders_OrderManagementPage")]
  public void Filter_Orders_OrderManagementPage_Total()
  {
    AdminLogin();

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    Actions actions = new(driver);
    var totalInput = driver.FindElement(By.Id("total"));

    actions.ClickAndHold(totalInput)
          .MoveByOffset(50, 0)
          .Release()
          .Perform();

    string totalValue = totalInput.GetAttribute("value");


    driver.FindElement(By.XPath("//button[text()='Filter']")).Click();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("total="));
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all?total=" + totalValue));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Filter failed");
    }
  }

  [Test] // T31.6.2
  [Category("Filter_Orders_OrderManagementPage")]
  [TestCase("22/02/2025 12:00")] // DD/MM/YYYY HH:MM
  public void Filter_Orders_OrderManagementPage_From(string from)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");


  }




  [TearDown]
  public void TearDown()
  {
    Thread.Sleep(1000);
    driver.Quit();
    driver.Dispose();
  }
}