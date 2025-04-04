using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;
using System.Xml.Serialization;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium.DevTools.V130.CSS;

namespace Reviewer_Test
{
    public class ReviwerReviewEditsTests : IDisposable
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
        public void ViewEditPage_ViewEditOptionTest()
        {
            var viewEditOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[4]/a"));

            Assert.IsTrue(viewEditOption.Enabled);
            Assert.IsTrue(viewEditOption.Displayed);
            Assert.AreEqual(viewEditOption.Text, "View/Edit");
            Assert.AreEqual(viewEditOption.GetAttribute("custom-data"), "View/Edit");
            Assert.AreEqual(viewEditOption.GetAttribute("aria-expanded"), "false");
        }

        [Test]
        public void ViewEditPage_OpenPage()
        {
            driver.Navigate().GoToUrl("http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Assets/AssetClass");
        }

        [Test]
        public void ViewEditPage_HiHostOperatorTest()
        {
            // to open View Edit page 
            ViewEditPage_OpenPage();

            var hiHostOperator = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            Assert.AreEqual(hiHostOperator.Text, "HI,");
            Assert.True(hiHostOperator.Displayed);
            Assert.True(hiHostOperator.Enabled);

            var username = driver.FindElement(By.Id("UserName"));
            Assert.True(username.Displayed);
            Assert.True(username.Enabled);
        }

        [Test]
        public void ViewEditPage_LogoutBtn()
        {
            // to open View Edit page 
            ViewEditPage_OpenPage();

            var hiHostOperator = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            hiHostOperator.Click();

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
        public void ViewEditPage_ModalTest()
        {
            // to open View Edit page 
            ViewEditPage_OpenPage();

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
        public void ViewEditPage_NotificationTest()
        {
            // to open View Edit page 
            ViewEditPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void ViewEditPage_PageTitleTest()
        {
            // to open View Edit page 
            ViewEditPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "View/Edit");
        }

        [Test]
        public void ViewEditPage_DashboardNavigationLinkTest()
        {
            // Open View Edit Page
            ViewEditPage_OpenPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a"));
            Assert.IsTrue(dashboardBtn.Displayed);
            Assert.IsTrue(dashboardBtn.Enabled);
            Assert.AreEqual(dashboardBtn.Text,"Dashboard");

            var UrlBeforeClick = driver.Url;
            dashboardBtn.Click();
            var UrlAfterClick = driver.Url;
            Assert.AreNotEqual(UrlBeforeClick,UrlAfterClick);
        }

        [Test]
        public void ViewEditPage_ViewEditNavigationLinkTest()
        {
            // Open View Edit Page
            ViewEditPage_OpenPage();

            var viewEditBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            Assert.IsTrue(viewEditBtn.Displayed);
            Assert.IsTrue(viewEditBtn.Enabled);
            Assert.AreEqual(viewEditBtn.Text, "View/Edit");
        }

        [Test]
        public void ViewEditPage_AssetSubclassDropdownlist()
        {
            // Open View Edit Page
            ViewEditPage_OpenPage();

            var AssetSubclassLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div[1]/div[1]/div[1]/div/div/label"));
            Assert.IsTrue(AssetSubclassLabel.Displayed);
            Assert.IsTrue(AssetSubclassLabel.Enabled);
            Assert.AreEqual(AssetSubclassLabel.Text,"Asset Subclass");

            var AssetSubclassInput = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            Assert.IsTrue(AssetSubclassInput.Displayed);
            Assert.IsTrue(AssetSubclassInput.Enabled);
            var SelectedAssetSubclass = new SelectElement(AssetSubclassInput);
            SelectedAssetSubclass.SelectByIndex(0);
        }

        [Test]
        public void ViewEditPage_AssetTypeDropdownlistTest()
        {
            // open edit view page
            ViewEditPage_OpenPage();

            var AssetTypeLabel = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div/div/label"));
            Assert.IsTrue(AssetTypeLabel.Displayed);
            Assert.IsTrue(AssetTypeLabel.Enabled);
            Assert.AreEqual(AssetTypeLabel.Text, "Asset Type");

            var AssetTypeInput = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            Assert.IsTrue(AssetTypeInput.Displayed);
            Assert.IsTrue(AssetTypeInput.Enabled);
            var SelectedAssetType = new SelectElement(AssetTypeInput);
            SelectedAssetType.SelectByIndex(0);
        }

        [Test]
        public void ViewEditPage_ConfigurationBtnTest()
        {
            // open edit view page
            ViewEditPage_OpenPage();

            var configBtn = driver.FindElement(By.Id("configBtn"));
            Assert.IsFalse(configBtn.Enabled);
            Assert.IsTrue(configBtn.Displayed);
            Assert.AreEqual(configBtn.Text,"Configuration");
        }

        [Test]
        public void ViewEditPage_DataTableLengthTest()
        {
            // open View Edit Page
            ViewEditPage_OpenPage();

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
        public void ViewEditPage_DataTableFilterTest()
        {
            // Open View Edit Page 
            ViewEditPage_OpenPage();

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
        public void ViewEditPage_ReOrderTableTest()
        {
            // open view Edit Page
            ViewEditPage_OpenPage();

            var columns = driver.FindElements(By.Id("AssetsTable"));
            foreach(var column in columns)
            {
                Assert.IsTrue(column.Displayed);
                Assert.IsTrue(column.Enabled);
            }

            var AssetId = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[1]"));
            Assert.IsTrue(AssetId.Displayed);
            Assert.IsTrue(AssetId.Enabled);
            Assert.AreEqual(AssetId.Text,"Asset ID");
            Assert.AreEqual(AssetId.GetAttribute("aria-controls"), "AssetsTable");
            //AssetId.Click();

            var AssetClass = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[2]"));
            Assert.IsTrue(AssetClass.Displayed);
            Assert.IsTrue(AssetClass.Enabled);
            Assert.AreEqual(AssetClass.Text, "Asset Class");
            Assert.AreEqual(AssetClass.GetAttribute("aria-controls"), "AssetsTable");
            //AssetClass.Click();

            var AssetSubClass = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[3]"));
            Assert.IsTrue(AssetSubClass.Displayed);
            Assert.IsTrue(AssetSubClass.Enabled);
            Assert.AreEqual(AssetSubClass.Text, "Asset Subclass");
            Assert.AreEqual(AssetSubClass.GetAttribute("aria-controls"), "AssetsTable");
            //AssetSubClass.Click();

            var AssetType = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[4]"));
            Assert.IsTrue(AssetType.Displayed);
            Assert.IsTrue(AssetType.Enabled);
            Assert.AreEqual(AssetType.Text, "Asset Type");
            Assert.AreEqual(AssetType.GetAttribute("aria-controls"), "AssetsTable");
            //AssetType.Click();

            var Operator = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[5]"));
            Assert.IsTrue(Operator.Displayed);
            Assert.IsTrue(Operator.Enabled);
            Assert.AreEqual(Operator.Text, "Operator");
            Assert.AreEqual(Operator.GetAttribute("aria-controls"), "AssetsTable");
            //Operator.Click();

            var Asset = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[6]"));
            Assert.IsTrue(Asset.Displayed);
            Assert.IsTrue(Asset.Enabled);
            Assert.AreEqual(Asset.Text, "Asset");
            Assert.AreEqual(Asset.GetAttribute("aria-controls"), "AssetsTable");
            //Asset.Click();

            var State = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[7]"));
            Assert.IsTrue(State.Displayed);
            Assert.IsTrue(State.Enabled);
            Assert.AreEqual(State.Text, "State");
            Assert.AreEqual(State.GetAttribute("aria-controls"), "AssetsTable");
            //State.Click();

            var CreatedTime = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[8]"));
            Assert.IsTrue(CreatedTime.Displayed);
            Assert.IsTrue(CreatedTime.Enabled);
            Assert.AreEqual(CreatedTime.Text, "Created Time");
            Assert.AreEqual(CreatedTime.GetAttribute("aria-controls"), "AssetsTable");
            //CreatedTime.Click();

            var Actions = driver.FindElement(By.XPath("//*[@id=\"AssetsTable\"]/thead/tr/th[9]"));
            Assert.IsTrue(Actions.Displayed);
            Assert.IsTrue(Actions.Enabled);
            Assert.AreEqual(Actions.Text, "Actions");
            Assert.AreEqual(Actions.GetAttribute("aria-controls"), "AssetsTable");
            //Actions.Click();
        }

        [Test]
        public void ViewEditPage_FooterCopyrightTest()
        {
            // to open View Edit page
            ViewEditPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void ViewEditPage_MinimizeToggle()
        {
            // to open ViewEdit page
            ViewEditPage_OpenPage();
            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}