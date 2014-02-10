Feature: Create a new team
	In order to allow new teams to be created
	As a user
	I want to be able to create new teams

@NCrunch.Framework.ExclusivelyUsesAttribute_Database
Scenario: Happy path
	Given There is a registered user with id 'Bob'	
	And I am logged in as 'Bob'
	When I enter the full details for creating a team called 'BobsTeam'
	Then a team called 'BobsTeam' should be created and 'Bob' should be owner of that team
	And I should be redirected to the team details page

@NCrunch.Framework.ExclusivelyUsesAttribute_Database
Scenario: Cannot Create team with samer name
	Given There is a registered user with id 'Bob'	
	And I am logged in as 'Bob'
	When I enter the full details for creating a team called 'BobsTeam'
	And I enter the full details for creating a team called 'BobsTeam'
	Then only one team called 'BobsTeam' should not be created
	And I should be redirected to back to the create a team page
	And there should be an error message

