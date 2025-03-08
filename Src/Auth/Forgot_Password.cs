using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ASC_Testing.Src.Auth;

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

  private void ForgotPassword(string email)
  {
    driver.Navigate().GoToUrl(Constants.BASE_URL + "/auth/forgot-password");
    driver.FindElement(By.Id("email")).SendKeys(email);
    driver.FindElement(By.XPath("//button[text()='Gửi mã']")).Click();
  }

  [Test]
  [Category("Forgot_Password_Fail")]
  [TestCase("")]
  public void Forgot_Password_Fail_EmailEmpty(string email)
  {
    ForgotPassword(email);

    try
    {
      string currentUrl = driver.Url;
      var emailInput = driver.FindElement(By.Id("email"));
      bool isFocused = emailInput.Equals(driver.SwitchTo().ActiveElement());

      Assert.Multiple(() =>
      {
        Assert.That(currentUrl, Is.EqualTo(Constants.BASE_URL + "/auth/forgot-password"), "Current URL is not correct.");
        Assert.That(isFocused, Is.True, "Email input is not focused.");
      });

    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Error message element was not found.");
    }
  }

  [Test]
  [Category("Forgot_Password_Fail")]
  [TestCase("xxxxx@xxx.xxx")]
  public void Forgot_Password_Fail_EmailNotExists(string email)
  {
    ForgotPassword(email);

    try
    {
      var errorMessage = wait.Until(driver =>
          driver.FindElement(By.XPath("//span[contains(text(),'Email không tồn tại')]"))
      );

      Assert.That(errorMessage.Displayed, Is.True, "Expected error message is not displayed.");
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Error message element was not found.");
    }
  }

  [Test]
  [Category("Forgot_Password_Fail")]
  [TestCase("diwas118151@gmail.com")]
  public void Forgot_Password_Fail_GoogleAuthType(string email)
  {
    ForgotPassword(email);

    try
    {
      var errorMessage = wait.Until(driver =>
          driver.FindElement(By.XPath("//span[contains(text(),'Email này được xác thực bởi google, bạn không thể thực hiện tính năng này')]"))
      );

      Assert.That(errorMessage.Displayed, Is.True, "Expected error message is not displayed.");
    }
    catch (NoSuchElementException)
    {
      Assert.Fail("Error message element was not found.");
    }
  }

  // [Test]
  // [Category("Forgot_Password_Success")]
  // [TestCase("lxopersy@gmail.com")]
  // public void Forgot_Password_Success(string email)
  // {
  //   ForgotPassword(email);

  //   try
  //   {
  //     var toast = wait.Until(driver =>
  //     {
  //       var element = driver.FindElement(By.XPath("//div[@role='status']"));
  //       return element.Text.Contains("Link khôi phục mật khẩu đã được gửi đến email của bạn") ? element : null;
  //     });

  //     Assert.That(toast.Displayed, Is.True, "Expected toast message is not displayed.");

  //   }
  //   catch (NoSuchElementException)
  //   {
  //     Assert.Fail("Error message element was not found.");
  //   }
  // }



  [TearDown]
  public void TearDown()
  {
    Thread.Sleep(1000);
    driver.Quit();
    driver.Dispose();
  }
}