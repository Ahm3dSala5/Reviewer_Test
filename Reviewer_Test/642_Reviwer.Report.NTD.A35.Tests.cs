using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportNTDA35Tests : IDisposable
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
        public void ReviwerReportNTD_WhenClickOnReportsOption_MustOpenDropdownlist()
        {
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnNTDOption_MustOpenDropdownlist()
        {
            // to click on report option
            ReviwerReportNTD_WhenClickOnReportsOption_MustOpenDropdownlist();

            var NTDOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[5]/a/span/span"));
            NTDOption.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page()
        {
            // to click on NTD option 
            ReviwerReportNTD_WhenClickOnNTDOption_MustOpenDropdownlist();

            var A35Option = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[5]/nav/ul/li[3]/a/span/span"));
            A35Option.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnAssetClassAndSelectIt_MustSelectAssetClass()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var assetType = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            var selectedAssetClass = new SelectElement(assetType);
            selectedAssetClass.SelectByIndex(0);
            assetType.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnA35Btn_MustOpenSamePage()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var facilityBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            var expectedUrl = driver.Url;
            facilityBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(actualUrl,expectedUrl);
        }

        // this must names as Dashboard rather then report
        [Test]
        public void ReviwerReportNTD_WhenClickOnReportBtnBtn_MustGoToDashboardPage()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

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
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var numPerPage = driver.FindElement
                (By.XPath("//*[@id=\"a35Details_length\"]/label/select"));
            numPerPage.SendKeys("25");
        }

        [Test]
        public void ReviwerReportNTD_WhenWriteSearchWord_MustFilterAllAsset()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var searchField = driver.FindElement
                (By.XPath("//*[@id=\"a35Details_filter\"]/label/input"));
            searchField.SendKeys("Search Test");
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var perviousBtn = driver.FindElement(By.Id("a35Details_next"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnPerviousBtn_MustGoToPreviousPage()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var perviousBtn = driver.FindElement(By.Id("a35Details_previous"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviwerReportNTD_WhenClickOnExportCSV_MustDownloadFile()
        {
            // to open A35 Page
            ReviwerReportNTD_WhenClickOnA35_MustOpenA35Page();

            var exportCSVBtn = driver.FindElement(By.Id("ExportCSVLink"));
            exportCSVBtn.Click();
        }
    }
}