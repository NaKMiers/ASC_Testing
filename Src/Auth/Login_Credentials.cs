using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ASC_Testing.Src.Auth;

public class Login_Credentials
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

  private void Login(string usernameOrEmail = "", string password = "")
  {
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/login");
    driver.FindElement(By.Id("usernameOrEmail")).SendKeys(usernameOrEmail);
    driver.FindElement(By.Id("password")).SendKeys(password);
    driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
    Thread.Sleep(2000);
  }

  [Test]
  [Category("Login_Credentials_Fail")]
  [TestCase("", "Asdasd@1")] // T1.1.1
  public void Login_Credentials_Fail_UsernameOrEmailEmpty(string username, string password)
  {
    Login(username, password);

    // still stay in login page and cursor auto focus to username input
    var input = driver.FindElement(By.Id("usernameOrEmail"));
    bool isFocused = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return document.activeElement === arguments[0];", input);

    Assert.Multiple(() =>
    {
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/auth/login"));
      Assert.That(isFocused, Is.True, "The input field is not focused.");
    });
  }

  [Test]
  [Category("Login_Credentials_Fail")]
  [TestCase("user123", "")] // T1.1.2
  public void Login_Credentials_Fail_PasswordEmpty(string username, string password)
  {
    Login(username, password);

    // still stay in login page and cursor auto focus to password input
    var input = driver.FindElement(By.Id("password"));
    bool isFocused = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return document.activeElement === arguments[0];", input);

    Assert.Multiple(() =>
    {
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/auth/login"));
      Assert.That(isFocused, Is.True, "The input field is not focused.");
    });
  }

  [Test]
  [Category("Login_Credentials_Fail")]
  [TestCase("userxxx", "Asdasd@1")] // T1.1.3
  [TestCase("user123", "xxxxxx")] // T1.1.4
  public void Login_Credentials_Fail_WrongUsername(string username, string password)
  {
    Login(username, password);

    // Verify
    var toast = driver.FindElement(By.XPath("//div[@role='status']"));
    Assert.That(toast.Text, Is.EqualTo("Tài khoản hoặc mật khẩu không đúng"));
  }

  [Test]
  [Category("Login_Credentials_Success")]
  [TestCase("user123", "Asdasd@1")] // T1.2.1
  public void Login_Credentials_Success(string username, string password)
  {
    Login(username, password);

    // redirect to home page
    try
    {
      wait.Until(driver => driver.Url == Constants.BASE_URL + "/");
      Assert.That(driver.Url, Is.EqualTo(Constants.BASE_URL + "/"));
    }
    catch (WebDriverTimeoutException)
    {
      Assert.Fail("No redirect to home page");
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