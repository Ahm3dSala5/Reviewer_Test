using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviewerAboutTest : IDisposable
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
        public void AboutPage_AboutOptionTest()
        {
            var aboutOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[2]/a"));

            Assert.IsTrue(aboutOption.Enabled);
            Assert.IsTrue(aboutOption.Displayed);
            Assert.AreEqual(aboutOption.Text, "About");
            Assert.AreEqual(aboutOption.GetAttribute("custom-data"), "About");
            Assert.AreEqual(aboutOption.GetAttribute("aria-expanded"), "false");
            Assert.AreEqual(aboutOption.GetAttribute("target"), "_self");
            Assert.AreEqual(aboutOption.GetAttribute("href"), "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/About");
        }

        [Test]
        public void AboutPage_OpenPage()
        {
            var aboutOption = driver.FindElement
              (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[2]/a"));
            aboutOption.Click();
        }

        [Test]
        public void AboutPage_ParagraphTest()
        {
            // to open about page 
            AboutPage_OpenPage();

            var paragraph = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/p[1]"));
            Assert.IsTrue(paragraph.Displayed);
            Assert.IsTrue(paragraph.Enabled);
            string text = "The Transit Asset Management Database is a relational database that integrates the asset inventory and condition data used to develop Connecticut DOT’s Transit Asset Management Plan (TAMP), as well as the Group TAMP for Tier II providers in Connecticut. Using a web-based user interface, agencies can enter data with review and approval by CTDOT.";
            Assert.AreEqual(text, paragraph.Text);

            var paragraph2 = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/p[2]"));
            string text2 = "The purpose of the system is threefold:";
            Assert.IsTrue(paragraph2.Displayed);
            Assert.IsTrue(paragraph2.Enabled);
            Assert.AreEqual(paragraph2.Text,text2);

            var OrderedList = driver.FindElements(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/ol"));
            foreach(var phrase in OrderedList)
            {
                Assert.IsTrue(phrase.Displayed);
                Assert.IsTrue(phrase.Enabled);
            }

            var paragraph3 = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/p[3]"));
            string text3 = "The Database stores data on facilities, revenue vehicles, fixed guideway, and equipment.";
            Assert.IsTrue(paragraph3.Displayed);
            Assert.IsTrue(paragraph3.Enabled);
            Assert.AreEqual(paragraph3.Text, text3);

            var paragraph4 = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/p[4]"));
            string text4 = "Group Plan members update the Database with inventory, condition, and other data for revenue vehicles (rolling stock) and equipment (non-revenue service vehicles).";
            Assert.IsTrue(paragraph4.Displayed);
            Assert.IsTrue(paragraph4.Enabled);
            Assert.AreEqual(paragraph4.Text, text4);
        }

        [Test]
        public void AboutPage_HiHostOperatorTest()
        {
            // to open about page 
            AboutPage_OpenPage();

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
        public void AboutPage_LogoutBtn()
        {
            // to open about page 
            AboutPage_OpenPage();

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
        public void AboutPage_ModalTest()
        {
            // to open about page 
            AboutPage_OpenPage();

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
        public void AboutPage_NotificationTest()
        {
            // to open about page 
            AboutPage_OpenPage();

            var notificationIcon = driver.FindElement
                (By.XPath("//*[@id=\"m_header_topbar\"]/div/ul/li[1]/a"));
            Assert.IsTrue(notificationIcon.Displayed);
            Assert.IsTrue(notificationIcon.Enabled);
            Assert.AreEqual(notificationIcon.GetAttribute("href"),
                "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/Messages");
            notificationIcon.Click();
        }

        [Test]
        public void AboutPage_PageTitleTest()
        {
            // to open about page 
            AboutPage_OpenPage();

            var title = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/h1/span"));
            Assert.IsTrue(title.Displayed);
            Assert.IsTrue(title.Enabled);
            Assert.AreEqual(title.Text,"About");
        }

        [Test]
        public void AboutPage_DashboardBtnTest()
        {
            // top open about page 
            AboutPage_OpenPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a"));
            Assert.IsTrue(dashboardBtn.Enabled);
            Assert.IsTrue(dashboardBtn.Displayed);
            Assert.AreEqual(dashboardBtn.Text,"Dashboard");

            var UrlBeforeClick = driver.Url;
            dashboardBtn.Click();
            var UrlAfterClick = driver.Url;
            Assert.AreNotEqual(UrlAfterClick,UrlBeforeClick);
        }

        [Test]
        public void AboutPage_FooterCopyrightTest()
        {
            // to open about page
            AboutPage_OpenPage();

            var CopyRight = driver.FindElement(By.XPath("/html/body/footer/div/div/div[1]/span"));
            Assert.IsTrue(CopyRight.Displayed);
            Assert.IsTrue(CopyRight.Enabled);
            Assert.AreEqual(CopyRight.Text, "2025 © CTDOT (Ver .)");
        }

        [Test]
        public void AboutPage_MinimizeToggle()
        {
            // to open about page
            AboutPage_OpenPage();
            var Toggle = driver.FindElement(By.Id("m_aside_left_minimize_toggle"));
            Assert.IsTrue(Toggle.Displayed);
            Assert.IsTrue(Toggle.Enabled);
            Toggle.Click();
        }
    }
}