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
        public void ReviewerConfigurationAttributes_WhenClickOnConfiguration_MustOpenDropdownlist()
        {
            var confiOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/a/span/span"));
            confiOption.Click();
        }

        [Test]
        public void ReviewerConfigurationAttributes_WhenClickOnAttributesOption_MustOpenAttributesPage()
        {
            // to click on configuration btn 
            ReviewerConfigurationAttributes_WhenClickOnConfiguration_MustOpenDropdownlist();

            var attributesBtn = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[3]/nav/ul/li/a/span/span"));
            attributesBtn.Click();
        }

        [Test]
        public void ReviewerConfigurationAttributes_WhenSelectAssetClass_MustFilterAllAssetsByClass()
        {
            // to click on configuration btn 
            ReviewerConfigurationAttributes_WhenClickOnAttributesOption_MustOpenAttributesPage();

            var assetClass = driver.FindElement(By.Id("AssetClassIdChange"));
            var selectedAssetClass = new SelectElement(assetClass);
            selectedAssetClass.SelectByIndex(0);
            assetClass.Click();
        }

        [Test]
        public void ReviewerConfigurationAttributes_WhenSelectAssetSubClass_MustFilterAllAssetsBySubClass()
        {
            // to click on configuration btn 
            ReviewerConfigurationAttributes_WhenClickOnAttributesOption_MustOpenAttributesPage();

            var assetSubClass = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            var selectedAssetSubClass = new SelectElement(assetSubClass);
            selectedAssetSubClass.SelectByIndex(0);
            assetSubClass.Click();
        }

        [Test]
        public void ReviewerConfigurationAttributes_WhenSelectAssetType_MustFilterAllAssetsByType()
        {
            // to click on configuration btn 
            ReviewerConfigurationAttributes_WhenClickOnAttributesOption_MustOpenAttributesPage();

            var assetClass = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            var selectedAssetClass = new SelectElement(assetClass);
            selectedAssetClass.SelectByIndex(0);
            assetClass.Click();
        }
    }
}