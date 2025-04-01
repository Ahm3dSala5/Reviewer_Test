using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;
using System.Xml.Serialization;

namespace Reviewer_Test
// this pice not work 135
{
    public class ReviwerReviewEditsTests : IDisposable
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
        public void ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage()
        {
            var reviewEdits = driver.FindElement
                (By.XPath("//*[@id=\"m_ver_menu\"]/ul/li[5]/a/span"));
            reviewEdits.Click();
        }

        [Test]
        public void ReviewerReviweEdit_WhenSelectAssetState_MustFilterAllAssetsByState()
        {
            // to click on ReviweEdit page 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var assetState = driver.FindElement(By.Id("AssetStateIdChange"));
            var selectedAssetState = new SelectElement(assetState);
            selectedAssetState.SelectByIndex(0);
            assetState.Click();
        }

        [Test]
        public void ReviewerReviweEdit_WhenSelectAssetClass_MustFilterAllAssetsByClass()
        {
            // to click on configuration btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var assetClass = driver.FindElement(By.Id("AssetClassIdChange"));
            var selectedAssetClass = new SelectElement(assetClass);
            selectedAssetClass.SelectByIndex(0);
            assetClass.Click();
        }

        [Test]
        public void ReviewerReviweEdit_WhenSelectAssetSubClass_MustFilterAllAssetsBySubClass()
        {
            // to click on ReviweEdit page
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var assetSubClass = driver.FindElement(By.Id("AssetSubClassDropDownChange"));
            var selectedAssetSubClass = new SelectElement(assetSubClass);
            selectedAssetSubClass.SelectByIndex(0);
            assetSubClass.Click();
        }

        [Test]
        public void ReviewerReviweEdit_WhenSelectAssetType_MustFilterAllAssetsByType()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var assetType = driver.FindElement(By.Id("AssetTypeDropDownChange"));
            var selectedAssetType = new SelectElement(assetType);
            selectedAssetType.SelectByIndex(0);
            assetType.Click();
        }

        [Test]
        public void ReviwerReviewEdit_WhenClickOnReviewEdit_MustOpenSamePage()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var reviewEditBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[3]/span"));
            var expectedUrl = driver.Url;
            reviewEditBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl,actualUrl);
        }

        [Test]
        public void ReviwerReviewEdit_WhenClickOnDashboardBtn_MustOpenDashboardPage()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var dashboardBtn = driver.FindElement
                (By.XPath("/html/body/div[1]/div/div[2]/div[1]/div/div/ul/li[1]/a/span"));
            var expectedUrl = "http://ec2-34-226-24-71.compute-1.amazonaws.com/App/ReviewerDashboard";
            dashboardBtn.Click();
            var actualUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);
        }


        // this pice not work
        [Test]
        public void ReviwerReviewEdit_WhenSelectShowingAssetBy25_MustDisplay25PerPAge()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var showingPerPage = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable_length\"]"));
            showingPerPage.SendKeys("25");
        }

        [Test]
        public void ReviwerReviewEdit_WhenClickOnReviewIcon_MustOpenReviewForm()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var showingPerPage = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[3]/td[9]/a[6]/i"));
            showingPerPage.Click();

            var reviewMessage = driver.FindElement(By.Id("reviewValue"));
            reviewMessage.SendKeys("review Message");
        }


        [Test]
        public void ReviewerReviewEdit_WhenCliclOnEditIcon_MustOpenEditForm()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var editIcon = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[1]/i"));
            editIcon.Click();
        }

        [Test]
        public void ReviewerReviewEdit_WhenEditAssetByValidData_MustSubmitEdit()
        {
            // to open edit form
            ReviewerReviewEdit_WhenCliclOnEditIcon_MustOpenEditForm();

            var assetName = driver.FindElement(By.Id("AssetDes"));
            assetName.SendKeys("Test Asset Name");

            var assetDescription = driver.FindElement(By.Id("AssetTypesDesc"));
            assetDescription.SendKeys("Test Asset Description");

            var saveBtn = driver.FindElement(By.XPath("//*[@id=\"AssetCreateModal\"]/form/div[11]/button[1]"));
            saveBtn.Click();
        }

        [Test]
        public void ReviewerReviewEdit_WhenClickOnStatusIcon_MustOpenStatusForm()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var statusIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[4]/i"));
            statusIcon.Click();
        }

        [Test]
        public void ReviewerReviewEdit_WhenClickOnDeleteIcon_MustOpenConfirmationDeleteForm()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var deleteIcon = driver.FindElement
               (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[3]/i"));
            deleteIcon.Click();

            var confirmationForm = driver.FindElement
                (By.XPath("//*[@id=\"DeleteRequestModal\"]/div/div/div[1]"));
            Assert.NotNull(confirmationForm);
        }

        [Test]
        public void ReviewerReviewEdit_WhenClickOnApprovedIcon_MustOpenApprovedForm()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var approvedIcon = driver.FindElement
                (By.XPath("//*[@id=\"AssetsTable\"]/tbody/tr[2]/td[9]/a[5]/i"));
            approvedIcon.Click();

            var approvedMessage = driver.FindElement
                (By.XPath("//*[@id=\"ApprovedValue\"]"));
            approvedMessage.SendKeys("Test Approved Message");

            var saveBtn = driver.FindElement
                (By.XPath("//*[@id=\"ApproveSaveBtn\"]"));
            saveBtn.Click();
        }

        [Test]
        public void ReviewerReviewEdit_WhenClickOnNextBtn_MustGoToNextPage()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var nextBtn = driver.FindElement(By.Id("AssetsTable_next"));
            nextBtn.Click();
        }

        [Test]
        public void ReviewerReviewEdit_WhenClickOnPreviousBtn_MustGoToPreviousPage()
        {
            // to click on ReviewEdit btn 
            ReviewerReviweEdit_WhenClickOnReviewEdit_MustGoToReviewEditPage();

            var previousBtn = driver.FindElement(By.Id("AssetsTable_previous"));
            previousBtn.Click();
        }
    }
}