Feature: Google Search

Scenario: Search Item on Google
	Given I navigate to google application
	And I enter some test
	When I click on Search
	Then I verify text