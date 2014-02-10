Feature: TeamMembership
	In order to allow changes in team membership
	as a team owner
	I want to be able to manage who is a member of the team

Scenario: Owners should be able to remove members of a team
    Given There is a registered user with id 'Bob'
    And There is a registered user with id 'Tom'
    And Team '1' is owned by 'Tom' 
    And 'Bob' is a member Team '1'
    And I am logged in as 'Tom'
	When I remove 'Bob' from team '1'
	Then 'Bob' should not be a member of team '1' any more

Scenario: Owners should be not be able to remove themselves from a team
    Given There is a registered user with id 'Bob'
    And Team '1' is owned by 'Bob'     
    And I am logged in as 'Bob'
	When I remove 'Bob' from team '1'
	Then a team called '1' should be created and 'Bob' should be owner of that team

Scenario: Non Owners should not be able to remove other members of a team
    Given There is a registered user with id 'Bob'
    And There is a registered user with id 'Tom'
    And There is a registered user with id 'Jon'
    And Team '1' is owned by 'Tom' 
    And 'Bob' is a member Team '1'
    And 'Jon' is a member Team '1'
    And I am logged in as 'Bob'
	When I remove 'Jon' from team '1'
	Then I should be redirected to team details page for team '1' 
	And 'Bob' should be a full member of team '1'

Scenario: Non Owners should be able to remove themselves from a team
    Given There is a registered user with id 'Bob'
    And There is a registered user with id 'Tom'
    And There is a registered user with id 'Jon'
    And Team '1' is owned by 'Tom' 
    And 'Bob' is a member Team '1'
    And 'Jon' is a member Team '1'
    And I am logged in as 'Bob'
	When I remove 'Bob' from team '1'
	Then 'Bob' should not be a member of team '1' any more