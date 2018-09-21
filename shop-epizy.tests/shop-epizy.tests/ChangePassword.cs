using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace shop_epizy.tests
{
    [TestFixture]
    public class ChangePassword
    {
        private IWebDriver driver;
        [OneTimeSetUp]
        public void BeforeAllMethods()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            

        }
        [Test]
        public void Tets()
        {
            //Arrange
            string email = "Ostap@gmail.com";
            string oldPassword = "qwerty123";
            string newPassword = "q1w2e3r4t5y6";

            //Act
            LogIn(email, oldPassword);
            ChangePass(newPassword);
            LogOut();

        }

        private void LogIn(string email, string password)
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/login");

            driver.FindElement(By.Id("input-email")).Click();
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys(email);

            driver.FindElement(By.Id("input-password")).Click();
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys(password);

            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            //  Thread.Sleep(2000);
        }

        private void ChangePass(string newPassword)
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/password");

            driver.FindElement(By.Id("input-password")).Click();
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys(newPassword);

            driver.FindElement(By.Id("input-confirm")).Click();
            driver.FindElement(By.Id("input-confirm")).Clear();
            driver.FindElement(By.Id("input-confirm")).SendKeys(newPassword);

            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
          //  Thread.Sleep(2000);
        }

        private void LogOut()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/logout");
            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
           // Thread.Sleep(2000);
        }


        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            string email = "Ostap@gmail.com";
            string oldPassword = "qwerty123";
            string newPassword = "q1w2e3r4t5y6";
            LogOut();
            LogIn(email, newPassword);
            ChangePass(oldPassword);
            LogOut();
            driver.Quit();
        }
    }
}
