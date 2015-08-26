using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Bing();
            //Google();
        }

        private static void Google()
        {
            var driver = new ChromeDriver();
            driver.Url = "http://google.co.uk";
            driver.Close();
        }

        private static void Bing()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://wwww.bing.com");
            driver.Manage().Window.Maximize();

            IWebElement searchInput = driver.FindElement(By.Id("sb_form_q"));

            if (searchInput != null)
            {
                searchInput.SendKeys("Hello World");
                searchInput.SendKeys(Keys.Enter);
            }
        }
    }
}
