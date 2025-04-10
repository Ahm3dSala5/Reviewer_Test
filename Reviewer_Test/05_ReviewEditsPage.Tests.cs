using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviewEditsPageTests :IDisposable
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
        public void ReviewEditsPage_ReviewEditsOptionTest()
        {
            var ReviewEditsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[5]/a"));

            Assert.IsTrue(ReviewEditsOption.Enabled);
            Assert.IsTrue(ReviewEditsOption.Displayed);
            Assert.AreEqual(ReviewEditsOption.Text, "Review Edits");
            Assert.AreEqual(ReviewEditsOption.GetAttribute("target"), "_self");
            Assert.AreEqual(ReviewEditsOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(ReviewEditsOption.GetAttribute("custom-data"), "Review Edits");
            Assert.AreEqual(ReviewEditsOption.GetAttribute("href"), "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Assets/Management");
        }

        [Test]
        public void ReviewEditPage_OpenPage()
        {
            var ReviewEditsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[5]/a"));
            ReviewEditsOption.Click();
        }

        [Test]
        public void ReviewEditsPage_HiHostReviewerTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

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
        public void ReviewEditsPage_LogoutBtn()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

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
        public void ReviewEditsPage_ModalTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

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
        public void ReviewEditsPage_NotificationTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void ReviewEditsPage_PageTitleTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Review Edits");
        }

        [Test]
        public void ReviewEditsPage_DashboardNavigationLinkTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

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
        public void ReviewEditsPage_ReviewEditsNavigationLinkTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var reviewEdits = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            Assert.IsTrue(reviewEdits.Displayed);
            Assert.IsTrue(reviewEdits.Enabled);
            Assert.AreEqual(reviewEdits.Text, "Review Edits");
        }

        [Test]
        public void ReviewEditPage_AssetStateDropdownlistTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var AssetStateLabel =driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/div/div/label"));
            Assert.IsTrue(AssetStateLabel.Enabled);
            Assert.IsTrue(AssetStateLabel.Displayed);
            Assert.AreEqual(AssetStateLabel.Text,"Asset State");

            var AssetStateInput = driver.FindElement(By.Id("AssetStateIdChange"));
            Assert.IsTrue(AssetStateInput.Enabled);
            Assert.IsTrue(AssetStateInput.Displayed);

            var SelectedAssetState = new SelectElement(AssetStateInput);
            SelectedAssetState.SelectByIndex(1);
        }

        [Test]
        public void ReviewEditsPage_AssetClassDropdownlistTest()
        {
            // Open Review Edit Page
            ReviewEditPage_OpenPage();

            var AssetClassLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[2]/div/div/label"));

            Assert.IsTrue(AssetClassLabel.Enabled);
            Assert.IsTrue(AssetClassLabel.Displayed);
            Assert.AreEqual(AssetClassLabel.Text, "Asset Class");

            var AssetClassInput = driver.FindElement(By.Id("AssetClassIdChange"));
            Assert.IsTrue(AssetClassInput.Enabled);
            Assert.IsTrue(AssetClassInput.Displayed);

            var SelectedAssetClass = new SelectElement(AssetClassInput);
            SelectedAssetClass.SelectByIndex(1);
        }

        [Test]
        public void ReviewEditsPage_AssetSubClassDropdowlistTest()
        {
            // Open Review Edit Page
            ReviewEditPage_OpenPage();

            var AssetSubclassLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[3]/div/div/label"));

            Assert.IsTrue(AssetSubclassLabel.Enabled);
            Assert.IsTrue(AssetSubclassLabel.Displayed);
            Assert.AreEqual(AssetSubclassLabel.Text, "Asset Subclass");

            var AssetSubclassInput = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            Assert.IsTrue(AssetSubclassInput.Enabled);
            Assert.IsTrue(AssetSubclassInput.Displayed);

            var Option = driver.FindElement(By.XPath("//*[@id=\"AssetSubClassDropDownChange\"]/option"));
            Assert.AreEqual(Option.Text, "No Asset Subclass");
        }

        [Test]
        public void ReviewEditsPage_AssetTypeDropdownlistTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var AssetTypeLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div/div[4]/div/div/label"));

            Assert.IsTrue(AssetTypeLabel.Enabled);
            Assert.IsTrue(AssetTypeLabel.Displayed);
            Assert.AreEqual(AssetTypeLabel.Text, "Asset Type");

            var AssetTypeInput = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            Assert.IsTrue(AssetTypeInput.Enabled);
            Assert.IsTrue(AssetTypeInput.Displayed);

            var Option = driver.FindElement(By.XPath("//*[@id=\"AssetTypeDropDownChange\"]/option"));
            Assert.AreEqual(Option.Text, "No Asset Types");
        }

        [Test]
        public void ReviewEditsPage_DataTableLengthTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

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
        public void ReviewEditPage_DataTableFilterTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

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
        public void ReviewEditsPage_ApprovedBtnTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var ApprovedBtn = driver.FindElement(By.Id("btnApprovedMultiple"));
            Assert.IsFalse(ApprovedBtn.Enabled);
            Assert.IsTrue(ApprovedBtn.Displayed);

            Assert.AreEqual(ApprovedBtn.Text,"Approve");
            Assert.AreEqual(ApprovedBtn.GetAttribute("type"),"button");
            Assert.AreEqual(ApprovedBtn.GetAttribute("disabled"),"true");
        }

        [Test]
        public void ReviewEditPage_ReviewBtnTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();
            
            var ReviewBtn = driver.FindElement(By.Id("btnReviewMultiple"));
            Assert.IsFalse(ReviewBtn.Enabled);

            Assert.AreEqual(ReviewBtn.Text, "Review");
            Assert.AreEqual(ReviewBtn.GetAttribute("type"), "button");
            Assert.AreEqual(ReviewBtn.GetAttribute("disabled"), "true");
        }

        [Test]
        public void ReviewEditPage_DataTable_BtnSelectAllTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var SelectAllBtn = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[1]/label/span"));
            Assert.IsTrue(SelectAllBtn.Enabled);
            Assert.IsTrue(SelectAllBtn.Displayed);
            SelectAllBtn.Click();
        }

        [Test]
        public void ReviewEditsPage_ReOrderTableTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var columns = driver.FindElements(By.Id("AssetsTable"));
            foreach (var column in columns)
            {
                Assert.IsTrue(column.Displayed);
                Assert.IsTrue(column.Enabled);
            }

            var AssetId = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[2]"));
            Assert.IsTrue(AssetId.Displayed);
            Assert.IsTrue(AssetId.Enabled);
            Assert.AreEqual(AssetId.Text, "Asset ID");
            Assert.AreEqual(AssetId.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(AssetId.GetAttribute("aria-label"), "Asset ID: activate to sort column ascending");
            //AssetId.Click();

            var AssetClass = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[3]"));
            Assert.IsTrue(AssetClass.Displayed);
            Assert.IsTrue(AssetClass.Enabled);
            Assert.AreEqual(AssetClass.Text, "Asset Class");
            Assert.AreEqual(AssetClass.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(AssetClass.GetAttribute("aria-label"), "Asset Class: activate to sort column ascending");
            //AssetClass.Click();

            var AssetSubClass = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[4]"));
            Assert.IsTrue(AssetSubClass.Displayed);
            Assert.IsTrue(AssetSubClass.Enabled);
            Assert.AreEqual(AssetSubClass.Text, "Asset Subclass");
            Assert.AreEqual(AssetSubClass.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(AssetSubClass.GetAttribute("aria-label"), "Asset Subclass: activate to sort column ascending");
            //AssetSubClass.Click();

            var AssetType = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[5]"));
            Assert.IsTrue(AssetType.Displayed);
            Assert.IsTrue(AssetType.Enabled);
            Assert.AreEqual(AssetType.Text, "Asset Type");
            Assert.AreEqual(AssetType.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(AssetType.GetAttribute("aria-label"), "Asset Type: activate to sort column ascending");
            //AssetType.Click();

            var Asset = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[6]"));
            Assert.IsTrue(Asset.Displayed);
            Assert.IsTrue(Asset.Enabled);
            Assert.AreEqual(Asset.Text, "Asset");
            Assert.AreEqual(Asset.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(Asset.GetAttribute("aria-label"), "Asset: activate to sort column ascending");

            //Asset.Click();
            var State = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[7]"));
            Assert.IsTrue(State.Displayed);
            Assert.IsTrue(State.Enabled);
            Assert.AreEqual(State.Text, "State");
            Assert.AreEqual(State.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(State.GetAttribute("aria-label"), "State: activate to sort column ascending");
            //State.Click();

            var CreatedTime = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[8]"));
            Assert.IsTrue(CreatedTime.Displayed);
            Assert.IsTrue(CreatedTime.Enabled);
            Assert.AreEqual(CreatedTime.Text, "Created Time");
            Assert.AreEqual(CreatedTime.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(CreatedTime.GetAttribute("aria-label"), "Created Time: activate to sort column ascending");
            //CreatedTime.Click();

            var Actions = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[9]"));
            Assert.IsTrue(Actions.Displayed);
            Assert.IsTrue(Actions.Enabled);
            Assert.AreEqual(Actions.Text, "Actions");
            Assert.AreEqual(Actions.GetAttribute("aria-controls"), "AssetsTable");
            Assert.AreEqual(Actions.GetAttribute("aria-label"), "Actions");
            //Actions.Click();
        }

        [Test]
        public void ReviewEditsPage_EditIconTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var editIcon = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[1]"));
            Assert.IsTrue(editIcon.Displayed);
            Assert.IsTrue(editIcon.Enabled);
            Assert.AreEqual(editIcon.GetAttribute("title"),"Edit");
            Assert.IsTrue(editIcon.GetAttribute("onclick").Contains("editAsset"));

            var editStyle = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[1]/i"));
            Assert.AreEqual(editStyle.GetAttribute("style"), "cursor: pointer;");
        }

        [Test]
        public void ReviewEditsPage_EditFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var editIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[1]"));
            editIcon.Click();

            var assetNameLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[1]/div/div/div/label"));
            Assert.IsTrue(assetNameLabel.Enabled);
            Assert.IsTrue(assetNameLabel.Displayed);
            Assert.AreEqual(assetNameLabel.Text,"Asset Name");

            var assetNameInput = driver.FindElement(By.Id("AssetDes"));
            Assert.IsTrue(assetNameInput.Enabled);
            Assert.IsTrue(assetNameInput.Displayed);
            Assert.AreEqual(assetNameInput.GetAttribute("type"), "text");
            Assert.AreEqual(assetNameInput.GetAttribute("required"), "true");
            Assert.AreEqual(assetNameInput.GetAttribute("data-val-length-max"), "500");
            Assert.AreEqual(assetNameInput.GetAttribute("data-val-length"), "The field AssetDesc must be a string with a maximum length of 500.");


            var assetDescriptionLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[2]/div/div/div/label"));
            Assert.IsTrue(assetDescriptionLabel.Enabled);
            Assert.IsTrue(assetDescriptionLabel.Displayed);
            Assert.AreEqual(assetDescriptionLabel.Text, "Asset Description");

            var assetDecriptionInput = driver.FindElement(By.Id("AssetTypesDesc"));
            Assert.IsTrue(assetDecriptionInput.Enabled);
            Assert.IsTrue(assetDecriptionInput.Displayed);
            Assert.AreEqual(assetDecriptionInput.GetAttribute("maxlength"), "255");
            Assert.AreEqual(assetDecriptionInput.GetAttribute("data-val-required"), "The AssetNote field is required.");
            Assert.AreEqual(assetDecriptionInput.GetAttribute("data-val-length"), "The field AssetNote must be a string with a maximum length of 255.");

            var ImageLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[3]/div/div/div/label"));
            Assert.IsTrue(ImageLabel.Enabled);
            Assert.IsTrue(ImageLabel.Displayed);
            Assert.AreEqual(ImageLabel.Text, "Current Asset Image");

            // test dropdownlist
            var AssetClassDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[5]/div/div/div/label"));
            Assert.IsTrue(AssetClassDropdownlistLabel.Enabled);
            Assert.IsTrue(AssetClassDropdownlistLabel.Displayed);
            Assert.AreEqual(AssetClassDropdownlistLabel.Text, "Asset Class");

            var AssetClassInput = driver.FindElement(By.Id("AssetClassId"));
            Assert.IsTrue(AssetClassInput.Enabled);
            Assert.IsTrue(AssetClassInput.Displayed);
            Assert.AreEqual(AssetClassInput.GetAttribute("required"), "true");
            var selectedAssetClassInput = new SelectElement(AssetClassInput);
            selectedAssetClassInput.SelectByIndex(1);


            var assetSubClassDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[6]/div/div/div/label"));
            Assert.IsTrue(assetSubClassDropdownlistLabel.Enabled);
            Assert.IsTrue(assetSubClassDropdownlistLabel.Displayed);
            Assert.AreEqual(assetSubClassDropdownlistLabel.Text, "Asset Subclass");

            var AssetSubClassInput = driver.FindElement(By.Id("AssetSubClassIdDropdown"));
            Assert.IsTrue(AssetSubClassInput.Enabled);
            Assert.IsTrue(AssetSubClassInput.Displayed);
            Assert.AreEqual(AssetSubClassInput.GetAttribute("required"), "true");
            var selectedAssetSubClassInput = new SelectElement(AssetSubClassInput);
            selectedAssetSubClassInput.SelectByIndex(0);


            var assetTypeDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[7]/div/div/div/label"));
            Assert.IsTrue(assetTypeDropdownlistLabel.Enabled);
            Assert.IsTrue(assetTypeDropdownlistLabel.Displayed);
            Assert.AreEqual(assetTypeDropdownlistLabel.Text, "Asset Type");

            var AssetTypeInput = driver.FindElement(By.Id("AssetTypeIdDropdown"));
            Assert.IsTrue(AssetTypeInput.Enabled);
            Assert.IsTrue(AssetTypeInput.Displayed);
            Assert.AreEqual(AssetTypeInput.GetAttribute("required"), "true");
            var selectedAssetTypeClassInput = new SelectElement(AssetTypeInput);
            selectedAssetTypeClassInput.SelectByIndex(1);

            var commentLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[8]/div/div/div/label"));
            Assert.IsTrue(commentLabel.Displayed);
            Assert.IsTrue(commentLabel.Enabled);
            Assert.AreEqual(commentLabel.Text,"Comments");

            var Commnets = driver.FindElement(By.Id("UnapprovedReason"));
            Assert.IsTrue(Commnets.Displayed);
            Assert.IsTrue(Commnets.Enabled);
            Commnets.SendKeys("Test Comments");

            var AssociatedAttributesTitle = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[9]/div[1]/div/div/h3"));
            Assert.IsTrue(AssociatedAttributesTitle.Displayed);
            Assert.IsTrue(AssociatedAttributesTitle.Enabled);
            Assert.AreEqual(AssociatedAttributesTitle.Text, "Please Add Associate Attribute");


            var addBtn = driver.FindElement(By.Id("btnAdd"));
            Assert.IsTrue(addBtn.Displayed);
            Assert.IsTrue(addBtn.Enabled);
            Assert.AreEqual(addBtn.Text, "ADD");

            var SaveBtn = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[11]/button[1]"));
            Assert.IsTrue(SaveBtn.Displayed);
            Assert.IsTrue(SaveBtn.Enabled);
            Assert.AreEqual(SaveBtn.Text, "Save");

            var cancelBtn = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[11]/button[2]"));
            Assert.IsTrue(cancelBtn.Displayed);
            Assert.IsTrue(cancelBtn.Enabled);
            Assert.AreEqual(cancelBtn.Text, "Cancel");
        }

        [Test]
        public void ReviewEditsPage_DeleteIConTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var deleteIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[3]"));
            Assert.IsTrue(deleteIcon.Displayed);
            Assert.IsTrue(deleteIcon.Enabled);
            Assert.IsTrue(deleteIcon.GetAttribute("onclick").Contains("deleteAsset"));
            Assert.AreEqual(deleteIcon.GetAttribute("title"), "Delete");

            var deleteStyle = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[1]/i"));
            Assert.AreEqual(deleteStyle.GetAttribute("style"), "cursor: pointer;");
        }

        [Test]
        public void ReviewEditsPage_DeleteFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var deleteIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[3]"));
            deleteIcon.Click();

            var formTitle = driver.FindElement(By.Id("DeleteRequestModal"));
            Assert.IsTrue(formTitle.Enabled);
            Assert.IsTrue(formTitle.Displayed);
            Assert.IsTrue(formTitle.Text.Contains("Delete Request"));

            var requestMessage = driver.FindElement(By.Id("DeleteRequestValue"));
            Assert.IsTrue(requestMessage.Displayed);
            Assert.IsTrue(requestMessage.Enabled);
            requestMessage.SendKeys("Test");

            var saveBtn = driver.FindElement(By.Id("DeleteRequestBtn"));
            Assert.IsTrue(saveBtn.Displayed);
            Assert.IsTrue(saveBtn.Enabled);
            Assert.AreEqual(saveBtn.Text,"Save");

            var cancelBtn = driver.FindElement
                (By.XPath("//*[@id=\"DeleteRequestModal\"]/div/div/div[2]/button[2]"));
            Assert.IsTrue(cancelBtn.Displayed);
            Assert.IsTrue(cancelBtn.Enabled);
            //Assert.AreEqual(saveBtn.Text,"Cancel");
        }

        [Test]
        public void ReviewEditsPage_StatusIconTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var statusIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[4]"));
            Assert.IsTrue(statusIcon.Displayed);
            Assert.IsTrue(statusIcon.Enabled);
            Assert.IsTrue(statusIcon.GetAttribute("onclick").Contains("statusAsset"));
            Assert.AreEqual(statusIcon.GetAttribute("title"), "Status");

            var statusStyle = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[4]/i"));
            Assert.AreEqual(statusStyle.GetAttribute("style"), "cursor: pointer;");
        }

        [Test]
        public void ReviewEditsPage_StatusFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var statusIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[4]"));
            statusIcon.Click();

            var formTitle = driver.FindElement(By.XPath("//*[@id=\"StatusModal\"]/div/div/div[1]/h4"));
            Assert.IsTrue(formTitle.Enabled);
            Assert.IsTrue(formTitle.Displayed);
            Assert.AreEqual(formTitle.Text, "Asset Status");

            var State = driver.FindElement(By.XPath("//*[@id=\"StatusTable\"]/thead/tr/th[1]"));
            Assert.IsTrue(State.Enabled);
            Assert.IsTrue(State.Displayed);
            Assert.AreEqual(State.Text, "State");
            Assert.AreEqual(State.GetAttribute("aria-label"), "State");

            var Remark = driver.FindElement(By.XPath("//*[@id=\"StatusTable\"]/thead/tr/th[2]"));
            Assert.IsTrue(Remark.Enabled);
            Assert.IsTrue(Remark.Displayed);
            Assert.AreEqual(Remark.Text, "Remarks");
            Assert.AreEqual(Remark.GetAttribute("aria-label"), "Remarks");

            var CreatedBy = driver.FindElement(By.XPath("//*[@id=\"StatusTable\"]/thead/tr/th[3]"));
            Assert.IsTrue(CreatedBy.Enabled);
            Assert.IsTrue(CreatedBy.Displayed);
            Assert.AreEqual(CreatedBy.Text, "Created By");
            Assert.AreEqual(CreatedBy.GetAttribute("aria-label"), "Created By");

            var CreatedTime = driver.FindElement(By.XPath("//*[@id=\"StatusTable\"]/thead/tr/th[4]"));
            Assert.IsTrue(CreatedTime.Enabled);
            Assert.IsTrue(CreatedTime.Displayed);
            Assert.AreEqual(CreatedTime.Text, "Created Time");
            Assert.AreEqual(CreatedTime.GetAttribute("aria-label"), "Created Time");

            var View = driver.FindElement(By.XPath("//*[@id=\"StatusTable\"]/thead/tr/th[5]"));
            Assert.IsTrue(View.Enabled);
            Assert.IsTrue(View.Displayed);
            Assert.AreEqual(View.Text, "View");
            Assert.AreEqual(View.GetAttribute("aria-label"), "View: activate to sort column ascending");

            var ViewIcon = driver.FindElement(By.XPath("//*[@id=\"StatusTable\"]/tbody/tr[1]/td[5]/a/i"));
            Assert.IsTrue(ViewIcon.Enabled);
            Assert.IsTrue(ViewIcon.Displayed);
            Assert.AreEqual(ViewIcon.GetAttribute("style"), "cursor: pointer;");
            ViewIcon.Click();

            ///////////////////////// View Form /////////////////////////
            var ViewFormTitle = driver.FindElement
                (By.XPath("//*[@id=\"viewModal\"]/div/div/div[1]/h4"));
            Assert.IsTrue(ViewFormTitle.Enabled);
            Assert.IsTrue(ViewFormTitle.Displayed);
            Assert.AreEqual(ViewFormTitle.Text,"View");

            //// Asset Changes ////
            var assetChangesTableTitle = driver.FindElement(By.XPath("//*[@id=\"viewModal\"]/div/div/h4[1]"));
            Assert.IsTrue(assetChangesTableTitle.Enabled);
            Assert.IsTrue(assetChangesTableTitle.Displayed);
            Assert.AreEqual(assetChangesTableTitle.Text, "Asset Changes");

            var AssetChangesTable = driver.FindElement
                (By.XPath("//*[@id=\"viewModal\"]/div/div/div[2]/div/div/table"));
            Assert.IsNotNull(AssetChangesTable);
            Assert.IsTrue(AssetChangesTable.Enabled);
            Assert.IsTrue(AssetChangesTable.Displayed);

            //// current  version//////
            var CurrentVersionTabelTitle = driver.FindElement
                (By.XPath("//*[@id=\"viewModal\"]/div/div/div[3]/div/div/h5"));
            Assert.IsTrue(CurrentVersionTabelTitle.Enabled);
            Assert.IsTrue(CurrentVersionTabelTitle.Displayed);
            Assert.AreEqual(CurrentVersionTabelTitle.Text, "Current Version");

            var CurrentVersionAttributes = driver.FindElement
                (By.XPath("//*[@id=\"CurrentViewTable\"]/thead/tr/th[1]"));
            Assert.IsTrue(CurrentVersionAttributes.Enabled);
            Assert.IsTrue(CurrentVersionAttributes.Displayed);
            Assert.AreEqual(CurrentVersionAttributes.GetAttribute("aria-label"),"Attribute");

            var CurrentVersionValue = driver.FindElement
                (By.XPath("//*[@id=\"CurrentViewTable\"]/thead/tr/th[2]"));
            Assert.IsTrue(CurrentVersionValue.Enabled);
            Assert.IsTrue(CurrentVersionValue.Displayed);
            Assert.AreEqual(CurrentVersionValue.GetAttribute("aria-label"), "Value");

            //// Previous Version ////
            var PreviousVersionTableTitle = driver.FindElement
                (By.XPath("//*[@id=\"viewModal\"]/div/div/div[4]/div/div/h5"));
            Assert.IsTrue(PreviousVersionTableTitle.Enabled);
            Assert.IsTrue(PreviousVersionTableTitle.Displayed);
            Assert.AreEqual(PreviousVersionTableTitle.Text, "Previous Version");

            var PreviousVersionAttributes = driver.FindElement
                (By.XPath("//*[@id=\"PreviousViewTable\"]/thead/tr/th[1]"));
            Assert.IsTrue(PreviousVersionAttributes.Enabled);
            Assert.IsTrue(PreviousVersionAttributes.Displayed);
            Assert.AreEqual(PreviousVersionAttributes.GetAttribute("aria-label"), "Attribute");

            var PreviousVersionValue = driver.FindElement
                (By.XPath("//*[@id=\"PreviousViewTable\"]/thead/tr/th[2]"));
            Assert.IsTrue(PreviousVersionValue.Enabled);
            Assert.IsTrue(PreviousVersionValue.Displayed);
            Assert.AreEqual(PreviousVersionValue.GetAttribute("aria-label"), "Value");

            var BackBtn = driver.FindElement(By.Id("viewBack"));
            Assert.IsTrue(BackBtn.Enabled);
            Assert.IsTrue(BackBtn.Displayed);
            Assert.AreEqual(BackBtn.Text, "Back");

            var CancelBtn = driver.FindElement(By.XPath("//*[@id=\"viewModal\"]/div/div/div[5]/button[2]"));
            Assert.IsTrue(CancelBtn.Enabled);
            Assert.IsTrue(CancelBtn.Displayed);
            Assert.AreEqual(CancelBtn.Text, "Cancel");
        }

        [Test]
        public void ReviewEditsPage_CopyIconTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var copyIcon = driver.FindElement
              (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[2]"));
            Assert.IsTrue(copyIcon.Displayed);
            Assert.IsTrue(copyIcon.Enabled);
            Assert.AreEqual(copyIcon.GetAttribute("title"), "Copy");
            Assert.IsTrue(copyIcon.GetAttribute("onclick").Contains("copyAsset"));

            var copyStyle = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[4]/i"));
            Assert.AreEqual(copyStyle.GetAttribute("style"), "cursor: pointer;");
        }

        [Test]
        public void ReviewEditsPage_CopyFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var copyIcon = driver.FindElement
             (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[2]"));
            copyIcon.Click();

            var assetNameLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[1]/div/div/div/label"));
            Assert.IsTrue(assetNameLabel.Enabled);
            Assert.IsTrue(assetNameLabel.Displayed);
            Assert.AreEqual(assetNameLabel.Text, "Asset Name");

            var assetNameInput = driver.FindElement(By.Id("AssetDes"));
            Assert.IsTrue(assetNameInput.Enabled);
            Assert.IsTrue(assetNameInput.Displayed);
            Assert.AreEqual(assetNameInput.GetAttribute("type"), "text");
            Assert.AreEqual(assetNameInput.GetAttribute("required"), "true");
            Assert.AreEqual(assetNameInput.GetAttribute("data-val-length-max"), "500");
            Assert.AreEqual(assetNameInput.GetAttribute("data-val-length"), "The field AssetDesc must be a string with a maximum length of 500.");


            var assetDescriptionLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[2]/div/div/div/label"));
            Assert.IsTrue(assetDescriptionLabel.Enabled);
            Assert.IsTrue(assetDescriptionLabel.Displayed);
            Assert.AreEqual(assetDescriptionLabel.Text, "Asset Description");

            var assetDecriptionInput = driver.FindElement(By.Id("AssetTypesDesc"));
            Assert.IsTrue(assetDecriptionInput.Enabled);
            Assert.IsTrue(assetDecriptionInput.Displayed);
            Assert.AreEqual(assetDecriptionInput.GetAttribute("maxlength"), "255");
            Assert.AreEqual(assetDecriptionInput.GetAttribute("data-val-required"), "The AssetNote field is required.");
            Assert.AreEqual(assetDecriptionInput.GetAttribute("data-val-length"), "The field AssetNote must be a string with a maximum length of 255.");

            var ImageLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[3]/div/div/div/label"));
            Assert.IsTrue(ImageLabel.Enabled);
            Assert.IsTrue(ImageLabel.Displayed);
            Assert.AreEqual(ImageLabel.Text, "Current Asset Image");

            // test dropdownlist
            var AssetClassDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[5]/div/div/div/label"));
            Assert.IsTrue(AssetClassDropdownlistLabel.Enabled);
            Assert.IsTrue(AssetClassDropdownlistLabel.Displayed);
            Assert.AreEqual(AssetClassDropdownlistLabel.Text, "Asset Class");

            var AssetClassInput = driver.FindElement(By.Id("AssetClassId"));
            Assert.IsTrue(AssetClassInput.Enabled);
            Assert.IsTrue(AssetClassInput.Displayed);
            Assert.AreEqual(AssetClassInput.GetAttribute("required"), "true");
            var selectedAssetClassInput = new SelectElement(AssetClassInput);
            selectedAssetClassInput.SelectByIndex(1);


            var assetSubClassDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[6]/div/div/div/label"));
            Assert.IsTrue(assetSubClassDropdownlistLabel.Enabled);
            Assert.IsTrue(assetSubClassDropdownlistLabel.Displayed);
            Assert.AreEqual(assetSubClassDropdownlistLabel.Text, "Asset Subclass");

            var AssetSubClassInput = driver.FindElement(By.Id("AssetSubClassIdDropdown"));
            Assert.IsTrue(AssetSubClassInput.Enabled);
            Assert.IsTrue(AssetSubClassInput.Displayed);
            Assert.AreEqual(AssetSubClassInput.GetAttribute("required"), "true");
            var selectedAssetSubClassInput = new SelectElement(AssetSubClassInput);
            selectedAssetSubClassInput.SelectByIndex(0);


            var assetTypeDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[7]/div/div/div/label"));
            Assert.IsTrue(assetTypeDropdownlistLabel.Enabled);
            Assert.IsTrue(assetTypeDropdownlistLabel.Displayed);
            Assert.AreEqual(assetTypeDropdownlistLabel.Text, "Asset Type");

            var AssetTypeInput = driver.FindElement(By.Id("AssetTypeIdDropdown"));
            Assert.IsTrue(AssetTypeInput.Enabled);
            Assert.IsTrue(AssetTypeInput.Displayed);
            Assert.AreEqual(AssetTypeInput.GetAttribute("required"), "true");
            var selectedAssetTypeClassInput = new SelectElement(AssetTypeInput);
            selectedAssetTypeClassInput.SelectByIndex(1);

            var commentLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[8]/div/div/div/label"));
            Assert.IsTrue(commentLabel.Displayed);
            Assert.IsTrue(commentLabel.Enabled);
            Assert.AreEqual(commentLabel.Text, "Comments");

            var Commnets = driver.FindElement(By.Id("UnapprovedReason"));
            Assert.IsTrue(Commnets.Displayed);
            Assert.IsTrue(Commnets.Enabled);
            Commnets.SendKeys("Test Comments");

            var AssociatedAttributesTitle = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[9]/div[1]/div/div/h3"));
            Assert.IsTrue(AssociatedAttributesTitle.Displayed);
            Assert.IsTrue(AssociatedAttributesTitle.Enabled);
            Assert.AreEqual(AssociatedAttributesTitle.Text, "Please Add Associate Attribute");


            var addBtn = driver.FindElement(By.Id("btnAdd"));
            Assert.IsTrue(addBtn.Displayed);
            Assert.IsTrue(addBtn.Enabled);
            Assert.AreEqual(addBtn.Text, "ADD");

            var SaveBtn = driver.FindElement
                (By.Id("saveButton"));
            Assert.IsTrue(SaveBtn.Displayed);
            Assert.IsTrue(SaveBtn.Enabled);
            Assert.AreEqual(SaveBtn.Text, "Save");

            var cancelBtn = driver.FindElement
                (By.XPath("//*[@id=\"cancle\"]"));
            Assert.IsTrue(cancelBtn.Displayed);
            Assert.IsTrue(cancelBtn.Enabled);
            Assert.AreEqual(cancelBtn.Text, "Cancel");
        }

        [Test]
        public void ReviewEditPage_ReviewIconTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var reviewIcon = driver.FindElement
              (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[6]"));
            Assert.IsTrue(reviewIcon.Displayed);
            Assert.IsTrue(reviewIcon.Enabled);
            Assert.IsTrue(reviewIcon.GetAttribute("onclick").Contains("PendingRequest"));
            Assert.AreEqual(reviewIcon.GetAttribute("title"), "Review");

            var reviewStyle = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[6]/i"));
            Assert.AreEqual(reviewStyle.GetAttribute("style"), "cursor: pointer;");
        }

        [Test]
        public void ReviewEditsPage_ReviewFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var reviewIcon = driver.FindElement
              (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[5]"));
            reviewIcon.Click();

            var reviewFormTitle = driver.FindElement(By.Id("ReviewModal"));
            Assert.IsTrue(reviewFormTitle.Enabled);
            Assert.IsTrue(reviewFormTitle.Displayed);
            Assert.IsTrue(reviewFormTitle.Text.Contains("Review"));

            var reviewMessage = driver.FindElement
                (By.XPath("//*[@id=\"ReviewModal\"]"));
            Assert.IsTrue(reviewMessage.Enabled);
            Assert.IsTrue(reviewMessage.Displayed);
            reviewMessage.SendKeys("Review Test Message");

            var SaveBtn = driver.FindElement(By.Id("reviewSaveBtn"));
            Assert.IsTrue(SaveBtn.Enabled);
            Assert.IsTrue(SaveBtn.Displayed);
            Assert.AreEqual(SaveBtn.Text, "Save");
            Assert.AreEqual(SaveBtn.GetAttribute("type"), "submit");

            var CancelBtn = driver.FindElement(By.XPath("//*[@id=\"ReviewModal\"]/div/div/div[2]/button[2]"));
            Assert.IsTrue(CancelBtn.Enabled);
            Assert.IsTrue(CancelBtn.Displayed);
            Assert.AreEqual(CancelBtn.Text, "Cancel");
            Assert.AreEqual(CancelBtn.GetAttribute("type"), "button");

            var CloseIcon = driver.FindElement
                (By.XPath("//*[@id=\"ReviewModal\"]/div/div/div[1]/button"));
            Assert.IsTrue(CloseIcon.Enabled);
            Assert.IsTrue(CloseIcon.Displayed);
            Assert.AreEqual(CloseIcon.GetAttribute("type"), "button");
        }

        [Test]
        public void ReviewEditsPage_ApproveIconTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var approveIcon = driver.FindElement
              (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[5]"));
            Assert.IsTrue(approveIcon.Displayed);
            Assert.IsTrue(approveIcon.Enabled);
            Assert.IsTrue(approveIcon.GetAttribute("onclick").Contains("approvedAsset"));
            Assert.AreEqual(approveIcon.GetAttribute("title"), "Approve");

            var reviewStyle = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[5]/i"));
            Assert.AreEqual(reviewStyle.GetAttribute("style"), "cursor: pointer;");
        }

        [Test]
        public void ReviewEditsPage_ApproveFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var approveIcon = driver.FindElement
              (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[5]"));
            approveIcon.Click();

            var approveFormTitle = driver.FindElement(By.Id("ApprovedModal"));
            Assert.IsTrue(approveFormTitle.Enabled);
            Assert.IsTrue(approveFormTitle.Displayed);
            Assert.IsTrue(approveFormTitle.Text.Contains("Approve"));

            var approveMessage = driver.FindElement
                (By.XPath("//*[@id=\"ApprovedValue\"]"));
            Assert.IsTrue(approveMessage.Enabled);
            Assert.IsTrue(approveMessage.Displayed);
            approveMessage.SendKeys("Approve Test Message");

            var SaveBtn = driver.FindElement(By.Id("ApproveSaveBtn"));
            Assert.IsTrue(SaveBtn.Enabled);
            Assert.IsTrue(SaveBtn.Displayed);
            Assert.AreEqual(SaveBtn.Text,"Save");
            Assert.AreEqual(SaveBtn.GetAttribute("type"),"submit");

            var CancelBtn = driver.FindElement(By.XPath("//*[@id=\"ApprovedModal\"]/div/div/div[2]/button[2]"));
            Assert.IsTrue(CancelBtn.Enabled);
            Assert.IsTrue(CancelBtn.Displayed);
            Assert.AreEqual(CancelBtn.Text, "Cancel");
            Assert.AreEqual(CancelBtn.GetAttribute("type"),"button");

            var CloseIcon = driver.FindElement
                (By.XPath("//*[@id=\"ApprovedModal\"]/div/div/div[1]/button"));
            Assert.IsTrue(CloseIcon.Enabled);
            Assert.IsTrue(CloseIcon.Displayed);
            Assert.AreEqual(CloseIcon.GetAttribute("type"),"button");
        }

        [Test]
        public void ReviewEditsPage_RejectIconTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            // to get all deleted requests and get rejected assets
            var searchInput = driver.FindElement(By.XPath("//*[@id=\"AssetsTable_filter\"]/label/input"));
            searchInput.SendKeys("Delete");

            var RejectIcon = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[1]/td[9]/a[3]"));

            Assert.IsTrue(RejectIcon.Enabled);
            Assert.IsTrue(RejectIcon.Displayed);
            Assert.AreEqual(RejectIcon.GetAttribute("title"), "Reject");
        }

        [Test]
        public void ReviewEditsPage_RejectFormTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            // to get all deleted requests and get rejected assets
            var searchInput = driver.FindElement(By.XPath("//*[@id=\"AssetsTable_filter\"]/label/input"));
            searchInput.SendKeys("Delete");

            var RejectIcon = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[1]/td[9]/a[3]"));
            RejectIcon.Click();

            var rejectFornTitle = driver.FindElement(By.Id("RejectRequestModal"));
            Assert.IsTrue(rejectFornTitle.Enabled);
            Assert.IsTrue(rejectFornTitle.Displayed);
            Assert.IsTrue(rejectFornTitle.Text.Contains("Reject Request"));

            var rejectMessage = driver.FindElement
                (By.Id("RejectRequestValue"));
            Assert.IsTrue(rejectMessage.Enabled);
            Assert.IsTrue(rejectMessage.Displayed);
            rejectMessage.SendKeys("Reject Test Message");

            var SaveBtn = driver.FindElement(By.Id("RejectRequestBtn"));
            Assert.IsTrue(SaveBtn.Enabled);
            Assert.IsTrue(SaveBtn.Displayed);
            Assert.AreEqual(SaveBtn.Text, "Save");
            Assert.AreEqual(SaveBtn.GetAttribute("type"), "submit");

            var CancelBtn = driver.FindElement(By.XPath("//*[@id=\"RejectRequestModal\"]/div/div/div[2]/button[2]"));
            Assert.IsTrue(CancelBtn.Enabled);
            Assert.IsTrue(CancelBtn.Displayed);
            Assert.AreEqual(CancelBtn.Text, "Cancel");
            Assert.AreEqual(CancelBtn.GetAttribute("type"), "button");

            var CloseIcon = driver.FindElement
                (By.XPath("//*[@id=\"RejectRequestModal\"]/div/div/div[1]/button"));
            Assert.IsTrue(CloseIcon.Enabled);
            Assert.IsTrue(CloseIcon.Displayed);
            Assert.AreEqual(CloseIcon.GetAttribute("type"), "button");
        }

        [Test]
        public void ReviewEditsPage_PaginateTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var PreviousBtn = driver.FindElement(By.Id("AssetsTable_previous"));
            Assert.IsTrue(PreviousBtn.Enabled);
            Assert.IsTrue(PreviousBtn.Displayed);
            Assert.AreEqual(PreviousBtn.Text, "Previous");

            var NextBtn = driver.FindElement(By.Id("AssetsTable_next"));
            Assert.IsTrue(NextBtn.Enabled);
            Assert.IsTrue(NextBtn.Displayed);
            Assert.AreEqual(NextBtn.Text, "Next");

            var Pages = driver.FindElements(By.Id("AssetsTable_paginate"));
            foreach(var page in Pages)
            {
                Assert.IsTrue(page.Displayed);
                Assert.IsTrue(page.Enabled);
                page.Click();
            }
        }

        [Test]
        public void ReviewEditPage_FooterCopyrightTest()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void ReviewEditPage_MinimizeToggle()
        {
            // Open Review Edits Page
            ReviewEditPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}