using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectTest.PageObjects
{
    public class HomePage
    {
        private IWebDriver _driver;
        private readonly AppSettings _appSettings;
        private Utilities utilities;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _appSettings = new AppSettings();
        }


        IWebElement communityPageLink  => _driver.FindElement(By.XPath("//div[contains(text(),'Communities')]"));

      


        public void NavigateToCommunityPage()
        {
            utilities.WaitUntilElementClickable(communityPageLink);
            communityPageLink.Click();

           
        }

        public void NavigateToUrl()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:url"]);
        }

    }
}
