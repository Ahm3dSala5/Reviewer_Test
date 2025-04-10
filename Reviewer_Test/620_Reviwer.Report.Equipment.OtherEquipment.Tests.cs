using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportEquipmentOtherEquipmentTests : IDisposable
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
        public void OtherEquipmentPage_ReportsOptionTest()
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
        public void OtherEquipmentPage_EquipmentOptionTest()
        {
            var ReportsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var equipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/a"));
            Assert.IsTrue(equipmentOption.Enabled);
            Assert.IsTrue(equipmentOption.Displayed);
            Assert.AreEqual(equipmentOption.Text, "Equipment");
            Assert.AreEqual(equipmentOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(equipmentOption.GetAttribute("custom-data"), "Equipment");
            Assert.AreEqual(equipmentOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void OtherEquipmentPage_OtherEquipmentOptionTest()
        {
            var ReportsOption = driver.FindElement
             (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var equipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/a"));
            equipmentOption.Click();

            var otherEquipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/nav/ul/li[1]/a"));
            Assert.IsTrue(otherEquipmentOption.Enabled);
            Assert.IsTrue(otherEquipmentOption.Displayed);
            Assert.AreEqual(otherEquipmentOption.Text, "Other Equipment");
            Assert.AreEqual(otherEquipmentOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(otherEquipmentOption.GetAttribute("custom-data"), "Other Equipment");
            Assert.AreEqual(otherEquipmentOption.GetAttribute("target"), "_self");
            Assert.AreEqual(otherEquipmentOption.GetAttribute("href"), 
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/OtherEquipmentDetails");
        }

        [Test]
        public void OtherEquipmentPage_OpenPage()
        {
            driver.Navigate().
                GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/OtherEquipmentDetails");
        }

        [Test]
        public void ReportBuilderPage_HiHostReviewerTest()
        {
            // Open Report Builder Page
            OtherEquipmentPage_OpenPage();

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
        public void OtherEquipmentPage_LogoutBtn()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

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
        public void OtherEquipmentPage_ModalTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

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
        public void OtherEquipmentPage_NotificationTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void OtherEquipmentPage_PageTitleTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Other Equipment Details");
        }

        [Test]
        public void OtherEquipmentPage_ReportsNavigationLinkTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

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
        public void OtherEquipmentPage_EquipmentNavigationLinkTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var equipmentBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            Assert.IsTrue(equipmentBtn.Displayed);
            Assert.IsTrue(equipmentBtn.Enabled);
            Assert.AreEqual(equipmentBtn.Text, "Equipment");
        }

        [Test]
        public void OtherEquipmentPage_OtherEquipmentDetailsNavigationLinkTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var OtherEquipmentDetailsBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            Assert.IsTrue(OtherEquipmentDetailsBtn.Displayed);
            Assert.IsTrue(OtherEquipmentDetailsBtn.Enabled);
            Assert.AreEqual(OtherEquipmentDetailsBtn.Text, "Other Equipment Details");
        }

        [Test]
        public void OtherEquipmentPage_AssetClassTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var assetClassLabel = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/div/div/label"));
            Assert.IsTrue(assetClassLabel.Enabled);
            Assert.IsTrue(assetClassLabel.Displayed);
            Assert.AreEqual(assetClassLabel.Text, "Asset Class");
            Assert.AreEqual(assetClassLabel.GetAttribute("for"), "AssetClass");

            var equipmentHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/div/div/b"));
            Assert.IsTrue(equipmentHeader.Enabled);
            Assert.IsTrue(equipmentHeader.Displayed);
            Assert.IsTrue(equipmentHeader.Text.Equals("Equipment"));
        }

        [Test]
        public void OtherEquipmentPage_AssetSubclassTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var assetSubclassLabel = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/label"));
            Assert.IsTrue(assetSubclassLabel.Enabled);
            Assert.IsTrue(assetSubclassLabel.Displayed);
            Assert.AreEqual(assetSubclassLabel.Text, "Asset Subclass");
            Assert.AreEqual(assetSubclassLabel.GetAttribute("for"), "AssetSubClass");

            var OtherEquipmentHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/b"));
            Assert.IsTrue(OtherEquipmentHeader.Enabled);
            Assert.IsTrue(OtherEquipmentHeader.Displayed);
            Assert.IsTrue(OtherEquipmentHeader.Text.Equals("Other Equipment"));
        }


        // this label must be for asset type but is linked with  asset subclass
        [Test]
        public void OtherEquipmentPage_AssetTypeTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var assetType = driver.FindElement
           (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[3]/div/div/label"));
            Assert.IsTrue(assetType.Enabled);
            Assert.IsTrue(assetType.Displayed);
            Assert.AreEqual(assetType.Text, "Asset Type");
            Assert.AreEqual(assetType.GetAttribute("for"), "AssetType");

            var OtherEquipmentHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[3]/div/div/b"));
            Assert.IsTrue(OtherEquipmentHeader.Enabled);
            Assert.IsTrue(OtherEquipmentHeader.Displayed);
            Assert.IsTrue(OtherEquipmentHeader.Text.Equals("Equipment-Other"));
        }

        [Test]
        public void OtherEquipmentPage_ExportBtnTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var exportBtn = driver.FindElement(By.Id("ExportCSVLink"));
            Assert.IsTrue(exportBtn.Enabled);
            Assert.IsTrue(exportBtn.Displayed);
            Assert.IsTrue(exportBtn.Text.Equals("Export CSV"));
            exportBtn.Click();
        }

        [Test]
        public void OtherEquipmentPage_DataTableLengthTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

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
        public void OtherEquipmentPage_DataTableFilterTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var SearchLabel = driver.FindElement(By.Id("otherEquipmentDetails_filter"));
            Assert.IsTrue(SearchLabel.Enabled);
            Assert.IsTrue(SearchLabel.Displayed);
            Assert.AreEqual(SearchLabel.Text, "Search:");

            var searchInput = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails_filter\"]/label/input"));
            Assert.IsTrue(searchInput.Displayed);
            Assert.IsTrue(searchInput.Enabled);
            searchInput.SendKeys("test Word");
        }

        [Test]
        public void OtherEquipmentPage_ReOrderTableTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var columns = driver.FindElements(By.Id("otherEquipmentDetails"));
            foreach(var column in columns)
            {
                Assert.IsTrue(column.Enabled);
                Assert.IsTrue(column.Displayed);
            }

            var RowNo = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[1]"));
            Assert.IsTrue(RowNo.Enabled);
            Assert.IsTrue(RowNo.Displayed);
            Assert.IsTrue(RowNo.Text.Equals("Row No"));
            Assert.AreEqual(RowNo.GetAttribute("aria-sort"), "ascending");
            Assert.AreEqual(RowNo.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(RowNo.GetAttribute("aria-label"), "Row No: activate to sort column descending");
            RowNo.Click();

            var Year = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[2]"));
            Assert.IsTrue(Year.Enabled);
            Assert.IsTrue(Year.Displayed);
            Assert.IsTrue(Year.Text.Equals("Year"));
            Assert.AreEqual(Year.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Year.GetAttribute("aria-label"), "Year: activate to sort column ascending");
            Year.Click();

            var Equipment = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[3]"));
            Assert.IsTrue(Equipment.Enabled);
            Assert.IsTrue(Equipment.Displayed);
            Assert.IsTrue(Equipment.Text.Equals("Equipment #"));
            Assert.AreEqual(Equipment.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Equipment.GetAttribute("aria-label"), "Equipment #: activate to sort column ascending");
            Equipment.Click();

            var Make = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[4]"));
            Assert.IsTrue(Make.Enabled);
            Assert.IsTrue(Make.Displayed);
            Assert.IsTrue(Make.Text.Equals("Make"));
            Assert.AreEqual(Make.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Make.GetAttribute("aria-label"), "Make: activate to sort column ascending");
            Make.Click();

            var Model = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[5]"));
            Assert.IsTrue(Model.Enabled);
            Assert.IsTrue(Model.Displayed);
            Assert.IsTrue(Model.Text.Equals("Model"));
            Assert.AreEqual(Model.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Model.GetAttribute("aria-label"), "Model: activate to sort column ascending");
            Model.Click();

            var Type = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[6]"));
            Assert.IsTrue(Type.Enabled);
            Assert.IsTrue(Type.Displayed);
            Assert.IsTrue(Type.Text.Equals("Type"));
            Assert.AreEqual(Type.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Type.GetAttribute("aria-label"), "Type: activate to sort column ascending");
            Type.Click();

            var Vin = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[7]"));
            Assert.IsTrue(Vin.Enabled);
            Assert.IsTrue(Vin.Displayed);
            Assert.IsTrue(Vin.Text.Equals("Vin #"));
            Assert.AreEqual(Vin.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Vin.GetAttribute("aria-label"), "Vin #: activate to sort column ascending");
            Vin.Click();

            var Reg = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[8]"));
            Assert.IsTrue(Reg.Enabled);
            Assert.IsTrue(Reg.Displayed);
            Assert.IsTrue(Reg.Text.Equals("Reg"));
            Assert.AreEqual(Reg.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Reg.GetAttribute("aria-label"), "Reg: activate to sort column ascending");
            Reg.Click();

            var ServiceType = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[9]"));
            Assert.IsTrue(ServiceType.Enabled);
            Assert.IsTrue(ServiceType.Displayed);
            Assert.IsTrue(ServiceType.Text.Equals("Service Type"));
            Assert.AreEqual(ServiceType.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(ServiceType.GetAttribute("aria-label"), "Service Type: activate to sort column ascending");
            ServiceType.Click();

            var UsefulLife = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[10]"));
            Assert.IsTrue(UsefulLife.Enabled);
            Assert.IsTrue(UsefulLife.Displayed);
            Assert.IsTrue(UsefulLife.Text.Equals("Useful Life"));
            Assert.AreEqual(UsefulLife.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(UsefulLife.GetAttribute("aria-label"), "Useful Life: activate to sort column ascending");
            UsefulLife.Click();

            var Grant = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[11]"));
            Assert.IsTrue(Grant.Enabled);
            Assert.IsTrue(Grant.Displayed);
            Assert.IsTrue(Grant.Text.Equals("Grant #"));
            Assert.AreEqual(Grant.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Grant.GetAttribute("aria-label"), "Grant #: activate to sort column ascending");
            Grant.Click();

            var PurchasePrice = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[12]"));
            Assert.IsTrue(PurchasePrice.Enabled);
            Assert.IsTrue(PurchasePrice.Displayed);
            Assert.IsTrue(PurchasePrice.Text.Equals("Purchase Price"));
            Assert.AreEqual(PurchasePrice.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(PurchasePrice.GetAttribute("aria-label"), "Purchase Price: activate to sort column ascending");
            PurchasePrice.Click();

            var DeliveryDate = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[13]"));
            Assert.IsTrue(DeliveryDate.Enabled);
            Assert.IsTrue(DeliveryDate.Displayed);
            Assert.IsTrue(DeliveryDate.Text.Equals("Delivery Date"));
            Assert.AreEqual(DeliveryDate.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(DeliveryDate.GetAttribute("aria-label"), "Delivery Date: activate to sort column ascending");
            DeliveryDate.Click();

            var AcceptanceServiceDate = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[14]"));
            Assert.IsTrue(AcceptanceServiceDate.Enabled);
            Assert.IsTrue(AcceptanceServiceDate.Displayed);
            Assert.IsTrue(AcceptanceServiceDate.Text.Equals("Acceptance /Service Date"));
            Assert.AreEqual(AcceptanceServiceDate.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(AcceptanceServiceDate.GetAttribute("aria-label"), "Acceptance /Service Date: activate to sort column ascending");
            AcceptanceServiceDate.Click();

            var ReplacesVIN = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[15]"));
            Assert.IsTrue(ReplacesVIN.Enabled);
            Assert.IsTrue(ReplacesVIN.Displayed);
            Assert.IsTrue(ReplacesVIN.Text.Equals("Replaces VIN #"));
            Assert.AreEqual(ReplacesVIN.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(ReplacesVIN.GetAttribute("aria-label"), "Replaces VIN #: activate to sort column ascending");
            ReplacesVIN.Click();

            var VehicleLeasedTo = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[16]"));
            Assert.IsTrue(VehicleLeasedTo.Enabled);
            Assert.IsTrue(VehicleLeasedTo.Displayed);
            Assert.IsTrue(VehicleLeasedTo.Text.Equals("Vehicle Leased To"));
            Assert.AreEqual(VehicleLeasedTo.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(VehicleLeasedTo.GetAttribute("aria-label"), "Vehicle Leased To: activate to sort column ascending");
            VehicleLeasedTo.Click();

            var Comments = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[17]"));
            Assert.IsTrue(Comments.Enabled);
            Assert.IsTrue(Comments.Displayed);
            Assert.IsTrue(Comments.Text.Equals("Comments"));
            Assert.AreEqual(Comments.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(Comments.GetAttribute("aria-label"), "Comments: activate to sort column ascending");
            Comments.Click();

            var ColorCode1 = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[18]"));
            Assert.IsTrue(ColorCode1.Enabled);
            Assert.IsTrue(ColorCode1.Displayed);
            Assert.IsTrue(ColorCode1.Text.Equals("Color Code 1"));
            Assert.AreEqual(ColorCode1.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(ColorCode1.GetAttribute("aria-label"), "Color Code 1: activate to sort column ascending");
            ColorCode1.Click();

            var colorCode2 = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[19]"));
            Assert.IsTrue(colorCode2.Enabled);
            Assert.IsTrue(colorCode2.Displayed);
            Assert.IsTrue(colorCode2.Text.Equals("Color Code 2"));
            Assert.AreEqual(colorCode2.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(colorCode2.GetAttribute("aria-label"), "Color Code 2: activate to sort column ascending");
            colorCode2.Click();

            var colorCode3 = driver.FindElement(By.XPath("//*[@id=\"otherEquipmentDetails\"]/thead/tr/th[20]"));
            Assert.IsTrue(colorCode3.Enabled);
            Assert.IsTrue(colorCode3.Displayed);
            Assert.IsTrue(colorCode3.Text.Equals("Color Code 3"));
            Assert.AreEqual(colorCode3.GetAttribute("aria-controls"), "otherEquipmentDetails");
            Assert.AreEqual(colorCode3.GetAttribute("aria-label"), "Color Code 3: activate to sort column ascending");
            colorCode3.Click();
        }

        [Test]
        public void OtherEquiomentPage_PaginateTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("otherEquipmentDetails_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("otherEquipmentDetails_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("otherEquipmentDetails_paginate"));
            foreach (var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
            }
        }

        [Test]
        public void ReportBuilderPage_FooterCopyrightTest()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void OtherEquipmentPage_MinimizeToggle()
        {
            // Open Other Equipment Page
            OtherEquipmentPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}