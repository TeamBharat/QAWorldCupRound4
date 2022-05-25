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
            utilities = new Utilities(_driver);
        }


        
        //Locator for All hyperlink on Community Page
        IWebElement allHyperlink => _driver.FindElement(By.XPath("//span[@class='ms-Pivot-linkContent linkContent-758']//span[contains(text(),'All')]"));

        IList<IWebElement> groupNames => _driver.FindElements(By.XPath("//span[contains(@class,'groupName')]"));

        IList<IWebElement> communityCount => _driver.FindElements(By.XPath("//div[contains(@class,'entityCardHeader')]/following-sibling::div/descendant::span[5]"));

        public void ClickOnAllHyperLink()
        {
            utilities.WaitUntilElementClickable(allHyperlink);
            allHyperlink.Click();
        }

        public void NavigateToUrl()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:url"]);
        }

        public int GetCommunityName()
        {
           return groupNames.Count();
        }

        public IList<IWebElement> GetCommunity()
        {
            return groupNames;
        }

        public IList<IWebElement> GetCommunityCount()
        {
            return communityCount;
        }

        public void GetCommunitDetails()
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            int j = 1;

            for (int i = 0; i <= groupNames.Count(); i++)
            {
                map.Add(groupNames[i].GetAttribute("title"),Int16.Parse(communityCount[i].GetAttribute("text")));
            }
            utilities.SetCellData("Sheetname", 0, 0, "GroupName");
            utilities.SetCellData("Sheetname", 0, 1, "MemberCount");

            foreach (KeyValuePair<string, int> community in map)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                community.Key, community.Value);
                utilities.SetCellData("Sheetname", j, 1, community.Key);
                 utilities.SetCellData("Sheetname", j, 2, community.Value.ToString());
                 j = j + 1;
            }
            utilities.CloseExcel();
        }

    }
}
