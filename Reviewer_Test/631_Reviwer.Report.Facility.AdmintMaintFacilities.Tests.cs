using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
        public void AdminMaintFacilitiesPage_ReportsOptionTest()
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
        public void AdminMaintFacilitiesPage_FacilityOptionTest()
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
        public void AdminMaintFacilitiesPage_TestAdminMaintOption()
        {
            var ReportsOption = driver.FindElement
             (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var FacilityOption = driver.FindElement
              (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/a"));
            FacilityOption.Click();

            var adminMaintFacilitiesOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/nav/ul/li[2]/a"));
            Assert.IsTrue(adminMaintFacilitiesOption.Enabled);
            Assert.IsTrue(adminMaintFacilitiesOption.Displayed);
            Assert.IsTrue(adminMaintFacilitiesOption.Text.Equals("Admin/Maint Facilities"));
            Assert.IsTrue(adminMaintFacilitiesOption.GetAttribute("custom-data").Equals("Admin/Maint Facilities"));
            Assert.AreEqual(adminMaintFacilitiesOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(adminMaintFacilitiesOption.GetAttribute("target"), "_self");
            Assert.AreEqual(adminMaintFacilitiesOption.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/Facilities");
        }

        [Test]
        public void AdninMaintFacilitiesPage_OpenPage()
        {
            driver.Navigate().
                GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/Facilities");
        }

        [Test]
        public void AdminMaintFacilitiesPage_HiHostReviewerTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
        public void AdminMaintFacilitiesPage_LogoutBtnTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
        public void AdminMaintFacilitiesPage_ModalTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
        public void AdminMaintFacilitiesPage_NotificationTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        // Sub Title 
        [Test]
        public void AdminMaintFacilitiesPage_PageSubHeaderTitleTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Facilities");
        }

        [Test]
        public void AdminMaintFacilitiesPage_ReportsNavigationLinkTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
        public void AdminMainFacilitiesPage_FacilityNavigationLinkTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var facilityBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            Assert.IsTrue(facilityBtn.Displayed);
            Assert.IsTrue(facilityBtn.Enabled);
            Assert.AreEqual(facilityBtn.Text, "Facility");
        }

        [Test]
        public void AdminMaintFacilitiesPage_FacilitiesNavigationLinkTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var BuidlingsBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            Assert.IsTrue(BuidlingsBtn.Displayed);
            Assert.IsTrue(BuidlingsBtn.Enabled);
            Assert.AreEqual(BuidlingsBtn.Text, "Facilities");
        }

        [Test]
        public void AdminMaintFacilitiesPage_AssetClassTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
        public void AdminMaintFacilitiesPage_AssetSubclassTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
            Assert.IsTrue(SelectedAssetSubclassValue.Text.Equals("Administrative/Maintenance Facilities"));
        }

        // Asset Type label must be for asset type but is linked with  asset subclass
        [Test]
        public void AdminMaintFacilitiesPage_AssetTypeTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

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
            Assert.IsTrue(SelectedAssetTypeValue.Text.Equals("Administrative/Maintenance Facility"));
        }

        [Test]
        public void AdminMaintFacilitiesPage_DataTableLengthTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var ShowLabel = driver.FindElement
                (By.XPath("//*[@id=\"facilities_length\"]/label"));
            Assert.IsTrue(ShowLabel.Enabled);
            Assert.IsTrue(ShowLabel.Displayed);
            Assert.IsTrue(ShowLabel.Text.Contains("Show"));

            var TableLength = driver.FindElement(By.Name("facilities_length"));
            Assert.IsTrue(TableLength.Enabled);
            Assert.IsTrue(TableLength.Displayed);
            var selectedTableLength = new SelectElement(TableLength);
            selectedTableLength.SelectByIndex(1);
        }

        [Test]
        public void AdminMaintFacilitiesPage_DataTableFilterTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var SearchLabel = driver.FindElement(By.Id("facilities_filter"));
            Assert.IsTrue(SearchLabel.Enabled);
            Assert.IsTrue(SearchLabel.Displayed);
            Assert.AreEqual(SearchLabel.Text, "Search:");

            var searchInput = driver.FindElement(By.XPath("//*[@id=\"facilities_filter\"]/label/input"));
            Assert.IsTrue(searchInput.Displayed);
            Assert.IsTrue(searchInput.Enabled);
            searchInput.SendKeys("test Word");
        }

        [Test]
        public void AdminMaintFacilitiesPage_ReOrderTableTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var columns = driver.FindElements(By.Id("facilities"));
            foreach (var column in columns)
            {
                Assert.IsTrue(column.Enabled);
                Assert.IsTrue(column.Displayed);
            }

            var RowNo = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[1]"));
            Assert.IsTrue(RowNo.Enabled);
            Assert.IsTrue(RowNo.Displayed);
            Assert.AreEqual(RowNo.Text,"Row No");
            Assert.AreEqual(RowNo.GetAttribute("aria-sort"), "ascending");
            Assert.AreEqual(RowNo.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(RowNo.GetAttribute("aria-label"), "Row No: activate to sort column descending");
            RowNo.Click();

            var FacilityId = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[2]"));
            Assert.IsTrue(FacilityId.Enabled);
            Assert.IsTrue(FacilityId.Displayed);
            Assert.AreEqual(FacilityId.Text, "Facility Id");
            Assert.AreEqual(FacilityId.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(FacilityId.GetAttribute("aria-label"), "Facility Id: activate to sort column ascending");
            FacilityId.Click();

            var FacilityName = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[3]"));
            Assert.IsTrue(FacilityName.Enabled);
            Assert.IsTrue(FacilityName.Displayed);
            Assert.AreEqual(FacilityName.Text, "Facility Name");
            Assert.AreEqual(FacilityName.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(FacilityName.GetAttribute("aria-label"), "Facility Name: activate to sort column ascending");
            FacilityName.Click();

            var Type = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[4]"));
            Assert.IsTrue(Type.Enabled);
            Assert.IsTrue(Type.Displayed);
            Assert.AreEqual(Type.Text, "Type");
            Assert.AreEqual(Type.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(Type.GetAttribute("aria-label"), "Type: activate to sort column ascending");
            Type.Click();

            var StreetAddress = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[5]"));
            Assert.IsTrue(StreetAddress.Enabled);
            Assert.IsTrue(StreetAddress.Displayed);
            Assert.AreEqual(StreetAddress.Text, "Street Address");
            Assert.AreEqual(StreetAddress.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(StreetAddress.GetAttribute("aria-label"), "Street Address: activate to sort column ascending");
            StreetAddress.Click();

            var City = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[6]"));
            Assert.IsTrue(City.Enabled);
            Assert.IsTrue(City.Displayed);
            Assert.AreEqual(City.Text, "City");
            Assert.AreEqual(City.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(City.GetAttribute("aria-label"), "City: activate to sort column ascending");
            City.Click();

            var State = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[7]"));
            Assert.IsTrue(State.Enabled);
            Assert.IsTrue(State.Displayed);
            Assert.AreEqual(State.Text, "State");
            Assert.AreEqual(State.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(State.GetAttribute("aria-label"), "State: activate to sort column ascending");
            State.Click();

            var Zip = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[8]"));
            Assert.IsTrue(Zip.Enabled);
            Assert.IsTrue(Zip.Displayed);
            Assert.AreEqual(Zip.Text, "Zip");
            Assert.AreEqual(Zip.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(Zip.GetAttribute("aria-label"), "Zip: activate to sort column ascending");
            Zip.Click();

            var YearBuiltOrReconstructed = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[9]"));
            Assert.IsTrue(YearBuiltOrReconstructed.Enabled);
            Assert.IsTrue(YearBuiltOrReconstructed.Displayed);
            Assert.AreEqual(YearBuiltOrReconstructed.Text, "Year Built Or Reconstructed");
            Assert.AreEqual(YearBuiltOrReconstructed.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(YearBuiltOrReconstructed.GetAttribute("aria-label"), "Year Built Or Reconstructed: activate to sort column ascending");
            YearBuiltOrReconstructed.Click();

            var FacilityArea = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[10]"));
            Assert.IsTrue(FacilityArea.Enabled);
            Assert.IsTrue(FacilityArea.Displayed);
            Assert.AreEqual(FacilityArea.Text, "Facility Area");
            Assert.AreEqual(FacilityArea.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(FacilityArea.GetAttribute("aria-label"), "Facility Area: activate to sort column ascending");
            FacilityArea.Click();

            var OverallCondition = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[11]"));
            Assert.IsTrue(OverallCondition.Enabled);
            Assert.IsTrue(OverallCondition.Displayed);
            Assert.AreEqual(OverallCondition.Text, "Overall Condition");
            Assert.AreEqual(OverallCondition.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(OverallCondition.GetAttribute("aria-label"), "Overall Condition: activate to sort column ascending");
            OverallCondition.Click();

            var ConditionAssessmentDate = driver.FindElement(By.XPath("//*[@id=\"facilities\"]/thead/tr/th[12]"));
            Assert.IsTrue(ConditionAssessmentDate.Enabled);
            Assert.IsTrue(ConditionAssessmentDate.Displayed);
            Assert.AreEqual(ConditionAssessmentDate.Text, "Condition Assessment Date");
            Assert.AreEqual(ConditionAssessmentDate.GetAttribute("aria-controls"), "facilities");
            Assert.AreEqual(ConditionAssessmentDate.GetAttribute("aria-label"), "Condition Assessment Date: activate to sort column ascending");
            ConditionAssessmentDate.Click();
        }

        [Test]
        public void AdminMaintFacilitiesPage_ExportBtnTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var exportBtn = driver.FindElement(By.Id("ExportCSVLink"));
            Assert.IsTrue(exportBtn.Enabled);
            Assert.IsTrue(exportBtn.Displayed);
            Assert.IsTrue(exportBtn.Text.Equals("Export CSV"));
            exportBtn.Click();
        }

        [Test]
        public void AdminMaintFacilitiesPage_PaginateTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("facilities_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("facilities_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("facilities_paginate"));
            foreach (var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
            }
        }

        [Test]
        public void AdminMaintFacilitiePage_FooterCopyrightTest()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void AdminMaintFacilitiesPage_MinimizeToggle()
        {
            // Open Admin Main Facilities Page
            AdninMaintFacilitiesPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}

// you can solve logout test case by using sleep when time equals 7.5 seconds