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
          //  driver.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
              // driver.Quit();
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
        public void AssetAuditPage_ReportsOptionTest()
        {
            var ReportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            Assert.IsTrue(ReportOption.Enabled);
            Assert.IsTrue(ReportOption.Displayed);
            Assert.AreEqual(ReportOption.Text, "Reports");
            Assert.AreEqual(ReportOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(ReportOption.GetAttribute("custom-data"), "Reports");
            Assert.AreEqual(ReportOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void AssetAuditPage_AssetAuditOptionTest()
        {
            var ReportOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportOption.Click();

            var AssetAuditOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[1]/a"));
            Assert.IsTrue(AssetAuditOption.Enabled);
            Assert.IsTrue(AssetAuditOption.Displayed);
            Assert.AreEqual(AssetAuditOption.Text, "Asset Audit");
            Assert.AreEqual(AssetAuditOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(AssetAuditOption.GetAttribute("custom-data"), "Asset Audit");
            Assert.AreEqual(AssetAuditOption.GetAttribute("target"), "_self");
            Assert.AreEqual(AssetAuditOption.GetAttribute("href"), "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Assets/AssetAudit");
        }

        [Test]
        public void AssetAuditPage_OpenPage()
        {
            var ReportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportOption.Click();

            var AssetAuditOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[1]/a"));
            AssetAuditOption.Click();
        }

        [Test]
        public void AssetAuditPage_HiHostReviewerTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

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
        public void AssetAuditPage_LogoutBtn()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

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
        public void AssetAuditPage_ModalTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

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
        public void AssetAuditPage_NotificationTest()
        {
            // Open Create Asset Page
            AssetAuditPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void AssetAuditPage_PageTitleTest()
        {
            // Open Create Asset Page
            AssetAuditPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Asset Audit");
        }

        // Dashboard Navegation Btn Named Dashboard As therfore return error
        [Test]
        public void AssetAuditPage_DashboardNavigationLinkTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

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
        public void AssetAuditPage_ReviewEditsNavigationLinkTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var viewEditBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            Assert.IsTrue(viewEditBtn.Displayed);
            Assert.IsTrue(viewEditBtn.Enabled);
            Assert.AreEqual(viewEditBtn.Text, "Asset Audit");
        }

        [Test]
        public void AssetAuditPage_AgencyNameTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var agencyNameHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/label/b"));
            Assert.IsTrue(agencyNameHeader.Enabled);
            Assert.IsTrue(agencyNameHeader.Displayed);
            Assert.AreEqual(agencyNameHeader.Text,"Select Agency Name");

            var agencyNameBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/button"));
            Assert.IsTrue(agencyNameHeader.Enabled);
            Assert.IsTrue(agencyNameHeader.Displayed);

            // after click on agency btn will display window contain all agencies 
            agencyNameBtn.Click();

            var searchInput = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/ul/li[1]/div/input"));
            Assert.IsTrue(searchInput.Enabled);
            Assert.IsTrue(searchInput.Displayed);
            Assert.AreEqual(searchInput.GetAttribute("type"),"text");
            Assert.AreEqual(searchInput.GetAttribute("placeholder"), "Search");

            var Options = driver.FindElements(By.XPath
                ("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/ul"));
            foreach(var option in Options)
            {
                Assert.IsTrue(option.Enabled);
                Assert.IsTrue(option.Displayed);
            }
        }

        [Test]
        public void AssetAuditPage_AssetClassDropdownlistTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var assetClassLabel = driver.FindElement
                (By.XPath("//*[@id=\"New Filter\"]/div[1]/div/div[1]/label"));
            Assert.IsTrue(assetClassLabel.Enabled);
            Assert.IsTrue(assetClassLabel.Displayed);
            Assert.AreEqual(assetClassLabel.Text,"Asset Class");
            Assert.AreEqual(assetClassLabel.GetAttribute("for"),"AssetClass");

            var assetclassDropdownlist = driver.FindElement(By.Id("AssetClassIdChange"));
            Assert.IsTrue(assetclassDropdownlist.Enabled);
            Assert.IsTrue(assetclassDropdownlist.Displayed);

            var selectedAssetClass = new SelectElement(assetclassDropdownlist);
            selectedAssetClass.SelectByIndex(1);
        }

        [Test]
        public void AssetAuditPage_AssetSubclassDropdownlistTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var assetSubclassLabel = driver.FindElement
                (By.XPath("//*[@id=\"New Filter\"]/div[2]/div/div[1]/label"));
            Assert.IsTrue(assetSubclassLabel.Enabled);
            Assert.IsTrue(assetSubclassLabel.Displayed);
            Assert.AreEqual(assetSubclassLabel.Text, "Asset Subclass");
            Assert.AreEqual(assetSubclassLabel.GetAttribute("for"), "AssetSubClass");

            var assetSubclassDropdownlist = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            Assert.IsTrue(assetSubclassDropdownlist.Enabled);
            Assert.IsTrue(assetSubclassDropdownlist.Displayed);

            var Option = driver.FindElement(By.XPath("//*[@id=\"AssetSubClassDropDownChange\"]/option"));
            Assert.IsTrue(Option.Enabled);
            Assert.IsTrue(Option.Displayed);
            Assert.AreEqual(Option.Text, "No Asset Subclass");
        }

        // AssetTyope Label Must Be Designed for Asset Type But is Linked With Asset SubClass
        [Test]
        public void AssetAuditPage_AssetTypeDropdownlistTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var assetTypeLabel = driver.FindElement
           (By.XPath("//*[@id=\"New Filter\"]/div[3]/div/div[1]/label"));
            Assert.IsTrue(assetTypeLabel.Enabled);
            Assert.IsTrue(assetTypeLabel.Displayed);
            Assert.AreEqual(assetTypeLabel.Text, "Asset Type");
            Assert.AreEqual(assetTypeLabel.GetAttribute("for"), "AssetType");

            var assetTypeDropdownlist = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            Assert.IsTrue(assetTypeDropdownlist.Enabled);
            Assert.IsTrue(assetTypeDropdownlist.Displayed);

            var Option = driver.FindElement(By.XPath("//*[@id=\"AssetTypeDropDownChange\"]/option"));
            Assert.IsTrue(Option.Enabled);
            Assert.IsTrue(Option.Displayed);
            Assert.AreEqual(Option.Text, "No Asset Type");
        }

        
        // Asset State Label Must Be Linked With Asset State But Is Linked With Srart Date 
        [Test]
        public void AssetAuditPage_AssetStateDropdownlistTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var assetStateLabel = driver.FindElement
                (By.XPath("//*[@id=\"New Filter\"]/div[4]/div/div/label"));
            Assert.IsTrue(assetStateLabel.Enabled);
            Assert.IsTrue(assetStateLabel.Displayed);
            Assert.AreEqual(assetStateLabel.Text, "Asset State");
            Assert.AreEqual(assetStateLabel.GetAttribute("for"), "AssetState");

            var assetStateDropdownlist = driver.FindElement(By.Id("AssetStateIdChange"));
            Assert.IsTrue(assetStateDropdownlist.Enabled);
            Assert.IsTrue(assetStateDropdownlist.Displayed);

            var selectedAssetClass = new SelectElement(assetStateDropdownlist);
            selectedAssetClass.SelectByIndex(1);
        }

        [Test]
        public void AssetAuditPage_StartDateTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var startDateLabel = driver.FindElement
                (By.XPath("//*[@id=\"New Filter\"]/div[5]/div/div/label")); 
            Assert.IsTrue(startDateLabel.Enabled);
            Assert.IsTrue(startDateLabel.Displayed);
            Assert.AreEqual(startDateLabel.Text, "Start Date");
            Assert.AreEqual(startDateLabel.GetAttribute("for"), "StartDate");

            var StartDate = driver.FindElement(By.Id("StartDate"));
            Assert.IsTrue(StartDate.Enabled);
            Assert.IsTrue(StartDate.Displayed);
            Assert.AreEqual(StartDate.GetAttribute("type"), "date");
            Assert.IsTrue(StartDate.GetAttribute("max").Contains($"{DateTime.UtcNow.Year}"));
        }

        // End Date Label Must Linked With End Date But Is Linked With Start Date
        [Test]
        public void AssetAuditPage_EndDateTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var endDateLabel = driver.FindElement
                (By.XPath("//*[@id=\"New Filter\"]/div[6]/div/div/label"));
            Assert.IsTrue(endDateLabel.Enabled);
            Assert.IsTrue(endDateLabel.Displayed);
            Assert.AreEqual(endDateLabel.Text, "End Date");
            Assert.AreEqual(endDateLabel.GetAttribute("for"), "EndDate");

            var EndDate = driver.FindElement(By.Id("EndDate"));
            Assert.IsTrue(EndDate.Enabled);
            Assert.IsTrue(EndDate.Displayed);
            Assert.AreEqual(EndDate.GetAttribute("type"), "date");
            Assert.AreEqual(EndDate.GetAttribute("max"), "9999-12-31");
        }

        [Test]
        public void AssetAuditPage_DataTableLengthTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var ShowLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable_length\"]/label"));
            Assert.IsTrue(ShowLabel.Enabled);
            Assert.IsTrue(ShowLabel.Displayed);
            Assert.IsTrue(ShowLabel.Text.Contains("Show"));

            var TableLength = driver.FindElement(By.Name("AssetsTable_length"));
            Assert.IsTrue(TableLength.Enabled);
            Assert.IsTrue(TableLength.Displayed);
            var selectedTableLength = new SelectElement(TableLength);
            selectedTableLength.SelectByIndex(1);
        }

        [Test]
        public void AssetAuditPage_DataTableFilterTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var SearchLabel = driver.FindElement(By.Id("AssetsTable_filter"));
            Assert.IsTrue(SearchLabel.Enabled);
            Assert.IsTrue(SearchLabel.Displayed);
            Assert.AreEqual(SearchLabel.Text, "Search:");

            var searchInput = driver.FindElement(By.XPath("//*[@id=\"AssetsTable_filter\"]/label/input"));
            Assert.IsTrue(searchInput.Displayed);
            Assert.IsTrue(searchInput.Enabled);
            searchInput.SendKeys("test Word");
        }

        [Test]
        public void AssetAuditPage_PaginateTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("AssetsTable_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("AssetsTable_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("AssetsTable_paginate"));
            foreach (var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
            }
        }


        [Test]
        public void AssetAuditPage_FooterCopyrightTest()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void AuditAssetPage_MinimizeToggle()
        {
            // Open Asset Audit Page Page
            AssetAuditPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}