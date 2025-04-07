using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class Test : IDisposable
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
        public void CreateAssetPage_OperationsOptionTest()
        {
            var OperationsOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/a"));

            Assert.IsTrue(OperationsOption.Enabled);
            Assert.IsTrue(OperationsOption.Displayed);
            Assert.AreEqual(OperationsOption.Text, "Operations");
            Assert.AreEqual(OperationsOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(OperationsOption.GetAttribute("custom-data"), "Operations");
            Assert.AreEqual(OperationsOption.GetAttribute("href"), $"{driver.Url}#");
        }

        [Test]
        public void CreateAssetPage_CreateAssetOptionTest()
        {
            var OperationsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/a"));
            OperationsOption.Click();

            var createAssetOperation = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/nav/ul/li/a"));
            Assert.IsTrue(createAssetOperation.Enabled);
            Assert.IsTrue(createAssetOperation.Displayed);
            Assert.AreEqual(createAssetOperation.Text, "Create Asset");
            Assert.AreEqual(createAssetOperation.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(createAssetOperation.GetAttribute("custom-data"), "Create Asset");
            Assert.AreEqual(createAssetOperation.GetAttribute("target"), "_self");
            Assert.AreEqual(createAssetOperation.GetAttribute("href"), "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Assets/Create");
        }

        [Test]
        public void CreateAssetPage_OpenPage()
        {
            var OperationsOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/a"));
            OperationsOption.Click();

            var createAssetOperation = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/nav/ul/li/a"));
            createAssetOperation.Click();
        }

        [Test]
        public void CreateAssetPage_HiHostReviewerTest()
        {
            // Open Create Asset Page Page
            CreateAssetPage_OpenPage();

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
        public void CreateAssetPage_LogoutBtn()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

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
        public void CreateAssetPage_ModalTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

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
        public void CreateAssetPage_NotificationTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void CreateAssetPage_PageTitleTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Create Asset");
        }

        [Test]
        public void CreateAssetPage_DashboardNavigationLinkTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

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
        public void CreateAssetPage_ReviewEditsNavigationLinkTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var viewEditBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            Assert.IsTrue(viewEditBtn.Displayed);
            Assert.IsTrue(viewEditBtn.Enabled);
            Assert.AreEqual(viewEditBtn.Text, "Create Asset");
        }

        [Test]
        public void CreateAssetPage_AssetNameInputTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

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
        }

        [Test]
        public void CreateAssetPage_AssetDecriptionInputTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

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
        }

        [Test]
        public void CreateAssetPage_ImageFileTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var ImageLabel = driver.FindElement(By.Id("AssetCreateModal"));
            Assert.IsTrue(ImageLabel.Enabled);
            Assert.IsTrue(ImageLabel.Displayed);
            Assert.IsTrue(ImageLabel.Text.Contains("Asset Image"));

            var ImageFile = driver.FindElement(By.Id("ImagePath"));
            Assert.IsTrue(ImageFile.Enabled);
            Assert.IsTrue(ImageFile.Displayed);
            Assert.AreEqual(ImageFile.GetAttribute("type"),"file");
            Assert.AreEqual(ImageFile.GetAttribute("accept"), "image/*,.pdf");
        }

        [Test]
        public void CreateAssetPage_AssetClassDropdownlistTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            // test dropdownlist
            var AssetClassDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[4]/div/div/div/label"));
            Assert.IsTrue(AssetClassDropdownlistLabel.Enabled);
            Assert.IsTrue(AssetClassDropdownlistLabel.Displayed);
            Assert.AreEqual(AssetClassDropdownlistLabel.Text, "Asset Class");

            var AssetClassInput = driver.FindElement(By.Id("AssetClassId"));
            Assert.IsTrue(AssetClassInput.Enabled);
            Assert.IsTrue(AssetClassInput.Displayed);
            Assert.AreEqual(AssetClassInput.GetAttribute("required"), "true");
            var selectedAssetClassInput = new SelectElement(AssetClassInput);
            selectedAssetClassInput.SelectByIndex(1);
        }

        [Test]
        public void CreateAssetPage_AssetSubclassDropdownlistTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var assetSubClassDropdownlistLabel = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[5]/div/div/div/label"));
            Assert.IsTrue(assetSubClassDropdownlistLabel.Enabled);
            Assert.IsTrue(assetSubClassDropdownlistLabel.Displayed);
            Assert.AreEqual(assetSubClassDropdownlistLabel.Text, "Asset Subclass");

            var AssetSubClassInput = driver.FindElement(By.Id("AssetSubClassIdDropdown"));
            Assert.IsTrue(AssetSubClassInput.Enabled);
            Assert.IsTrue(AssetSubClassInput.Displayed);
            Assert.AreEqual(AssetSubClassInput.GetAttribute("required"), "true");
        }

        [Test]
        public void CreateAssetPage_AssetTypeDropdownlistTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var assetTypeDropdownlistLabel = driver.FindElement
              (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[6]/div/div/div/label"));
            Assert.IsTrue(assetTypeDropdownlistLabel.Enabled);
            Assert.IsTrue(assetTypeDropdownlistLabel.Displayed);
            Assert.AreEqual(assetTypeDropdownlistLabel.Text, "Asset Type");

            var AssetTypeInput = driver.FindElement(By.Id("AssetTypeIdDropdown"));
            Assert.IsTrue(AssetTypeInput.Enabled);
            Assert.IsTrue(AssetTypeInput.Displayed);
            Assert.AreEqual(AssetTypeInput.GetAttribute("required"), "true");
        }

        [Test]
        public void CreateAssetPage_AssociatedAttibutesHeaderTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var header = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[7]/div[1]/div/div/h3"));
            Assert.IsTrue(header.Enabled);
            Assert.IsTrue(header.Displayed);
            Assert.AreEqual(header.Text, "Please Add Associate Attribute");
        }

        [Test]
        public void CreateAssetPage_AddBtnTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var addBtn = driver.FindElement(By.Id("btnAdd"));
            Assert.IsTrue(addBtn.Displayed);
            Assert.IsTrue(addBtn.Enabled);
            Assert.AreEqual(addBtn.Text, "ADD");
        }

        [Test]
        public void CreateAssetPage_SaveBtnTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var SaveBtn = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[9]/button[1]"));
            Assert.IsTrue(SaveBtn.Displayed);
            Assert.IsTrue(SaveBtn.Enabled);
            Assert.AreEqual(SaveBtn.Text, "Save");
        }

        [Test]
        public void CreateAssetPage_WhenClickOnAddAttributesBtn()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var addBtn = driver.FindElement(By.Id("btnAdd"));
            addBtn.Click();

            var SelectAttributes = driver.FindElement(By.Id("selectListItem1"));
            Assert.IsTrue(SelectAttributes.Enabled);
            Assert.IsTrue(SelectAttributes.Displayed);

            var AttributesValue = driver.FindElement(By.Id("asstibuteValueBox1"));
            Assert.IsTrue(AttributesValue.Enabled);
            Assert.IsTrue(AttributesValue.Displayed);

            var CloseIcon = driver.FindElement(By.Id("deleteButton1"));
            Assert.IsTrue(CloseIcon.Enabled);
            Assert.IsTrue(CloseIcon.Displayed);
        }

        [Test]
        public void CreateAssetPage_CancelBtnTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var cancelBtn = driver.FindElement
                (By.Id("cancle"));
            Assert.IsTrue(cancelBtn.Displayed);
            Assert.IsTrue(cancelBtn.Enabled);
            Assert.AreEqual(cancelBtn.Text, "Cancel");
        }

        [Test]
        public void CreateAssetPage_FooterCopyrightTest()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void CreateAssetPage_MinimizeToggle()
        {
            // Open Create Asset Page
            CreateAssetPage_OpenPage();

            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}