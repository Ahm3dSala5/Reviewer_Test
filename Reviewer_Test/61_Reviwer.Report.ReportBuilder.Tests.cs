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
            //driver.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
              //driver.Quit();
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
        public void ReportBuilderPage_OperationsOptionTest()
        {
            var ReportsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));

            Assert.IsTrue(ReportsOption.Enabled);
            Assert.IsTrue(ReportsOption.Displayed);
            Assert.AreEqual(ReportsOption.Text, "Reports");
            Assert.AreEqual(ReportsOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(ReportsOption.GetAttribute("custom-data"), "Reports");
            Assert.AreEqual(ReportsOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void ReportBuilderPage_ReportBuilderTest()
        {
            var ReportsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var reportBuilderOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[2]/a"));
            Assert.IsTrue(reportBuilderOption.Enabled);
            Assert.IsTrue(reportBuilderOption.Displayed);
            Assert.AreEqual(reportBuilderOption.Text, "Report Builder");
            Assert.AreEqual(reportBuilderOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(reportBuilderOption.GetAttribute("custom-data"), "Report Builder");
            Assert.AreEqual(reportBuilderOption.GetAttribute("target"), "_self");
            Assert.AreEqual(reportBuilderOption.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReportBuilder");
        }

        [Test]
        public void ReportBuilderPage_OpenPage()
        {
            var ReportsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var reportBuilderOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[2]/a"));
            reportBuilderOption.Click();
        }

        [Test]
        public void ReportBuilderPage_HiHostReviewerTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var HIReviewer = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            Assert.AreEqual(HIReviewer.Text, "HI,");
            Assert.True(HIReviewer.Displayed);
            Assert.True(HIReviewer.Enabled);

            var username = driver.FindElement(By.Id("UserName"));
            Assert.True(username.Displayed);
            Assert.True(username.Enabled);
        }

        [Test]
        public void ReportBuilderPage_LogoutBtn()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var HiReviewer = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            HiReviewer.Click();

            var logoutBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/div/div/div/div/ul/li[4]/a"));
            Assert.IsTrue(logoutBtn.Enabled);
            Assert.IsTrue(logoutBtn.Displayed);
            Assert.AreEqual(logoutBtn.Text, "Logout");

            var UrlBeforeClickOnLogout = driver.Url;
            logoutBtn.Click();
            var UrlAfterClickOnLogout = driver.Url;
            Assert.AreNotEqual(UrlBeforeClickOnLogout, UrlAfterClickOnLogout);
        }

        [Test]
        public void ReportBuilderPage_ModalTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var modalTitle = driver.FindElement(By.XPath("//*[@id=\"demo\"]"));
            Assert.IsFalse(modalTitle.Displayed);
            Assert.IsTrue(modalTitle.Enabled);

            var selectedTenant = driver.FindElement(By.Id("TenantDropDownChange"));
            var selectedTenantValue = new SelectElement(selectedTenant);
            selectedTenantValue.SelectByIndex(0);

            var closeBtn = driver.FindElement(By.Id("close"));
            Assert.AreEqual(closeBtn.GetAttribute("type"), "button");
            Assert.IsFalse(closeBtn.Displayed);
            Assert.IsTrue(closeBtn.Enabled);
        }

        [Test]
        public void ReportBuilderPage_NotificationTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void ReportBuilderPage_PageTitleTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Report Builder");
        }

        [Test]
        public void ReportBuilderPage_DashboardNavigationLinkTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a"));
            Assert.IsTrue(dashboardBtn.Displayed);
            Assert.IsTrue(dashboardBtn.Enabled);
            Assert.AreEqual(dashboardBtn.Text, "Dashboard");

            var UrlBeforeClick = driver.Url;
            dashboardBtn.Click();
            var UrlAfterClick = driver.Url;
            Assert.AreNotEqual(UrlBeforeClick, UrlAfterClick);
        }

        [Test]
        public void ReportBuilderPage_ReviewEditsNavigationLinkTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var reportBuilderBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            Assert.IsTrue(reportBuilderBtn.Displayed);
            Assert.IsTrue(reportBuilderBtn.Enabled);
            Assert.AreEqual(reportBuilderBtn.Text, "Report Builder");
        }

        [Test]
        public void ReportsBuilderPage_SelectAgenciesTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var agenciesLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div[1]/div[1]/div[1]/div/div/label"));
            Assert.IsTrue(agenciesLabel.Enabled);
            Assert.IsTrue(agenciesLabel.Displayed);
            Assert.AreEqual(agenciesLabel.Text, "Select Agencies");
            Assert.AreEqual(agenciesLabel.GetAttribute("for"), "AssetState");

            var selectedAgency = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div[1]/div[1]/div[1]/div/div/input[1]"));
            Assert.IsTrue(agenciesLabel.Enabled);
            Assert.IsTrue(agenciesLabel.Displayed);
            Assert.AreEqual(selectedAgency.GetAttribute("type"),"text");
            Assert.AreEqual(selectedAgency.GetAttribute("readonly"),"true");
            Assert.AreEqual(selectedAgency.GetAttribute("placeholder"), "Click to select agencies");

            // when click on Agencies //
            selectedAgency.Click();

            var AgenciesHeader = driver.FindElement
                (By.XPath("//*[@id=\"myModal\"]/div/div/div[1]/h4"));
            Assert.IsTrue(AgenciesHeader.Enabled);
            Assert.IsTrue(AgenciesHeader.Displayed);
            //Assert.AreEqual(AgenciesHeader.Text,"Agencies");

            var Tier1 = driver.FindElement
                (By.XPath("//*[@id=\"myModal\"]/div/div/div[2]/div[1]/div[2]/label/input"));
            Assert.IsTrue(Tier1.Enabled);
            Assert.IsTrue(Tier1.Displayed);
            Assert.AreEqual(Tier1.Text,"Tier 1");
            Assert.AreEqual(Tier1.GetAttribute("type"), "checkbox");
            Tier1.Click();

            var Tier2 = driver.FindElement
                (By.XPath("//*[@id=\"myModal\"]/div/div/div[2]/div[1]/div[3]/label/input"));
            Assert.IsTrue(Tier2.Enabled);
            Assert.IsTrue(Tier2.Displayed);
            Assert.AreEqual(Tier2.Text, "Tier 2");
            Assert.AreEqual(Tier2.GetAttribute("type"), "checkbox");
            Tier2.Click();

            var selectAgenciesBtn = driver.FindElement(By.XPath("//*[@id=\"myModal\"]/div/div/div[3]/button[1]"));
            Assert.IsTrue(selectAgenciesBtn.Enabled);
            Assert.IsTrue(selectAgenciesBtn.Displayed);
            Assert.AreEqual(selectAgenciesBtn.Text, "Select Agencies");
            Assert.AreEqual(selectAgenciesBtn.GetAttribute("type"), "button");

            var CancelBtn = driver.FindElement(By.XPath("//*[@id=\"myModal\"]/div/div/div[3]/button[2]"));
            Assert.IsTrue(CancelBtn.Enabled);
            Assert.IsTrue(CancelBtn.Displayed);
            Assert.AreEqual(CancelBtn.Text, "Cancel");
            Assert.AreEqual(CancelBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void ReportBuilderPage_AssetClassDropdownlistTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var assetClassLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div[1]/div[1]/div[2]/div/div/label"));
            Assert.IsTrue(assetClassLabel.Enabled);
            Assert.IsTrue(assetClassLabel.Displayed);
            Assert.AreEqual(assetClassLabel.Text, "Asset Class");
            Assert.AreEqual(assetClassLabel.GetAttribute("for"), "AssetClass");

            var assetclassDropdownlist = driver.FindElement(By.Id("AssetClassIdChange"));
            Assert.IsTrue(assetclassDropdownlist.Enabled);
            Assert.IsTrue(assetclassDropdownlist.Displayed);

            var selectedAssetClass = new SelectElement(assetclassDropdownlist);
            selectedAssetClass.SelectByIndex(1);
        }

        [Test]
        public void ReportBuilderPage_AssetSubclassDropdownlistTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var assetSubclassLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div[1]/div[1]/div[3]/div/div/label"));
            Assert.IsTrue(assetSubclassLabel.Enabled);
            Assert.IsTrue(assetSubclassLabel.Displayed);
            Assert.AreEqual(assetSubclassLabel.Text, "Asset Subclass");
            Assert.AreEqual(assetSubclassLabel.GetAttribute("for"), "AssetSubClass");

            var assetSubclassDropdownlist = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            Assert.IsTrue(assetSubclassDropdownlist.Enabled);
            Assert.IsTrue(assetSubclassDropdownlist.Displayed);
        }

        [Test]
        public void ReportBuilderPage_AssetTypeDropdownlistTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var assetTypeLabel = driver.FindElement
           (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div[1]/div[1]/div[4]/div/div/label"));
            Assert.IsTrue(assetTypeLabel.Enabled);
            Assert.IsTrue(assetTypeLabel.Displayed);
            Assert.AreEqual(assetTypeLabel.Text, "Asset Type");
            Assert.AreEqual(assetTypeLabel.GetAttribute("for"), "AssetType");

            var assetTypeDropdownlist = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            Assert.IsTrue(assetTypeDropdownlist.Enabled);
            Assert.IsTrue(assetTypeDropdownlist.Displayed);
        }

        [Test]
        public void ReportBuilderPage_FooterCopyrightTest()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void ReportBuilderPage_MinimizeToggle()
        {
            // Open Report Builder Page
            ReportBuilderPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}