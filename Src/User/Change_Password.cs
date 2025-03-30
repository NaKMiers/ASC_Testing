using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ASC_Testing.Src.NonUser;

public class Change_Password
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
  [Category("Change_Password_GoogleAuth")]
  public void Change_Password_GoogleAuth()
  {
  }

  [Test]
  [Category("Change_Password_GithubAuth")]
  public void Change_Password_GithubAuth()
  {

  }

    public void Login()
    {
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/login");
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
        var usernameOrEmail = driver.FindElement(By.CssSelector("input[id='usernameOrEmail']"));
        var password = driver.FindElement(By.CssSelector("input[id='password']"));
        usernameOrEmail.SendKeys("lehothanhtai@gmail.com");
        password.SendKeys("Thanhtai123");
        driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void View_SecurityPage_NonUser()
    {
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void View_SecurityPage_Success_Normally()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void View_SecurityPage_Success_By_URL()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_Empty_OldPW()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("");
        newPassword.SendKeys("Asdasd@2");
        reNewPassword.SendKeys("Asdasd@2");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_Empty_NewPW()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Asdasd@1");
        newPassword.SendKeys("");
        reNewPassword.SendKeys("Asdasd@2");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_Empty_ReNewPW()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Asdasd@1");
        newPassword.SendKeys("Asdasd@2");
        reNewPassword.SendKeys("");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_Empty_SameOldPW()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Thanhtai123");
        newPassword.SendKeys("Thanhtai123");
        reNewPassword.SendKeys("Thanhtai123");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_NoUpper()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Thanhtai123");
        newPassword.SendKeys("thanhtai123");
        reNewPassword.SendKeys("thanhtai123");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_NoLower()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Thanhtai123");
        newPassword.SendKeys("THANHTAI123");
        reNewPassword.SendKeys("THANHTAI123");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_NoNumber()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Thanhtai123");
        newPassword.SendKeys("Thanhtai");
        reNewPassword.SendKeys("Thanhtai");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
        Thread.Sleep(2000);
    }

    [Test]
    [Category("Change_Password_Credentials_Fail")]
    public void Change_Password_Credentials_Fail_NotMatchReEnterPW()
    {
        Login();
        driver.Navigate().GoToUrl(Constants.BASE_URL + "/user/security");
        var oldPassword = driver.FindElement(By.CssSelector("input[id='oldPassword']"));
        var newPassword = driver.FindElement(By.CssSelector("input[id='newPassword']"));
        var reNewPassword = driver.FindElement(By.CssSelector("input[id='reNewPassword']"));
        oldPassword.SendKeys("Thanhtai123");
        newPassword.SendKeys("Thanhtai456");
        reNewPassword.SendKeys("Thanhtai789");
        driver.FindElement(By.XPath("//button[text()='Lưu']")).Click();
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