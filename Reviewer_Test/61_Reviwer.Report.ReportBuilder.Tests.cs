using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportReportBuilderTests : IDisposable
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
        public void ReviwerReportsReportBuilder_WhenClickOnReportOption_MustOpenDropdownlist()
        {
            // to click on Report Option 
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportsReportBuilder_WhenClickOnAssetAudit_MustOpenAssetAuditPage()
        {
            // to open report builder 
            ReviwerReportsReportBuilder_WhenClickOnReportOption_MustOpenDropdownlist();

            var reportBuilder = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[2]/a/span/span"));

            reportBuilder.Click();
        }

        [Test]
        public void ReportsReportBuilder_WhenClickOnDashboardBtn_MustGoToDashboardPage()
        {
            // to open report builder page
            ReviwerReportsReportBuilder_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashboardBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReportsReportBuilder_WhenSelectAssetClass_MustOpenDropdownlistAndSelectAssetClass()
        {
            // to open report builder page
            ReviwerReportsReportBuilder_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetClass = driver.FindElement
                (By.Id("AssetClassIdChange"));
            var selectedAssetClass = new SelectElement(assetClass);
            selectedAssetClass.SelectByIndex(0);
            assetClass.Click();
        }

        [Test]
        public void ReportsReportBuilder_WhenSelectAssetSubClass_MustOpenDropdownlistAndSelectAssetSubClass()
        {
            // to open report builder page
            ReviwerReportsReportBuilder_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetSubClass = driver.FindElement
                (By.Id("AssetSubClassDropDownChange"));
            var selectedAssetClass = new SelectElement(assetSubClass);
            selectedAssetClass.SelectByIndex(0);
            assetSubClass.Click();
        }

        [Test]
        public void ReportsReportBuilder_WhenSelectAssetType_MustOpenDropdownlistAndSelectAssetType()
        {
            // to open report builder page
            ReviwerReportsReportBuilder_WhenClickOnAssetAudit_MustOpenAssetAuditPage();

            var assetType = driver.FindElement
                (By.Id("AssetTypeDropDownChange"));
            var selectedAssetClass = new SelectElement(assetType);
            selectedAssetClass.SelectByIndex(0);
            assetType.Click();
        }
    }
}