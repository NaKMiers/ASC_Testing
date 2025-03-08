using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Globalization;

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
  [TestCase("?sort=createdAt|1")] // T32.3.1
  public void View_AccountManagementPage_With_QueryParams(string query)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all" + query);

    Assert.Multiple(
      () =>
      {
        Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all" + query));
        Thread.Sleep(1000);
        Assert.That(driver.FindElement(By.Id("sort")).GetAttribute("value"), Is.EqualTo("createdAt|1"));
      }
    );
  }


  [Test]
  [Category("Search_Accounts_AccountManagementPage_Valid")]
  [TestCase("@gmail.com")] // T32.4.1
  public void Search_Accounts_AccountManagementPage_Valid(string search)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

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
  [TestCase("")]
  public void Search_Accounts_AccountManagementPage_Invalid_Empty(string search)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    driver.FindElement(By.Id("search")).SendKeys(search);
    Filter();

    Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/admin/account/all"));
  }

  [Test] // T32.6.2
  [Category("Filter_Accounts_AccountManagementPage")]
  [TestCase("Netflix Premium (1 Tuần) - Siêu Nét, Siêu Tiện Lợi")]
  public void Filter_Accounts_AccountManagementPage_Type(string type)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    Thread.Sleep(500);
    var allButton = driver.FindElement(By.XPath("//*[text()='All']"));
    allButton.Click();
    var element = driver.FindElement(By.XPath($"//*[@title='{type}']"));
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
  [TestCase("active", "false")] // T32.6.2
  [TestCase("usingUser", "true")] // T32.6.4
  [TestCase("expire", "true")] // T32.6.6 
  [TestCase("renew", "true")] // T32.6.8 
  [Category("Sort_Accounts_AccountManagementPage")]
  [TestCase("sort", "updatedAt|-1", true)] // T32.7.1
  [TestCase("sort", "updatedAt|1")] // T32.7.2
  [TestCase("sort", "createdAt|-1")] // T32.7.3
  [TestCase("sort", "createdAt|1")] // T32.7.4
  public void Filter_Accounts_AccountManagementPage_Selection(string inputId, string value, bool? isDefault = true)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

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
  [TestCase("Netflix Premium (1 Tuần) - Siêu Nét, Siêu Tiện Lợi", "active", "false")] // T32.6.3
  [TestCase("Netflix Premium (1 Tuần) - Siêu Nét, Siêu Tiện Lợi", "usingUser", "false")] // T32.6.5
  [TestCase("Netflix Premium (1 Tuần) - Siêu Nét, Siêu Tiện Lợi", "expire", "true")] // T32.6.7
  [TestCase("Netflix Premium (1 Tuần) - Siêu Nét, Siêu Tiện Lợi", "renew", "true")] // T32.6.9
  public void Filter_Accounts_AccountManagementPage_TypeAndDropdown(string type, string dropdownId, string value)
  {
    AdminLogin();
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/admin/account/all");

    // Select type
    Thread.Sleep(500);
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

  [TearDown]
  public void TearDown()
  {
    Thread.Sleep(1000);
    driver.Quit();
    driver.Dispose();
  }
}