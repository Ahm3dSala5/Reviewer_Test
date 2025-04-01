using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Reviewer_Test
{
    public class ReviwerReportEquipmentOtherEquipmentTests : IDisposable
    {
        private IWebDriver driver;

        public void Dispose()
        {
            driver.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
               driver.Quit();
            }
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait.Add(TimeSpan.FromSeconds(5));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/Account/Login");

            // this for skip login page
            var usernameField = driver.FindElement(By.Name("usernameOrEmailAddress"));
            var passwordField = driver.FindElement(By.Name("Password"));
            var signInButton = driver.FindElement(By.Id("LoginButton"));
            usernameField.SendKeys("Reviewer_test1");
            passwordField.SendKeys("NpSCiS5X");
            signInButton.Click();

            driver.Navigate().GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Dashboard");
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnReportOption_MustOpenDropdownlist()
        {
            // to click on Report Option 
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnEquipmentOption_MustOpenDropdownlist()
        {
            // to click on equipment option 
            ReviwerReportsEquipment_WhenClickOnReportOption_MustOpenDropdownlist();

            var equipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/a/span/span"));
            equipmentOption.Click();
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage()
        {
            // to click on report option 
            ReviwerReportsEquipment_WhenClickOnEquipmentOption_MustOpenDropdownlist();

            var otherEquipmentPage = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/nav/ul/li[1]/a/span/span"));
            otherEquipmentPage.Click();
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnEquipmentBtn_MustGoToEquipmentPage()
        {
            // to click on report option 
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var equipmentBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a"));
            equipmentBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/OtherEquipmentDetails";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // wrong btn name Must be Dashbaord 
        [Test]
        public void ReviwerReportsEquipment_WhenClickOnReportBtn_MustGoToDashboardPagePage()
        {
            // to open other equipment page
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var equipmentBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            equipmentBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportsEquipment_WhenSelectAssetEntirePage_MustShowingSelectedNumber()
        {
            // to open other equipment page
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var numPerPage = driver.FindElement
                (By.XPath("//*[@id=\"otherEquipmentDetails_length\"]/label/select"));
            numPerPage.SendKeys("25");
        }

        [Test]
        public void ReviwerReportsEquipment_WhenWriteSearchWord_MustFilterAllAsset()
        {
            // to open other equipment page
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var searchField = driver.FindElement
                (By.XPath("//*[@id=\"otherEquipmentDetails_filter\"]/label/input"));
            searchField.SendKeys("Search Test");
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnPerviousBtn_MustGoToPreviousPage()
        {
            // to open other equipment page
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var perviousBtn = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails_previous\"]"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnExportCSV_MustDownloadFile()
        {
            // to open other equipment page
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var exportCSVBtn = driver.FindElement(By.XPath("//*[@id=\"ExportCSVLink\"]"));
            exportCSVBtn.Click();
        }

        [Test]
        public void ReviwerReportsEquipment_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open other equipment page
            ReviwerReportsEquipment_WhenClickOnOtherEquipmentOption_MustOtherEquipmentPage();

            var nextBtn = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails_next\"]"));
            nextBtn.Click();
        }
    }
}