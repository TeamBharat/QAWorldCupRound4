using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectTest.PageObjects
{
    public class NQLBCommunityPage
    {
        private IWebDriver _driver;
        private readonly AppSettings _appSettings;
        private Utilities utilities;

        public NQLBCommunityPage(IWebDriver driver)
        {
            _driver = driver;
            _appSettings = new AppSettings();
        }


        IWebElement memberCountLink => _driver.FindElement(By.XPath("//span[text()='Members']/ancestor::h2/following-sibling::span/button"));

        IWebElement Password => _driver.FindElement(By.Name("Password"));

        IWebElement Submit => _driver.FindElement(By.Name("Login"));




        public void LoginToApplication()
        {
            utilities.WaitUntilElementClickable(memberCountLink);
            memberCountLink.Click();
         
        }

        public void NavigateToNQLBCommunityPage()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:NQLB"]);
        }

    }
}
