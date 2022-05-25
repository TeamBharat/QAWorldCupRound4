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


        IList<IWebElement> MemberName => _driver.FindElements(By.XPath("//span[contains(text(),'Community members')]/../following-sibling::ul/descendant::span/span[@dir='auto']"));


        IList<IWebElement> MemberEmailId => _driver.FindElements(By.XPath("//span[contains(text(),'Community members')]/../following-sibling::ul/descendant::div[contains(text(),'@nagarro.com')]"));





        public void LoginToApplication()
        {
            utilities.WaitUntilElementClickable(memberCountLink);
            memberCountLink.Click();
         
        }

        public void NavigateToNQLBCommunityPage()
        {
            _driver.Navigate().GoToUrl(_appSettings._configuration["Environments:NQLB"]);
        }
        public void ClickonmemberCountLink()
        {
            memberCountLink.Click();
        }
        public void MemberNameList()
        {

            Dictionary<string, string> map = new Dictionary<string, string>();
            int j = 1;

            for (int i = 0; i <= MemberName.Count(); i++)
            {
                map.Add(MemberName[i].Text, MemberEmailId[i].Text);
            }
            utilities.SetCellData("Sheetname", 0, 0, "MemberName");
            utilities.SetCellData("Sheetname", 0, 1, "Member Email");

            foreach (KeyValuePair<string, string> community in map)
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
