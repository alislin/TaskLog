using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Linq;

namespace UITest
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private IWebDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            //var options = new EdgeOptions
            //{
            //    PageLoadStrategy = PageLoadStrategy.Normal
            //};
            //_driver = new EdgeDriver(options);

            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://localhost:44383/";
            Assert.AreEqual("TaskLog", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void ModalTest()
        {
            _driver.Url = "https://localhost:44383/";
            var mlist = _driver.FindElements(By.TagName("button"));
            var modal = mlist.FirstOrDefault();
            mlist[0].Click();
            var s = "";
        }
    }
}
