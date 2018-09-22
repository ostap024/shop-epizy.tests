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

        [SetUp]
        public void SetUp()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/");
        }

        [Test]
        public void Tets()
        {
            //Arrange
            string email = "Ostap@gmail.com";
            string currPassword = "qwerty123";
            string newPassword = "q1w2e3r4t5y6";
            string changePassExpected = "Success: Your password has been successfully updated.";
            string logInExpected = "Login success";
            //Act
            string logInActual = LogIn(email, currPassword);
            Assert.AreEqual(logInExpected, logInActual, logInActual);//робити в кінці чи після кожного виклику


            string changePassActual = ChangePass(newPassword);
            Assert.AreEqual(changePassExpected, changePassActual);

            LogOut();

            string secondLogInActual = LogIn(email, newPassword);
            Assert.AreEqual(logInExpected, secondLogInActual, secondLogInActual);//робити в кінці чи після кожного виклику

        }

        private string LogIn(string email, string password)
        {
            driver.FindElement(By.XPath("//a[@title='My Account']")).Click();
            driver.FindElement(By.XPath("//ul[@class='dropdown-menu dropdown-menu-right']//a['login' = substring(@href, string-length(@href) - string-length('login') +1)]")).Click();

            driver.FindElement(By.Id("input-email")).SendKeys(email);

            driver.FindElement(By.Id("input-password")).SendKeys(password);
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();

            driver.FindElement(By.XPath("//a[@title='My Account']")).Click();//open dropdown menu
            if (IsElementPresent(By.XPath("//ul[@class='dropdown-menu dropdown-menu-right']//a['account' = substring(@href, string-length(@href) - string-length('account') +1)]")))
            {
                driver.FindElement(By.XPath("//a[@title='My Account']")).Click();//close dropdown menu
                return "Login success";
            }
            else
            {
                driver.FindElement(By.XPath("//a[@title='My Account']")).Click();//close dropdown menu
                return "Login fail";
            }
           

        }


        private string ChangePass(string newPassword)
        {

            driver.FindElement(By.XPath("//a[@title='My Account']")).Click();
            driver.FindElement(By.XPath("//ul[@class='dropdown-menu dropdown-menu-right']//a['account' = substring(@href, string-length(@href) - string-length('account') +1)]")).Click();
            driver.FindElement(By.XPath("//div[@id='content']//a['password' = substring(@href, string-length(@href) - string-length('password') +1)]")).Click();
            driver.FindElement(By.Id("input-password")).SendKeys(newPassword);

            driver.FindElement(By.Id("input-confirm")).SendKeys(newPassword);

            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            return driver.FindElement(By.CssSelector("div.alert.alert-success")).Text;
        }

        private void LogOut()
        {
            driver.FindElement(By.XPath("//a[@title='My Account']")).Click();
            driver.FindElement(By.XPath("//ul[@class='dropdown-menu dropdown-menu-right']//a['logout' = substring(@href, string-length(@href) - string-length('logout') +1)]")).Click();

            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
            // Thread.Sleep(2000);
        }
        private void LogOutURL()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/logout");
            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
            Thread.Sleep(2000);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            string email = "Ostap@gmail.com";
            string currPassword = "q1w2e3r4t5y6";
            string newPassword = "qwerty123";
            LogOutURL();
            LogIn(email, currPassword);
            ChangePass(newPassword);
            LogOut();
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
