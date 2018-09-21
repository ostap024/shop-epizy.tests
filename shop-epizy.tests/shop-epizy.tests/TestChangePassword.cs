using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace shop_epizy.tests
{
    [TestFixture]
    public class TestChangePassword
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
            string currPassword = "qwerty123";
            string newPassword = "q1w2e3r4t5y6";

            //Act
            LogIn(email, currPassword);
            ChangePass(newPassword);
            LogOut();
        }

        [TearDown]
        public void TearDown()
        {
            string email = "Ostap@gmail.com";
            string currPassword = "q1w2e3r4t5y6";
            string newPassword = "qwerty123";
            LogOut();
            LogIn(email, currPassword);
            LogOut();
        }
        private void LogIn(string email, string password)
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/login");

            driver.FindElement(By.Id("input-email")).SendKeys(email);

            driver.FindElement(By.Id("input-password")).SendKeys(password);
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            Thread.Sleep(2000);
        }

        private void ChangePass(string newPassword)
        {
            string expected = "Success: Your password has been successfully updated.";
            //Debug.WriteLine("ddd");
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/password");

            driver.FindElement(By.Id("input-password")).SendKeys(newPassword);

            driver.FindElement(By.Id("input-confirm")).SendKeys(newPassword);

            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            string actual = driver.FindElement(By.CssSelector("div.alert.alert-success")).Text;
            Assert.AreEqual(expected, actual);
            Thread.Sleep(2000);
        }

        private void LogOut()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/logout");
            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
            Thread.Sleep(2000);
        }


        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            
            driver.Quit();
        }
        /*
         "#button-search + h2 + p"
"//input[@id='button-search']/following-sibling::p"
"//input[@id='button-search']/following-sibling::p[contains(text(),'matches')]"
         змінні в датапровайдер
         патерн стратегія
         "//div[contains(@class, 'product-layout')]//a[contains(text(),'Apple Cinema')]/../../following-sibling::div/button[contains(@data-original-title,'Wish')]"

         */
    }
}
