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

  // MARK: Common method
  private void AdminLogin(string usernameOrEmail = "diwas118151@gmail.com", string password = "Asdasd@1")
  {
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/login");
    driver.FindElement(By.Id("usernameOrEmail")).SendKeys(usernameOrEmail);
    driver.FindElement(By.Id("password")).SendKeys(password);
    driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
    Thread.Sleep(2000);
  }

  private void Filter()
  {
    Thread.Sleep(500);
    driver.FindElement(By.XPath("//button[text()='Filter']")).Click();
  }

  [Test]
  [Category("View_OrderManagementPage_Unauthorized")] // T31.2.1
  public void View_OrderManagementPage_Unauthorized()
  {
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");
    Assert.That(driver.Url.Trim('/'), Is.EqualTo(Constants.BASE_URL.Trim('/')));
  }

  [Test] // T31.3.1
  [Category("View_OrderManagementPage_Normally")]
  public void View_OrderManagementPage_Normally()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");
    Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all"));
  }

  [Category("View_OrderManagementPage_With_QueryParams")]
  [TestCase("?sort=createdAt|1")] // T31.3.2
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
    Filter();

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
    Filter();

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

    Filter();

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

  [Test]
  [Category("Filter_Orders_OrderManagementPage")]
  [TestCase("22/02/2025 12:00", null)] // T31.6.2
  [TestCase(null, "22/02/2025 12:00")] // T31.6.3
  [TestCase("22/02/2025 12:00", "28/02/2025 12:00")] // T31.6.4
  public void Filter_Orders_OrderManagementPage_FromTo(string? from, string? to)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    string fromDate = "";
    string toDate = "";

    if (from != null)
    {
      DateTime filterDate = DateTime.ParseExact(from, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
      IWebElement input = driver.FindElement(By.Id("from"));
      fromDate = filterDate.ToString("yyyy-MM-ddTHH:mm");
      driver.ExecuteScript("arguments[0].value = arguments[1];", input, fromDate);
      input.SendKeys(Keys.ArrowRight);
      input.SendKeys(Keys.Enter);
    }
    if (to != null)
    {
      DateTime filterDate = DateTime.ParseExact(to, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
      IWebElement input = driver.FindElement(By.Id("to"));
      toDate = filterDate.ToString("yyyy-MM-ddTHH:mm");
      driver.ExecuteScript("arguments[0].value = arguments[1];", input, toDate);
      input.SendKeys(Keys.ArrowRight);
      input.SendKeys(Keys.Enter);
    }

    Filter();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("from-to="));
      Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + "/admin/order/all?from-to=" + fromDate + "|" + toDate));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Filter failed");
    }
  }

  [Test]
  [Category("Filter_Orders_OrderManagementPage")]
  [TestCase("userId", "true")] // T31.6.5
  [TestCase("voucherApplied", "true")] // T31.6.6
  [TestCase("status", "pending")] // T31.6.7
  [Category("Sort_Orders_OrderManagementPage")]
  [TestCase("sort", "updatedAt|-1", true)] // T31.7.1
  [TestCase("sort", "updatedAt|1")] // T31.7.2
  [TestCase("sort", "createdAt|-1")] // T31.7.3
  [TestCase("sort", "createdAt|1")] // T31.7.4
  public void Filter_Orders_OrderManagementPage_Selection(string inputId, string value, bool? isDefault = true)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    Thread.Sleep(500);
    IWebElement dropdown = driver.FindElement(By.Id(inputId));
    dropdown.Click();
    IWebElement option = dropdown.FindElement(By.XPath($".//option[@value='{value}']"));
    option.Click();

    Filter();

    try
    {
      if (isDefault != true)
      {
        wait.Until(ExpectedConditions.UrlContains($"{inputId}="));
        Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + $"/admin/order/all?{inputId}={value}"));
      }
      else
      {
        Thread.Sleep(1000);
        Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + "/admin/order/all"));
      }
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Filter failed");
    }
  }

  [Test] // T31.8.1
  [Category("Reset_Filter_OrderManagementPage")]
  public void Reset_Filter_OrderManagementPage()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all?userId=false&voucherApplied=false&status=pending");

    driver.FindElement(By.XPath("//button[text()='Reset']")).Click();

    try
    {
      wait.Until(driver =>
      {
        string currentUrl = driver.Url;
        return !currentUrl.Contains("userId=") &&
              !currentUrl.Contains("voucherApplied=") &&
              !currentUrl.Contains("status=");
      });

      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all"));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Reset failed");
    }
  }

  [Test] // 31.9.1
  [Category("Delete_Orders")]
  public void Delete_Orders_Single()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    // click on delete button of the first order
    driver.FindElement(By.XPath("//button[@title='Delete']")).Click();
    driver.FindElement(By.CssSelector(".fixed:nth-child(2) .rounded-lg:nth-child(2)")).Click();

    Thread.Sleep(1000);

    try
    {
      var successMessage = wait.Until(driver =>
          driver.FindElement(By.ClassName("go2072408551"))
      );

      Assert.That(successMessage.Text, Does.Contain("been deleted"));
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Error message element was not found.");
    }
  }

  [Test] // T31.9.2
  [Category("Delete_Orders")]
  public void Delete_Orders_Multiple()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");
    Thread.Sleep(500);

    IReadOnlyList<IWebElement> codeElements = driver.FindElements(By.XPath("//p[@title='code']"));
    for (int i = 0; i < Math.Min(2, codeElements.Count); i++)
    {
      codeElements[i].Click();
    }

    driver.FindElement(By.XPath("//button[text()='Delete']")).Click();
    Thread.Sleep(500);
    driver.FindElement(By.CssSelector(".w-full > .fixed .rounded-lg:nth-child(2)")).Click();


    try
    {
      var successMessage = wait.Until(driver =>
          driver.FindElement(By.ClassName("go2072408551"))
      );

      Assert.That(successMessage.Text, Does.Contain("been deleted"));
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Error message element was not found.");
    }
  }

  [Test] // T31.10.1
  [Category("Deliver_Orders")]
  public void Deliver_Orders_NoMessage()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    // click on deliver button of the first order
    driver.FindElement(By.XPath("//button[@title='Deliver']")).Click();
    driver.FindElement(By.CssSelector(".fixed:nth-child(2) .rounded-lg:nth-child(2)")).Click();
    Thread.Sleep(10000);

    try
    {
      var successMessage = wait.Until(driver =>
          driver.FindElement(By.ClassName("go2072408551"))
      );

      Assert.That(successMessage.Text, Does.Contain("been delivered"));
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Error message element was not found.");
    }
  }

  [Test] // T31.13.1
  [Category("Order_Quick_Search")]
  public void Order_Quick_Search_Email()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    // click on the search icon of the first order
    driver.FindElement(By.CssSelector(".justify:nth-child(1) > .w-\\[calc\\(100\\%_-_44px\\)\\] .group:nth-child(1) > .wiggle")).Click();

    try
    {
      wait.Until(ExpectedConditions.UrlContains($"search="));
      Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + "/admin/order/all?search="));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Search failed");
    }
  }

  [Test] // T31.13.2
  [Category("Order_Quick_Search")]
  public void Order_Quick_Search_AccountEmail()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    // click on the search account icon of the first order
    driver.FindElement(By.CssSelector(".justify:nth-child(1) > .w-\\[calc\\(100\\%_-_44px\\)\\] .group:nth-child(2) > .wiggle")).Click();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("/admin/account/all"));
      Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + "/admin/account/all?search="));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Search failed");
    }
  }

  [TearDown]
  public void TearDown()
  {
    Thread.Sleep(1000);
    driver.Quit();
    driver.Dispose();
  }
}