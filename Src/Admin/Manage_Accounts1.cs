using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Testdoan1
{
	public class Manage_Accounts
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			driver.Manage().Window.Maximize();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

			driver.Navigate().GoToUrl("https://anphashop-clone.vercel.app/auth/login");

			// Đăng nhập
			var usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("usernameOrEmail")));
			usernameField.Clear();
			usernameField.SendKeys("lehothanhtai@gmail.com");

			var passwordField = driver.FindElement(By.Name("password"));
			passwordField.Clear();
			passwordField.SendKeys("Thanhtai123");

			var loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Đăng nhập')]"));
			loginButton.Click();
		}
		[Test]
		public void Deactivate_Multiple_Accounts()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(5000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(5000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(5000);
			Console.WriteLine("✅ Đã click 1");

			//var activeButton = wait.Until(ExpectedConditions.ElementToBeClickable
			//    (By.XPath("//*[@id=\"active\"]")));
			//activeButton.Click();

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(5000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(5000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(5000);
			// ✅ Nhấn nút Select All bằng Actions
			var selectAllBtn = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[3]/div/div[5]/button")));
			selectAllBtn.Click();
			Console.WriteLine("✅ Đã nhấn Select All");

			Thread.Sleep(5000); // Đợi giao diện cập nhật (nút Deactivate mới hiển thị)

			// ✅ Nhấn nút Deactivate
			var deactivateBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[3]/div/div[5]/button[2]")));
			deactivateBtn.Click();
			Console.WriteLine("✅ Đã nhấn Deactivate");
			Thread.Sleep(5000);

		}
		[Test]
		public void Deactivate_Single_Account()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));

			// ✅ Nhấn nút Deactivate cho 1 account
			var deactivateBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[5]/div[1]/div[2]/button[1]")));
			deactivateBtn.Click();

			// ✅ Kiểm tra thông báo
			var toast = wait.Until(ExpectedConditions.ElementIsVisible(
				By.XPath("//div[contains(text(),'1 account has been deactivated')]")));
			Assert.IsTrue(toast.Displayed);
			Console.WriteLine("✅ Đã deactivate 1 account thành công");
		}
		[Test]
		public void Activate_Accounts_Multiple()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(5000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(5000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(5000);
			Console.WriteLine("✅ Đã click 1");

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(5000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(5000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(5000);
			// ✅ Nhấn nút Select All bằng Actions
			var selectAllBtn = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[3]/div/div[5]/button")));
			selectAllBtn.Click();
			Console.WriteLine("✅ Đã nhấn Select All");

			Thread.Sleep(5000); // Đợi giao diện cập nhật (nút Deactivate mới hiển thị)

			// ✅ Nhấn nút Deactivate
			var deactivateBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[3]/div/div[5]/button[2]")));
			deactivateBtn.Click();
			Console.WriteLine("✅ Đã nhấn Deactivate");
			Thread.Sleep(5000);
			// ✅ Nhấn nút Activate
			var ActivateBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[3]/div/div[5]/button[2]")));
			ActivateBtn.Click();
			Console.WriteLine("✅ Đã nhấn Activate");
			Thread.Sleep(5000);
		}
		[Test]
		public void Activate_Accounts_Single()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();
			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));

			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");

			// ✅ Nhấn nút Activate (dạng svg)
			var activateButtonSvg = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[5]/div[1]/div[2]/button[1]")));
			activateButtonSvg.Click();
			Console.WriteLine("✅ Đã nhấn Activate 1 tài khoản");

			// ✅ Kiểm tra thông báo toast
			var toast = wait.Until(ExpectedConditions.ElementIsVisible(
				By.XPath("//div[contains(text(),'1 account has been activated')]")));
			Assert.That(toast.Displayed);
			Console.WriteLine("✅ Đã hiện thông báo kích hoạt thành công 1 tài khoản");
		}
		[Test]
		public void Add_Account_Invalid_Info_Empty()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			// Chờ đến khi URL chứa '/admin/account/all' để đảm bảo trang đã tải xong
			wait.Until(ExpectedConditions.UrlContains("/admin/account/all"));

			// Đảm bảo phần tử chính trong trang quản lý tài khoản đã có thể thao tác được
			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addAccountBtn.Click();

			wait.Until(ExpectedConditions.ElementIsVisible(By.Name("info"))).SendKeys("xxxxxx");

			driver.FindElement(By.Name("renew")).SendKeys("28/02/2025");
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("7");
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");

			var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addButton.Click();

			var infoField = driver.FindElement(By.Name("info"));
			string infoClass = infoField.GetAttribute("class");

			Assert.IsTrue(infoClass.Contains("error") || infoClass.Contains("invalid") || infoClass.Contains("border-red"),
				"❌ Không phát hiện lỗi khi Info để trống.");
		}
		[Test]
		public void Add_Account_Invalid_Info_Time_Zero()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();
			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));

			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addAccountBtn.Click();

			wait.Until(ExpectedConditions.ElementIsVisible(By.Name("info"))).SendKeys("xxxxxx");

			driver.FindElement(By.Name("renew")).SendKeys("28/02/2025");
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("0"); // Nhập 0
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");
			Thread.Sleep(5000);

			var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[5]/div/div[6]/button[1]")));
			addButton.Click();

			var daysField = driver.FindElement(By.Name("days"));
			string daysClass = daysField.GetAttribute("class");

			Assert.IsTrue(daysClass.Contains("error") || daysClass.Contains("invalid") || daysClass.Contains("border-red"),
				"❌ Không phát hiện lỗi khi Days = 0.");
		}
		[Test]
		public void Add_Account_Invalid_Info_TooLong()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			// Đảm bảo trang quản lý tài khoản đã tải xong và URL chứa '/admin/account/all'
			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));

			// Kiểm tra xem trang đã tải và phần tử "Add Account" có thể nhấn được
			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));

			// Đảm bảo nút "Add Account" có thể nhấn được
			addAccountBtn.Click();

			// Điền thông tin cho trường "info" quá dài
			driver.FindElement(By.Name("info")).SendKeys(new string('x', 2000));

			driver.FindElement(By.Name("renew")).SendKeys("28/02/2025");
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("7");
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");

			// Đảm bảo nút "Add" có thể nhấn được
			var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addButton.Click();

			var infoField = driver.FindElement(By.Name("info"));
			string infoClass = infoField.GetAttribute("class");

			Assert.IsTrue(infoClass.Contains("error") || infoClass.Contains("invalid") || infoClass.Contains("border-red"),
				"❌ Không phát hiện lỗi khi Info quá dài.");
		}
		[Test]
		public void Add_Account_Invalid_Renew_Empty()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();
			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));

			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addAccountBtn.Click();

			wait.Until(ExpectedConditions.ElementIsVisible(By.Name("info"))).SendKeys("xxxxxx");

			driver.FindElement(By.Name("renew")).Clear(); // Trường renew để trống
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("7");
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");

			var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addButton.Click();

			var renewField = driver.FindElement(By.Name("renew"));
			string renewClass = renewField.GetAttribute("class");

			Assert.IsTrue(renewClass.Contains("error") || renewClass.Contains("invalid") || renewClass.Contains("border-red"),
				"❌ Không phát hiện lỗi khi Renew để trống.");
		}
		[Test]
		public void Add_Account_Invalid_Type_Empty()
		{
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();
			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));

			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addAccountBtn.Click();

			wait.Until(ExpectedConditions.ElementIsVisible(By.Name("info"))).SendKeys("xxxxxx");

			driver.FindElement(By.Name("renew")).SendKeys("28/02/2025");
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("7");
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");

			var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addButton.Click();

			var typeDropdown = driver.FindElement(By.Name("type"));
			string className = typeDropdown.GetAttribute("class");

			Assert.IsTrue(className.Contains("error") || className.Contains("invalid") || className.Contains("border-red"),
				"❌ Không phát hiện lỗi khi Type để trống.");
		}

		[Test]
		public void Add_Accounts_Consecutively()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(5000);

			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addAccountBtn.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/add"));
			Console.WriteLine("✅ Đã vào trang thêm tài khoản");
			Thread.Sleep(5000);

			// Điền thông tin tài khoản
			driver.FindElement(By.Name("info")).SendKeys("Netflix 1 tháng");

			driver.FindElement(By.Name("renew")).SendKeys("28/02/2025");
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("7");
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");

			// Nhấn nút Add để thêm tài khoản
			var DuplicateButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[5]/div/div[6]/button[2]")));
			DuplicateButton.Click();
			Thread.Sleep(5000);

			var AddallButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[3]/button[1]")));
			AddallButton.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void Add_Accounts_Single()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(5000);

			var addAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addAccountBtn.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/add"));
			Console.WriteLine("✅ Đã vào trang thêm tài khoản");
			Thread.Sleep(5000);

			// Điền thông tin tài khoản
			driver.FindElement(By.Name("info")).SendKeys("Netflix 1 tháng");

			driver.FindElement(By.Name("renew")).SendKeys("28/02/2025");
			driver.FindElement(By.Name("days")).Clear();
			driver.FindElement(By.Name("days")).SendKeys("7");
			driver.FindElement(By.Name("hours")).SendKeys("0");
			driver.FindElement(By.Name("minutes")).SendKeys("0");
			driver.FindElement(By.Name("seconds")).SendKeys("0");

			// Nhấn nút Add để thêm tài khoản
			var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[1]/a[2]")));
			addButton.Click();
		}
		[Test]
		public void Edit_Account_Invalid_Info_Empty()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(4000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(4000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 1");

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(4000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(4000);

			var editAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
			By.XPath("/html/body/main/div/div[6]/div[1]/div[2]/a")));
			editAccountBtn.Click();
			Thread.Sleep(2000);

			wait.Until(ExpectedConditions.UrlContains("admin/account/67dfea207ce485a9a57914b3/edit"));

			driver.FindElement(By.Name("type")).SendKeys("Standard");
			var infoField = driver.FindElement(By.Name("info"));
			infoField.Clear();
			driver.FindElement(By.Name("renew")).SendKeys("23/04/2025");
			driver.FindElement(By.Name("days")).SendKeys("7");

			var SaveButton = wait.Until(ExpectedConditions.ElementToBeClickable
			  (By.XPath("/html/body/main/div/div[2]/button")));
			SaveButton.Click();
			Thread.Sleep(4000);
		}
		[Test]
		public void Edit_Account_Invalid_Message_TooLong()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(4000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(4000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 1");

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(4000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(4000);

			var editAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
			By.XPath("/html/body/main/div/div[6]/div[1]/div[2]/a")));
			editAccountBtn.Click();
			Thread.Sleep(2000);

			wait.Until(ExpectedConditions.UrlContains("admin/account/67dfea207ce485a9a57914b3/edit"));

			driver.FindElement(By.Name("type")).SendKeys("Premium");
			driver.FindElement(By.Name("info")).SendKeys("Netflix Premium");
			driver.FindElement(By.Name("renew")).SendKeys("23/04/2025");
			driver.FindElement(By.Name("days")).SendKeys("7");

			var messageField = driver.FindElement(By.Name("message"));
			messageField.SendKeys(new string('a', 2001)); // Nhập vào thông điệp quá dài

			var SaveButton = wait.Until(ExpectedConditions.ElementToBeClickable
			  (By.XPath("/html/body/main/div/div[2]/button")));
			SaveButton.Click();
			Thread.Sleep(4000);
		}
		[Test]
		public void Edit_Account_Invalid_Renew_Empty()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(4000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(4000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 1");

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(4000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(4000);

			var editAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
			By.XPath("/html/body/main/div/div[6]/div[1]/div[2]/a")));
			editAccountBtn.Click();
			Thread.Sleep(2000);

			wait.Until(ExpectedConditions.UrlContains("admin/account/67dfea207ce485a9a57914b3/edit"));

			driver.FindElement(By.Name("type")).SendKeys("Standard");
			driver.FindElement(By.Name("info")).SendKeys("Netflix Premium");
			driver.FindElement(By.Name("days")).SendKeys("7");

			var renewField = driver.FindElement(By.Name("renew"));
			renewField.Clear();

			var SaveButton = wait.Until(ExpectedConditions.ElementToBeClickable
			  (By.XPath("/html/body/main/div/div[2]/button")));
			SaveButton.Click();
			Thread.Sleep(4000);
		}
		[Test]
		public void Edit_Account_Invalid_Time_Zero()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(4000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(4000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 1");

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(4000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(4000);

			var editAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
			By.XPath("/html/body/main/div/div[6]/div[1]/div[2]/a")));
			editAccountBtn.Click();
			Thread.Sleep(2000);

			wait.Until(ExpectedConditions.UrlContains("admin/account/67dfea207ce485a9a57914b3/edit"));

			driver.FindElement(By.Name("type")).SendKeys("Premium");
			driver.FindElement(By.Name("info")).SendKeys("Netflix Premium");
			driver.FindElement(By.Name("renew")).SendKeys("23/04/2025");

			var daysField = driver.FindElement(By.Name("days"));
			daysField.Clear();
			driver.FindElement(By.Name("hours")).Clear();
			driver.FindElement(By.Name("minutes")).Clear();
			driver.FindElement(By.Name("seconds")).Clear();

			var SaveButton = wait.Until(ExpectedConditions.ElementToBeClickable
			  (By.XPath("/html/body/main/div/div[2]/button")));
			SaveButton.Click();
			Thread.Sleep(4000);
		}
		[Test]
		public void Edit_Account_Invalid_Type_Empty()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");
			Thread.Sleep(4000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(4000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 1");

			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"active\"]/option[1]")));
			allOption.Click();
			Thread.Sleep(4000);
			Console.WriteLine("✅ Đã click 2");
			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"usingUser\"]/option[1]")));
			AllOption.Click();
			Thread.Sleep(4000);
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("/html/body/main/div/div[2]/div/div[4]/button[1]")));
			filterButton.Click();
			Console.WriteLine("✅ Đã nhấn filterButton");
			Thread.Sleep(4000);

			var editAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
			By.XPath("/html/body/main/div/div[6]/div[1]/div[2]/a")));
			editAccountBtn.Click();
			Thread.Sleep(2000);

			wait.Until(ExpectedConditions.UrlContains("admin/account/67dfea207ce485a9a57914b3/edit"));

			// Clear type
			var typeDropdown = driver.FindElement(By.Name("type"));
			typeDropdown.Clear();
			driver.FindElement(By.Name("info")).SendKeys("Netflix Premium");
			driver.FindElement(By.Name("renew")).SendKeys("23/04/2025");
			driver.FindElement(By.Name("days")).SendKeys("7");

			var SaveButton = wait.Until(ExpectedConditions.ElementToBeClickable
			  (By.XPath("/html/body/main/div/div[2]/button")));
			SaveButton.Click();
			Thread.Sleep(4000);
		}
		[Test]
		public void Delete_Accounts_Multiple()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");

			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("sort")));
			sortButton.Click();
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='sort']//following-sibling::ul//li[text()='Newest']")));
			newestOption.Click();

			var activeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("active")));
			activeButton.Click();
			var allOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[text()='All']")));
			allOption.Click();

			var usingButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("usingUser")));
			usingButton.Click();
			var AllOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[text()='All']")));
			AllOption.Click();

			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[3]/div/div[4]/button[1]")));
			filterButton.Click();

			// Click nút Select All
			var selectAllButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[2]/div/div[5]/button")));
			selectAllButton.Click();

			// Click nút Delete
			var deleteButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[2]/div/div[5]/button[4]")));
			deleteButton.Click();

			// Click nút Đồng ý trong popup xác nhận
			var confirmButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[4]/div/div/button[2]")));
			confirmButton.Click();

			// Kiểm tra thông báo "tài khoản đã bị xoá"
			var toastMessage = wait.Until(ExpectedConditions.ElementIsVisible(
				By.XPath("//div[contains(text(), 'tài khoản đã bị xoá')]")));
			Assert.IsTrue(toastMessage.Displayed, "❌ Không hiển thị thông báo xoá tài khoản.");

		}
		[Test]
		public void Delete_Accounts_Single()
		{
			// Mở menu
			var menuButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/div[3]/div")));
			menuButton.Click();

			// Truy cập trang quản lý tài khoản
			var accountLink = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div/section[1]/div/header/div/ul/div/a[3]")));
			accountLink.Click();

			wait.Until(ExpectedConditions.UrlContains("admin/account/all"));
			Console.WriteLine("✅ Đã vào trang quản lý tài khoản");

			// Nhấn nút thùng rác (CLICK BUTTON chứ không phải SVG)
			var deleteButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[5]/div[1]/div[2]/button[2]")));

			// Dùng JavaScript để đảm bảo click được
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
			js.ExecuteScript("arguments[0].click();", deleteButton);
			Console.WriteLine("✅ Đã nhấn nút thùng rác");

			// Đợi và nhấn nút "Đồng ý"
			var confirmButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("/html/body/main/div/div[5]/div[2]/div/div/button[2]")));
			confirmButton.Click();
			Console.WriteLine("✅ Đã nhấn Đồng ý xoá");

			// Kiểm tra thông báo "1 tài khoản đã bị xoá"
			var toastMessage = wait.Until(ExpectedConditions.ElementIsVisible(
				By.XPath("//div[contains(text(), '1 tài khoản đã bị xoá')]")));
			Assert.IsTrue(toastMessage.Displayed, "❌ Không hiển thị thông báo xoá tài khoản.");
			Console.WriteLine("✅ Thông báo xác nhận xoá đã hiển thị.");
		}
		[TearDown]
		public void TearDown()
		{
			if (driver != null)
			{
				driver.Quit();
				driver.Dispose();
			}
		}
	}
}
