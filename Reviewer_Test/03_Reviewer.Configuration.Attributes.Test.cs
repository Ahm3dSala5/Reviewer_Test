using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviewerConfigurationAttributesTest :IDisposable
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
        public void AttributesPage_ConfigurationOptionTest()
        {
            var ConfigurationOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/a"));

            Assert.IsTrue(ConfigurationOption.Enabled);
            Assert.IsTrue(ConfigurationOption.Displayed);
            Assert.AreEqual(ConfigurationOption.Text, "Configuration");
            Assert.AreEqual(ConfigurationOption.GetAttribute("custom-data"), "Configuration");
            Assert.AreEqual(ConfigurationOption.GetAttribute("aria-expanded"), "false");
        }

        [Test]
        public void AtttributesPage_AttributeOptionTest()
        {
            var ConfigurationOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/a"));
            ConfigurationOption.Click();

            var AttributesOption = driver.FindElement(By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/nav/ul/li/a"));
            Assert.IsTrue(AttributesOption.Enabled);
            Assert.IsTrue(AttributesOption.Displayed);
            Assert.AreEqual(AttributesOption.Text, "Attributes");
            Assert.AreEqual(AttributesOption.GetAttribute("custom-data"), "Attributes");
            Assert.AreEqual(AttributesOption.GetAttribute("target"), "_self");
            Assert.AreEqual(AttributesOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(AttributesOption.GetAttribute("href"), "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/AssetsConfigaration");
        }

        [Test]
        public void AttributesPage_OpenPage()
        {
            var ConfigurationOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/a"));
            ConfigurationOption.Click();

            var AttibutesOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/nav/ul/li/a"));
            AttibutesOption.Click();
        }

        [Test]
        public void AttributesPage_HiReviewerTest()
        {
            // to open Attributes page 
            AttributesPage_OpenPage();

            var HiReviewer = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            Assert.AreEqual(HiReviewer.Text, "HI,");
            Assert.True(HiReviewer.Displayed);
            Assert.True(HiReviewer.Enabled);

            var username = driver.FindElement(By.Id("UserName"));
            Assert.True(username.Displayed);
            Assert.True(username.Enabled);
        }

        [Test]
        public void AttributesPage_LogoutBtn()
        {
            // to open Attributes page 
            AttributesPage_OpenPage();

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
        public void AttributesPage_ModalTest()
        {
            // to open Attributes page 
            AttributesPage_OpenPage();

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
        public void AttributesPage_NotificationTest()
        {
            // to open Attributes page 
            AttributesPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void AttributesPage_PageTitleTest()
        {
            // to open Attributes page 
            AttributesPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text, "Configuration - Attributes");
        }

        [Test]
        public void AttributesPage_AssetClassTest()
        {
            // open attributes page
            AttributesPage_OpenPage();

            var assetClassLabel = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div[1]/div[1]/div/div/label"));
            Assert.IsTrue(assetClassLabel.Displayed);
            Assert.IsTrue(assetClassLabel.Enabled);
            Assert.AreEqual(assetClassLabel.Text,"Asset Class");

            var asstClassInput = driver.FindElement(By.Id("AssetClassIdChange"));
            Assert.IsTrue(asstClassInput.Displayed);
            Assert.IsTrue(asstClassInput.Enabled);
            Assert.AreEqual(asstClassInput.GetAttribute("data-val-required"), "The ClassId field is required.");
            var selectedAssetClass = new SelectElement(asstClassInput);
            selectedAssetClass.SelectByIndex(0);
        }

        [Test]
        public void AttributesPage_AssetSubclass()
        {
            // open attributes page
            AttributesPage_OpenPage();

            var assetSubClassLabel = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div[1]/div[2]/div/div/label"));
            Assert.IsTrue(assetSubClassLabel.Displayed);
            Assert.IsTrue(assetSubClassLabel.Enabled);
            Assert.AreEqual(assetSubClassLabel.Text, "Asset Subclass");

            var asstSubClassInput = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            Assert.IsTrue(asstSubClassInput.Displayed);
            Assert.IsTrue(asstSubClassInput.Enabled);
            Assert.AreEqual(asstSubClassInput.GetAttribute("data-val-required"), "The SubClassId field is required.");
        }

        [Test]
        public void AttributesPage_AssetType()
        {
            // open attributes page
            AttributesPage_OpenPage();

            var assetSubClassLabel = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div[1]/div[1]/div[3]/div/div/label"));
            Assert.IsTrue(assetSubClassLabel.Displayed);
            Assert.IsTrue(assetSubClassLabel.Enabled);
            Assert.AreEqual(assetSubClassLabel.Text, "Asset Type");

            var asstSubClassInput = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            Assert.IsTrue(asstSubClassInput.Displayed);
            Assert.IsTrue(asstSubClassInput.Enabled);
            Assert.AreEqual(asstSubClassInput.GetAttribute("data-val-required"), "The TypeId field is required.");
        }

        [Test]
        public void AttributesPage_FooterCopyrightTest()
        {
            // to open Attributes page
            AttributesPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void AttributesPage_MinimizeToggle()
        {
            // to open Attributes page
            AttributesPage_OpenPage();
            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}