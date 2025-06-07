using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportFacilityPassengerFacilitiesTests : IDisposable
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
            driver.Manage().Timeouts().ImplicitWait.Add(TimeSpan.FromSeconds(7));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
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
        public void PassengerFacilitiesPage_ReportsOptionTest()
        {
            var ReportsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));

            Assert.IsTrue(ReportsOption.Enabled);
            Assert.IsTrue(ReportsOption.Displayed);
            Assert.IsTrue(ReportsOption.Text.Equals("Reports"));
            Assert.AreEqual(ReportsOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(ReportsOption.GetAttribute("custom-data"), "Reports");
            Assert.AreEqual(ReportsOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void PassengerFacilitiesPage_FacilityOptionTest()
        {
            var ReportsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var FacilityOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/a"));

            Assert.IsTrue(FacilityOption.Enabled);
            Assert.IsTrue(FacilityOption.Displayed);
            Assert.IsTrue(FacilityOption.Text.Equals("Facility"));
            Assert.AreEqual(FacilityOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(FacilityOption.GetAttribute("custom-data"), "Facility");
            Assert.AreEqual(FacilityOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void PassengerFacilitiesPage_TestAdminMaintOption()
        {
            var ReportsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var FacilityOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/a"));
            FacilityOption.Click();

            var passengerFacilitiesOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/nav/ul/li[3]/a/span"));
            Assert.IsTrue(passengerFacilitiesOption.Enabled);
            Assert.IsTrue(passengerFacilitiesOption.Displayed);
            Assert.IsTrue(passengerFacilitiesOption.Text.Equals("Passenger Facilities"));
            Assert.IsTrue(passengerFacilitiesOption.GetAttribute("custom-data").Equals("Passenger Facilities"));
            Assert.AreEqual(passengerFacilitiesOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(passengerFacilitiesOption.GetAttribute("target"), "_self");
            Assert.AreEqual(passengerFacilitiesOption.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/PassengerFacilities");
        }

        [Test]
        public void PassengerFacilitiesPage_OpenPage()
        {
            driver.Navigate().
                GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/PassengerFacilities");
        }

        [Test]
        public void PassengerFacilitiesPage_HiHostReviewerTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

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
        public void PassengerFacilitiesPage_LogoutBtnTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var HiReviewer = driver.FindElement
               (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            Thread.Sleep(7500);
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
        public void PassengerFacilitiesPage_ModalTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

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
        public void PassengerFacilitiesPage_NotificationTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void PassengerFacilitiesPage_ReportsNavigationLinkTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var reportsBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a"));
            Assert.IsTrue(reportsBtn.Displayed);
            Assert.IsTrue(reportsBtn.Enabled);
            Assert.AreEqual(reportsBtn.Text, "Reports");

            var UrlBeforeClick = driver.Url;
            reportsBtn.Click();
            var UrlAfterClick = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            Assert.AreNotEqual(UrlBeforeClick, UrlAfterClick);
        }

        [Test]
        public void PassengerFacilitiesPage_FacilityNavigationLinkTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var facilityBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            Assert.IsTrue(facilityBtn.Displayed);
            Assert.IsTrue(facilityBtn.Enabled);
            Assert.AreEqual(facilityBtn.Text, "Facility");
        }

        [Test]
        public void PassengerFacilitiesPage_FacilitiesNavigationLinkTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var passengerFacilitiesOption = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            Assert.IsTrue(passengerFacilitiesOption.Displayed);
            Assert.IsTrue(passengerFacilitiesOption.Enabled);
            Assert.AreEqual(passengerFacilitiesOption.Text, "Passenger Facilities");
        }

        // page sub Title 
        [Test]
        public void PassengerFacilitiesPage_PageSubHeaderTitleTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Passenger Facilities");
        }

        [Test]
        public void PassengerFacilitiesPage_AssetClassTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var assetClassLabel = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/div/div/label"));
            Assert.IsTrue(assetClassLabel.Enabled);
            Assert.IsTrue(assetClassLabel.Displayed);
            Assert.AreEqual(assetClassLabel.Text, "Asset Class");
            Assert.AreEqual(assetClassLabel.GetAttribute("for"), "AssetClass");

            var FacilityHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/div/div/b"));
            Assert.IsTrue(FacilityHeader.Enabled);
            Assert.IsTrue(FacilityHeader.Displayed);
            Assert.IsTrue(FacilityHeader.Text.Equals("Facility"));
        }

        [Test]
        public void PassengerFacilitiesPage_AssetSubclassTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var assetSubclassLabel = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/label"));
            Assert.IsTrue(assetSubclassLabel.Enabled);
            Assert.IsTrue(assetSubclassLabel.Displayed);
            Assert.AreEqual(assetSubclassLabel.Text, "Asset Subclass");
            Assert.AreEqual(assetSubclassLabel.GetAttribute("for"), "AssetSubClass");

            var SelectedAssetSubclassValue = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/b"));
            Assert.IsTrue(SelectedAssetSubclassValue.Enabled);
            Assert.IsTrue(SelectedAssetSubclassValue.Displayed);
            Assert.IsTrue(SelectedAssetSubclassValue.Text.Equals("Passenger Facilities"));
        }

        // Asset Type label must be for asset type but is linked with  asset subclass
        [Test]
        public void PassengerFacilitiesPage_AssetTypeTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var assetTypeLabel = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[3]/div/div/label"));
            Assert.IsTrue(assetTypeLabel.Enabled);
            Assert.IsTrue(assetTypeLabel.Displayed);
            Assert.AreEqual(assetTypeLabel.Text, "Asset Type");
            Assert.AreEqual(assetTypeLabel.GetAttribute("for"), "AssetType");

            var SelectedAssetTypeValue = driver.FindElement
                  (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/b"));
            Assert.IsTrue(SelectedAssetTypeValue.Enabled);
            Assert.IsTrue(SelectedAssetTypeValue.Displayed);
            Assert.IsTrue(SelectedAssetTypeValue.Text.Equals("Passenger Facility"));
        }

        [Test]
        public void AdminMaintFacilitiesPage_DataTableLengthTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var ShowLabel = driver.FindElement
                (By.XPath("//*[@id=\"passengerFacilities_length\"]/label"));
            Assert.IsTrue(ShowLabel.Enabled);
            Assert.IsTrue(ShowLabel.Displayed);
            Assert.IsTrue(ShowLabel.Text.Contains("Show"));

            var TableLength = driver.FindElement(By.Name("passengerFacilities_length"));
            Assert.IsTrue(TableLength.Enabled);
            Assert.IsTrue(TableLength.Displayed);
            var selectedTableLength = new SelectElement(TableLength);
            selectedTableLength.SelectByIndex(1);
        }

        [Test]
        public void PassengerFacilitiesPage_DataTableFilterTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var SearchLabel = driver.FindElement(By.Id("passengerFacilities_filter"));
            Assert.IsTrue(SearchLabel.Enabled);
            Assert.IsTrue(SearchLabel.Displayed);
            Assert.AreEqual(SearchLabel.Text, "Search:");

            var searchInput = driver.FindElement(By.XPath("//*[@id=\"passengerFacilities_filter\"]/label/input"));
            Assert.IsTrue(searchInput.Displayed);
            Assert.IsTrue(searchInput.Enabled);
            searchInput.SendKeys("test Word");
        }

        [Test]
        public void PassengerFacilitiesPage_ReOrderTableTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();
        }

        [Test]
        public void PassengerFacilitiesPage_ExportBtnTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var exportBtn = driver.FindElement(By.Id("ExportCSVLink"));
            Assert.IsTrue(exportBtn.Enabled);
            Assert.IsTrue(exportBtn.Displayed);
            Assert.IsTrue(exportBtn.Text.Equals("Export CSV"));
            exportBtn.Click();
        }

        [Test]
        public void PassengerFacilaitites_PaginateTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("passengerFacilities_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("passengerFacilities_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("passengerFacilities_paginate"));
            foreach (var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
            }
        }

        [Test]
        public void PassengerFacilitiePage_FooterCopyrightTest()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void PassengerFacilitiesPage_MinimizeToggle()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }

        [Test]
        public void PassengerFacility_TestReportsIcon()
        {
            // Open Passenger Facilities Page
            PassengerFacilitiesPage_OpenPage();

            var ReportsICon = driver.FindElement(By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/i[1]"));
            Assert.AreEqual(ReportsICon.GetAttribute("class"),
                "m-menu__link-icon flaticon-cogwheel");

            var ReportsArro = driver.FindElement(By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/i[2]"));
            Assert.AreEqual(ReportsArro.GetAttribute("class"), "m-menu__ver-arrow la la-angle-right");
        }
    }
}

// add arrow and icon for all dropdownlist and span 
// for this account review test and in host operator and in admin account 
// aplay showing .. for entires on all test cases of any page contains table
// for all dropdownlist test if this for class type form specific icon or not

// estemated time are ecpected eauals 46 hours 
