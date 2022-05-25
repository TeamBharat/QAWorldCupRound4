using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectTest.PageObjects
{
    public class TestAutomationPracticeCommunityPage
    {
        private IWebDriver _driver;
        private readonly AppSettings _appSettings;
        private Utilities utilities;

        public TestAutomationPracticeCommunityPage(IWebDriver driver)
        {
            _driver = driver;
            _appSettings = new AppSettings();
            utilities = new Utilities(_driver);
        }


        IWebElement UserName => _driver.FindElement(By.Name("UserName"));

        

        
        public void NavigateToNQLBCommunityPage()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:TAP"]);
        }

    }
}
