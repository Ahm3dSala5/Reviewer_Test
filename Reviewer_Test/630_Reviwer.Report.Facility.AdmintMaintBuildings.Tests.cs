using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportFacilityAdmintMaintBuildingsTests : IDisposable
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
        public void AdminMaintBuildingsPage_ReportsOptionTest()
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
        public void AdminMaintBuildingsPage_FacilityOptionTest()
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
        public void AdminMaintBuildingsPage_TestAdminMaintOption()
        {
            var ReportsOption = driver.FindElement
             (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var FacilityOption = driver.FindElement
              (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/a"));
            FacilityOption.Click();

            var adminMaintBuildingsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[4]/nav/ul/li[1]/a"));
            Assert.IsTrue(adminMaintBuildingsOption.Enabled);
            Assert.IsTrue(adminMaintBuildingsOption.Displayed);
            Assert.IsTrue(adminMaintBuildingsOption.Text.Equals("Admin/Maint Buildings"));
            Assert.IsTrue(adminMaintBuildingsOption.GetAttribute("custom-data").Equals("Admin/Maint Buildings"));
            Assert.AreEqual(adminMaintBuildingsOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(adminMaintBuildingsOption.GetAttribute("target"), "_self");
            Assert.AreEqual(adminMaintBuildingsOption.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/Buildings");
        }

        [Test]
        public void AdninMaintBuildings_OpenPage()
        {
            driver.Navigate().
                GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/Buildings");
        }

        [Test]
        public void AdminMaintBuildingsPage_HiHostReviewerTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
        public void AdminMaintBuidlingsPage_LogoutBtnTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var hiReviewer = driver.FindElement(By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a"));
            hiReviewer.Click();

            var logoutBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/div/div/div/div/ul/li[4]/a"));

            Assert.IsTrue(logoutBtn.Enabled);
            Assert.IsTrue(logoutBtn.Displayed);
            Assert.AreEqual("Logout", logoutBtn.Text.Trim());
            Assert.AreEqual("http://ec2-34-226-24-71.compute-1.amazonaws.com/Account/Login",
                logoutBtn.GetAttribute("href"));

            var urlBeforeClick = driver.Url;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", logoutBtn);

            var maxWait = DateTime.Now.AddSeconds(10);
            while (driver.Url == urlBeforeClick && DateTime.Now < maxWait)
            {
                Thread.Sleep(500);
            }

            var urlAfterClick = driver.Url;
            Assert.AreNotEqual(urlBeforeClick, urlAfterClick);
        }

        [Test]
        public void AdminMaintBuildingsPage_ModalTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
        public void AdminMaintBuildingsPage_NotificationTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
        public void AdminMaintBuildingsPage_PageSubHeaderTitleTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Buildings");
        }

        [Test]
        public void AdminMaintBuildingsPage_ReportsNavigationLinkTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
        public void AdminMainBuildingsPage_FacilityNavigationLinkTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var facilityBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            Assert.IsTrue(facilityBtn.Displayed);
            Assert.IsTrue(facilityBtn.Enabled);
            Assert.AreEqual(facilityBtn.Text, "Facility");
        }

        [Test]
        public void AdminMaintBuildingsPage_BuildingsNavigationLinkTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var BuidlingsBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            Assert.IsTrue(BuidlingsBtn.Displayed);
            Assert.IsTrue(BuidlingsBtn.Enabled);
            Assert.AreEqual(BuidlingsBtn.Text, "Buildings");
        }

        [Test]
        public void AdminMaintBuildingsPage_AssetClassTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
        public void AdminMaintBuildingsPage_AssetSubclassTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
        public void AdminMaintBuildningsPage_AssetTypeTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

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
            Assert.IsTrue(SelectedAssetTypeValue.Text.Equals("Administrative/Maintenance Building"));
        }

        [Test]
        public void AdminMaintBuildingsPage_DataTableLengthTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var ShowLabel = driver.FindElement
                (By.XPath("//*[@id=\"buildings_length\"]/label"));
            Assert.IsTrue(ShowLabel.Enabled);
            Assert.IsTrue(ShowLabel.Displayed);
            Assert.IsTrue(ShowLabel.Text.Contains("Show"));

            var TableLength = driver.FindElement(By.Name("buildings_length"));
            Assert.IsTrue(TableLength.Enabled);
            Assert.IsTrue(TableLength.Displayed);
            var selectedTableLength = new SelectElement(TableLength);
            selectedTableLength.SelectByIndex(1);
        }

        [Test]
        public void AdminMaintBuildingsPage_DataTableFilterTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var SearchLabel = driver.FindElement(By.Id("buildings_filter"));
            Assert.IsTrue(SearchLabel.Enabled);
            Assert.IsTrue(SearchLabel.Displayed);
            Assert.AreEqual(SearchLabel.Text, "Search:");

            var searchInput = driver.FindElement(By.XPath("//*[@id=\"buildings_filter\"]/label/input"));
            Assert.IsTrue(searchInput.Displayed);
            Assert.IsTrue(searchInput.Enabled);
            searchInput.SendKeys("test Word");
        }

        [Test]
        public void AdminMaintBuildingsPage_ReOrderTableTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var columns = driver.FindElements(By.Id("buildings"));
            foreach(var column in columns)
            {
                Assert.IsTrue(column.Enabled);
                Assert.IsTrue(column.Displayed);
            }

            var RowNo = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[1]"));
            Assert.IsTrue(RowNo.Enabled);
            Assert.IsTrue(RowNo.Displayed);
            Assert.IsTrue(RowNo.Text.Equals("Row No"));
            Assert.AreEqual(RowNo.GetAttribute("aria-sort"), "ascending");
            Assert.AreEqual(RowNo.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(RowNo.GetAttribute("aria-label"), "Row No: activate to sort column descending");
            RowNo.Click();

            var assetId = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[2]"));
            Assert.IsTrue(assetId.Enabled);
            Assert.IsTrue(assetId.Displayed);
            Assert.IsTrue(assetId.Text.Equals("Asset Id"));
            Assert.AreEqual(assetId.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(assetId.GetAttribute("aria-label"), "Asset Id: activate to sort column ascending");
            assetId.Click();

            var BuildingsDesc = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[3]"));
            Assert.IsTrue(BuildingsDesc.Enabled);
            Assert.IsTrue(BuildingsDesc.Displayed);
            Assert.IsTrue(BuildingsDesc.Text.Equals("Building Desc"));
            Assert.AreEqual(BuildingsDesc.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(BuildingsDesc.GetAttribute("aria-label"), "Building Desc: activate to sort column ascending");
            BuildingsDesc.Click();

            var BuildingArea = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[4]"));
            Assert.IsTrue(BuildingArea.Enabled);
            Assert.IsTrue(BuildingArea.Displayed);
            Assert.IsTrue(BuildingArea.Text.Equals("Building Area"));
            Assert.AreEqual(BuildingArea.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(BuildingArea.GetAttribute("aria-label"), "Building Area: activate to sort column ascending");
            BuildingArea.Click();

            var OverAllRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[5]"));
            Assert.IsTrue(OverAllRating.Displayed);
            Assert.AreEqual(OverAllRating.Text,"Overall Rating");
            Assert.AreEqual(OverAllRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(OverAllRating.GetAttribute("aria-label"), "Overall Rating: activate to sort column ascending");
            OverAllRating.Click();

            var ConditionAssessmentDate = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[6]"));
            Assert.IsTrue(ConditionAssessmentDate.Enabled);
            Assert.IsTrue(ConditionAssessmentDate.Displayed);
            Assert.IsTrue(ConditionAssessmentDate.Text.Equals("Condition Assessment Date"));
            Assert.AreEqual(ConditionAssessmentDate.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(ConditionAssessmentDate.GetAttribute("aria-label"), "Condition Assessment Date: activate to sort column ascending");
            ConditionAssessmentDate.Click();

            var SiteRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[7]"));
            Assert.IsTrue(SiteRating.Enabled);
            Assert.IsTrue(SiteRating.Displayed);
            Assert.IsTrue(SiteRating.Text.Equals("Site Rating"));
            Assert.AreEqual(SiteRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(SiteRating.GetAttribute("aria-label"), "Site Rating: activate to sort column ascending");
            SiteRating.Click();

            var SubStructureRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[8]"));
            Assert.IsTrue(SubStructureRating.Enabled);
            Assert.IsTrue(SubStructureRating.Displayed);
            Assert.IsTrue(SubStructureRating.Text.Equals("Substructure Rating"));
            Assert.AreEqual(SubStructureRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(SubStructureRating.GetAttribute("aria-label"), "Substructure Rating: activate to sort column ascending");
            SubStructureRating.Click();

            var ShellRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[9]"));
            Assert.IsTrue(ShellRating.Enabled);
            Assert.IsTrue(ShellRating.Displayed);
            Assert.IsTrue(ShellRating.Text.Equals("Shell Rating"));
            Assert.AreEqual(ShellRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(ShellRating.GetAttribute("aria-label"), "Shell Rating: activate to sort column ascending");
            ShellRating.Click();

            var InteriorRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[10]"));
            Assert.IsTrue(InteriorRating.Enabled);
            Assert.IsTrue(InteriorRating.Displayed);
            Assert.IsTrue(InteriorRating.Text.Equals("Interior Rating"));
            Assert.AreEqual(InteriorRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(InteriorRating.GetAttribute("aria-label"), "Interior Rating: activate to sort column ascending");
            InteriorRating.Click();

            var PlumbingRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[11]"));
            Assert.IsTrue(PlumbingRating.Enabled);
            Assert.IsTrue(PlumbingRating.Displayed);
            Assert.IsTrue(PlumbingRating.Text.Equals("Plumbing Rating"));
            Assert.AreEqual(PlumbingRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(PlumbingRating.GetAttribute("aria-label"), "Plumbing Rating: activate to sort column ascending");
            PlumbingRating.Click();

            var HVACRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[12]"));
            Assert.IsTrue(HVACRating.Enabled);
            Assert.IsTrue(HVACRating.Displayed);
            Assert.IsTrue(HVACRating.Text.Equals("HVAC Rating"));
            Assert.AreEqual(HVACRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(HVACRating.GetAttribute("aria-label"), "HVAC Rating: activate to sort column ascending");
            HVACRating.Click();

            var ElectricalRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[13]"));
            Assert.IsTrue(ElectricalRating.Enabled);
            Assert.IsTrue(ElectricalRating.Displayed);
            Assert.IsTrue(ElectricalRating.Text.Equals("Electrical Rating"));
            Assert.AreEqual(ElectricalRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(ElectricalRating.GetAttribute("aria-label"), "Electrical Rating: activate to sort column ascending");
            ElectricalRating.Click();

            var FireProtectionRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[14]"));
            Assert.IsTrue(FireProtectionRating.Enabled);
            Assert.IsTrue(FireProtectionRating.Displayed);
            Assert.IsTrue(FireProtectionRating.Text.Equals("Fire Protection Rating"));
            Assert.AreEqual(FireProtectionRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(FireProtectionRating.GetAttribute("aria-label"), "Fire Protection Rating: activate to sort column ascending");
            FireProtectionRating.Click();

            var ConveyanceRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[15]"));
            Assert.IsTrue(ConveyanceRating.Enabled);
            Assert.IsTrue(ConveyanceRating.Displayed);
            Assert.IsTrue(ConveyanceRating.Text.Equals("Conveyance Rating"));
            Assert.AreEqual(ConveyanceRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(ConveyanceRating.GetAttribute("aria-label"), "Conveyance Rating: activate to sort column ascending");
            ConveyanceRating.Click();

            var EquipmentRating = driver.FindElement(By.XPath("//*[@id=\"buildings\"]/thead/tr/th[16]"));
            Assert.IsTrue(EquipmentRating.Enabled);
            Assert.IsTrue(EquipmentRating.Displayed);
            Assert.IsTrue(EquipmentRating.Text.Equals("Equipment Rating"));
            Assert.AreEqual(EquipmentRating.GetAttribute("aria-controls"), "buildings");
            Assert.AreEqual(EquipmentRating.GetAttribute("aria-label"), "Equipment Rating: activate to sort column ascending");
            EquipmentRating.Click();
        }

        [Test]
        public void AdminMaintBuildingsPage_ExportBtnTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var exportBtn = driver.FindElement(By.Id("ExportCSVLink"));
            Assert.IsTrue(exportBtn.Enabled);
            Assert.IsTrue(exportBtn.Displayed);
            Assert.IsTrue(exportBtn.Text.Equals("Export CSV"));
            exportBtn.Click();
        }

        [Test]
        public void AdminMaintBuildingsPage_PaginateTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("buildings_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("buildings_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("buildings_paginate"));
            foreach (var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
            }
        }

        [Test]
        public void AdminMaintBuildingsPage_FooterCopyrightTest()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void AdminMaintBuildings_MinimizeToggle()
        {
            // Open Admin Maint Page
            AdninMaintBuildings_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}
