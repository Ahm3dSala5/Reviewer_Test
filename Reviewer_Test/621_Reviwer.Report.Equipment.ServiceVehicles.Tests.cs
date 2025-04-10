using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerReportEquipmentServiceVehiclesTests :IDisposable
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
        public void ServiceVehcilcePage_ReportsOptionTest()
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
        public void ServiceVehiclesPage_EquipmentOptionTest()
        {
            var ReportsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var equipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/a"));
            Assert.IsTrue(equipmentOption.Enabled);
            Assert.IsTrue(equipmentOption.Displayed);
            Assert.IsTrue(equipmentOption.Text.Equals("Equipment"));
            Assert.AreEqual(equipmentOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(equipmentOption.GetAttribute("custom-data"), "Equipment");
            Assert.AreEqual(equipmentOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void ServivceVehcilesPage_OtherEquipmentOptionTest()
        {
            var ReportsOption = driver.FindElement
             (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a"));
            ReportsOption.Click();

            var equipmentOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/a"));
            equipmentOption.Click();

            var serviceVehiclesOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/nav/ul/li[3]/nav/ul/li[2]/a"));
            Assert.IsTrue(serviceVehiclesOption.Enabled);
            Assert.IsTrue(serviceVehiclesOption.Displayed);
            Assert.IsTrue(serviceVehiclesOption.Text.Equals("Service Vehicles"));
            Assert.IsTrue(serviceVehiclesOption.GetAttribute("custom-data").Equals("Service Vehicles"));
            Assert.AreEqual(serviceVehiclesOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(serviceVehiclesOption.GetAttribute("target"), "_self");
            Assert.AreEqual(serviceVehiclesOption.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/ServiceVehiclesDetails");
        }

        [Test]
        public void ServiceVehiclesPage_OpenPage()
        {
            driver.Navigate().
                GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Report/ServiceVehiclesDetails");
        }

        [Test]
        public void ServiceVehiclesPage_HiHostReviewerTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

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
        public void ServiceVehiclesPage_LogoutBtn()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            ServiceVehiclesPage_OpenPage();

            var hiReviewer = driver.FindElement(By.Id("UserName"));
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
        public void ServiceVehiclesPage_ModalTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

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
        public void ServiceVehcilesPage_NotificationTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void ServiceVehiclesPage_PageTitleTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Service Vehicles Details");
        }

        [Test]
        public void ServiceVehiclesPage_ReportsNavigationLinkTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

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
        public void ServiceVehiclesPage_EquipmentNavigationLinkTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var equipmentBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            Assert.IsTrue(equipmentBtn.Displayed);
            Assert.IsTrue(equipmentBtn.Enabled);
            Assert.AreEqual(equipmentBtn.Text, "Equipment");
        }

        [Test]
        public void ServiceVehiclesPage_ServiceVehiclesDetailsNavigationLinkTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var serviceVehiclesDetailsBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[5]/a/span"));
            Assert.IsTrue(serviceVehiclesDetailsBtn.Displayed);
            Assert.IsTrue(serviceVehiclesDetailsBtn.Enabled);
            Assert.AreEqual(serviceVehiclesDetailsBtn.Text, "Service Vehicles Details");
        }

        [Test]
        public void OtherEquipmentPage_AssetClassTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

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
        public void ServiceVehiclesPage_AssetSubclassTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var assetSubclassLabel = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/label"));
            Assert.IsTrue(assetSubclassLabel.Enabled);
            Assert.IsTrue(assetSubclassLabel.Displayed);
            Assert.AreEqual(assetSubclassLabel.Text, "Asset Subclass");
            Assert.AreEqual(assetSubclassLabel.GetAttribute("for"), "AssetSubClass");

            var serviceVehiclesHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/b"));
            Assert.IsTrue(serviceVehiclesHeader.Enabled);
            Assert.IsTrue(serviceVehiclesHeader.Displayed);
            Assert.IsTrue(serviceVehiclesHeader.Text.Equals("Service Vehicles"));
        }


        // Asset Type label must be for asset type but is linked with  asset subclass
        [Test]
        public void ServiceVehcilesPage_AssetTypeTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var assetTypeLabel = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[3]/div/div/label"));
            Assert.IsTrue(assetTypeLabel.Enabled);
            Assert.IsTrue(assetTypeLabel.Displayed);
            Assert.AreEqual(assetTypeLabel.Text, "Asset Type");
            Assert.AreEqual(assetTypeLabel.GetAttribute("for"), "AssetType");

            var assetTypeDropdownlist = driver.FindElement
                (By.Id("AssetTypeDropDownChange"));
            Assert.IsTrue(assetTypeDropdownlist.Enabled);
            Assert.IsTrue(assetTypeDropdownlist.Displayed);

            var selectedAssetType = new SelectElement(assetTypeDropdownlist);
            selectedAssetType.SelectByIndex(0);

            var defualtOption = driver.FindElement
                (By.XPath("//*[@id=\"AssetTypeDropDownChange\"]/option[1]"));
            Assert.IsTrue(defualtOption.Enabled);
            Assert.IsTrue(defualtOption.Displayed);
            Assert.IsTrue(defualtOption.Text.Equals("Select Asset Type"));
        }

        [Test]
        public void ServiceVehcilesPage_DataTableLengthTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var ShowLabel = driver.FindElement
                (By.XPath("//*[@id=\"serviceVehiclesDetails_length\"]/label"));
            Assert.IsTrue(ShowLabel.Enabled);
            Assert.IsTrue(ShowLabel.Displayed);
            Assert.IsTrue(ShowLabel.Text.Contains("Show"));

            var TableLength = driver.FindElement(By.Name("serviceVehiclesDetails_length"));
            Assert.IsTrue(TableLength.Enabled);
            Assert.IsTrue(TableLength.Displayed);
            var selectedTableLength = new SelectElement(TableLength);
            selectedTableLength.SelectByIndex(1);
        }

        [Test]
        public void ServiceVehcilesPage_DataTableFilterTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var SearchLabel = driver.FindElement(By.Id("serviceVehiclesDetails_filter"));
            Assert.IsTrue(SearchLabel.Enabled);
            Assert.IsTrue(SearchLabel.Displayed);
            Assert.AreEqual(SearchLabel.Text, "Search:");

            var searchInput = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails_filter\"]/label/input"));
            Assert.IsTrue(searchInput.Displayed);
            Assert.IsTrue(searchInput.Enabled);
            searchInput.SendKeys("test Word");
        }

        [Test]
        public void ServiceVehcilesPage_ReOrderTableTest()
        {
            // Open Other Equipment Page
            ServiceVehiclesPage_OpenPage();

            var columns = driver.FindElements(By.Id("serviceVehiclesDetails"));
            foreach(var column in columns)
            {
                Assert.IsTrue(column.Enabled);
                Assert.IsTrue(column.Displayed);
            }

            var RowNo = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[1]"));
            Assert.IsTrue(RowNo.Enabled);
            Assert.IsTrue(RowNo.Displayed);
            Assert.IsTrue(RowNo.Text.Equals("Row No"));
            Assert.AreEqual(RowNo.GetAttribute("aria-sort"), "ascending");
            Assert.AreEqual(RowNo.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(RowNo.GetAttribute("aria-label"), "Row No: activate to sort column descending");
            RowNo.Click();

            var Year = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[2]"));
            Assert.IsTrue(Year.Enabled);
            Assert.IsTrue(Year.Displayed);
            Assert.IsTrue(Year.Text.Equals("Year"));
            Assert.AreEqual(Year.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Year.GetAttribute("aria-label"), "Year: activate to sort column ascending");
            Year.Click();

            var Veh = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[3]"));
            Assert.IsTrue(Veh.Enabled);
            Assert.IsTrue(Veh.Displayed);
            Assert.IsTrue(Veh.Text.Equals("Veh #"));
            Assert.AreEqual(Veh.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Veh.GetAttribute("aria-label"), "Veh #: activate to sort column ascending");
            Veh.Click();

            var Make = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[4]"));
            Assert.IsTrue(Make.Enabled);
            Assert.IsTrue(Make.Displayed);
            Assert.IsTrue(Make.Text.Equals("Make"));
            Assert.AreEqual(Make.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Make.GetAttribute("aria-label"), "Make: activate to sort column ascending");
            Make.Click();

            var Model = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[5]"));
            Assert.IsTrue(Model.Enabled);
            Assert.IsTrue(Model.Displayed);
            Assert.IsTrue(Model.Text.Equals("Model"));
            Assert.AreEqual(Model.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Model.GetAttribute("aria-label"), "Model: activate to sort column ascending");
            Model.Click();

            var Type = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[6]"));
            Assert.IsTrue(Type.Enabled);
            Assert.IsTrue(Type.Displayed);
            Assert.IsTrue(Type.Text.Equals("Type"));
            Assert.AreEqual(Type.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Type.GetAttribute("aria-label"), "Type: activate to sort column ascending");
            Type.Click();

            var SeatsWC = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[7]"));
            Assert.IsTrue(SeatsWC.Enabled);
            Assert.IsTrue(SeatsWC.Displayed);
            Assert.IsTrue(SeatsWC.Text.Equals("#Seats/WC"));
            Assert.AreEqual(SeatsWC.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(SeatsWC.GetAttribute("aria-label"), "#Seats/WC: activate to sort column ascending");
            SeatsWC.Click();

            var UsefulLife = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[8]"));
            Assert.IsTrue(UsefulLife.Enabled);
            Assert.IsTrue(UsefulLife.Displayed);
            Assert.IsTrue(UsefulLife.Text.Equals("Useful Life"));
            Assert.AreEqual(UsefulLife.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(UsefulLife.GetAttribute("aria-label"), "Useful Life: activate to sort column ascending");
            UsefulLife.Click();

            var Melieage = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[9]"));
            Assert.IsTrue(Melieage.Enabled);
            Assert.IsTrue(Melieage.Displayed);
            Assert.IsTrue(Melieage.Text.Equals("Mileage"));
            Assert.AreEqual(Melieage.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Melieage.GetAttribute("aria-label"), "Mileage: activate to sort column ascending");
            Melieage.Click();

            var DateMelieageRecorded = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[10]"));
            Assert.IsTrue(DateMelieageRecorded.Enabled);
            Assert.IsTrue(DateMelieageRecorded.Displayed);
            Assert.IsTrue(DateMelieageRecorded.Text.Equals("Date Mileage Recorded"));
            Assert.AreEqual(DateMelieageRecorded.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(DateMelieageRecorded.GetAttribute("aria-label"), "Date Mileage Recorded: activate to sort column ascending");
            DateMelieageRecorded.Click();

            var Vin = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[11]"));
            Assert.IsTrue(Vin.Enabled);
            Assert.IsTrue(Vin.Displayed);
            Assert.IsTrue(Vin.Text.Equals("Vin #"));
            Assert.AreEqual(Vin.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Vin.GetAttribute("aria-label"), "Vin #: activate to sort column ascending");
            Vin.Click();

            var Reg = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[12]"));
            Assert.IsTrue(Reg.Enabled);
            Assert.IsTrue(Reg.Displayed);
            Assert.IsTrue(Reg.Text.Equals("Reg"));
            Assert.AreEqual(Reg.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Reg.GetAttribute("aria-label"), "Reg: activate to sort column ascending");
            Reg.Click();

            var ServiceType = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[13]"));
            Assert.IsTrue(ServiceType.Enabled);
            Assert.IsTrue(ServiceType.Displayed);
            Assert.IsTrue(ServiceType.Text.Equals("Service Type"));
            Assert.AreEqual(ServiceType.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(ServiceType.GetAttribute("aria-label"), "Service Type: activate to sort column ascending");
            ServiceType.Click();

            var Grant = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[14]"));
            Assert.IsTrue(Grant.Enabled);
            Assert.IsTrue(Grant.Displayed);
            Assert.IsTrue(Grant.Text.Equals("Grant #"));
            Assert.AreEqual(Grant.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Grant.GetAttribute("aria-label"), "Grant #: activate to sort column ascending");
            Grant.Click();

            var PurchasePrice = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[15]"));
            Assert.IsTrue(PurchasePrice.Enabled);
            Assert.IsTrue(PurchasePrice.Displayed);
            Assert.IsTrue(PurchasePrice.Text.Equals("Purchase Price"));
            Assert.AreEqual(PurchasePrice.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(PurchasePrice.GetAttribute("aria-label"), "Purchase Price: activate to sort column ascending");
            PurchasePrice.Click();

            var DeliveryDate = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[16]"));
            Assert.IsTrue(DeliveryDate.Enabled);
            Assert.IsTrue(DeliveryDate.Displayed);
            Assert.IsTrue(DeliveryDate.Text.Equals("Delivery Date"));
            Assert.AreEqual(DeliveryDate.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(DeliveryDate.GetAttribute("aria-label"), "Delivery Date: activate to sort column ascending");
            DeliveryDate.Click();

            var AcceptanceServiceDate = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[17]"));
            Assert.IsTrue(AcceptanceServiceDate.Enabled);
            Assert.IsTrue(AcceptanceServiceDate.Displayed);
            Assert.IsTrue(AcceptanceServiceDate.Text.Equals("Acceptance /Service Date"));
            Assert.AreEqual(AcceptanceServiceDate.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(AcceptanceServiceDate.GetAttribute("aria-label"), "Acceptance /Service Date: activate to sort column ascending");
            AcceptanceServiceDate.Click();

            var ReplacesVIN = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[18]"));
            Assert.IsTrue(ReplacesVIN.Enabled);
            Assert.IsTrue(ReplacesVIN.Displayed);
            Assert.IsTrue(ReplacesVIN.Text.Equals("Replaces VIN #"));
            Assert.AreEqual(ReplacesVIN.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(ReplacesVIN.GetAttribute("aria-label"), "Replaces VIN #: activate to sort column ascending");
            ReplacesVIN.Click();

            var VehicleLeasedTo = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[19]"));
            Assert.IsTrue(VehicleLeasedTo.Enabled);
            Assert.IsTrue(VehicleLeasedTo.Displayed);
            Assert.IsTrue(VehicleLeasedTo.Text.Equals("Vehicle Leased To"));
            Assert.AreEqual(VehicleLeasedTo.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(VehicleLeasedTo.GetAttribute("aria-label"), "Vehicle Leased To: activate to sort column ascending");
            VehicleLeasedTo.Click();

            var Comments = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[20]"));
            Assert.IsTrue(Comments.Enabled);
            Assert.IsTrue(Comments.Displayed);
            Assert.IsTrue(Comments.Text.Equals("Comments"));
            Assert.AreEqual(Comments.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(Comments.GetAttribute("aria-label"), "Comments: activate to sort column ascending");
            Comments.Click();

            var ColorCode1 = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[21]"));
            Assert.IsTrue(ColorCode1.Enabled);
            Assert.IsTrue(ColorCode1.Displayed);
            Assert.IsTrue(ColorCode1.Text.Equals("Color Code 1"));
            Assert.AreEqual(ColorCode1.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(ColorCode1.GetAttribute("aria-label"), "Color Code 1: activate to sort column ascending");
            ColorCode1.Click();

            var colorCode2 = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[22]"));
            Assert.IsTrue(colorCode2.Enabled);
            Assert.IsTrue(colorCode2.Displayed);
            Assert.IsTrue(colorCode2.Text.Equals("Color Code 2"));
            Assert.AreEqual(colorCode2.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(colorCode2.GetAttribute("aria-label"), "Color Code 2: activate to sort column ascending");
            colorCode2.Click();

            var colorCode3 = driver.FindElement(By.XPath("//*[@id=\"serviceVehiclesDetails\"]/thead/tr/th[23]"));
            Assert.IsTrue(colorCode3.Enabled);
            Assert.IsTrue(colorCode3.Displayed);
            Assert.IsTrue(colorCode3.Text.Equals("Color Code 3"));
            Assert.AreEqual(colorCode3.GetAttribute("aria-controls"), "serviceVehiclesDetails");
            Assert.AreEqual(colorCode3.GetAttribute("aria-label"), "Color Code 3: activate to sort column ascending");
            colorCode3.Click();
        }

        [Test]
        public void ServiceVehcilesPage_ExportBtnTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var exportBtn = driver.FindElement(By.Id("ExportCSVLink"));
            Assert.IsTrue(exportBtn.Enabled);
            Assert.IsTrue(exportBtn.Displayed);
            Assert.IsTrue(exportBtn.Text.Equals("Export CSV"));
            exportBtn.Click();
        }

        [Test]
        public void ServiceVehiclesPage_PaginateTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("serviceVehiclesDetails_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("serviceVehiclesDetails_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("serviceVehiclesDetails_paginate"));
            foreach (var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
            }
        }

        [Test]
        public void ServiceVehclesPage_FooterCopyrightTest()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void ServiceVehiclesPage_MinimizeToggle()
        {
            // Open Service Vehciles Page
            ServiceVehiclesPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}