using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportAssetAuditTests :IDisposable
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
        public void ReviewerReportAssetAudit_WhenCLickOnReportOpption_MustOpenDropdownlist()
        {
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage()
        {
            // to click on report option 
            ReviewerReportAssetAudit_WhenCLickOnReportOpption_MustOpenDropdownlist();

            var assetAuditOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[1]/a/span/span"));
            assetAuditOption.Click();
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenClickOnDashboardBtn_MustGoToDashboardPage()
        {
            // to open audit asset page 
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashboardBtn.Click();


            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenClickOnSelectAgencyName_MustOpenDropdownlist()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var agencyName = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/button"));

            agencyName.Click();
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenSelectAssetClass_MustOpenDropdownlistAndSelectAssetClass()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetClass = driver.FindElement
                (By.Id("AssetClassIdChange"));
            var selectedAssetClass = new SelectElement(assetClass);
            selectedAssetClass.SelectByIndex(0);
            assetClass.Click();
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenSelectAssetSubClass_MustOpenDropdownlistAndSelectAssetSubClass()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetSubClass = driver.FindElement
                (By.Id("AssetSubClassDropDownChange"));
            var selectedAssetClass = new SelectElement(assetSubClass);
            selectedAssetClass.SelectByIndex(0);
            assetSubClass.Click();
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenSelectAssetType_MustOpenDropdownlistAndSelectAssetType()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetType = driver.FindElement
                (By.Id("AssetTypeDropDownChange"));
            var selectedAssetClass = new SelectElement(assetType);
            selectedAssetClass.SelectByIndex(0);
            assetType.Click();
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenSelectAssetState_MustOpenDropdownlistAndSelectAssetState()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetState = driver.FindElement
                (By.Id("AssetStateIdChange"));
            var selectedAssetClass = new SelectElement(assetState);
            selectedAssetClass.SelectByIndex(0);
            assetState.Click();
        }

        [Test]
        public void ReviewerReportsAsserAudit_WhenSelectStartDate_MustOpenDateForm()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var startDate = driver.FindElement(By.Id("StartDate"));
            startDate.Click();
        }

        [Test]
        public void ReviewerReportsAsserAudit_WhenSelectEndDate_MustOpenDateForm()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var endDate = driver.FindElement(By.Id("EndDate"));
            endDate.Click();
        }

        [Test]
        public void ReportsAssetAudit_WhenSelectAllFiledAsset_MustSubmitSuccessfully()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            // to select asset class
            ReviewerReportsAssetAudit_WhenSelectAssetClass_MustOpenDropdownlistAndSelectAssetClass();
            // to select asset subclass 
            ReviewerReportsAssetAudit_WhenSelectAssetSubClass_MustOpenDropdownlistAndSelectAssetSubClass();
            // to select asset audit
            ReviewerReportsAssetAudit_WhenSelectAssetType_MustOpenDropdownlistAndSelectAssetType();
            // to select start date 
            ReviewerReportsAsserAudit_WhenSelectStartDate_MustOpenDateForm();
            // tp select end date 
            ReviewerReportsAsserAudit_WhenSelectEndDate_MustOpenDateForm();

        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenSelectAssetEntirePage_MustShowingSelectedNumber()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var numPerPage = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable_length\"]/label/select"));
            numPerPage.SendKeys("25");
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenWriteSearchWord_MustFilterAllAsset()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var searchField = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable_filter\"]/label/input"));
            searchField.SendKeys("Search Test");
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenClickOnPerviousBtn_MustGoToPreviousPage()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var perviousBtn = driver.FindElement(By.XPath("//*[@id=\"AssetsTable_previous\"]"));
            perviousBtn.Click();
        }

        [Test]
        public void ReviewerReportsAssetAudit_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open asset audit
            ReviewerReportAssetAudit_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var nextBtn = driver.FindElement(By.XPath("//*[@id=\"AssetsTable_next\"]"));
            nextBtn.Click();
        }
    }
}