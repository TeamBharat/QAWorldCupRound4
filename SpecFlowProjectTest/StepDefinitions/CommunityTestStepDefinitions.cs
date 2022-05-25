using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlowProjectTest.StepDefinitions
{
    [Binding]
    public class CommunityTestStepDefinitions
    {
        private readonly CommunitiesPage _communityPage;
        private readonly HomePage _homePage;


        [Given(@"I Navigate to “Communities” using the left pane")]
        public void GivenINavigateToCommunitiesUsingTheLeftPane()
        {
            _communityPage.NavigateToUrl();
            _homePage.NavigateToCommunityPage();
        }

        [Given(@"I Click on “All” to view all available communities")]
        public void GivenIClickOnAllToViewAllAvailableCommunities()
        {
            _communityPage.ClickOnAllHyperLink();
        }

        [Given(@"I Fetch the details like Group name, member count")]
        public void GivenIFetchTheDetailsLikeGroupNameMemberCount()
        {
            <List> WebElement community =//span[contains(@class,'groupName')]
            < List > WebElement communitycount =//div[contains(@class,'entityCardHeader')]/following-sibling::div/descendant::span[5]
              Dictionary<string, int> map = new Dictionary<string, int>();

            for (int i = 0; i <= community.count(); i++) {
                map.add(community[i].getAttribute("title"), communitycount[i].gettext())
            }
            
            int i = 0
            foreach (KeyValuePair<string, Int16> community in map)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                community.Key, community.Value);
                setcelldata("Sheetname", i, 1, community.Key)
                 setcelldata("Sheetname", i, 2, community.value)
                 i = i + 1;
            }
        }

        [When(@"I Sort details fetched in above in ascending order of the team member count")]
        public void WhenISortDetailsFetchedInAboveInAscendingOrderOfTheTeamMemberCount()
        {
            throw new PendingStepException();
        }

        [Then(@"I save the data in Excel")]
        public void ThenISaveTheDataInExcel()
        {
            throw new PendingStepException();
        }
    }
}
