using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Reviewer_Test
{
    public class ReviwerReportMessageSentMessageTests : IDisposable
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
        public void ReviwerReportSentMessage_WhenClickOnMessage_MustOpenDropdownlist()
        {
            var messageBtn = driver.FindElement(By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[8]/a/span/span"));
            messageBtn.Click();
        }

        [Test]
        public void ReviwerReportSentMessage_WhenClickOnSentMessage_MustOpenSentMessagePage()
        {
            // to click on message option
            ReviwerReportSentMessage_WhenClickOnMessage_MustOpenDropdownlist();

            var sentMessageBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[8]/nav/ul/li[2]/a/span/span"));
            sentMessageBtn.Click();
        }

        [Test]
        public void ReviwerReportSentMessages_WhenClickOnSystemMessage_MustOpenSamePage()
        {
            // to open System sent Message Page
            ReviwerReportSentMessage_WhenClickOnSentMessage_MustOpenSentMessagePage();

            var sentMessage = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            var expectedUrl = driver.Url;
            sentMessage.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);

        }

        [Test]
        public void ReviwerReportSentMessages_WhenClickOnDashboardBtn_MustOpenDashboardPage()
        {
            // to open System sent Message Page
            ReviwerReportSentMessage_WhenClickOnSentMessage_MustOpenSentMessagePage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashboardBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportSentMessage_WhenClickOnHost_MustShowingAllDetails()
        {
            // to open System sent Message Page
            ReviwerReportSentMessage_WhenClickOnSentMessage_MustOpenSentMessagePage();

            var hostOperatorReviwerHeader = driver.FindElement
                (By.XPath("//*[@id=\"heading11663\"]/div[1]/div[2]"));
            hostOperatorReviwerHeader.Click();
        }

        [Test]
        public void ReviwerReportSentMessage_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to open System sent Message Page
            ReviwerReportSentMessage_WhenClickOnSentMessage_MustOpenSentMessagePage();

            var nextBtn = driver.FindElement(By.Id("messageTable_next"));
            nextBtn.Click();
        }

        [Test]
        public void ReviwerReportSentMessage_WhenClickOnPerviousBtn_MustGoToPerviousPage()
        {
            // to open System sent Message Page
            ReviwerReportSentMessage_WhenClickOnSentMessage_MustOpenSentMessagePage();

            var previousBtn = driver.FindElement(By.Id("messageTable_previous"));
            previousBtn.Click();
        }
    }
}