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
            //driver.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
              //  driver.Quit();
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
        public void DashboardPage_HiHostOperatorTest()
        {
            // to open dashboard page

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
            // to open dashboard page

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
            // to open dashboard page

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void ReviwerTest_WhenClickOnNotificationICon_MustGoToInboxPage()
        {
            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a/span[1]/i"));
            notificationIcon.Click();
        }

        [Test]
        public void ReviwerTest_WhenClickOnHiReviwer_MustOpenLogoutOption()
        {
            var HiReviwer = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/a/span[1]"));
            HiReviwer.Click();
        }

        public void ReviwerTest_WhenClickOnLogoutBtn_MustGoToSigninPage()
        {
            // to click on Hi User
            ReviwerTest_WhenClickOnHiReviwer_MustOpenLogoutOption();

            var logoutBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[4]/div/div/div/div/ul/li[4]/a"));
            logoutBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/Account/Login";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl,actualUrl);
        }

        [Test]
        public void ReviewerTest_OpenReviewrTestPage()
        {
            var dashboardBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[1]/a/span/span"));
            dashboardBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_ConnecticutDepartmentPending_MustGoToReviewPage()
        {
            var reviwe1Btn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[1]/button"));
            reviwe1Btn.Click();
        }

        [Test]
        public void ReviewerDashboard_ConnecticutDepartmentUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_ConnecticutDepartmentDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[3]/div/div/div/div[2]/div[2]/button"));
            reviewBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_CTTransitHartfordPending_MustGoToReviewPage()
        {
            var reviwe1Btn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[1]/button"));
            reviwe1Btn.Click();
        }

        [Test]
        public void ReviewerDashboard_CTTransitHartfordUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_CTTransitHartfordDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[4]/div/div/div/div[2]/div[3]/button"));
            reviewBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_CTTransitWaterburyPending_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[1]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_CTTransitWaterburyUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_CTTransitWaterburyDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[3]/button"));
            reviewBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_GreaterBridgeportTransitPending_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_GreaterBridgeportTransitUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[6]/div/div/div/div[2]/div[3]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_GreaterBridgeportTransitDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[5]/div/div/div/div[2]/div[3]/button"));
            reviewBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_EstuaryTransitDistrictPending_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[1]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_EstuaryTransitDistrictUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_EstuaryTransitDistrictDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[7]/div/div/div/div[2]/div[3]/button"));
            reviewBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_HousatonicAreaRegionalTransitPending_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[1]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_HousatonicAreaRegionalTransitUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_HousatonicAreaRegionalTransitDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[8]/div/div/div/div[2]/div[3]/button"));
            reviewBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_GreaterNewHavenTransitPending_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[1]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_GreaterNewHavenTransitPendingUnapproved_MustGoToReviewPage()
        {
            var reviweBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[2]/button"));
            reviweBtn.Click();
        }

        [Test]
        public void ReviewerDashboard_GreaterNewHavenTransitPendingDeleteRequest_MustGoToReviewPage()
        {
            var reviewBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[9]/div/div/div/div[2]/div[3]/button"));
            reviewBtn.Click();
        }
    }
}