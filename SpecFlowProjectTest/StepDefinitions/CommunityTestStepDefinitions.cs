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

            _communityPage.GetCommunityName();
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
