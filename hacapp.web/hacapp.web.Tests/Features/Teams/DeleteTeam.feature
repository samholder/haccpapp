Feature: DeleteTeam
	In order allow full control of teams
	As a team owner
	I want to be able to delete teams

@mytag
Scenario: Owners should be able to delete their teams
    Given There is a registered user with id 'Bob'
    And Team '1' is owned by 'Bob'     
    And I am logged in as 'Bob'
	When I delete team '1'
	Then team '1' should not exist
	And 'Bob' should not be a member of any teams

Scenario: Non owners should not be able to delete their teams
    Given There is a registered user with id 'Bob'
    Given There is a registered user with id 'Tom'
    And Team '1' is owned by 'Bob'     
    And I am logged in as 'Tom'
	When I delete team '1'
	Then a team called '1' should be created and 'Bob' should be owner of that team

Scenario: Non owner members should not be able to delete their teams
    Given There is a registered user with id 'Bob'
    Given There is a registered user with id 'Tom'
    And Team '1' is owned by 'Bob'  
	And 'Tom' is a member Team '1'   
    And I am logged in as 'Tom'
	When I delete team '1'
	Then a team called '1' should be created and 'Bob' should be owner of that team

Scenario: Site admin should be able to delete any team
    Given There is a registered user with id 'Bob'
    And Team '1' is owned by 'Bob'     
    And There is a registered user with id 'Sam'
    And 'Sam' is in the TeamManagement role
    And I am logged in as 'Sam'
	When I delete team '1'
	Then team '1' should not exist
	And 'Bob' should not be a member of any teams