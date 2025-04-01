using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Reviewer_Test
{
    public class ReviwerReportFacilityAdmintMaintFacilitiesTests : IDisposable
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
        public void ReviwerReportFacility_WhenClickOnReportsOption_MustOpenDropdownlist()
        {
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportFacility_WhenClickOnFacilityOption_MustOpenDropdownlist()
        {
            // to click on reports option 
            ReviwerReportFacility_WhenClickOnReportsOption_MustOpenDropdownlist();

            var faciltityBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/a/span/span"));
            faciltityBtn.Click();
        }

        [Test]
        public void ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage()
        {
            // to click on reports option 
            ReviwerReportFacility_WhenClickOnFacilityOption_MustOpenDropdownlist();

            var adminMaintOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/nav/ul/li[2]/a/span/span"));
            adminMaintOption.Click();
        }

        [Test]
        public void ReviwerReportFacility_WhenClickOnFacilitiesBtn_MustOpenSamePage()
        {
            // to open Admin Maint Facilitis Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var buildingsBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            buildingsBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/Facilities";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportFacility_WhenClickOnFacilityBtn_MustGoToFacilityPage()
        {
            // to open Admin Maint Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var facilityBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            facilityBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/Facilities";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        // this must names as Dashboard rather then report

        [Test]
        public void ReviwerReportFacility_WhenClickOnReportBtnBtn_MustGoToDashboardPage()
        {
            // to open Admin Maint buildings Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var dashbordBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashbordBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportFacility_WhenSelectAssetEntirePage_MustShowingSelectedNumber()
        {
            // to open Admin Maint buildings Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var numPerPage = driver.FindElement
                (By.XPath("//*[@id=\"facilities_length\"]/label/select"));
            numPerPage.SendKeys("25");
        }

        [Test]
        public void ReviwerReportFacility_WhenWriteSearchWord_MustFilterAllAsset()
        {
            // to open Admin Maint buildings Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var searchField = driver.FindElement
                (By.XPath("//*[@id=\"facilities_filter\"]/label/input"));
            searchField.SendKeys("Search Test");
        }


        [Test]
        public void ReviwerReportFacility_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open Admin Maint buildings Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var perviousBtn = driver.FindElement(By.XPath("//*[@id=\"facilities_next\"]"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportFacility_WhenClickOnPerviousBtn_MustGoToPreviousPage()
        {
            // to open Admin Maint buildings Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var perviousBtn = driver.FindElement(By.XPath("//*[@id=\"facilities_previous\"]"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportFacility_WhenClickOnExportCSV_MustDownloadFile()
        {
            // to open Admin Maint buildings Page
            ReviwerReportFacility_WhenClickOnAdminMaintFacilitiesOption_MustOpenAdminMaintFacilitiesPage();

            var exportCSVBtn = driver.FindElement(By.XPath("//*[@id=\"ExportCSVLink\"]"));
            exportCSVBtn.Click();
        }
    }
}