using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Reviewer_Test
{
    public class ReviwerReportMessageSystemMessageTests : IDisposable
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
        public void ReviwerReportSystemMessages_WhenClickOnReportsOption_MustOpenDropdownlist()
        {
            var reportOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[7]/a/span"));
            reportOption.Click();
        }

        [Test]
        public void ReviwerReportSystemMessages_WhenClickOnMessageOption_MustOpenDropdownlist()
        {
            // to open Message Page
            ReviwerReportSystemMessages_WhenClickOnReportsOption_MustOpenDropdownlist();

            var messageBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[8]/a/span/span"));
            messageBtn.Click();
        }

        [Test]
        public void ReviwerReportSystemMessages_WhenClickOnSystemMessage_MustOpenSystemMessagesPage()
        {
            // to open Message Page
            ReviwerReportSystemMessages_WhenClickOnMessageOption_MustOpenDropdownlist();

            var systemMessageBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[8]/nav/ul/li[3]/a/span/span"));
            systemMessageBtn.Click();
        }

        [Test]
        public void ReviwerReportSystemMessages_WhenClickOnSystemMessage_MustOpenSamePage()
        {
            // to open System Message Page
            ReviwerReportSystemMessages_WhenClickOnSystemMessage_MustOpenSystemMessagesPage();

            var systemMessages = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            var expectedUrl = driver.Url;
            systemMessages.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportSystemMessages_WhenClickOnDashboardBtn_MustOpenDashboardPage()
        {
            // to open System Message Page
            ReviwerReportSystemMessages_WhenClickOnSystemMessage_MustOpenSystemMessagesPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashboardBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportSystemMessage_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open System Message Page
            ReviwerReportSystemMessages_WhenClickOnSystemMessage_MustOpenSystemMessagesPage();

            var nextBtn = driver.FindElement(By.Id("messageTable_next"));
            nextBtn.Click();
        }

        [Test]
        public void ReviwerReportSystemMessage_WhenClickOnPerviousBtn_MustGoToPerviousPage()
        {
            // to open System Message Page
            ReviwerReportSystemMessages_WhenClickOnSystemMessage_MustOpenSystemMessagesPage();

            var previousBtn = driver.FindElement(By.Id("messageTable_previous"));
            previousBtn.Click();
        }
    }
}