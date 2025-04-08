using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Testdoan1
{
	public class Mange_Tags
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
		public void Filter_Tags_TagManagementPage()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);

			// Tìm thanh trượt Product Quantity
			var productQuantitySlider = driver.FindElement(By.Id("productQuantity"));

			// Lấy vị trí của thanh trượt
			var initialX = productQuantitySlider.Location.X;
			var initialY = productQuantitySlider.Location.Y;

			// Tính toán điểm cần kéo đến (giới hạn min và max từ thẻ input range)
			var maxValue = 20; // Bạn có thể thay đổi theo giá trị max thực tế từ trang
			var desiredValue = 2; // Giá trị bạn muốn chọn

			// Tính toán điểm tương ứng với giá trị cần chọn
			var desiredX = initialX + (desiredValue / maxValue) * productQuantitySlider.Size.Width;

			// Di chuyển thanh trượt đến giá trị mong muốn
			new Actions(driver)
				.MoveToElement(productQuantitySlider, desiredX - initialX, 0)
				.Click()
				.Perform();

			// Nhấn nút filter
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("//button[contains(text(),'Filter')]")));
			filterButton.Click();
			Thread.Sleep(4000); // Chờ kết quả hiển thị
		}
		[Test]
		public void Reset_Filter_Tags_TagManagementPage()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);

			// Tìm thanh trượt Product Quantity
			var productQuantitySlider = driver.FindElement(By.Id("productQuantity"));

			// Lấy vị trí của thanh trượt
			var initialX = productQuantitySlider.Location.X;
			var initialY = productQuantitySlider.Location.Y;

			// Tính toán điểm cần kéo đến (giới hạn min và max từ thẻ input range)
			var maxValue = 20; // Bạn có thể thay đổi theo giá trị max thực tế từ trang
			var desiredValue = 2; // Giá trị bạn muốn chọn

			// Tính toán điểm tương ứng với giá trị cần chọn
			var desiredX = initialX + (desiredValue / maxValue) * productQuantitySlider.Size.Width;

			// Di chuyển thanh trượt đến giá trị mong muốn
			new Actions(driver)
				.MoveToElement(productQuantitySlider, desiredX - initialX, 0)
				.Click()
				.Perform();

			// Nhấn nút filter
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("//button[contains(text(),'Filter')]")));
			filterButton.Click();
			Thread.Sleep(4000); // Chờ kết quả hiển thị
								// Nhấn nút reset
			var resetButton = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[2]/div/div[3]/button[2]")));
			resetButton.Click();
			Thread.Sleep(4000); // Chờ kết quả hiển thị
		}
		[Test]
		public void View_TagManagementPage_Normally()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void Sort_Tags_TagManagementPage_LatestAsc()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
			   (By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(5000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[3]")));
			newestOption.Click();
			Thread.Sleep(5000);
			// Nhấn nút filter
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("//button[contains(text(),'Filter')]")));
			filterButton.Click();
			Thread.Sleep(5000); // Chờ kết quả hiển thị
		}
		[Test]
		public void Sort_Tags_TagManagementPage_NewestDesc()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
			   (By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(5000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[1]")));
			newestOption.Click();
			Thread.Sleep(5000);
			// Nhấn nút filter
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("//button[contains(text(),'Filter')]")));
			filterButton.Click();
			Thread.Sleep(5000); // Chờ kết quả hiển thị
		}
		[Test]
		public void Sort_Tags_TagManagementPage_OldestAsc()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
			   (By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(5000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[2]")));
			newestOption.Click();
			Thread.Sleep(5000);
			// Nhấn nút filter
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("//button[contains(text(),'Filter')]")));
			filterButton.Click();
			Thread.Sleep(5000); // Chờ kết quả hiển thị
		}
		[Test]
		public void Sort_Tags_TagManagementPage_EarliestDesc()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable
			   (By.XPath("//*[@id=\"sort\"]")));
			sortButton.Click();
			Thread.Sleep(5000);
			var newestOption = wait.Until(ExpectedConditions.ElementToBeClickable
				(By.XPath("//*[@id=\"sort\"]/option[4]")));
			newestOption.Click();
			Thread.Sleep(5000);
			// Nhấn nút filter
			var filterButton = wait.Until(ExpectedConditions.ElementToBeClickable(
				By.XPath("//button[contains(text(),'Filter')]")));
			filterButton.Click();
			Thread.Sleep(5000); // Chờ kết quả hiển thị
		}
		[Test]
		public void Add_Tag_Invalid_Title_Empty()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var add = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[1]/a[2]")));
			add.Click();
			Thread.Sleep(5000);
			var title = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("title")));
			title.Clear();
			title.SendKeys("");
			var Add = wait.Until(ExpectedConditions.ElementToBeClickable(
			  By.XPath("/html/body/main/div/div[2]/button")));
			Add.Click();
		}
		[Test]
		public void Add_Tag_Invalid_Title_Duplicated()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var add = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[1]/a[2]")));
			add.Click();
			Thread.Sleep(5000);
			var title = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("title")));
			title.Clear();
			title.SendKeys("Giải Trí");
			var Add = wait.Until(ExpectedConditions.ElementToBeClickable(
			  By.XPath("/html/body/main/div/div[2]/button")));
			Add.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void Add_Tag_Valid()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var add = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[1]/a[2]")));
			add.Click();
			Thread.Sleep(5000);
			var title = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("title")));
			title.Clear();
			title.SendKeys("ffdf");
			var Add = wait.Until(ExpectedConditions.ElementToBeClickable(
			  By.XPath("/html/body/main/div/div[2]/button")));
			Add.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void Edit_Tag_Invalid_Title_Empty()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var edit = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[6]/div[1]/div/button[2]")));
			edit.Click();
			Thread.Sleep(5000);
			var title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div/div[6]/div[1]/input")));
			title.Clear();
			title.SendKeys("");
			var Save = wait.Until(ExpectedConditions.ElementToBeClickable(
			  By.XPath("/html/body/main/div/div[6]/div[1]/div/button[1]")));
			Save.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void Edit_Tag_Invalid_Title_Duplicated()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var edit = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[6]/div[1]/div/button[2]")));
			edit.Click();
			Thread.Sleep(5000);
			var title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div/div[6]/div[1]/input")));
			title.Clear();
			title.SendKeys("Xem Phim");
			var Save = wait.Until(ExpectedConditions.ElementToBeClickable(
			  By.XPath("/html/body/main/div/div[6]/div[1]/div/button[1]")));
			Save.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void Edit_Tag_Valid()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var tagLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[7]/a[1]")));
			tagLink.Click();
			Thread.Sleep(5000);
			var edit = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/main/div/div[6]/div[1]/div/button[2]")));
			edit.Click();
			Thread.Sleep(5000);
			var title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div/div[6]/div[1]/input")));
			title.Clear();
			title.SendKeys("Phim Mới");
			var Save = wait.Until(ExpectedConditions.ElementToBeClickable(
			  By.XPath("/html/body/main/div/div[6]/div[1]/div/button[1]")));
			Save.Click();
			Thread.Sleep(5000);
		}
		[Test]
		public void View_FlashSaleManagementPage_Normally()
		{
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

			var menuadLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/button")));
			menuadLink.Click();
			Thread.Sleep(5000);

			var FlashSalesLink = wait.Until(ExpectedConditions.ElementToBeClickable(
			   By.XPath("/html/body/div[7]/ul/li[10]/a[1]")));
			FlashSalesLink.Click();
			Thread.Sleep(5000);
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