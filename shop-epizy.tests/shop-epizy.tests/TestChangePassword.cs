using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/");
        }
        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            driver.Quit();
        }


        [Test, TestCaseSource(typeof(TestData), "GetTestData")]
        public void Test(string email, string currPassword, string newPassword)
        {
            //Arrange
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

            LogOut();
            LogIn(email, newPassword);
            ChangePass(currPassword);
            LogOut();
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
            driver.FindElement(By.XPath("//a[@title='My Account']")).Click();//open dropdown menu
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


        /*
         * ///повідомлення про забагатовводів неправильних паролів
         "#button-search + h2 + p"
    "//input[@id='button-search']/following-sibling::p"
    "//input[@id='button-search']/following-sibling::p[contains(text(),'matches')]"
         змінні в датапровайдер
         патерн стратегія
         "//div[contains(@class, 'product-layout')]//a[contains(text(),'Apple Cinema')]/../../following-sibling::div/button[contains(@data-original-title,'Wish')]"

         */
    }
}
