using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using System.Text.RegularExpressions;

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
  [TestCase("khoa.json")] // T31.3.2
  public void View_OrderManagementPage_With_QueryParams(string dataFile)
  {
    AdminLogin();
    Dictionary<string, string> result = Files.Read(dataFile);

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all" + result["query"]);

    Assert.Multiple(
      () =>
      {
        Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all" + result["query"]));
        Thread.Sleep(2000);
        Assert.That(driver.FindElement(By.Id("sort")).GetAttribute("value"), Is.EqualTo("createdAt|1"));
      }
    );
  }


  [Test]
  [Category("Search_Orders_OrderManagementPage_Valid")]
  [TestCase("khoa.json")] // T31.4.1
  public void Search_Orders_OrderManagementPage_Valid(string dataFile)
  {
    AdminLogin();
    Dictionary<string, string> result = Files.Read(dataFile);

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    driver.FindElement(By.Id("search")).SendKeys(result["search"]);
    Filter();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("search="));
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/order/all?search=" + result["search"]));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Search failed");
    }
  }

  [Test] // T31.5.1
  [Category("Search_Orders_OrderManagementPage_Invalid")]
  public void Search_Orders_OrderManagementPage_Invalid_Empty()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    driver.FindElement(By.Id("search")).SendKeys("");
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
  [TestCase("khoa.json", 0)] // T31.6.2
  [TestCase("khoa.json", 1)] // T31.6.3
  [TestCase("khoa.json", 2)] // T31.6.4
  public void Filter_Orders_OrderManagementPage_FromTo(string dataFile, int index)
  {
    AdminLogin();
    Dictionary<string, string> result = Files.Read(dataFile, index);
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    string fromDate = "";
    string toDate = "";

    if (result["from"] != "null")
    {
      DateTime filterDate = DateTime.ParseExact(result["from"], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
      IWebElement input = driver.FindElement(By.Id("from"));
      fromDate = filterDate.ToString("yyyy-MM-ddTHH:mm");
      driver.ExecuteScript("arguments[0].value = arguments[1];", input, fromDate);
      input.SendKeys(Keys.ArrowRight);
      input.SendKeys(Keys.Enter);
    }
    if (result["to"] != "null")
    {
      DateTime filterDate = DateTime.ParseExact(result["to"], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
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
  [TestCase("khoa.json", 0)] // T31.6.5
  [TestCase("khoa.json", 1)] // T31.6.6
  [TestCase("khoa.json", 2)] // T31.6.7
  [Category("Sort_Orders_OrderManagementPage")]
  [TestCase("khoa.json", 3)] // T31.7.1
  [TestCase("khoa.json", 4)] // T31.7.2
  [TestCase("khoa.json", 5)] // T31.7.3
  [TestCase("khoa.json", 6)] // T31.7.4
  public void Filter_Orders_OrderManagementPage_Selection(string dataFile, int index)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    Dictionary<string, string> result = Files.Read(dataFile, index);
    string inputId = result["inputId"];
    string value = result["value"];
    bool? isDefault = result["isDefault"] == "true";


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

    // click on delete button of the first order
    driver.FindElement(By.XPath("//button[@title='Deliver']")).Click();
    driver.FindElement(By.CssSelector(".border-yellow-500")).Click();
    Thread.Sleep(5000);

    try
    {
      var successMessage = wait.Until(driver => driver.FindElement(By.ClassName("go2072408551")));
      Assert.That(successMessage.Text, Does.Contain("Deliver Order Successfully"));
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Success message element was not found.");
    }
  }

  [Test] // T31.11.1
  [Category("Redeliver_Orders")]
  public void Redeliver_Orders_NoMessage()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    // click on delete button of the first order
    driver.FindElement(By.XPath("//button[@title='Re-Deliver']")).Click();
    driver.FindElement(By.CssSelector(".border-sky-500")).Click();
    Thread.Sleep(2000);

    try
    {
      var successMessage = wait.Until(driver => driver.FindElement(By.ClassName("go2072408551")));
      Assert.That(successMessage.Text, Does.Contain("Re-deliver Order Successfully!"));
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Success message element was not found.");
    }
  }

  [Test] // T31.12.1
  [Category("Edit_OrderDetails")]
  public void View_OrderDetails()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    // click on the view icon of the first order
    driver.FindElement(By.XPath("//a[@title='Detail']")).Click();

    try
    {
      wait.Until(driver => !driver.Url.Contains("/all"));
      Assert.That(Regex.IsMatch(driver.Url, Constants.BASE_URL + @"/admin/order/[A-Za-z0-9]{5}$"), Is.True, "Cannot view order details");
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("View failed");
    }
  }

  [Test]
  [Category("Edit_OrderDetails")]
  [TestCase("khoa.json", 0)] // T31.12.2
  [TestCase("khoa.json", 1)] // T31.12.3
  [TestCase("khoa.json", 2)] // T31.12.4
  [TestCase("khoa.json", 3)] // T31.12.5
  public void Edit_OrderDetails_Fail_Field_EmptyOrZero(string dataFile, int index)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    Dictionary<string, string> result = Files.Read(dataFile, index);
    string inputId = result["inputId"];
    string value = result["value"];

    // click on the view icon of the first order
    driver.FindElement(By.XPath("//a[@title='Detail']")).Click();
    wait.Until(driver => !driver.Url.Contains("/all"));

    // click on the edit button
    driver.FindElement(By.CssSelector("button.text-secondary")).Click();
    Thread.Sleep(1000);

    var input = driver.FindElement(By.Id(inputId));
    input.Clear();
    if (value != "") input.SendKeys(value);
    Thread.Sleep(1000);

    // click on save button
    driver.FindElement(By.CssSelector("button.text-rose-500")).Click();

    try
    {
      // if cursor is focused on the createdAt input is pass
      var activeElement = driver.SwitchTo().ActiveElement();
      wait.Until(driver => input.Equals(activeElement));
      Assert.That(input, Is.EqualTo(activeElement));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Edit failed");
    }
  }

  [Test] // T31.12.6
  [Category("Edit_OrderDetails")]
  public void Edit_OrderDetails_Fail_Items_Empty()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");
    Thread.Sleep(1000);

    // click on the view icon of the first order
    driver.FindElement(By.XPath("//a[@title='Detail']")).Click();
    wait.Until(driver => !driver.Url.Contains("/all"));
    Thread.Sleep(1000);

    // click on the edit button
    driver.FindElement(By.CssSelector("button.text-secondary")).Click();

    Thread.Sleep(1000);
    var removeItemButtons = driver.FindElements(By.CssSelector("button.h-6.border-rose-500.text-rose-500"));
    foreach (var button in removeItemButtons)
    {
      button.Click();
    }

    // click on save button
    Thread.Sleep(500);
    driver.FindElement(By.CssSelector("button.text-rose-500")).Click();

    try
    {
      var toast = wait.Until(driver => driver.FindElement(By.ClassName("go2072408551")));
      Assert.That(toast.Text, Does.Contain("Order items cannot be empty"));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Edit failed");
    }
  }

  [Test]
  [Category("Edit_OrderDetails")]
  [TestCase("khoa.json")] // T31.12.7
  public void Edit_OrderDetails_Success(string dataFile)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/order/all");

    Dictionary<string, string> result = Files.Read(dataFile, 0);
    string date = result["date"];
    string status = result["status"];
    string email = result["email"];
    double total = double.Parse(result["total"]);

    // click on the view icon of the first order
    driver.FindElement(By.XPath("//a[@title='Detail']")).Click();
    wait.Until(driver => !driver.Url.Contains("/all"));
    Thread.Sleep(1000);

    // click on the edit button
    driver.FindElement(By.CssSelector("button.text-secondary")).Click();

    // update date
    if (date != null && date != "")
    {
      Thread.Sleep(1000);
      DateTime filterDate = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
      IWebElement input = driver.FindElement(By.Id("createdAt"));
      string buyDate = filterDate.ToString("yyyy-MM-ddTHH:mm");
      driver.ExecuteScript("arguments[0].value = arguments[1];", input, buyDate);
      input.SendKeys(Keys.ArrowRight);
      input.SendKeys(Keys.Enter);
    }

    // update status
    if (status != null && status != "")
    {
      IWebElement dropdown = driver.FindElement(By.Id("status"));
      dropdown.Click();
      IWebElement option = dropdown.FindElement(By.XPath($".//option[@value='{status}']"));
      option.Click();
    }

    // update email
    if (email != null && email != "")
    {
      Thread.Sleep(1000);
      var input = driver.FindElement(By.Id("email"));
      input.Clear();
      input.SendKeys(email);
    }

    // update total
    if (total != 0)
    {
      Thread.Sleep(1000);
      var input = driver.FindElement(By.Id("total"));
      input.Clear();
      input.SendKeys(total.ToString());
    }

    // click on save button
    Thread.Sleep(1000);
    driver.FindElement(By.CssSelector("button.text-rose-500")).Click();
    Thread.Sleep(2000);

    try
    {
      var toast = wait.Until(driver => driver.FindElement(By.ClassName("go2072408551")));
      Assert.That(toast.Text, Does.Contain("Order has been updated"));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Edit failed");
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