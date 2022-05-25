using SpecFlowProjectTest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpecFlowProjectTest.PageObjects
{
    public class SamplePage
    {
        private IWebDriver _driver;
        private readonly AppSettings _appSettings;

        public SamplePage(IWebDriver driver)
        {
            _driver = driver;
            _appSettings = new AppSettings();
        }

       
        IWebElement UserName => _driver.FindElement(By.Name("UserName"));

        IWebElement Password => _driver.FindElement(By.Name("Password"));

        IWebElement Submit => _driver.FindElement(By.Name("Login"));



                
        public void LoginToApplication()
        {
            UserName.SendKeys("TestUser_1");
            Password.SendKeys("Test@123");
            Submit.Submit();
        }

        public void NavigateToUrl()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:url"]);
        }
    }
}
