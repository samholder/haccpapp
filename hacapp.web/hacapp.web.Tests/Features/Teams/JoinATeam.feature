Feature: JoinATeam
	In order to allow people to join teams
	As a user
	I want to be see which teams I can join

Scenario: User wants to choose a team to join
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team 'A' is owned by 'Tom'
   And I am logged in as 'Bob'
   When I vist the join a team page
   Then The list of teams to join should contain Team 'A'

Scenario: User wants to choose a team to join but should not see teams he is already a member of
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team 'B' is owned by 'Tom'
   And Team 'A' is owned by 'Bob'
   And I am logged in as 'Bob'
   When I vist the join a team page
   Then The list of teams to join should contain Team 'B'
   And The list of teams to join should not contain Team 'A'

Scenario: User tries to join a new team should have a pending membership
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom'
   And I am logged in as 'Bob'
   When I try and join team '1'
   Then 'Bob' should have a pending membership for team '1'
   And I should be redirected to my teams page
   And Team '1' should be listed on my teams page

Scenario: Pending memberships should be listed in team details view
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom'
   And 'Bob' has pending membership for Team '1'
   And I am logged in as 'Tom'
   When I visit the team details page for team '1'
   Then 'Bob' should be listed in the pending members

Scenario: Confirmed memberships should be listed in team details view
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom'
   And 'Bob' is a member Team '1'
   And I am logged in as 'Tom'
   When I visit the team details page for team '1'
   Then 'Bob' should be listed in the confirmed members


Scenario: User wants to choose a team to join, but no teams exist so they should be shown view to create a team
   Given There is a registered user with id 'Bob'
   And I am logged in as 'Bob'
   When I vist the join a team page
   Then I should be shown the No Teams Exist view

Scenario: Owners should not be able to join their own teams
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom'
   And I am logged in as 'Tom'
   When I try and join team '1'
   Then 'Tom' should not have a pending membership for team '1'
   And I should be redirected to my teams page
   And Team '1' should be listed on my teams page

Scenario: Owner accepts pending membership
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom' 
   And 'Bob' has pending membership for Team '1'
   And I am logged in as 'Tom'
   When I accept pending membership for 'Bob' to join team '1'
   Then 'Bob' should be a full member of team '1' 

Scenario: Non owner cannot accept pending membership
   Given There is a registered user with id 'Bob'
   And There is a registered user with id 'Tom'
   And Team '1' is owned by 'Tom' 
   And 'Bob' has pending membership for Team '1'
   And I am logged in as 'Bob'
   When I accept pending membership for 'Bob' to join team '1'
   Then 'Bob' should have a pending membership for team '1'
   And I should be redirected to team details page for team '1'  

Scenario: New users should be given the choice to create or join a new team
	Given I am logged in as 'Bob'
	When I go to the home page
	Then I should the create or join view
