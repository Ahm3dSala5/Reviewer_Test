using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportEquipmentServiceVehiclesTests :IDisposable
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
        public void ReviwerReportServiceVehicles_WhenClickOnReportOption_MustOpenDropdownlist()
        {
            // to click on Report Option 
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportEquipment_WhenClickOnEquipmentOption_MustOpenDropdownlist()
        {
            // to click on equipment option 
            ReviwerReportServiceVehicles_WhenClickOnReportOption_MustOpenDropdownlist();

            var equipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/a/span/span"));
            equipmentOption.Click();
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage()
        {
            // to click on equipment option 
            ReviwerReportEquipment_WhenClickOnEquipmentOption_MustOpenDropdownlist();

            var serviceVehiclesOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/nav/ul/li[2]/a/span/span"));
            serviceVehiclesOption.Click();
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenSelectAssetEntirePage_MustShowingSelectedNumber()
        {
            // to open service Vehicles page
            ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage();

            var numPerPage = driver.FindElement
                (By.XPath("//*[@id=\"serviceVehiclesDetails_length\"]/label/select"));
            numPerPage.SendKeys("25");
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenWriteSearchWord_MustFilterAllAsset()
        {
            // to open service Vehicles page
            ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage();

            var searchField = driver.FindElement
                (By.XPath("//*[@id=\"serviceVehiclesDetails_filter\"]/label/input"));
            searchField.SendKeys("Search Test");
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenSelectAssetType_MustOpenDropdownlistAndSelectAssetType()
        {
            // to open service Vehicles page
            ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage();

            var assetType = driver.FindElement
                (By.Id("AssetTypeDropDownChange"));
            var selectedAssetClass = new SelectElement(assetType);
            selectedAssetClass.SelectByIndex(0);
            assetType.Click();
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenClickOnPerviousBtn_MustGoToPreviousPage()
        {
            // to open service Vehicles page
            ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage();

            var perviousBtn = driver.FindElement
                (By.XPath("//*[@id=\"serviceVehiclesDetails_previous\"]"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open service Vehicles page
            ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage();

            var nextBtn = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails_next\"]"));
            nextBtn.Click();
        }

        [Test]
        public void ReviwerReportServiceVehicles_WhenClickOnExportCSV_MustDownloadFile()
        {
            // to open service Vehicles page
            ReviwerReportServiceVehicles_WhenClickServiceVehicles_MustOpenServiceVehiclesPage();

            var exportCSVBtn = driver.FindElement(By.XPath("//*[@id=\"ExportCSVLink\"]"));
            exportCSVBtn.Click();
        }
    }
}