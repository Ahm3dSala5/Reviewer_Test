using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Reviewer_Test
{
    public class ReviwerOperationCreateAssetTests : IDisposable
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
        public void RevieweOperationCreateAsser_WhenCLickOnOperationOption_MustOpenDropdownlist()
        {
            var operationOption = driver.FindElement(By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/a/span/span"));
            operationOption.Click();
        }

        [Test]
        public void RevieweOperationCreateAsset_WhenCLickOnCreateAssetOption_MustOpenCreateAssetPage()
        {
            // to create create asser option
            RevieweOperationCreateAsser_WhenCLickOnOperationOption_MustOpenDropdownlist();

            var createAssetOption = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[6]/nav/ul/li/a/span/span"));
            createAssetOption.Click();
        }

        [Test]
        public void RevieweOperationCreateAsset_WhenClickOnCreateAssetBtn_MustOpenSamePage()
        {
            // to open create asset page
            RevieweOperationCreateAsset_WhenCLickOnCreateAssetOption_MustOpenCreateAssetPage();

            var createAssetBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            var expectedUrl = driver.Url;
            createAssetBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void RevieweOperationCreateAsset_WhenClickOnDashboardBtn_MustOpenDashboardPage()
        {
            // to open create asset page
            RevieweOperationCreateAsset_WhenCLickOnCreateAssetOption_MustOpenCreateAssetPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            dashboardBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void RevieweOperationCreateAsset_WhenCreateAssetByValidData_MustSubmitSuccessfully()
        {
            // to open create asset page
            RevieweOperationCreateAsset_WhenCLickOnCreateAssetOption_MustOpenCreateAssetPage();

            var assetName = driver.FindElement(By.Id("AssetDes"));
            assetName.SendKeys("Name Test");

            var assetDescription = driver.FindElement(By.Id("AssetTypesDesc"));
            assetDescription.SendKeys("Dewcription Test");

            var assetClass = driver.FindElement(By.Id("AssetClassId"));
            var selectedAssetClass = new SelectElement(assetClass);
            selectedAssetClass.SelectByIndex(0);
            assetClass.Click();

            var assetSubClass = driver.FindElement(By.Id("AssetSubClassIdDropdown"));
            var selectedAssetSubClass = new SelectElement(assetClass);
            selectedAssetSubClass.SelectByIndex(0);
            assetSubClass.Click();

            var assetType = driver.FindElement(By.Id("AssetTypeIdDropdown"));
            var selectedAssetType = new SelectElement(assetClass);
            selectedAssetType.SelectByIndex(0);
            assetType.Click();

            var saveBtn = driver.FindElement
                (By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[9]/button[1]"));
            saveBtn.Click();
        }

        [Test]
        public void RevieweOperationCreateAsset_WhenCreateAssetByValidDataAndClickOnCancelBtn_MustCanceCreate()
        {
            // to create asset 
            RevieweOperationCreateAsset_WhenCreateAssetByValidData_MustSubmitSuccessfully();

            var cancelBtn = driver.FindElement(By.Id("cancle"));
            cancelBtn.Click();
        }

        [Test]
        public void RevieweOperationCreateAsset_WhenCreateAssetByValidDataAndClickOnCancelBtn_MustOpenAddAttributesInputs()
        {
            // to create asset 
            RevieweOperationCreateAsset_WhenCreateAssetByValidData_MustSubmitSuccessfully();

            var addAttributesBtn = driver.FindElement(By.Id("btnAdd"));
            addAttributesBtn.Click();
        }
    }
}