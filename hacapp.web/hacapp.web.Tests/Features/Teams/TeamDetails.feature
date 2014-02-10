Feature: TeamDetails
	In order to know who is in and manage a team
	as a user
	I want to be able to view the details of a team

Scenario: Users should be able to see the details of teams that they are member of 
	Given There is a registered user with id 'Bob'
	And Team '1' is owned by 'Bob'
	And I am logged in as 'Bob'
	When I visit the details page for team '1'
	Then I should see the details for the team '1'

Scenario: Users should not be able to see the details of teams that they are not members of 
	Given There is a registered user with id 'Bob'
	And There is a registered user with id 'Tom'
	And Team '1' is owned by 'Bob'
	And I am logged in as 'Tom'
	When I visit the details page for team '1'
	Then I should see a not authorized

Scenario: Users should not be able to see the details of teams that they are pending members of 
    Given There is a registered user with id 'Bob'
    And There is a registered user with id 'Tom'
    And Team '1' is owned by 'Tom' 
    And 'Bob' has pending membership for Team '1'
	And I am logged in as 'Bob'
	When I visit the details page for team '1'
	Then I should see a not authorized

