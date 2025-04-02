using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviewerDashboardTest : IDisposable
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
        public void DashboardPage_DashboardOptionTest()
        {
            var dashboardOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[1]/a"));

            Assert.IsTrue(dashboardOption.Enabled);
            Assert.IsTrue(dashboardOption.Displayed);
            Assert.AreEqual(dashboardOption.Text,"Dashboard");
            Assert.AreEqual(dashboardOption.GetAttribute("custom-data"),"Dashboard");
            Assert.AreEqual(dashboardOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(dashboardOption.GetAttribute("target"), "_self");
            Assert.AreEqual(dashboardOption.GetAttribute("href"), "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard");
        }

        [Test]
        public void DashboardPage_OpenPage()
        {
            var dashboardOption = driver.FindElement
               (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[1]/a"));
            dashboardOption.Click();
        }

        [Test]
        public void DashbpoardPage_ParagraphTest()
        {
            var paragraph = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/h3/span"));
            Assert.IsTrue(paragraph.Displayed);
            Assert.IsTrue(paragraph.Enabled);

            string text = "Welcome to the CTDOT Transit Asset Management Database!\r\n\r\nThis database stores asset inventory data of Connecticut transit providers. Please use the menu bar on the left or dashboard controls to view, edit, create or delete assets. Note that any edits made by a transit operator must be approved before they can be incorporated in the inventory.";
            Assert.AreEqual(paragraph.Text,text);
        }

        [Test]
        public void DashboardPage_HiHostOperatorTest()
        {
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
        public void DashboardPage_LogoutBtn()
        {
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
        public void DashboardPage_ModalTest()
        {
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
        public void DashboardPage_NotificationTest()
        {
            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void DashboardPage_ConnecticutDepartmentofTransportationnewNamenewNamenewNameTest()
        {
            var Header = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Connecticut Department of TransportationnewNamenewNamenewName");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text,"Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text,"Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"),"button");
            
            var unApprovedHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_CTTransitHartfordTest()
        {
            var Header = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "CTTransit Hartford");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");
            
            var deleteRequestHeader = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_CTTransitWaterburyTest()
        {
            var Header = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "CTTransit Waterbury");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_GreaterBridgeportTransitTest()
        {
            var Header = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Greater Bridgeport Transit");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_EstuaryTransitDistrictTest()
        {
            var Header = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Estuary Transit District");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_HousatonicAreaRegionalTransit()
        {
            var Header = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Housatonic Area Regional Transit");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_GreaterNewHavenTransitDistrictTest()
        {
            var Header = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Greater New Haven Transit District");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_NorwalkTransitDistrictTest()
        {
            var Header = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Norwalk Transit District");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[10]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }

        [Test]
        public void DashboardPage_MetroNorthRailroadTest()
        {
            var Header = driver.FindElement
              (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/h2"));
            Assert.IsTrue(Header.Displayed);
            Assert.IsTrue(Header.Enabled);
            Assert.AreEqual(Header.Text, "Metro North Railroad");

            var pendingHeader = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/div[2]/div[1]/div/span"));
            Assert.IsTrue(pendingHeader.Displayed);
            Assert.IsTrue(pendingHeader.Enabled);
            Assert.AreEqual(pendingHeader.Text, "Pending");

            var pendingReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/div[2]/div[1]/button"));
            Assert.IsTrue(pendingReviewBtn.Displayed);
            Assert.IsTrue(pendingReviewBtn.Enabled);
            Assert.AreEqual(pendingReviewBtn.Text, "Review");
            Assert.AreEqual(pendingReviewBtn.GetAttribute("type"), "button");

            var unApprovedHeader = driver.FindElement
               (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/div[2]/div[2]/div/span"));
            Assert.IsTrue(unApprovedHeader.Displayed);
            Assert.IsTrue(unApprovedHeader.Enabled);
            Assert.AreEqual(unApprovedHeader.Text, "Unapproved");

            var unApprovedReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/div[2]/div[2]/button"));
            Assert.IsTrue(unApprovedReviewBtn.Displayed);
            Assert.IsTrue(unApprovedReviewBtn.Enabled);
            Assert.AreEqual(unApprovedReviewBtn.Text, "Review");
            Assert.AreEqual(unApprovedReviewBtn.GetAttribute("type"), "button");

            var deleteRequestHeader = driver.FindElement
            (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/div[2]/div[3]/div/span"));
            Assert.IsTrue(deleteRequestHeader.Displayed);
            Assert.IsTrue(deleteRequestHeader.Enabled);
            Assert.AreEqual(deleteRequestHeader.Text, "Delete Request");

            var deleteRequestReviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[11]/div/div/div/div[2]/div[3]/button"));
            Assert.IsTrue(deleteRequestReviewBtn.Displayed);
            Assert.IsTrue(deleteRequestReviewBtn.Enabled);
            Assert.AreEqual(deleteRequestReviewBtn.Text, "Review");
            Assert.AreEqual(deleteRequestReviewBtn.GetAttribute("type"), "button");
        }
    }
}
