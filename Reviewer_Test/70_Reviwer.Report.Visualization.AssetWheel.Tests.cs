using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Reviewer_Test
{
    public class ReviwerReportVisualizationAssetWheelTests : IDisposable
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
        public void ReviwerReportVisualization_WhenClickOnVisualization_MustOpenDropdownlist()
        {
            var visualizationBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[9]/a/span/span"));

            visualizationBtn.Click();
        }

        [Test]
        public void ReviwerReportVisualization_WhenClickOnAssetWheel_MustOpenAssetWheelPage()
        {
            // to clicl on visualization 
            ReviwerReportVisualization_WhenClickOnVisualization_MustOpenDropdownlist();

            var assetWheelBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[9]/nav/ul/li/a/span/span"));

            assetWheelBtn.Click();
        }
    }
}