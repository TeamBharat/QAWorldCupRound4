Feature: CommunityTest

Community page sorting data
@tag1
Scenario: Sorting of Group Name and Memeber Count using Left Panel
	Given I Navigate to “Communities” using the left pane
	And   I Click on “All” to view all available communities
	And   I Fetch the details like Group name, member count
	When  I Sort details fetched in above in ascending order of the team member count
	Then  I save the data in Excel
