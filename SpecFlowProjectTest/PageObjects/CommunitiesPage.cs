using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectTest.PageObjects
{
    public class CommunitiesPage
    {
        private IWebDriver _driver;
        private readonly AppSettings _appSettings;
        private Utilities utilities;

        public CommunitiesPage(IWebDriver driver)
        {
            _driver = driver;
            _appSettings = new AppSettings();
        }


        
        //Locator for All hyperlink on Community Page
        IWebElement allHyperlink => _driver.FindElement(By.XPath("//span[@class='ms-Pivot-linkContent linkContent-758']//span[contains(text(),'All')]"));

        IList<IWebElement> groupNames => _driver.FindElements(By.XPath("//span[contains(@class,'groupName')]"));

        public void ClickOnAllHyperLink()
        {
            utilities.WaitUntilElementClickable(allHyperlink);
            allHyperlink.Click();
        }

        public void NavigateToUrl()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:url"]);
        }

    }
}
