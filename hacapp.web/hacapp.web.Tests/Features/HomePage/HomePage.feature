Feature: HomePage
	In order to user the application effectively
	As a user
	I want the home page to show the most relevant

@mytag
Scenario: Pending memberships should be listed on the home page for the team owner
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom'
   And 'Bob' has pending membership for Team '1'
   And I am logged in as 'Tom'
   When I visit the home page
   Then I should see that 'Bob' has a pending membership for team '1'
