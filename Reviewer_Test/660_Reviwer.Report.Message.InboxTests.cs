using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Reviewer_Test
{
    public class ReviwerReportMessageInboxTests : IDisposable
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
        public void ReviwerReportMessages_WhenClickOnMessageOption_MustOpenDropdownlist()
        {
            var messsagesBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[8]/a/span/span"));
            messsagesBtn.Click();
        }

        [Test]
        public void ReviwerReportMessages_WhenClickOnIbox_MustOpenInboxPage()
        {
            // to open Message Page
            ReviwerReportMessages_WhenClickOnMessageOption_MustOpenDropdownlist();

            var inboxBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[8]/nav/ul/li[1]/a/span/span"));
            inboxBtn.Click();
        }

        [Test]
        public void ReviwerReportMessages_WhenClickOnMessageBtn_MustGoToSamePage()
        {
            // to open Inbox Page
            ReviwerReportMessages_WhenClickOnIbox_MustOpenInboxPage();

            var messageBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/a/span"));
            var expectedUrl = driver.Url;
            messageBtn.Click();
            var actualUrl = driver.Url;
            
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportMessages_WhenClickOnDashbpoardBtn_MustGoToDashboardPage()
        {
            // to open Inbox Page
            ReviwerReportMessages_WhenClickOnIbox_MustOpenInboxPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashboardBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ReviwerReportMessages_WhenWriteMessageContent_MustWriteSuccessfully()
        {
            // to open Inbox Page
            ReviwerReportMessages_WhenClickOnIbox_MustOpenInboxPage();

            var messageContent = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[2]/label[2]/input"));
            messageContent.SendKeys("Test Message");
        }

        [Test]
        public void ReviwerReportMessageInbox_WhenClickOnSentMessage_MustGoToSentMessagePage()
        {
            // to open Inbox Page
            ReviwerReportMessages_WhenClickOnIbox_MustOpenInboxPage();

            var sentMessagebtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[1]/button"));
            sentMessagebtn.Click();
        }

        [Test]
        public void ReviwerReportMessageInbox_WhenSendMessageByValidData_MustSubmitMessage()
        {
            // to open subnit message page
            ReviwerReportMessageInbox_WhenClickOnSentMessage_MustGoToSentMessagePage();

            var messageSubjectInput = driver.FindElement(By.Id("Subject"));
            messageSubjectInput.SendKeys("Test Subject");

            var message = driver.FindElement(By.Id("Message"));
            message.SendKeys("Test Message");

            var sendBtn = driver.FindElement
                (By.XPath("//*[@id=\"SendMessage\"]/form/div/div[4]/button"));
            sendBtn.Click();
        }
    }
}