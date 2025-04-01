using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
        public void ReviewerAbout_WhenClickOnAboutBtn_MustGoToAboutPage()
        {
            var aboutOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[2]/a/span"));
            aboutOption.Click();
        }

        [Test]
        public void ReviewerAbout_WhenOpenReviewePage_MustDisplayParagraph()
        {
            // to open reviewer page
            ReviewerAbout_WhenClickOnAboutBtn_MustGoToAboutPage();
            
            var paragraphArea = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/p[1]"));
            Assert.NotNull(paragraphArea);
        }

        [Test]
        public void ReviewerAbout_WhenCLickOnDashboardBtn_MustGoToDashboardPage()
        {
            // to open reviwer page
            ReviewerAbout_WhenClickOnAboutBtn_MustGoToAboutPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            dashboardBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl,actualUrl);
        }

        [Test]
        public void ReviewerAbout_WhenCLickOnDashboardBtn_MustOpenSamePage()
        {
            // to open reviwer page
            ReviewerAbout_WhenClickOnAboutBtn_MustGoToAboutPage();

            var aboutBtn = driver.FindElement
                (By.XPath(" /html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            aboutBtn.Click();

            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/About";
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}