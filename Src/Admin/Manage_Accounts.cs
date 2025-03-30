using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_Testing.Src.Admin;

public class Manage_Accounts
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

  [Test]
  [Category("View_AccountManagementPage_Unauthorized")] // T32.1.1
  public void View_AccountManagementPage_Unauthorized()
  {
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");
    Assert.That(driver.Url.Trim('/'), Is.EqualTo(Constants.BASE_URL));
  }

  [Test] // T32.2.1
  [Category("View_AccountManagementPage_Normally")]
  public void View_AccountManagementPage_Normally()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");
    Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all"));
  }

  [Category("View_AccountManagementPage_With_QueryParams")]
  [TestCase("khoa.json")] // T32.3.1
  public void View_AccountManagementPage_With_QueryParams(string dataFile)
  {
    AdminLogin();
    Dictionary<string, string> result = Files.Read(dataFile);
    string query = result["query"];

    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all" + query);

    Assert.Multiple(
      () =>
      {
        Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all" + query));
        Thread.Sleep(2000);
        Assert.That(driver.FindElement(By.Id("sort")).GetAttribute("value"), Is.EqualTo("createdAt|1"));
      }
    );
  }


  [Test]
  [Category("Search_Accounts_AccountManagementPage_Valid")]
  [TestCase("khoa.json")] // T32.4.1
  public void Search_Accounts_AccountManagementPage_Valid(string dataFile)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    Dictionary<string, string> result = Files.Read(dataFile);
    string search = result["search"];

    driver.FindElement(By.Id("search")).SendKeys(search);
    Filter();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("search="));
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all?search=" + search));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Search failed");
    }
  }

  [Test] // T32.5.1
  [Category("Search_Accounts_AccountManagementPage_Invalid")]
  public void Search_Accounts_AccountManagementPage_Invalid_Empty()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    driver.FindElement(By.Id("search")).SendKeys("");
    Filter();

    Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all"));
  }

  [Test]
  [Category("Filter_Accounts_AccountManagementPage")]
  [TestCase("khoa.json")] // T32.6.2
  public void Filter_Accounts_AccountManagementPage_Type(string dataFile)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    Dictionary<string, string> result = Files.Read(dataFile);

    Thread.Sleep(500);
    var allButton = driver.FindElement(By.XPath("//*[text()='All']"));
    allButton.Click();
    var element = driver.FindElement(By.XPath($"//*[@title='{result["type"]}']"));
    element.Click();

    Filter();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("product="));
      Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + "/admin/account/all?product="));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Filter failed");
    }
  }


  [Test]
  [Category("Filter_Accounts_AccountManagementPage")]
  [TestCase("khoa.json", 0)] // T32.6.2
  [TestCase("khoa.json", 1)] // T32.6.4
  [TestCase("khoa.json", 2)] // T32.6.6 
  [TestCase("khoa.json", 3)] // T32.6.8 
  [Category("Sort_Accounts_AccountManagementPage")]
  [TestCase("khoa.json", 4)] // T32.7.1
  [TestCase("khoa.json", 5)] // T32.7.2
  [TestCase("khoa.json", 6)] // T32.7.3
  [TestCase("khoa.json", 7)] // T32.7.4
  public void Filter_Accounts_AccountManagementPage_Selection(string dataFile, int index)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    Dictionary<string, string> result = Files.Read(dataFile);
    string inputId = result["inputId"];
    string value = result["value"];
    bool? isDefault = result["isDefault"] == "true";

    Thread.Sleep(3000);
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
        Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + $"/admin/account/all?{inputId}={value}"));
      }
      else
      {
        Thread.Sleep(1000);
        Assert.That(driver.Url, Does.Contain(Constants.BASE_URL + "/admin/account/all"));
      }
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Filter failed");
    }
  }

  [Test]
  [Category("Filter_Accounts_AccountManagementPage")]
  [TestCase("khoa.json", 0)] // T32.6.3
  [TestCase("khoa.json", 1)] // T32.6.5
  [TestCase("khoa.json", 2)] // T32.6.7
  [TestCase("khoa.json", 3)] // T32.6.9
  public void Filter_Accounts_AccountManagementPage_TypeAndDropdown(string dataFile, int index)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    Dictionary<string, string> result = Files.Read(dataFile, index);
    string type = result["type"];
    string dropdownId = result["dropdownId"];
    string value = result["value"];

    // Select type
    Thread.Sleep(3000);
    var allButton = driver.FindElement(By.XPath("//*[text()='All']"));
    allButton.Click();
    var element = driver.FindElement(By.XPath($"//*[@title='{type}']"));
    element.Click();

    // Select dropdown
    Thread.Sleep(500);
    IWebElement dropdown = driver.FindElement(By.Id(dropdownId));
    dropdown.Click();
    IWebElement option = dropdown.FindElement(By.XPath($".//option[@value='{value}']"));
    option.Click();

    Filter();

    try
    {
      wait.Until(ExpectedConditions.UrlContains("product="));
      Assert.Multiple(() =>
        {
          Assert.That(driver.Url, Does.Contain($"product="));
          Assert.That(driver.Url, Does.Contain($"{dropdownId}={value}"));
        }
      );
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Filter failed");
    }
  }

  [Test] // T32.8.1
  [Category("Reset_Filter_AccountManagementPage")]
  public void Reset_Filter_AccountManagementPage()
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all?usingUser=false&product=65631d1687665e6a3329ac8a");

    Thread.Sleep(1000);
    driver.FindElement(By.XPath("//button[text()='Reset']")).Click();

    try
    {
      wait.Until(driver =>
      {
        string currentUrl = driver.Url;
        return !currentUrl.Contains("usingUser=") && !currentUrl.Contains("product=");
      });

      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all"));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("Reset failed");
    }
  }

  [Test]
  public void Test_Delete_Account_Single()
  {
    AdminLogin();
    ClickElement(By.Id("btnDeleteSingle"));
    ClickElement(By.Id("btnConfirm"));
    Assert.That(driver.PageSource.Contains("account has been deleted"), Is.True, "Xóa tài khoản đơn lẻ thất bại.");
  }

  [Test]
  public void Test_Delete_Account_Multiple()
  {
    AdminLogin();
    SelectMultipleAccounts();
    ClickElement(By.Id("btnDeleteMultiple"));
    ClickElement(By.Id("btnConfirm"));
    Assert.That(driver.PageSource.Contains("accounts have been deleted"), Is.True, "Xóa nhiều tài khoản thất bại.");
  }

  [Test]
  public void Test_Deactivate_Account_Single()
  {
    AdminLogin();
    ClickElement(By.Id("btnDeactivateSingle"));
    Assert.That(driver.PageSource.Contains("1 account has been deactivated"), Is.True, "Vô hiệu hóa tài khoản đơn lẻ thất bại.");
  }

  [Test]
  public void Test_Deactivate_Account_Multiple()
  {
    AdminLogin();
    SelectMultipleAccounts();
    ClickElement(By.Id("btnDeactivateMultiple"));
    Assert.That(driver.PageSource.Contains("2 accounts have been deactivated"), Is.True, "Vô hiệu hóa nhiều tài khoản thất bại.");
  }

  [Test]
  public void Test_Activate_Account_Single()
  {
    AdminLogin();
    ClickElement(By.Id("btnActivateSingle"));
    Assert.That(driver.PageSource.Contains("1 account has been activated"), Is.True, "Kích hoạt tài khoản đơn lẻ thất bại.");
  }

  [Test]
  public void Test_Activate_Account_Multiple()
  {
    AdminLogin();
    SelectMultipleAccounts();
    ClickElement(By.Id("btnActivateMultiple"));
    Assert.That(driver.PageSource.Contains("2 accounts have been activated"), Is.True, "Kích hoạt nhiều tài khoản thất bại.");
  }
  [Test]
  public void Test_Add_Account_Invalid_Type_Empty()
  {
    AdminLogin();
    ClickElement(By.Id("btnAddAccount"));
    ClickElement(By.Id("btnSubmit"));
    Assert.That(driver.PageSource.Contains("error color on type input"), Is.True, "Không hiển thị lỗi khi bỏ trống loại tài khoản.");
  }

  [Test]
  public void Test_Add_Account_Invalid_Info_Empty()
  {
    AdminLogin();
    ClickElement(By.Id("btnAddAccount"));
    driver.FindElement(By.Id("txtType")).SendKeys("Grammarly Premium");
    ClickElement(By.Id("btnSubmit"));
    Assert.That(driver.PageSource.Contains("error color on info input"), Is.True, "Không hiển thị lỗi khi bỏ trống thông tin tài khoản.");
  }

  [Test]
  public void Test_Add_Account_Invalid_Info_TooLong()
  {
    AdminLogin();
    ClickElement(By.Id("btnAddAccount"));
    driver.FindElement(By.Id("txtType")).SendKeys("Grammarly Premium");
    driver.FindElement(By.Id("txtInfo")).SendKeys(new string('x', 2000));
    ClickElement(By.Id("btnSubmit"));
    Assert.That(driver.PageSource.Contains("error message on description"), Is.True, "Không hiển thị lỗi khi nhập thông tin quá dài.");
  }

  [Test]
  public void Test_Add_Account_Invalid_Time_Zero()
  {
    AdminLogin();
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
    AdminLogin();
    ClickElement(By.Id("btnAddAccount"));
    driver.FindElement(By.Id("txtType")).SendKeys("Netflix 1 tuần");
    ClickElement(By.Id("btnSubmit"));
    Assert.That(driver.PageSource.Contains("error color on renew input"), Is.True, "Không hiển thị lỗi khi bỏ trống trường gia hạn.");
  }
  [Test]
  public void Test_Add_Account_Single()
  {
    AdminLogin();
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
    AdminLogin();
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
    AdminLogin();
    ClickElement(By.Id("btnEditAccount"));
    driver.FindElement(By.Id("txtType")).Clear();
    ClickElement(By.Id("btnSave"));
    Assert.That(driver.PageSource.Contains("error color on type input"), Is.True, "Kiểm thử lỗi khi loại tài khoản trống thất bại.");
  }

  [Test]
  public void Test_Edit_Account_Invalid_Info_Empty()
  {
    AdminLogin();
    ClickElement(By.Id("btnEditAccount"));
    driver.FindElement(By.Id("txtInfo")).Clear();
    ClickElement(By.Id("btnSave"));
    Assert.That(driver.PageSource.Contains("error color on info input"), Is.True, "Kiểm thử lỗi khi thông tin trống thất bại.");
  }

  [Test]
  public void Test_Edit_Account_Invalid_Info_TooLong()
  {
    AdminLogin();
    ClickElement(By.Id("btnEditAccount"));
    driver.FindElement(By.Id("txtInfo")).SendKeys(new string('x', 2001));
    ClickElement(By.Id("btnSave"));
    Assert.That(driver.PageSource.Contains("error message on info"), Is.True, "Kiểm thử lỗi khi thông tin quá dài thất bại.");
  }

  [Test]
  public void Test_Edit_Account_Invalid_Time_Empty()
  {
    AdminLogin();
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
    AdminLogin();
    ClickElement(By.Id("btnEditAccount"));
    driver.FindElement(By.Id("txtRenew")).Clear();
    ClickElement(By.Id("btnSave"));
    Assert.That(driver.PageSource.Contains("error color on renew input"), Is.True, "Kiểm thử lỗi khi ngày gia hạn trống thất bại.");
  }

  [Test]
  public void Test_Edit_Account_Invalid_Message_TooLong()
  {
    AdminLogin();
    ClickElement(By.Id("btnEditAccount"));
    driver.FindElement(By.Id("txtMessage")).SendKeys(new string('x', 2001));
    ClickElement(By.Id("btnSave"));
    Assert.That(driver.PageSource.Contains("error message on message input"), Is.True, "Kiểm thử lỗi khi tin nhắn quá dài thất bại.");
  }

  [TearDown]
  public void TearDown()
  {
    Thread.Sleep(1000);
    driver.Quit();
    driver.Dispose();
  }
}