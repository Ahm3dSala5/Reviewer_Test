using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportVehiclesBus : IDisposable
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
        public void ReviwerReportVehicle_WhenClickOnReportsOption_MustOpenDropdownlist()
        {
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickOnVehicle_MustOpenDropdownlist()
        {
            // to click on report option
            ReviwerReportVehicle_WhenClickOnReportsOption_MustOpenDropdownlist();

            var vehiclesBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[6]/a/span"));
            vehiclesBtn.Click();
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickBus_MustOpenBusPage()
        {
            // to click on vehicle btn
            ReviwerReportVehicle_WhenClickOnVehicle_MustOpenDropdownlist();

            var busBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[6]/nav/ul/li/a/span"));
            busBtn.Click();
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickOnAssetTypeAndSelectIt_MustSelectAssetType()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var assetType = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            var selectedAssetClass = new SelectElement(assetType);
            selectedAssetClass.SelectByIndex(0);
            assetType.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnBusBtn_MustOpenSamePage()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var facilityBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            var expectedUrl = driver.Url;
            facilityBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(actualUrl, expectedUrl);
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickOnVehicleBtn_MustGoToVehiclePage()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var vehicleBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/BusDetails";
            vehicleBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // this must names as Dashboard rather then report
        [Test]
        public void ReviwerReportNTD_WhenClickOnReportBtnBtn_MustGoToDashboardPage()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var dashbordBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            
            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            dashbordBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl,actualUrl);
        }

        [Test]
        public void ReviwerReportNTD_WhenSelectAssetEntirePage_MustShowingSelectedNumber()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var numPerPage = driver.FindElement
                (By.XPath("//*[@id=\"busDetails_length\"]/label/select"));
            numPerPage.SendKeys("25");
        }

        [Test]
        public void ReviwerReportNTD_WhenWriteSearchWord_MustFilterAllAsset()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var searchField = driver.FindElement
                (By.XPath("//*[@id=\"busDetails_filter\"]/label/input"));
            searchField.SendKeys("Search Test");
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var NextBtn = driver.FindElement(By.Id("busDetails_next"));
            NextBtn.Click();
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickOnPerviousBtn_MustGoToPreviousPage()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var perviousBtn = driver.FindElement(By.Id("busDetails_previous"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportVehicle_WhenClickOnExportCSV_MustDownloadFile()
        {
            // to open bus btn
            ReviwerReportVehicle_WhenClickBus_MustOpenBusPage();

            var exportCSVBtn = driver.FindElement(By.Id("ExportCSVLink"));
            exportCSVBtn.Click();
        }
    }
}