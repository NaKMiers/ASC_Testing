using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ASC_Testing.Src.Auth;

public class Login_Credentials
{
    private ChromeDriver driver;
    private readonly string BASE_URL = "https://anphashop-clone.vercel.app";

    private void Login(string usernameOrEmail = "", string password = "")
    {
        // 1. Move to /auth/login
        driver.Navigate().GoToUrl(BASE_URL + "/auth/login");
        driver.FindElement(By.Id("usernameOrEmail")).SendKeys(usernameOrEmail);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.XPath("//button[text()='Đăng nhập']")).Click();
        Thread.Sleep(2000);
    }

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(BASE_URL);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [Test]
    public void Login_Credentials_Fail_EmptyForm()
    {
        Login();
        Assert.That(driver.Url, Is.EqualTo(BASE_URL + "/auth/login"));
    }

    [Test]
    [TestCase("userxxx", "Asdasd@1")]
    public void Login_Credentials_Fail_WrongUsername(string usernameOrEmail, string password)
    {
        Login(usernameOrEmail, password);

        // Verify
        var toast = driver.FindElement(By.XPath("//div[@role='status']"));
        Assert.That(toast.Text, Is.EqualTo("Tài khoản hoặc mật khẩu không đúng"));
    }

    [Test]
    [TestCase("user123", "xxxxxx")]
    public void Login_Credentials_Fail_WrongPassword(string usernameOrEmail, string password)
    {
        Login(usernameOrEmail, password);

        // Verify
        var toast = driver.FindElement(By.XPath("//div[@role='status']"));
        Assert.That(toast.Text, Is.EqualTo("Tài khoản hoặc mật khẩu không đúng"));
    }

    [Test]
    [TestCase("user123", "Asdasd@1")]
    public void Login_Credentials_Success(string usernameOrEmail, string password)
    {
        Login(usernameOrEmail, password);

        // Verify
        var toast = driver.FindElement(By.XPath("//div[@role='status']"));

        // show toast message and redirect to home page
        Assert.That(toast.Text, Is.EqualTo("Đăng nhập thành công"));
        Thread.Sleep(2000);
        Assert.That(driver.Url.TrimEnd('/'), Is.EqualTo(BASE_URL));
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(1000);
        driver.Quit();
        driver.Dispose();
    }
}