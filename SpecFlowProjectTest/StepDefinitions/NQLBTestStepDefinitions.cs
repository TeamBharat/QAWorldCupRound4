using SpecFlowProjectTest.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProjectTest.StepDefinitions
{
    [Binding]
    public class NQLBTestStepDefinitions
    {
        private readonly NQLBCommunityPage _NQLBCommunityPage;


        [Given(@" I Fetch the member name and email id in alphabetical ascending order ")]
        public void IFetchthemembernameandemailidinalphabeticalascendingorder()
        {
            _NQLBCommunityPage.NavigateToNQLBCommunityPage();
            _NQLBCommunityPage.ClickonmemberCountLink();
        }

    }
}
